using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shove.Gateways.Weixin.Gongzhong;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public partial class Weixingongzhong : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string toKen = "token"; //token值（在微信公众平台申请开发者自定义的一个值）;
        //发送微信信息，回复给用户
        string errorDescription = "";
        //将收到的文件保存到数据库中(测试案例例子)

        if (!Shove.Gateways.Weixin.Gongzhong.UserMessage.Handle(toKen, GetData, ref errorDescription))
        {
            Shove._IO.Log log = new Shove._IO.Log("Weixingongzhong");

            log.Write(errorDescription);
        }
    }

    ///// <summary>
    ///// 回调方法,取得回复用户的信息,
    ///// </summary>
    ///// <param name="message">用户请求的信息,实体对象(通过对象获取用户点击菜单对应Key)</param>
    ///// <param name="MsgType">用户请求的消息类型</param>
    ///// <returns>返回,回复的消息内容</returns>
    public List<Message> GetData(Message requestMessage, string MsgType, ref string errorDescription)
    {
        //保存消息对象集合
        List<Message> replyMessage = new List<Message>();

        switch (MsgType)
        {
            case "text"://文本
                RequestTextMessage TextMessage = requestMessage as RequestTextMessage;
                #region 测试案例
                //将收到的文件保存到数据库中(测试案例例子)  //string.Conten = TextMessage.Content;//得到用户发送内容
                //string.FromUserName = TextMessage.FromUserName;//发送发账号
                //string.ToUserName = TextMessage.ToUserName;//公众账号
                //string.MsgType = TextMessage.MsgType;//消息类型
                //string.CreateTime = TextMessage.CreateTime;//发送时间
                #endregion 测试案例

                replyMessage.Add(TextMessage);
                break;
            case "event"://菜单事件 

                RepuestGeographicalPositionmMessage Position = null;
                RequestEventMessage img = null;

                //如果是上报地理位置事件
                if (requestMessage is RepuestGeographicalPositionmMessage)
                {
                    Position = requestMessage as RepuestGeographicalPositionmMessage;

                    #region 测试案例
                    //将用户发来的语音信息文件保存到数据库中(测试案例例子)
                    //string.Location = Position.Location;//得到用户上报的详细位置
                    //string.FromUserName = Position.FromUserName;//发送发账号
                    //string.ToUserName = Position.ToUserName;//公众账号
                    //string.MsgType = Position.MsgType;//消息类型
                    //string.DateTime = Position.CreateTime;//发送时间
                    #endregion 测试案例
                }
                else
                {
                    img = requestMessage as RequestEventMessage;

                    //判断是否是第一次关注,(两种关注 1:扫描二维码关注,推送二维码参数 2:手动关注，没有二维码)
                    if (img.Event.Equals("subscribe"))
                    {
                        TextMessage txtmag1 = new TextMessage("欢迎关注英迈思,科技创造新世界<a href=\"http://www.xiniuyun.com/\">4006-854-855</a>");
                        replyMessage.Add(txtmag1);

                        #region 测试案例
                        //将用户发来的语音信息文件保存到数据库中(测试案例例子)
                        // string.Ticket = img.Ticket;//用户扫描二维码关注公众账号的场景ID(二维码参数)
                        // string.FromUserName = img.FromUserName;//发送方账号
                        // string.ToUserName = img.ToUserName;//公众账号
                        // string.MsgType= img.MsgType;//消息类型
                        //  string.DateTime = img.CreateTime;//发送时间
                        #endregion
                    }
                    else//菜单点击事件
                    {
                        //判断用户点击的是哪个菜单栏,【img.EventKey】对应菜单的key,通过key做出对应的数据回复
                        if (img.EventKey == "21")
                        {
                            #region 构建实体类,
                            //图文对象 默认第一个为大图
                            ImageTextMessage imageText =
                                new ImageTextMessage("2", "英迈思公共事业部", "中国第一高端品牌,英迈思旗下品牌",
                                    "http://test004.shovesoft.com//images/logo.jpg", "http://www.myeims.com/");

                            ImageTextMessage imageText1 =
                                new ImageTextMessage("2", "犀牛云网站", "让企业牛起来!",
                                   "http://test004.shovesoft.com//images/logo.jpg", "http://www.xiniu.me/");

                            //将构建好的实体放入集合中
                            replyMessage.Add(imageText);
                            replyMessage.Add(imageText1);

                            #endregion
                        }
                        else if (img.EventKey == "3")
                        {
                            TextMessage txtmag2 = new TextMessage("英迈思,<a href=\"http://www.xiniuyun.com/\">更多,4006-854-855</a>");
                            replyMessage.Add(txtmag2);
                        }
                    }
                }

                break;

            case "image"://图片

                #region 测试案例
                //RequestImageMessage ImageMessage = requestMessage as RequestImageMessage;
                //string CreateTime = ImageMessage.CreateTime.ToString();//消息发送时间
                //string FromUserName = ImageMessage.FromUserName;//发送方账号
                //string MediaId = ImageMessage.MediaId;//图片文件ID；
                //string MsgTypes = ImageMessage.MsgType;//消息类型
                //string PicUrl = ImageMessage.PicUrl;//图片URL
                //string ToUserName = ImageMessage.ToUserName;//公众账号

                //调用下载媒体接口，通过文件ID下载文件
                //Utility.DownloadFile(MediaId, ACCESS_TOKEN, ref error);//下载图片

                #endregion

                break;
            case "link"://链接



                break;
            case "location"://地理位置


                break;
            case "voice"://声音

               
                RequestVoiceMessage VoiceMessage = requestMessage as RequestVoiceMessage;

                 #region 测试案例
                //将用户发来的语音信息文件保存到数据库中(测试案例例子)
                //string.Recognition = VoiceMessage.Recognition;//语音的分辨后的结果
                //string.FromUserName = VoiceMessage.FromUserName;//公众账号
                //string.ToUserName = VoiceMessage.ToUserName;//发送发账号
                //string.MsgType= VoiceMessage.MsgType;//消息类型
                //string.DateTime = VoiceMessage.CreateTime;//发送时间
                //string MediaId=  VoiceMessage.MediaId;//语音文件ID；

                //调用接口下载语音文件,下载后的文件保存在Downloads/Weixin/中  [测试]
                // bool Isbool = Utility.DownloadFile(VoiceMessage.MediaId, ACCESS_TOKEN, ref errorDescription); 
                #endregion

                break;

            case "video"://视频

                #region 测试案例
                //RequestVideoMessage VideoMessage = requestMessage as RequestVideoMessage;

                //string MediaId = VideoMessage.MediaId;//视频ID
                //string ThumbMediaId = VideoMessage.ThumbMediaId;//缩略图ID
                //string FromUserName = VideoMessage.FromUserName;
                //string ToUserName = VideoMessage.ToUserName;
                //string CreateTime = VideoMessage.CreateTime.ToString();
                //string MsgTypes = VideoMessage.MsgType;
                //string error = "";

                //调用下载媒体接口，通过文件ID下载文件

                //Utility.DownloadFile(MediaId, ACCESS_TOKEN, ref error);//下载视频
                //Utility.DownloadFile(ThumbMediaId, ACCESS_TOKEN, ref error);//下载缩略图
                #endregion

                break;

            default:

                break;

        }
        return replyMessage;
    }
}