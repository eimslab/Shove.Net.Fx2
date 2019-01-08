/*
  btn = refrence to link button
  index = the index of the link button
  tabControlId = the client id of the TabsView control
  hfId = hidden field id
  unSelectedCSS = unselected css class name
  seletedCSS = selected css class name
*/


function OnTabClick(btn , index , tabControlId , hfId , unSelectedCSS , seletedCSS) 
{
  document.getElementById(tabControlId + "_TabHeader").className = "TabHeaderCSS";

  var exprArray = "tabButtons_" + tabControlId;
  var length = eval(exprArray + ".length");
    
  for ( var i=0; i<length;i++)
  {
    var exprElement = "tabButtons_" + tabControlId + "[" + i + "]";
    var btnId = eval(exprElement);
    //alert(id == btn.id);
    var contentId = eval("tabContents_" + tabControlId + "[" + i + "]");
    
    if ( btnId == btn.id)//when link button selected.
    {
      
      //document.getElementById(btnId).className = seletedCSS;
      document.getElementById(btnId + "_Parent" + i).className = seletedCSS;
      document.getElementById(contentId).style.display = "block";
      document.getElementById(hfId).value = i;//store the current index in hidden field.
    }
    else
    {
      //document.getElementById(btnId).className = unSelectedCSS;// "UnSelectedStyle";
      document.getElementById(btnId + "_Parent" + i).className = unSelectedCSS;
      document.getElementById(contentId).style.display = "none";
    }
  } 
  
 return false;    
}

function SelectTab(index , tabControlId , hfId , unSelectedCSS , seletedCSS)
{
    var exprElement = "tabButtons_" + tabControlId + "[" + index + "]";
    var btnId = eval(exprElement)
    OnTabClick(document.getElementById(btnId) , index , tabControlId , hfId , unSelectedCSS , seletedCSS );
}
