using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shove.Gateways.Weixin.Gongzhong;
using System.Net;
using System.IO;
using System.Text;

public partial class _Default : System.Web.UI.Page 
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //初始化令牌方法,每个页面如果要调用接口,必须先初始化令牌
        Utility.GetAccessToken("APPID(唯一凭证)", "secret(第三方用户唯一凭证密钥)");
    }

    /// <summary>
    /// 创建菜单单击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string AppID = this.txt_AppId.Text;//唯一凭证(这个在微信公众平台申请自定义菜单获取)
        string AppSecret = this.txt_Appsecret.Text;//钥密(这个在微信公众平台申请自定义菜单获取)

        string errorDescription = "";
        //调用创建菜单方法(参数1：菜单集合的回调方法，参数2：唯一凭证,参数3：钥密，参数4：错误代码);
        bool successed = Shove.Gateways.Weixin.Gongzhong.MenuManager.Create(GetMenuData(), ref errorDescription);

    }

    /// <summary>
    /// 回调方法,
    /// </summary>
    /// <returns>返回菜单导航条集合</returns>
    public static List<MenuView> GetMenuData()
    {
        //保存所有菜单对象
        List<MenuView> listMenu = new List<MenuView>();

        //构建父级菜单对象【没有子级导航对象】
        MenuView parent = new MenuView();
        parent.Name = "成功案例";//菜单名称
        parent.Type = "click";//菜单类型
        parent.Key = "3";//菜单对象的key
        //将父级菜单对象（parent1）添加入菜单集合（listMenu）
        listMenu.Add(parent);


        //构建父级菜单对象【带有子级导航】
        MenuView parent1 = new MenuView();
        parent1.Name = "关于英迈思";//父菜单名称
        //构建子级导航对象
        Shove.Gateways.Weixin.Gongzhong.Menu submenu = new Shove.Gateways.Weixin.Gongzhong.Menu();
        submenu.Name = "关于";//子菜单名称
        submenu.Type = "view";//子菜单类型

        #region 如果需要网页授权,设置View事件的URL时如下：

        string KeyUrl = "http://test004.shovesoft.com/WeixingongzhongCallbackPage.aspx";//授权域名下的页面
        string appid = "wxdf9f01a2ae215175";//公众账号的APPID

        submenu.Key = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=123#wechat_redirect", appid, KeyUrl);//子菜单对象的key
        
        #endregion

        #region 如果不需要网页授权,设置View事件时只需要把域名填写上去：

       // submenu.Key = "http://www.myeims.com/";

        #endregion



        //将子级导航对象(submenu)添加入父级菜单（parent1）的list集合
        parent1.SubMenus.Add(submenu);
        //将父级菜单对象（parent1）添加入菜单集合（listMenu）
        listMenu.Add(parent1);


        ////构建父级菜单对象(parent2)[带有子导航]
        MenuView parent2 = new MenuView();
        parent2.Name = "最佳签约";//父菜单名称
        //构建子导航对象实体(submenu1)
        Shove.Gateways.Weixin.Gongzhong.Menu submenu1 = new Shove.Gateways.Weixin.Gongzhong.Menu();
        submenu1.Name = "签约";//子菜单名称
        submenu1.Type = "click";//子菜单类型
        submenu1.Key = "21";//子菜单对象的key
        //将子级导航(submenu1)添加入父级(parent2)对象的list集合中
        parent2.SubMenus.Add(submenu1);
        //将父级菜单对象(parent2)添加入菜单集合（listMenu）
        listMenu.Add(parent2);

        //返回菜单集合
        return listMenu;
    }

    /// <summary>
    /// 删除菜单事件,
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bnt_del_Click(object sender, EventArgs e)
    {
        // 唯一凭证
        string AppID = this.txt_AppIddel.Text;
        // 钥密
        string AppSecret = this.txt_Appsecretdel.Text;
        
        // 删除菜单
        string errorDescription = "";
        bool successed = Shove.Gateways.Weixin.Gongzhong.MenuManager.Delete(ref errorDescription);
    }
}