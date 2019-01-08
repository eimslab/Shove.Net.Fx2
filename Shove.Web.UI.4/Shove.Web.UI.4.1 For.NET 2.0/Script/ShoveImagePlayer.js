/*
*/
//ShoveWebUI_ShoveImagePlayer.js
var Stylestr="";
Stylestr=Stylestr+"        <style>";
Stylestr=Stylestr+"			/* 数字按钮框样式 */";
Stylestr=Stylestr+"			#imgTitle {FILTER:ALPHA(opacity=70);position:relative;left:0px;text-align:left;overflow: hidden;}			";
Stylestr=Stylestr+"			#imgTitle_up {left:0px;text-align: left; height:1px; width:inherit; }";
Stylestr=Stylestr+"			#imgTitle_down {left:0px;text-align: right; width:inherit; padding-right:5px;}";
Stylestr=Stylestr+"			#imgTitle_down_txt{text-align: center; vertical-align:bottom; padding-bottom:2px; width:inherit;}";
Stylestr=Stylestr+"			/* 图片框样式 */";
Stylestr=Stylestr+"			.imgClass {border: 0px solid #000;  padding-top:3px; z-index:-1;}";
Stylestr=Stylestr+"			/* 图片文字框样式 */";
Stylestr=Stylestr+"			#txtFrom {text-align: center;vertical-align: middle;}";
Stylestr=Stylestr+"			/* 数字按钮样式 */";
Stylestr=Stylestr+"			.button {text-decoration: none;padding: 2px 2px;background: #7B7B63;margin: 0px;font: bold 9px sans-serif; border-left:#fff 1px solid;}";
Stylestr=Stylestr+"			a.button, a.button:link, a.button:visited {font-family: sans-serif;text-decoration: none;color: #FFFFFF;background-color: #000000;}";
Stylestr=Stylestr+"			a.button:hover {font-family: sans-serif;text-decoration: none;color: #fff;background:#fff; }";
Stylestr=Stylestr+"			.buttonDiv {background: #000000;height: 1px;width: 21px;float: left;text-align: center;	vertical-align: middle;}";
Stylestr=Stylestr+"			/*渐变*/ ";
Stylestr=Stylestr+"			.trans { width:90px; background-color: #000;filter : progid:DXImageTransform.Microsoft.Alpha(startX=0, startY=0, finishX=100, finishY=100,style=1,opacity=0,finishOpacity=40);}";
Stylestr=Stylestr+"	    </style>";
 
document.write(Stylestr);

var ShoveWebUI_ShoveImagePlayer_imgHeight;			//图片高
var ShoveWebUI_ShoveImagePlayer_textFromHeight;			//焦点字框高度 (单位为px)
var ShoveWebUI_ShoveImagePlayer_imgWidth;				//图片宽
var ShoveWebUI_ShoveImagePlayer_textStyle;			//焦点字class style (不是连接class)
var ShoveWebUI_ShoveImagePlayer_textLinkStyle;		//焦点字连接class style
var ShoveWebUI_ShoveImagePlayer_buttonLineOn;		//button下划线on的颜色
var ShoveWebUI_ShoveImagePlayer_buttonLineOff;		//button下划线off的颜色
var ShoveWebUI_ShoveImagePlayer_TimeOut;				//每张图切换时间 (单位毫秒);

var ShoveWebUI_ShoveImagePlayer_imgUrl = new Array(); 
var ShoveWebUI_ShoveImagePlayer_imgLink = new Array();
var ShoveWebUI_ShoveImagePlayer_imgtext = new Array();
var ShoveWebUI_ShoveImagePlayer_imgAlt = new Array();

var count=0;
var theTimer = 0;
var key=0;

var ShoveWebUI_ShoveImagePlayer_adNum = 0;

