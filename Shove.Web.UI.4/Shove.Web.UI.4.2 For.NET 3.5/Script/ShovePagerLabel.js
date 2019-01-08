
/*
    实现用文本的分页显示
    作者：shove
    时间：2007-11-21
*/

function ShoveWebUI_ShovePagerLabel_OnPageChanged(PageID, IDPreFix)
{
    var i = 0;
    var o_Page;
    
    while (true)
    {
        o_Page = document.getElementById(IDPreFix + i);
        
        if (!o_Page)
        {
            break;
        }
        
        o_Page.style.display = "none";
        
        i++;
    }
    
    o_Page = document.getElementById(PageID);

    if (!o_Page)
    {
        return;
    }

    o_Page.style.display = "";
}