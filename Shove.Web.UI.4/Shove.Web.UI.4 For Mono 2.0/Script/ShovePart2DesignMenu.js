
/*
    页面设计浮动菜单(边栏)。
    作者：sunny
    时间：2007-8-2
*/
var ShovePart2DesignMenu_ie = document.all ? 1 : 0;
var ShovePart2DesignMenu_n = document.layers ? 1 : 0;
var ShovePart2DesignMenu_leftshow = 0;
var ShovePart2DesignMenu_menuSpeed = 40;

var ShovePart2DesignMenu_move = 10;
var ShovePart2DesignMenu_moveOnScroll = true;

var ShovePart2DesignMenu_time;
var ShovePart2DesignMenu_ltop;
var ShovePart2DesignMenu_imgEdit;


 var ShovePart2DesignMenu=null;

function ShovePart2DesignMenu_make(obj, nest)
{
//    nest = (!nest) ? '' : 'document.' + nest + '.';
//    
   // this.css = (ShovePart2DesignMenu_n) ? eval(nest + 'document.' + obj) : eval(obj + '.style');
//    
//    this.state = 1;
//    this.go = 0;
//    this.width = ShovePart2DesignMenu_n ? this.css.document.width : eval(obj + '.offsetWidth');
//    this.left = ShovePart2DesignMenu_getleft;
//    this.obj = obj + "Object"; 
//    
//    eval(this.obj + "=this");

    //nest = (!nest) ? '' : 'document.' + nest + '.';

    this.css = (ShovePart2DesignMenu_n) ? eval("document.getElementById('" + obj+"')") : eval("document.getElementById('" + obj+"').style");
    
    this.state = 1;
    this.go = 0;
    this.width = ShovePart2DesignMenu_n ? this.css.document.width : eval("document.getElementById('" + obj+"').offsetWidth");
    this.left = ShovePart2DesignMenu_getleft;
    this.obj = obj + "Object"; 
    
    eval(this.obj + "=this");
}

function ShovePart2DesignMenu_getleft()
{
    var gleft = (ShovePart2DesignMenu_n) ? eval(this.css.left) : eval(this.css.pixelLeft);
    
    return gleft;
}

function ShovePart2DesignMenu_moveMenu()
{

    if(!ShovePart2DesignMenu.state)
    {
        clearTimeout(ShovePart2DesignMenu_time);
        ShovePart2DesignMenu_MoveIn();
    }
    else
    {
        clearTimeout(ShovePart2DesignMenu_time);
        ShovePart2DesignMenu_MoveOut();
    }
}

function ShovePart2DesignMenu_MoveIn()
{

    if(ShovePart2DesignMenu.left() > - ShovePart2DesignMenu.width + ShovePart2DesignMenu_leftshow)
    {

        ShovePart2DesignMenu.go = 1;
        ShovePart2DesignMenu.css.left = ShovePart2DesignMenu.left() - ShovePart2DesignMenu_move;
        ShovePart2DesignMenu_time = setTimeout("ShovePart2DesignMenu_MoveIn()", ShovePart2DesignMenu_menuSpeed);
    }
    else
    {

        ShovePart2DesignMenu.go = 0;
        ShovePart2DesignMenu.state = 1;
    }
        
    ShovePart2DesignMenu_imgEdit = document.getElementById("ShovePart2_btnEdit");
    ShovePart2DesignMenu_imgEdit.src = "ShoveWebUI_client/Images/DesignerMenuOpen.gif";
}

function ShovePart2DesignMenu_MoveOut()
{
    if(ShovePart2DesignMenu.left() < 0)
    {
        ShovePart2DesignMenu.go = 1;
        ShovePart2DesignMenu.css.left = ShovePart2DesignMenu.left() + ShovePart2DesignMenu_move;
        ShovePart2DesignMenu_time = setTimeout("ShovePart2DesignMenu_MoveOut()", ShovePart2DesignMenu_menuSpeed);
    }
    else
    {
        ShovePart2DesignMenu.go = 0;
        ShovePart2DesignMenu.state = 0;
    }
    
    ShovePart2DesignMenu_imgEdit = document.getElementById("ShovePart2_btnEdit"); 
    ShovePart2DesignMenu_imgEdit.src = "ShoveWebUI_client/Images/DesignerMenuClose.gif";
}

function ShovePart2DesignMenu_checkScrolled()
{
    //if (!ShovePart2DesignMenu.go)
    //{
        ShovePart2DesignMenu.css.top = (eval(scrolled) + parseInt((ShovePart2DesignMenu_ltop + "").replace("px", ""), 10)) + "px";
    //}
    
    if (ShovePart2DesignMenu_n) 
    {
        setTimeout('ShovePart2DesignMenu_checkScrolled()', 30);
    }
}

if (document.all)
{
    window.attachEvent('onload',ShovePart2DesignMenu_Init)//对于IE
}
else
{
    window.addEventListener('load',ShovePart2DesignMenu_Init,false);//对于FireFox
}

function ShovePart2DesignMenu_Init()
{
    ShovePart2DesignMenu = new ShovePart2DesignMenu_make('ShovePart2_divMenu');
    scrolled = ShovePart2DesignMenu_n ? "window.pageYOffset" : "document.body.scrollTop + document.documentElement.scrollTop";
    ShovePart2DesignMenu.css.left = -ShovePart2DesignMenu.width + ShovePart2DesignMenu_leftshow;
    ShovePart2DesignMenu_ltop = (ShovePart2DesignMenu_n) ? ShovePart2DesignMenu.css.top : ShovePart2DesignMenu.css.pixelTop;
    ShovePart2DesignMenu.css.visibility = 'visible';
   // ShovePart2DesignMenu.css.display = 'block';
    if (ShovePart2DesignMenu_moveOnScroll)
    {
        ShovePart2DesignMenu_ie ? window.onscroll = ShovePart2DesignMenu_checkScrolled : ShovePart2DesignMenu_checkScrolled();
    }
}

function ShovePart2DesignMenu_AddNewPage_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShovePart2AddNewPage.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=340px;dialogHeight=120px;center: Yes;");
}


function ShovePart2DesignMenu_AddNewPageLayout_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShovePart2LoadLayout.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=600px;dialogHeight=400px;center: Yes;");
}

function ShovePart2DesignMenu_RestoreSiteLayout_Open(SiteID, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/SiteLayoutManager.aspx?SiteID=" + SiteID, "null", "dialogWidth=600px;dialogHeight=400px;center: Yes;");
}

function ShovePart2DesignMenu_RestorePageLayout_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShovePart2RestorePageLayout.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=600px;dialogHeight=400px;center: Yes;");
}

function ShovePart2DesignMenu_UpLoadStyle_Open(SiteID, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/UpLoadStyle.aspx?SiteID=" + SiteID, "null", "dialogWidth=600px;dialogHeight=300px;center: Yes;");
}

function ShovePart2DesignMenu_PageList_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/ShovePart2PageList.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=340px;dialogHeight=120px;center: Yes;");
}

function ShovePart2DesignMenu_CopyPage_Open(SiteID, PageName, SupportDir)
{
    return window.showModalDialog(SupportDir + "/Page/CopyPage.aspx?SiteID=" + SiteID + "&PageName=" + PageName, "null", "dialogWidth=340px;dialogHeight=120px;center: Yes;");
}


