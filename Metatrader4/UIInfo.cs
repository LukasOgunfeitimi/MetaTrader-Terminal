using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metatrader4 {
    class UIInfo {
        public TextBox statusText;
        public TextBox usernameText;
        public TextBox passwordText;
        public TextBox serverText;

        public string StatusText {
            get { return statusText.Text; }
            set { statusText.Text = "Status: " + value; }
        }


        public UIInfo(TextBox status, TextBox user, TextBox pass, TextBox server) {
            statusText = status;
            usernameText = user;
            passwordText = pass;
            serverText = server;
        }
    }
}
