

namespace Metatrader4
{
    public partial class Form1 : Form
    {
        private Socket socket;
        public Form1()
        {
            socket = new Socket(this);
            InitializeComponent();
        }

        public string StatusText
        {
            get { return statusText.Text; }
            set { statusText.Text = "Status: " + value; }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await socket.Connect();
            } catch (Exception ex)
            {

            }
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
    }
}
