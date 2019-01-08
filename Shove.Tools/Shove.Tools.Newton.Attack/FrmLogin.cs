using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shove.Tools.Newton.Attack
{
    public partial class FrmLogin : Form
    {
        public bool isOK = false;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbPassword.Text == "a8g502shovee" + DateTime.Now.Day.ToString())
            {
                isOK = true;
            }
        }
    }
}
