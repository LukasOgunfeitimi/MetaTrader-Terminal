using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Metatrader4
{
    class InfoResponse {
        String key;
        String token;
    }
    class Socket
    {
        private MT4 MT;
        private Form1 _MainForm;
        private ClientWebSocket mtSocket;
        public Socket(Form1 MainForm)
        {
            MT = new MT4();
            _MainForm = MainForm;
        }
        public async Task Connect()
        {
            GetInfo();
            return;
            _MainForm.StatusText = "Connecting";

            try
            {
                mtSocket = new ClientWebSocket();
                await mtSocket.ConnectAsync(new Uri("wss://gwt6.mql5.com:443"), CancellationToken.None);

                _MainForm.StatusText = "Connected";

                byte[] token = MT.Token();

                Send(token);
            } catch (Exception ex)
            {
                _MainForm.StatusText = "Failed";
            }


        }

        public async Task<string> GetInfo() {
            String UserName = _MainForm.UsernameText.Text;
            String Password = _MainForm.PasswordText.Text;
            String Server = _MainForm.ServerText.Text;

            String Response = await SendRequest(UserName, Password, Server);

            return "d";
        }

        public async Task<string> SendRequest(string Username, string Password, string Server) {
            using (HttpClient client = new HttpClient()) {
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");
                client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("Sec-CH-UA", "\"Google Chrome\";v=\"119\", \"Chromium\";v=\"119\", \"Not?A_Brand\";v=\"24\"");
                client.DefaultRequestHeaders.Add("Sec-CH-UA-Mobile", "?0");
                client.DefaultRequestHeaders.Add("Sec-CH-UA-Platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                client.DefaultRequestHeaders.Add("Referer", "https://metatraderweb.app/trade");
                client.DefaultRequestHeaders.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                client.DefaultRequestHeaders.Add("Cookie", "_fz_uniq=5188426201580743742; _fz_fvdt=1699563630; _fz_ssn=1700875826366010805");

                var content = new StringContent($"login={Username}&trade_server={Server}&gwt=6", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync("https://metatraderweb.app/trade/json", content);

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                return responseBody;
            }
        }

        void Send(byte[] message) {

        }
    }
}
