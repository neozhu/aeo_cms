function combobox(sobj,al_v,al_t)
{

 var rmopo = window.createPopup();

 function rm(i,oct,h)
 {   
  var i2=eval(i);
  var oct=eval(oct);
  var w=eval(i).offsetWidth;
  var h=eval(h);
  var lefter = i2.offsetLeft-1; var topper = i2.offsetHeight;
  rmopo.document.body.innerHTML = oct.innerHTML;
  rmopo.document.body.style.border="1px solid #3162A6";
  rmopo.document.body.style.background="#F6F6F6";
  rmopo.show(lefter, topper, w, h, i2);
 }

 loadcombobox(sobj,al_v,al_t);

 function loadcombobox(obj,al_v,al_t)
 {
  var obj = eval(obj)
  theListArrayV = al_v;
  theListArrayT = al_t;

  var tempStr='<DIV id="'+obj.id+'showcombox" style="position:relative;visibility:hidden">'
    +'<DIV class="ac_menu" id="'+obj.id+'ListDiv" style="FONT-SIZE: 12px; Z-INDEX: 10; POSITION: absolute;OVERFLOW-Y:auto; WIDTH:expression('+obj.offsetWidth+'-1);">'
  for(var i=0;i<theListArrayV.length;i++)
   tempStr+='<DIV class="ac_menuitem" onmouseover="this.style.backgroundColor=\'#D6DEEC\';" onmouseout="this.style.backgroundColor=\'\';" onclick="this.selectedflag=1;parent.document.all.'+obj.id+'.value=this.value;parent.document.all.'+obj.id+'.blur();" style="cursor:default;" value="'+htmlEncode(theListArrayV[i])+'" textvalue="'+htmlEncode(theListArrayT[i])+'">'+htmlEncode(theListArrayT[i])+'</DIV>';
  tempStr+='</DIV></DIV>';

  obj.insertAdjacentHTML("afterEnd",tempStr);
  obj.onfocus=AC_OnFocus;
  obj.onclick=AC_OnFocus;
  obj.onblur=AC_OnBlur;
  obj.onkeydown=AC_OnKeyDown;
  obj.autoComplete="off";
  //obj.onpropertychange=AC_OnPropertyChange;
 }

 function AC_OnFocus(obj)
 {
  if(obj==null) obj=event.srcElement;
  popmenu=eval(obj.id+"showcombox");
  rm(obj,popmenu,130);
  //AC_OnPropertyChange(obj);
 }


 function AC_OnBlur(obj)
 {
  rmopo.hide();
 }

 function AC_OnPropertyChange(obj)
 {
  if(obj==null) obj=event.srcElement;

  var dv = eval("rmopo.document.all['"+obj.id+"ListDiv']");
  theListDiv = dv
  if(theListDiv==null) return ;
  var theListDivChildren=theListDiv.children;

  theListDiv.selectedIndex=-1;
  var theFirstVisibleIndex=-1;

  var objValue=obj.value;

  for(var i=0;i<theListDivChildren.length;i++)
  {
   if(theListDiv.children[i].textvalue.indexOf(objValue)==0)
   {
    if(theFirstVisibleIndex==-1) theFirstVisibleIndex=i;
    theListDivChildren[i].style.backgroundColor="#F6F6F6";
    theListDivChildren[i].style.display="";
   }
   else
    theListDivChildren[i].style.display="none";
   if(theListDiv.selectedIndex==-1 && theListDiv.children[i].textvalue==objValue)
   {
    theListDiv.selectedIndex=i;
   }
  }
  if(theListDiv.selectedIndex==-1 && theFirstVisibleIndex!=-1) 
  {
   theListDiv.selectedIndex=theFirstVisibleIndex;
  }
  if(theListDiv.selectedIndex!=-1)
  {
   theListDiv.children[theListDiv.selectedIndex].style.backgroundColor="#D6DEEC";
  }
  adjustListDivScroll(obj);
 }

 function AC_OnKeyDown(obj)
 {
  if(obj==null) obj=event.srcElement;

  var AC_TAB = 9;
  var AC_ENTER = 13;
  var AC_UP_ARROW = 38;
  var AC_DOWN_ARROW = 40;

  var dv = eval("rmopo.document.all['"+obj.id+"ListDiv']");
  theListDiv = dv
  if(theListDiv==null) return ;

  var keyCode=event.keyCode;
  if(keyCode==AC_ENTER) keyCode=event.keyCode=AC_TAB;

  if(keyCode==AC_TAB && theListDiv.selectedIndex!=-1) 
  {
   obj.value=theListDiv.children[theListDiv.selectedIndex].value;
   rmopo.hide() ;
  }
  
  if(keyCode==AC_UP_ARROW && theListDiv.selectedIndex!=-1)
  {
   for(var i=theListDiv.selectedIndex-1;i>-1;i--)
   {
    if(theListDiv.children[i].style.display!="none")
    {
     theListDiv.children[theListDiv.selectedIndex].style.backgroundColor="#F6F6F6";
     theListDiv.selectedIndex=i;
     theListDiv.children[theListDiv.selectedIndex].style.backgroundColor="#D6DEEC";
     adjustListDivScroll(obj);
     break;
    }
   }
  }
 
  if(keyCode==AC_DOWN_ARROW && theListDiv.selectedIndex!=-1)
  {
   for(var i=theListDiv.selectedIndex*1+1;i<theListDiv.children.length;i++)
   {
    if(theListDiv.children[i].style.display!="none")
    {
     theListDiv.children[theListDiv.selectedIndex].style.backgroundColor="#F6F6F6";
     theListDiv.selectedIndex=i;
     theListDiv.children[theListDiv.selectedIndex].style.backgroundColor="#D6DEEC";
     adjustListDivScroll(obj);
     break;
    }
   }
   return;
  }
 
 }

 function adjustListDivScroll(obj)
 {
  if(obj==null) obj=event.srcElement;

  var dv = eval("rmopo.document.all['"+obj.id+"ListDiv']");
  theListDiv = dv

  if ( theListDiv==null || theListDiv.selectedIndex==-1 ) return ;
  var i=theListDiv.selectedIndex;
  if((theListDiv.children[i].offsetTop<theListDiv.scrollTop)||(theListDiv.children[i].offsetTop>theListDiv.scrollTop+120))
   theListDiv.scrollTop=theListDiv.children[i].offsetTop-85;
 }

 function htmlEncode(str)
 {
  if(str==null) return "";
  str=str.replace(/</ig,"<")
  str=str.replace(/>/ig,">");
  //str=str.replace(/"/ig,""");
  return str;
 }
}