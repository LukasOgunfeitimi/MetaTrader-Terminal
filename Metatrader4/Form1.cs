using System.Net.WebSockets;
using Metatrader4.Order;
namespace Metatrader4
{
    public partial class Form1 : Form
    {
        public string TerminalText
        {
            get { return terminalText.Text; }
            set {
                //DateTime now = DateTime.Now;
                //terminalText.AppendText(now.ToString("HH:mm:ss.fff: "));
                terminalText.AppendText(value);
                terminalText.AppendText(Environment.NewLine);
            }

        }
        private Socket socket1;
        private Socket socket2;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            socket1 = new Socket(this, new UIInfo(statusText1, UsernameText1, PasswordText1, ServerText1), "parent");
            socket1.neworder += SendOrderToChild;
            try
            {
                await socket1.Connect();
            }
            catch (Exception ex)
            {

            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            socket2 = new Socket(this, new UIInfo(statusText2, UsernameText2, PasswordText2, ServerText2), "child");
            try
            {
                await socket2.Connect();
            }
            catch (Exception ex)
            {

            }
        }

        private void SendOrderToChild(OrderResponse order) {
            socket2.SendOrder(order);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void statusText_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordText2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UsernameText1_TextChanged(object sender, EventArgs e)
        {

        }

        private void terminalText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