function ShoveWebUI_ShoveImagePlayer_Init(imgWidth, imgHeight, textFromHeight, textStyle, textLinkStyle, buttonLineOn,
    buttonLineOff, TimeOut,
    imgUrl, imgLink, imgtext, imgAlt)
{
    ShoveWebUI_ShoveImagePlayer_imgHeight = imgHeight;			    //图片高
    ShoveWebUI_ShoveImagePlayer_textFromHeight = textFromHeight;	//焦点字框高度 (单位为px)
    ShoveWebUI_ShoveImagePlayer_textStyle = textStyle;			    //焦点字class style (不是连接class)
    ShoveWebUI_ShoveImagePlayer_imgWidth = imgWidth;				//图片宽
    ShoveWebUI_ShoveImagePlayer_textLinkStyle = textLinkStyle;		//焦点字连接class style
    ShoveWebUI_ShoveImagePlayer_buttonLineOn = buttonLineOn;		//button下划线on的颜色
    ShoveWebUI_ShoveImagePlayer_buttonLineOff = buttonLineOff;		//button下划线off的颜色
    ShoveWebUI_ShoveImagePlayer_TimeOut = TimeOut * 1000;				    //每张图切换时间 (秒);
    
	var TmpimgUrl = new Array(); 
	var TmpimgLink = new Array();
	var Tmpimgtext = new Array();
	var TmpimgAlt = new Array();

	var TmpimgUrl = imgUrl.split('|');
	var TmpimgLink = imgLink.split('|');
	var Tmpimgtext = imgtext.split('|');
	var TmpimgAlt = imgtext.split('|');
    var ImgCount = FilterRepeated(TmpimgUrl.length + "," + TmpimgLink.length + "," + Tmpimgtext.length);
    
    // 填充图片数组
    for (var i = 0; i < ImgCount; i ++)
    {
        ShoveWebUI_ShoveImagePlayer_imgUrl[i + 1] = TmpimgUrl[i];
        ShoveWebUI_ShoveImagePlayer_imgLink[i + 1] = TmpimgLink[i];
        ShoveWebUI_ShoveImagePlayer_imgtext[i + 1] = Tmpimgtext[i];
        ShoveWebUI_ShoveImagePlayer_imgAlt[i + 1] = TmpimgAlt[i];
    }


    //焦点字框高度样式表 开始
    document.write('<style type="text/css">');
    document.write('#focuseFrom{width:' + (ShoveWebUI_ShoveImagePlayer_imgWidth + 2) + ';margin: 0px; padding:0px;height:' + (ShoveWebUI_ShoveImagePlayer_imgHeight + ShoveWebUI_ShoveImagePlayer_textFromHeight) + 'px; overflow:hidden;}');
    document.write('#txtFrom{height:' + ShoveWebUI_ShoveImagePlayer_textFromHeight + 'px;line-height:' + ShoveWebUI_ShoveImagePlayer_textFromHeight + 'px;width:' + ShoveWebUI_ShoveImagePlayer_imgWidth + 'px;overflow:hidden;}');
    document.write('#imgTitle{width:' + ShoveWebUI_ShoveImagePlayer_imgWidth + ';top:-' + (ShoveWebUI_ShoveImagePlayer_textFromHeight + 24) + 'px;height:24px}');
    document.write('</style>');
    document.write('<div id="focuseFrom">');
    //焦点字框高度样式表 结束


    //NetScape开始
    if (navigator.appName == "Netscape")
    {
        document.write('<style type="text/css">');
        document.write('.buttonDiv{height:4px;width:21px;}');
        document.write('</style>');


        document.write('<a id="imgLink" href="' + ShoveWebUI_ShoveImagePlayer_imgLink[1] + '" target=_blank class="p1"><img src="' + ShoveWebUI_ShoveImagePlayer_imgUrl[1] + '" name="imgInit" width=' + ShoveWebUI_ShoveImagePlayer_imgWidth + ' height=' + ShoveWebUI_ShoveImagePlayer_imgHeight + ' border=1 alt="' + ShoveWebUI_ShoveImagePlayer_imgAlt[1] + '" class="imgClass"></a><div id="txtFrom"><span id="focustext" class="' + ShoveWebUI_ShoveImagePlayer_textStyle + '">' + ShoveWebUI_ShoveImagePlayer_imgtext[1] + '</span></div>')
        document.write('<div id="imgTitle">');
        document.write('<div id="imgTitle_down">');
        
        //数字按钮代码结束
        document.write('</div>');
        document.write('</div>');
        document.write('</div>');
        NetscapenextAd();
    }   //NetScape结束
    else    //IE开始
    {
        for (i=1;i<ShoveWebUI_ShoveImagePlayer_imgUrl.length;i++) {
            if( (ShoveWebUI_ShoveImagePlayer_imgUrl[i]!="") && (ShoveWebUI_ShoveImagePlayer_imgLink[i]!="")&&(ShoveWebUI_ShoveImagePlayer_imgtext[i]!="")&&(ShoveWebUI_ShoveImagePlayer_imgAlt[i]!="") ) {
	            count++;
            } else {
	            break;
            }
        }
        //alert(document.all);
        document.write('<a target=_self href="javascript:goUrl()"><img style="FILTER: revealTrans(duration=1,transition=5);" src="javascript:nextAd()" width=' + ShoveWebUI_ShoveImagePlayer_imgWidth + ' height=' + (ShoveWebUI_ShoveImagePlayer_imgHeight - 10) + ' border=0 vspace="0" name=imgInit class="imgClass"></a>');
        document.write('<div id="txtFrom"><span id="focustext" class="' + ShoveWebUI_ShoveImagePlayer_textStyle + '"></span></div>');
        document.write('<div id="imgTitle">');
        document.write('<div id="imgTitle_down"><a class="trans"></a>');
        //数字按钮代码开始
        for(var i = 1;i < ShoveWebUI_ShoveImagePlayer_imgUrl.length;i++){document.write('<a id="link' + i + '"  href="javascript:changeimg(' + i + ')" class="button" style="cursor:hand" title="' + ShoveWebUI_ShoveImagePlayer_imgAlt[i] + '" onFocus="this.blur()">' + i + '</a>');}
        //数字按钮代码结束
        document.write('</div>');
        document.write('</div>');
        document.write('</div>');
        nextAd();
    }   //IE结束
}
    
