using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Metatrader4
{
    class AccountResponse {
        public String key { get; set; }
        public String token { get; set; }
    }
    class Socket
    {
        private MT4 MT;
        private Form1 _MainForm;
        private ClientWebSocket mtSocket;
        public Socket(Form1 MainForm) {
            _MainForm = MainForm;
        }
        public async Task Connect() {
            AccountResponse account = await GetInfo();
            MT = new MT4(account);

            _MainForm.StatusText = "Connecting";

            try
            {
                mtSocket = new ClientWebSocket();
                await mtSocket.ConnectAsync(new Uri("wss://gwt6.mql5.com:443"), CancellationToken.None);

                _MainForm.StatusText = "Connected";

                byte[] token = MT.Token();

                Send(token);

                await ReceiveMessagesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                _MainForm.StatusText = "Failed";
            }


        }

        public async Task<AccountResponse> GetInfo() {
            String UserName = _MainForm.UsernameText.Text;
            String Password = _MainForm.PasswordText.Text;
            String Server = _MainForm.ServerText.Text;

            String Response = await SendRequest(UserName, Password, Server);
            AccountResponse res = JsonSerializer.Deserialize<AccountResponse>(Response);
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

            reader.ReadByte();
            reader.ReadByte();

            ushort opcode = reader.ReadUInt16();

            reader.ReadByte();


            switch (opcode) {
                case 0:
                    Console.WriteLine("ID: " + reader.ReadUInt16());

                    byte[] Password = MT.Password();
                    break;
            }
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
    }
}
