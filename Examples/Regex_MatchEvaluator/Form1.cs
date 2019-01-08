using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[<]br[>][\r\n\t]*?(?<G0>[\d]{4}[-][\d]{2}[-][\d]{2}[^<]*?)[<]br[>]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            textBox2.Text = regex.Replace(textBox1.Text, new MatchEvaluator(MyReplace));
        }

        private static string MyReplace(Match match) 
        {
            string matchValue = match.Value;

            return "\r\n===========开始分割符号，你自己设置，替换了一个br===========" + match.Groups["G0"].Value + "===========结束分割符号，你自己设置，替换了一个br,要不要由你===========\r\n";
        }
    }
}
