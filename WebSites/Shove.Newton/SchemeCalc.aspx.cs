using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SchemeCalc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        double code = Shove._Convert.StrToDouble(TextBox3.Text, 0);

        double pd_scale = Math.Round(0.15 - ((code / 30000.00) * (15.00 / 40.00)), 4);
        double ui_scale = Math.Round(0.15 - ((code / 30000.00) * (15.00 / 40.00)), 4);
        double code_scale = Math.Round(0.6 + (code / 30000.00), 4);
        double qc_scale = Math.Round(1 - pd_scale - ui_scale - code_scale, 4);

        double pd = Math.Round(code / code_scale * pd_scale, 2);
        double ui = Math.Round(code / code_scale * ui_scale, 2);
        double qc = Math.Round(code / code_scale * qc_scale, 2);
        double sum = pd + ui + code + qc;

        double pd_month = Math.Round(pd / 20 / 8, 2);
        double ui_month = Math.Round(ui / 20 / 8, 2);
        double code_month = Math.Round(code / 20 / 8, 2);
        double qc_month = Math.Round(qc / 20 / 8, 2);
        double sum_month = Math.Round(sum / 20 / 8, 2);

        TextBox1.Text = pd.ToString();
        TextBox7.Text = pd_month.ToString();

        TextBox2.Text = ui.ToString();
        TextBox8.Text = ui_month.ToString();

        TextBox9.Text = code_month.ToString();

        TextBox4.Text = qc.ToString();
        TextBox10.Text = qc_month.ToString();

        TextBox5.Text = sum.ToString();
        TextBox11.Text = sum_month.ToString();


        int person = Shove._Convert.StrToInt(TextBox6.Text, 1);
        if (TextBox6.Text.Trim() != person.ToString())
        {
            TextBox6.Text = person.ToString();
        }

        double session = Math.Round(pd_month + qc_month + code_month / person, 2);
        TextBox12.Text = session.ToString();

        TextBox13.Text = (pd_scale * 100).ToString() + "%";
        TextBox14.Text = (ui_scale * 100).ToString() + "%";
        TextBox15.Text = (code_scale * 100).ToString() + "%";
        TextBox16.Text = (qc_scale * 100).ToString() + "%";
        TextBox17.Text = "100%";
    }
}