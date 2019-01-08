using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.Text;

public partial class py : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string input = tbPy.Text.Trim().ToLower();

        if (!Regex.IsMatch(input, "^[a-zA-Z]{2,4}$"))
        {
            labResult.Text = "请输入有效的声母组合。";

            return;
        }

        StringBuilder result = new StringBuilder();
        SQLiteConnection conn = new SQLiteConnection(Shove._Web.WebConfig.GetAppSettingsString("ConnectionString"));
        conn.Open();
        string ignore = "";
        string input2 = input;

        while (!String.IsNullOrEmpty(input2))
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from Words where py = \"" + input2 + "\"", conn);
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                result.Append(dr["Word"].ToString() + " " + ignore + "<br />");
            }

            dr.Close();

            result.Append("<br />");
            ignore = input2.Substring(input2.Length - 1) + ignore;
            input2 = input2.Substring(0, input2.Length - 1);
        }

        input2 = input;
        ignore = input2.Substring(0, 1);
        input2 = input2.Substring(1);

        while (!String.IsNullOrEmpty(input2))
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from Words where py = \"" + input2 + "\"", conn);
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                result.Append(ignore + (String.IsNullOrEmpty(ignore) ? "" : " ") + dr["Word"].ToString() + "<br />");
            }

            dr.Close();

            result.Append("<br />");
            try { ignore += input2.Substring(0, 1); }
            catch { }
            input2 = input2.Substring(1);
        }

        conn.Close();

        labResult.Text = result.ToString();
    }
}