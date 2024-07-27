using System.Net.WebSockets;
using System.Text.Json;
using Metatrader4.Binary;
using Metatrader4.Order;

namespace Metatrader4
{
    class AccountResponse {
        public String key { get; set; }
        public String token { get; set; }
        public String password;
    }
    class Socket
    {
        private MT4 MT;
        private Form1 _MainForm;
        private UIInfo _info;
        public ClientWebSocket mtSocket;
        private String _type;

        public event Action<OrderResponse> neworder;

        public Socket(Form1 MainForm, UIInfo info, String type) {
            _MainForm = MainForm;
            _info = info;
            _type = type;
        }
        public async Task Connect() {
            AccountResponse account = await GetInfo();
            MT = new MT4(account);

            _info.StatusText = "Connecting";
            UpdateTerminal("attempting connection");

            try
            {
                mtSocket = new ClientWebSocket();
                await mtSocket.ConnectAsync(new Uri("wss://gwt6.mql5.com:443"), CancellationToken.None);

                _info.StatusText = "Connected";
                UpdateTerminal("connected");

                byte[] token = MT.Token();

                Send(token);

                while (mtSocket.State == WebSocketState.Open) 
                     await ReceiveMessagesAsync();

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                _info.StatusText = "Failed";
                UpdateTerminal("failed");
            }


        }

        public async Task<AccountResponse> GetInfo() {
            String UserName = _info.usernameText.Text;
            String Password = _info.passwordText.Text;
            String Server = _info.serverText.Text;

            String Response = await SendRequest(UserName, Password, Server);

            AccountResponse res = JsonSerializer.Deserialize<AccountResponse>(Response);
            res.password = Password;
            return res;
        }

        public async Task<string> SendRequest(string Username, string Password, string Server) {
            using var client = new HttpClient();
            var response = await client.GetAsync(String.Format("https://metatraderweb.app/trade/json?login={0}&trade_server={1}&gwt=6", Username, Server));
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;

        }

        private async Task ReceiveMessagesAsync() {
            byte[] buffer = new byte[1024 * 4];

            if (mtSocket.State != WebSocketState.Open) return;

            var result = await mtSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            var messageLength = BitConverter.ToUInt32(buffer, 0);
            
            byte[] MessageBuffer = new byte[messageLength];

            Array.Copy(buffer, 8, MessageBuffer, 0, messageLength);

            byte[] decryptedMessage = MT.Decrypt(MessageBuffer);
            HandleMessage(decryptedMessage);
        }

        private void HandleMessage(byte[] message) {
            using var memoryStream = new MemoryStream(message);
            using var reader = new BinaryReader(memoryStream);

            int offset = 5; // For manual reading

            reader.ReadByte();
            reader.ReadByte();

            ushort opcode = reader.ReadUInt16();

            reader.ReadByte();

            switch (opcode) {
                case 0:
                    byte[] Password = MT.Password();
                    Send(Password);
                    break;
                case 15:
                    uint successStatus = reader.ReadUInt32();

                    if (successStatus == 0) {
                        Console.WriteLine("Logged in");

                        UpdateTerminal("logged in succesfully");

                    } else Console.WriteLine("Failed");
                    break;
                case 10: // Order Recieved
                    OrderResponse order = new OrderResponse();
                    offset += (16 + 8);

                    uint ticket = Reader.ReadUInt32(message, offset);
                    offset += 4;
                    string asset = Reader.ReadString(message, offset, 12);
                    offset += 4;

                    offset += 12;
                    byte direction = message[offset];

                    order.ticketNumber = ticket;
                    order.asset = asset;
                    order.direction = direction;

                    UpdateOrder("recieved " + order.GetDirectionName() + " order number: " + ticket + " for " + asset);
                    if (_type == "parent")
                        neworder(order);
                    break;

            }
        }

        public void SendOrder(OrderResponse order) {
            if (mtSocket.State != WebSocketState.Open) return;
            if (_type != "child") return;

            byte[] OrderToSend = MT.InitOrder(order);
            UpdateOrder("sent " + order.ticketNumber);
            Send(OrderToSend);
        }

        async void Send(byte[] message) {
            int msgLen = message.Length;

            byte[] MessageToSend = new byte[msgLen + 8];
            int offset = 0;

            byte[] MessageLengthBytes = BitConverter.GetBytes((int)msgLen);
            MessageToSend[offset++] = MessageLengthBytes[0];
            MessageToSend[offset++] = MessageLengthBytes[1];
            MessageToSend[offset++] = MessageLengthBytes[2];
            MessageToSend[offset++] = MessageLengthBytes[3];

            MessageToSend[offset] = 1;
            offset += 4;

            for (int i = 0; i < msgLen; i++) {
                MessageToSend[offset++] = message[i];
            }
            var buffer = new ArraySegment<byte>(MessageToSend);

            await mtSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None);
        }

        private void UpdateTerminal(string update) {
            _MainForm.TerminalText = _type + " " + update;
        }
        private void UpdateOrder(string update) {
            _MainForm.TerminalText = _type + " " + update;
        }
    }
}