function FilterRepeated(Sort)
{
	var ResultSort = new Array();

	ResultSort = Sort.split(',');
	if (ResultSort.length == 1)
	{
		return 1;
	}

	var TemResultSort = "";
	var ReconstructionResult = 0;
	var intResultJ = 0;
	var intResultJ1 = 0;
	for (var i = 0; i < ResultSort.length; i++)
	{
		for (var j = 1; j < ResultSort.length - i; j++)
		{
			intResultJ = eval(ResultSort[j]);

			intResultJ1 = eval(ResultSort[j - 1]);

			if (intResultJ < intResultJ1)
			{
				TemResultSort = ResultSort[j - 1];
				ResultSort[j - 1] = ResultSort[j];
				ResultSort[j] = TemResultSort;
			}
		}
	}

	ReconstructionResult = eval(ResultSort[0]);

	return ReconstructionResult;
}

function changeimg(n)
{
	ShoveWebUI_ShoveImagePlayer_adNum = n;
	window.clearInterval(theTimer);
	ShoveWebUI_ShoveImagePlayer_adNum = ShoveWebUI_ShoveImagePlayer_adNum - 1;
	nextAd();
}

function NetscapenextAd()
{
    if(ShoveWebUI_ShoveImagePlayer_adNum < (ShoveWebUI_ShoveImagePlayer_imgUrl.length - 1))
    {
        ShoveWebUI_ShoveImagePlayer_adNum++;
    }
    else
    {
        ShoveWebUI_ShoveImagePlayer_adNum = 1;
    }
    
    theTimer=setTimeout("NetscapenextAd()", ShoveWebUI_ShoveImagePlayer_TimeOut);
    document.images.imgInit.src=ShoveWebUI_ShoveImagePlayer_imgUrl[ShoveWebUI_ShoveImagePlayer_adNum];
    document.images.imgInit.alt=ShoveWebUI_ShoveImagePlayer_imgAlt[ShoveWebUI_ShoveImagePlayer_adNum];	
    document.getElementById('focustext').innerHTML=ShoveWebUI_ShoveImagePlayer_imgtext[ShoveWebUI_ShoveImagePlayer_adNum];
    document.getElementById('imgLink').href=ShoveWebUI_ShoveImagePlayer_imgLink[ShoveWebUI_ShoveImagePlayer_adNum];
}

