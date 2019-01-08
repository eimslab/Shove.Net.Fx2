using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SQLite;

public partial class Temp_Notify_GetData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // int LastID = int.Parse(Request["LastID"]);
        // 从数据库中读取 ID > LastID 的所有的记录，按下面的格式组成字符串，直接 Response
        // 数据库字段为：
        // ID StartTime EndTime Content
        // -----------------------------------

        string result = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                    "<Messages>" +
                    "  <Message>" +
                    "    <ID>1</ID>" +
                    "    <StartTime>2012-06-01 00:00:00</StartTime>" +
                    "    <EndTime>2012-06-30 00:00:00</EndTime>" +
                    "    <Content>你好,新的一天开始啦！</Content>" +
                    "  </Message>" +
                    "  <Message>" +
                    "    <ID>2</ID>" +
                    "    <StartTime>2012-06-02 00:00:00</StartTime>" +
                    "    <EndTime>2012-06-30 00:00:00</EndTime>" +
                    "    <Content>你好,新的二天开始啦！</Content>" +
                    "  </Message>" +
                    "  <Message>" +
                    "    <ID>3</ID>" +
                    "    <StartTime>2012-06-03 00:00:00</StartTime>" +
                    "    <EndTime>2012-06-30 00:00:00</EndTime>" +
                    "    <Content>你好,新的三天开始啦！</Content>" +
                    "  </Message>" +
                    "  <Message>" +
                    "    <ID>4</ID>" +
                    "    <StartTime>2012-06-04 00:00:00</StartTime>" +
                    "    <EndTime>2012-06-30 00:00:00</EndTime>" +
                    "    <Content>你好,新的四天开始啦！</Content>" +
                    "  </Message>" +
                    "</Messages>";


        this.Response.Write(result);
        this.Response.End();
    }
}
