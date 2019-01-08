
/*
    Flash 播放控件，为了解决 IE Flash 周围边框，并需要单击激活的问题。
    作者：shove
    时间：2007-8-20
*/

function ShoveWebUI_ShoveFlashPlayer_Play(Src, Width, Height)
{
    document.write('<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,18,0"\n');
    document.write('height="' + Height + '" width="' + Width + '" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" VIEWASTEXT>\n');
    document.write('<PARAM NAME="_cx" VALUE="16536">\n');
    document.write('<PARAM NAME="_cy" VALUE="1852">\n');
    document.write('<PARAM NAME="FlashVars" VALUE="">\n');
    document.write('<PARAM NAME="Movie" VALUE="' + Src + '">\n');
    document.write('<PARAM NAME="Src" VALUE="' + Src + '">\n');
    document.write('<PARAM NAME="WMode" VALUE="Opaque">\n');
    document.write('<PARAM NAME="Play" VALUE="-1">\n');
    document.write('<PARAM NAME="Loop" VALUE="-1">\n');
    document.write('<PARAM NAME="Quality" VALUE="High">\n');
    document.write('<PARAM NAME="SAlign" VALUE="">\n');
    document.write('<PARAM NAME="Menu" VALUE="-1">\n');
    document.write('<PARAM NAME="Base" VALUE="">\n');
    document.write('<PARAM NAME="AllowScriptAccess" VALUE="">\n');
    document.write('<PARAM NAME="Scale" VALUE="ShowAll">\n');
    document.write('<PARAM NAME="DeviceFont" VALUE="0">\n');
    document.write('<PARAM NAME="EmbedMovie" VALUE="0">\n');
    document.write('<PARAM NAME="BGColor" VALUE="">\n');
    document.write('<PARAM NAME="SWRemote" VALUE="">\n');
    document.write('<PARAM NAME="MovieData" VALUE="">\n');
    document.write('<PARAM NAME="SeamlessTabbing" VALUE="1">\n');
    document.write('<PARAM NAME="Profile" VALUE="0">\n');
    document.write('<PARAM NAME="ProfileAddress" VALUE="">\n');
    document.write('<PARAM NAME="ProfilePort" VALUE="0">\n');
    document.write('<PARAM NAME="AllowNetworking" VALUE="all">\n');
    document.write('<embed pluginspage="http://www.macromedia.com/go/getflashplayer" src="' + Src + '" width="' + Width + '" height="' + Height + '" type="application/x-shockwave-flash" quality="high"></embed>\n');
    document.write('</OBJECT>\n');
}