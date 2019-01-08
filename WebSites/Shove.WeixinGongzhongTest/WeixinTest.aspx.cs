using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shove.Gateways.Weixin.Gongzhong;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Xml;

public partial class WeixinTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //初始化令牌方法,每个页面如果要调用接口,必须先初始化令牌
        Utility.GetAccessToken("APPID(唯一凭证)", "secret(第三方用户唯一凭证密钥)");
    }

    /// <summary>
    /// 发送图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string error = "错误的描述";

        // 2.1例:如果选择发送图片,如下 
        ImageServiceMessage ImageService = new ImageServiceMessage("图片文件ID", "用户账号openId");

        //调用接口发送
        int ErrorCode = UserMessage.SendMessage(ImageService, ref error);

        //4:如果发送媒体文件，需要验证媒体文件ID是否过期,如果过期需要重新上传,然后再重新发送

        string FileId = "返回上传文件的ID";//获取文件ID后,需要更新我们服务器上的ID

        if (ErrorCode == -2)
        {
            Utility.UploadFile("文件路径", "上传文件的类型",ref FileId, ref  error);
        }

        //5：上传成功后，将更新后得到的文件ID替换我们保存之前保存的文件ID
        //....

        //6:重新构建实体类重新发送,
        ImageServiceMessage ImageServices = new ImageServiceMessage("图片文件ID", "用户账号openId");

        ErrorCode = UserMessage.SendMessage(ImageServices, ref error);

        //5:处理信息发送失败
        if (ErrorCode < 0)
        {
            //显示错误信息
            Response.Write(error);
            return;
        }

        Response.Write("成功");
    }

    /// <summary>
    /// 获取关注列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button6_Click(object sender, EventArgs e)
    {
        string error = "";

        FansListInformation Fans = Information.GetFansList("",ref error);

        //验证是否获取成功
        if (Fans == null)
        {
            //显示错误列表
            Response.Write(error);
        }

        //通过Fans.Data集合属性得到用户openId（由于人数过多，我这演示只显示10条）
        for (int i = 0; i < Fans.Data.Count - 820; i++)
        {
            //显示
            Label10.Text += "<br/>用户账号：" + Fans.Data[i];
        }
    }

    /// <summary>
    /// 修改分组名称
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button9_Click(object sender, EventArgs e)
    {
        //修改的名称
        string GroupsName = TextBox1.Text;
        string error = "";//错误描述

        bool IsBool = Information.UpdateGroupsName("分组Id", GroupsName, ref error);

        if (!IsBool)
        {
            Response.Write(error);
            return;
        }

        Response.Write("修改成功");
    }

    /// <summary>
    /// 移动用户分组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button10_Click(object sender, EventArgs e)
    {
        //要移动的用户Id
        string openid = TextBox2.Text;
        string groupId = TextBox3.Text;
        string error = "";

        bool IsBool = Information.ShiftUserGroups("用户OpenID", "要移动至分组的ID", ref error);

        if (!IsBool)
        {
            Response.Write(error);
            return;
        }

        Response.Write("移动成功");
    }

    /// <summary>
    /// 创建分组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button11_Click(object sender, EventArgs e)
    {
        string GroupsName = TextBox4.Text;
        int GruopId = 0;//创建成功后,微信服务器返回GruopId
        string error = "";

        bool IsBool = Information.CreateGroups(GroupsName, ref error, ref GruopId);

        if (!IsBool)
        {
            Response.Write(error);
            return;
        }

        Response.Write("创建成功");
    }

    /// <summary>
    /// 获取用户基本信息 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button8_Click(object sender, EventArgs e)
    {
        string error = "";

        UserInformation user = Information.GetUserInformation("用户OpenID", ref error);

        if (user == null)
        {
            Response.Write(error);

            return;
        }

        //读取对象信息
        Label3.Text = user.Nickname;//用户昵称
        string portrait = user.Headimgurl;//用户头像....微信服务器地址图片...
        Label2.Text = user.Openid;//用户账号..
        Label4.Text = user.Sex;//性别
        Label1.Text = user.Subscribe;
        Label5.Text = user.Language;
        Label6.Text = user.City;
        Label7.Text = user.Province;
        Label8.Text = user.Country;
        Image1.ImageUrl = user.Headimgurl;
        Label9.Text = user.Subscribe_time;
    }

    /// <summary>
    /// 获取分组列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void group_Click(object sender, EventArgs e)
    {
        string error = "";

        //调用获取分组接口
        List<GroupsInfromation> user = Information.AllGroupsInfromation(ref error);

        if (user == null)
        {
            Response.Write(error);
            return;
        }

        if (user.Count <=0)
        {
            Response.Write(error);
            return;   
        }

        groupList.InnerText.Remove(0);

        //循环集合获取
        for (int i = 0; i < user.Count; i++)
        {
            //显示
            groupList.InnerText += user[i].Name + "(" + user[i].Id + ")，";
        }
        Response.Write("成功");
    }

    /// <summary>
    /// 创建临时二维码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button12_Click(object sender, EventArgs e)
    {
        string scene_id = TextBox6.Text;
        string ticket = "";//创建二维码成功后,返回二维码对应的ticket,可以通过ticket下载二维码图片
        string error = "";

        //创建临时
        bool IsBool = Information.CreateTemporary_Two_Dimension_Code(scene_id, ref ticket, ref error);

        if (!IsBool)
        {
            Response.Write(error);
        }

        Response.Write("成功");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button13_Click(object sender, EventArgs e)
    {
        string scene_id = TextBox5.Text;
        string ticket = "";//创建二维码成功后,返回二维码对应的ticket,可以通过ticket下载二维码图片
        string error = "";

        //创建临时
        bool IsBool = Information.CreatePerpetual_Two_Dimension_Code(scene_id, ref ticket, ref error);

        if (!IsBool)
        {
            Response.Write(error);
        }

        Response.Write("成功");
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button14_Click(object sender, EventArgs e)
    {
        string error = "";
        string file = "D:/XXX.jpg";//要上传的文件路径
        string FileID = "";//上传成功后微信服务器返回文件的ID

        bool IsBool = Utility.UploadFile(file, "thumb", ref  FileID, ref error);

        if (!IsBool)
        {
            Response.Write(error);
            return;
        }

        Response.Write("上传成功");

    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button15_Click(object sender, EventArgs e)
    {
        string error = "";
        string FileID = "要下载的文件ID";
        string SaveFilePath = "";//如果为空默认Downloads/Weixin/文件夹

        bool IsBool = Utility.DownloadFile(FileID, SaveFilePath, ref error);

        if (!IsBool)
        {
            Response.Write(error);
            return;
        }

        Response.Write("上传成功");
    }

    /// <summary>
    /// 下载二维码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button6_Click1(object sender, EventArgs e)
    {
        string error = "";
        string ticket = TextBox7.Text;
        string SaveFilePath = "";//如果为空默认Downloads/Weixin/文件夹

        bool IsBool = Utility.DownloadTwo_Dimension_Code(ticket,SaveFilePath, ref error);

        if (!IsBool)
        {
            Response.Write(error);
            return;
        }

        Response.Write("上传成功");
    }

    /// <summary>
    /// 发送文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button16_Click(object sender, EventArgs e)
    {
        string error = "";

        //发送文本
        TextServiceMessage TextService =
            new TextServiceMessage("文本内容...", "微信用户OpenId");

        int ErrorCode = UserMessage.SendMessage(TextService,ref error);

        if (ErrorCode < 0)
        {
            //显示错误信息
            Response.Write(error);
            return;
        }

        Response.Write("成功");

    }

    /// <summary>
    /// 发送声音
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button17_Click(object sender, EventArgs e)
    {
        string error = "";

        //2.2如果发送语音:如下
        VoiceServiceMessage voice = new VoiceServiceMessage("语音文件Id", "要发送的微信用户Id");

        int ErrorCode = UserMessage.SendMessage(voice, ref error);

        //4:如果发送媒体文件，需要验证媒体文件ID是否过期,如果过期需要重新上传,然后再重新发送

        string FileId = "上传文件成功,返回上传文件的ID";//获取文件ID后,需要更新我们服务器上的ID

        if (ErrorCode == -2)
        {
            bool Is = Utility.UploadFile("文件路径", "上传文件的类型",ref FileId, ref  error);
        }

        //5：上传成功后，将更新后得到的文件ID替换我们保存之前保存的文件ID
        //....

        //6:重新构建实体类重新发送,
        ImageServiceMessage ImageServices = new ImageServiceMessage("图片文件ID", "用户账号openId");

        ErrorCode = UserMessage.SendMessage(ImageServices,ref error);

        //5:处理信息发送失败
        if (ErrorCode < 0)
        {
            //显示错误信息
            Response.Write(error);
            return;
        }

        Response.Write("成功");
    }

    /// <summary>
    /// 发送图文
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button18_Click(object sender, EventArgs e)
    {
        string error = "";

        //2.4：图文:
        ImageTextServiceMessage imtext = new ImageTextServiceMessage("爱情", "就是爱情","点击图片跳转链接", "图片路径(必须是服务器上的文件)", "要发送的微信用户ID");
        ImageTextServiceMessage imtext1 = new ImageTextServiceMessage("爱情", "就是爱情", "点击图片跳转链接", "图片路径(必须是服务器上的文件)", "要发送的微信用户ID");

        //3;调用发送客服信息接口   
        //       0:成功,
        //      -1:请求失败, 
        //      -2:文件过期, 需要重新调用上传媒体文件接口再次上传文件,上传成功后使用新的文件ID,重新发送
        int ErrorCode = UserMessage.SendMessage(imtext1, ref error);

        if (ErrorCode < 0)
        {
            //显示错误信息
            Response.Write(error);
            return;
        }
        Response.Write("成功");
    }
}