function goUrl(){
	window.open(ShoveWebUI_ShoveImagePlayer_imgLink[ShoveWebUI_ShoveImagePlayer_adNum], '_blank');
}

function playTran(){
    if (document.all)
    {
	    document.images.imgInit.filters.revealTrans.play();
	}
}    
    
function nextAd(){
    if (ShoveWebUI_ShoveImagePlayer_adNum < count)
    {
        ShoveWebUI_ShoveImagePlayer_adNum ++ ;
    }
    else
    {
        ShoveWebUI_ShoveImagePlayer_adNum = 1;
    }

    if( key == 0 )
    {
	    key = 1;
    }
    else if (document.all)
    {
	    document.images.imgInit.filters.revealTrans.Transition = 6;
	    document.images.imgInit.filters.revealTrans.apply();
		playTran();
    }

    document.images.imgInit.src=ShoveWebUI_ShoveImagePlayer_imgUrl[ShoveWebUI_ShoveImagePlayer_adNum];
    document.images.imgInit.alt=ShoveWebUI_ShoveImagePlayer_imgAlt[ShoveWebUI_ShoveImagePlayer_adNum];	
    document.getElementById('link'+ShoveWebUI_ShoveImagePlayer_adNum).style.background=ShoveWebUI_ShoveImagePlayer_buttonLineOn;
    
    for (var i=1;i<=count;i++)
    {
	    if (i!=ShoveWebUI_ShoveImagePlayer_adNum){document.getElementById('link'+i).style.background=ShoveWebUI_ShoveImagePlayer_buttonLineOff;}
    }
    
    document.all.focustext.innerHTML=ShoveWebUI_ShoveImagePlayer_imgtext[ShoveWebUI_ShoveImagePlayer_adNum];
    theTimer=setTimeout("nextAd()", ShoveWebUI_ShoveImagePlayer_TimeOut);
}

