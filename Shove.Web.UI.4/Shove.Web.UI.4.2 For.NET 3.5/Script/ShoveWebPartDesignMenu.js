
/*
    页面设计浮动菜单(边栏)。
    作者：sunny
    时间：2007-8-2
*/
var ShoveWebPartDesignMenu_ie = document.all ? 1 : 0;
var ShoveWebPartDesignMenu_n = document.layers ? 1 : 0;
var ShoveWebPartDesignMenu_leftshow = 0;
var ShoveWebPartDesignMenu_menuSpeed = 40;

var ShoveWebPartDesignMenu_move = 10;
var ShoveWebPartDesignMenu_moveOnScroll = true;

var ShoveWebPartDesignMenu_time;
var ShoveWebPartDesignMenu_ltop;
var ShoveWebPartDesignMenu_imgEdit;


 var ShoveWebPartDesignMenu=null;

function ShoveWebPartDesignMenu_make(obj, nest)
{
//    nest = (!nest) ? '' : 'document.' + nest + '.';
//    
   // this.css = (ShoveWebPartDesignMenu_n) ? eval(nest + 'document.' + obj) : eval(obj + '.style');
//    
//    this.state = 1;
//    this.go = 0;
//    this.width = ShoveWebPartDesignMenu_n ? this.css.document.width : eval(obj + '.offsetWidth');
//    this.left = ShoveWebPartDesignMenu_getleft;
//    this.obj = obj + "Object"; 
//    
//    eval(this.obj + "=this");

    //nest = (!nest) ? '' : 'document.' + nest + '.';

    this.css = (ShoveWebPartDesignMenu_n) ? eval("document.getElementById('" + obj+"')") : eval("document.getElementById('" + obj+"').style");
    
    this.state = 1;
    this.go = 0;
    this.width = ShoveWebPartDesignMenu_n ? this.css.document.width : eval("document.getElementById('" + obj+"').offsetWidth");
    this.left = ShoveWebPartDesignMenu_getleft;
    this.obj = obj + "Object"; 
    
    eval(this.obj + "=this");
}

function ShoveWebPartDesignMenu_getleft()
{
    var gleft = (ShoveWebPartDesignMenu_n) ? eval(this.css.left) : eval(this.css.pixelLeft);
    
    return gleft;
}

function ShoveWebPartDesignMenu_moveMenu()
{

    if(!ShoveWebPartDesignMenu.state)
    {
        clearTimeout(ShoveWebPartDesignMenu_time);
        ShoveWebPartDesignMenu_MoveIn();
    }
    else
    {
        clearTimeout(ShoveWebPartDesignMenu_time);
        ShoveWebPartDesignMenu_MoveOut();
    }
}

function ShoveWebPartDesignMenu_MoveIn()
{

    if(ShoveWebPartDesignMenu.left() > - ShoveWebPartDesignMenu.width + ShoveWebPartDesignMenu_leftshow)
    {

        ShoveWebPartDesignMenu.go = 1;
        ShoveWebPartDesignMenu.css.left = ShoveWebPartDesignMenu.left() - ShoveWebPartDesignMenu_move;
        ShoveWebPartDesignMenu_time = setTimeout("ShoveWebPartDesignMenu_MoveIn()", ShoveWebPartDesignMenu_menuSpeed);
    }
    else
    {

        ShoveWebPartDesignMenu.go = 0;
        ShoveWebPartDesignMenu.state = 1;
    }
        
    ShoveWebPartDesignMenu_imgEdit = document.getElementById("ShoveWebPart_btnEdit");
    ShoveWebPartDesignMenu_imgEdit.src = "ShoveWebUI_client/Images/DesignerMenuOpen.gif";
}

function ShoveWebPartDesignMenu_MoveOut()
{
    if(ShoveWebPartDesignMenu.left() < 0)
    {
        ShoveWebPartDesignMenu.go = 1;
        ShoveWebPartDesignMenu.css.left = ShoveWebPartDesignMenu.left() + ShoveWebPartDesignMenu_move;
        ShoveWebPartDesignMenu_time = setTimeout("ShoveWebPartDesignMenu_MoveOut()", ShoveWebPartDesignMenu_menuSpeed);
    }
    else
    {
        ShoveWebPartDesignMenu.go = 0;
        ShoveWebPartDesignMenu.state = 0;
    }
    
    ShoveWebPartDesignMenu_imgEdit = document.getElementById("ShoveWebPart_btnEdit"); 
    ShoveWebPartDesignMenu_imgEdit.src = "ShoveWebUI_client/Images/DesignerMenuClose.gif";
}

function ShoveWebPartDesignMenu_checkScrolled()
{
    //if (!ShoveWebPartDesignMenu.go)
    //{
        ShoveWebPartDesignMenu.css.top = (eval(scrolled) + parseInt((ShoveWebPartDesignMenu_ltop + "").replace("px", ""), 10)) + "px";
    //}
    
    if (ShoveWebPartDesignMenu_n) 
    {
        setTimeout('ShoveWebPartDesignMenu_checkScrolled()', 30);
    }
}

if (document.all)
{
    window.attachEvent('onload',ShoveWebPartDesignMenu_Init)//对于IE
}
else
{
    window.addEventListener('load',ShoveWebPartDesignMenu_Init,false);//对于FireFox
}

function ShoveWebPartDesignMenu_Init()
{
    ShoveWebPartDesignMenu = new ShoveWebPartDesignMenu_make('ShoveWebPart_divMenu');
    scrolled = ShoveWebPartDesignMenu_n ? "window.pageYOffset" : "document.body.scrollTop + document.documentElement.scrollTop";
    ShoveWebPartDesignMenu.css.left = -ShoveWebPartDesignMenu.width + ShoveWebPartDesignMenu_leftshow;
    ShoveWebPartDesignMenu_ltop = (ShoveWebPartDesignMenu_n) ? ShoveWebPartDesignMenu.css.top : ShoveWebPartDesignMenu.css.pixelTop;
    ShoveWebPartDesignMenu.css.visibility = 'visible';
   // ShoveWebPartDesignMenu.css.display = 'block';
    if (ShoveWebPartDesignMenu_moveOnScroll)
    {
        ShoveWebPartDesignMenu_ie ? window.onscroll = ShoveWebPartDesignMenu_checkScrolled : ShoveWebPartDesignMenu_checkScrolled();
    }
}

function ShoveWebPartDesignMenu_AddNewPage_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShoveWebPartAddNewPage.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=340px;dialogHeight=120px;center: Yes;");
}


function ShoveWebPartDesignMenu_AddNewPageLayout_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShoveWebPartLoadLayout.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=600px;dialogHeight=400px;center: Yes;");
}

function ShoveWebPartDesignMenu_RestoreSiteLayout_Open(SiteID, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/SiteLayoutManager.aspx?SiteID=" + SiteID, "null", "dialogWidth=600px;dialogHeight=400px;center: Yes;");
}

function ShoveWebPartDesignMenu_RestorePageLayout_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShoveWebPartRestorePageLayout.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=600px;dialogHeight=400px;center: Yes;");
}

function ShoveWebPartDesignMenu_UpLoadStyle_Open(SiteID, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/UpLoadStyle.aspx?SiteID=" + SiteID, "null", "dialogWidth=600px;dialogHeight=300px;center: Yes;");
}

function ShoveWebPartDesignMenu_PageList_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShoveWebPartPageList.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=340px;dialogHeight=120px;center: Yes;");
}

function ShoveWebPartDesignMenu_CopyPage_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/CopyPage.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=340px;dialogHeight=120px;center: Yes;");
}