//Flash播放Images
//function ShoveWebUI_ShoveImagePlayerFlash_Init()
//{
//	if(typeof(shove)!="object"){var shove={}}
//shove.$=function(objId){if(!objId){throw new Error("shove.$(String objId)参数必须")}
//if(document.getElementById){return eval('document.getElementById("'+objId+'")')}else if(document.layers){return eval("document.layers['"+objId+"']")}else{return eval('document.all.'+objId)}}
//shoveFlash=function(C,v,x,V,c,X,i,O,I,l,o){var z=this;if(!document.createElement||!document.getElementById){return}
//z.id=v?v:"";z.classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000";z.codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version="+(c?c:"7")+",0,0,0";z.width=x;z.height=V;z.movie=C;z.bgcolor=X?X:null;z.quality=O?O:"high";z.src=z.movie;z.pluginspage="http://www.macromedia.com/go/getflashplayer";z.type="application/x-shockwave-flash";z.useExpressInstall=i?i:null;z.xir=(I)?I:window.location;z.redirectUrl=l?l:null;z.detectKey=o?o:null;z.escapeIs=false;z.objAttrs={};z.params={};z.flashVars=[];z.flashVarsStr="";z.embedAttrs={};z.forSetAttribute("id",z.id);z.objAttrs["classid"]=z.classid;z.forSetAttribute("codebase",z.codebase);z.forSetAttribute("width",z.width);z.forSetAttribute("height",z.height);z.forSetAttribute("movie",z.movie);z.forSetAttribute("quality",z.quality);z.forSetAttribute("pluginspage",z.pluginspage);z.forSetAttribute("type",z.type);z.forSetAttribute("bgcolor",z.bgcolor)}
//shoveFlash.prototype={getFlashHtml:function(){var I=this,i='<object ';for(var l in I.objAttrs){i+=l+'="'+I.objAttrs[l]+'" '}
//i+='>';for(var l in I.params){i+='<param name="'+l+'" value="'+I.params[l]+'" /> '}
//if(I.flashVarsStr!=""){i+='<param name="FlashVars" value="'+I.flashVarsStr+'" /> '}
//i+='<embed ';for(var l in I.embedAttrs){i+=l+'="'+I.embedAttrs[l]+'" '}
//i+=' ></embed></object>';return i},forSetAttribute:function(I,i){var l=this;I=I.toLowerCase();switch(I){case "classid":break;case "pluginspage":l.embedAttrs["pluginspage"]=i;break;case "src":l.embedAttrs["src"]=i;l.params["movie"]=i;break;case "movie":l.params["movie"]=i;l.embedAttrs["src"]=i;break;case "onafterupdate":case "onbeforeupdate":case "onblur":case "oncellchange":case "onclick":case "ondblClick":case "ondrag":case "ondragend":case "ondragenter":case "ondragleave":case "ondragover":case "ondrop":case "onfinish":case "onfocus":case "onhelp":case "onmousedown":case "onmouseup":case "onmouseover":case "onmousemove":case "onmouseout":case "onkeypress":case "onkeydown":case "onkeyup":case "onload":case "onlosecapture":case "onpropertychange":case "onreadystatechange":case "onrowsdelete":case "onrowenter":case "onrowexit":case "onrowsinserted":case "onstart":case "onscroll":case "onbeforeeditfocus":case "onactivate":case "onbeforedeactivate":case "ondeactivate":case "type":l.embedAttrs["type"]=i;break;case "codebase":l.objAttrs["codebase"]=i;break;case "width":l.objAttrs["width"]=i;l.embedAttrs["width"]=i;break;case "height":l.objAttrs["height"]=i;l.embedAttrs["height"]=i;break;case "align":l.objAttrs["align"]=i;l.embedAttrs["align"]=i;break;case "vspace":l.objAttrs["vspace"]=i;l.embedAttrs["vspace"]=i;break;case "hspace":l.objAttrs["hspace"]=i;l.embedAttrs["hspace"]=i;break;case "class":l.objAttrs["class"]=i;l.embedAttrs["class"]=i;break;case "title":l.objAttrs["title"]=i;break;case "accesskey":l.objAttrs["accesskey"]=i;break;case "name":l.objAttrs["name"]=i;l.embedAttrs["name"]=i;break;case "id":l.objAttrs["id"]=i;l.embedAttrs["name"]=i;break;case "tabindex":l.objAttrs["tabindex"]=i;break;default:l.params[I]=l.embedAttrs[I]=i}},forGetAttribute:function(i){var I=this;i=i.toLowerCase();if(I.objAttrs[i]!=undefined){return I.objAttrs[i]}else if(I.embedAttrs[i]!=undefined){return I.embedAttrs[i]}else if(I.embedAttrs!=undefined){return I.embedAttrs[i]}else{return null}},setAttribute:function(I,i){this.forSetAttribute(I,i)},getAttribute:function(i){return this.forGetAttribute(i)},addVariable:function(I,i){var l=this;if(l.escapeIs){I=escape(I);i=escape(i)}
//if(l.flashVarsStr==""){l.flashVarsStr=I+"="+i}else{l.flashVarsStr+="&"+I+"="+i}
//l.embedAttrs["FlashVars"]=l.flashVarsStr},getVariable:function(I){var o=this,i=o.flashVarsStr;if(o.escapeIs){I=escape(I)}
//var l=new RegExp(I+"=([^\\&]*)(\\&?)","i").exec(i);if(o.escapeIs){return unescape(RegExp.$1)}
//return RegExp.$1},addParam:function(I,i){this.forSetAttribute(I,i)},getParam:function(){return this.forGetAttribute(name)},write:function(i){var I=this;if(typeof i=="string"){shove.$(i).innerHTML=I.getFlashHtml()}else if(typeof i=="object"){i.innerHTML=I.getFlashHtml()}}}
//}

function ShoveWebUI_ShoveImagePlayerFlash_Onload(ID, FocusWidth, FocusHeight, TextHeight, Pics, Links, Texts, FlashAddress, TitleBgColor)
{
	var ShoveWebUI_ShoveImagePlayer_FocusWidth = FocusWidth;
	var ShoveWebUI_ShoveImagePlayer_FocusHeight = FocusHeight;
	var ShoveWebUI_ShoveImagePlayer_TextHeight = TextHeight;
	var ShoveWebUI_ShoveImagePlayer_SwfHeight = ShoveWebUI_ShoveImagePlayer_FocusHeight + ShoveWebUI_ShoveImagePlayer_TextHeight;
	
	document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="'+ ShoveWebUI_ShoveImagePlayer_FocusWidth +'" height="'+ ShoveWebUI_ShoveImagePlayer_SwfHeight +'">');
    document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="' + FlashAddress + '"> <param name="quality" value="high"><param name="bgcolor" value="' + TitleBgColor + '">');
    document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
    document.write('<param name="FlashVars" value="pics='+ Pics +'&links='+ Links +'&texts='+ Texts +'&borderwidth='+ ShoveWebUI_ShoveImagePlayer_FocusWidth +'&borderheight='+ ShoveWebUI_ShoveImagePlayer_FocusHeight +'&textheight='+ ShoveWebUI_ShoveImagePlayer_TextHeight +'">');
    document.write('<embed src="' + FlashAddress + '" wmode="opaque" FlashVars="pics='+ Pics +'&links='+ Links +'&texts='+ Texts +'&borderwidth='+ ShoveWebUI_ShoveImagePlayer_FocusWidth +'&borderheight='+ ShoveWebUI_ShoveImagePlayer_FocusHeight +'&textheight='+ ShoveWebUI_ShoveImagePlayer_TextHeight +'" menu="false" bgcolor="#ffffff" quality="high" width="'+ ShoveWebUI_ShoveImagePlayer_FocusWidth +'" height="'+ ShoveWebUI_ShoveImagePlayer_SwfHeight +'" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />');
    document.write('</object>');

//	var ShoveWebUI_ShoveImagePlayer_Curhref = document.location.href;
// 
//	var ImageFlashPlay = new shoveFlash(FlashAddress, "focusflash", ShoveWebUI_ShoveImagePlayer_FocusWidth, ShoveWebUI_ShoveImagePlayer_SwfHeight, "7", TitleBgColor, false, "High");
//	ImageFlashPlay.addParam("allowScriptAccess", "sameDomain");
//	ImageFlashPlay.addParam("menu", "false");
//	ImageFlashPlay.addParam("wmode", "opaque");
//	
//	ImageFlashPlay.addVariable("pics", Pics);
//	ImageFlashPlay.addVariable("links", Links);
//	ImageFlashPlay.addVariable("texts", Texts);
//	ImageFlashPlay.addVariable("borderwidth", ShoveWebUI_ShoveImagePlayer_FocusWidth);
//	ImageFlashPlay.addVariable("borderheight", ShoveWebUI_ShoveImagePlayer_FocusHeight);
//	ImageFlashPlay.addVariable("textheight", ShoveWebUI_ShoveImagePlayer_TextHeight);
//	ImageFlashPlay.addVariable("curhref", ShoveWebUI_ShoveImagePlayer_Curhref);
//	ImageFlashPlay.write(ID);
}