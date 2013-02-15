// JScript File

//copy selected option elements from one select element to another
function copyItem(fromlistboxid, tolistboxid, maxDestinationItems, errorMsgControl)
{
  var fromLB = document.getElementById(fromlistboxid);
	var toLB = document.getElementById(tolistboxid);

  //remove GuidEmpty item
  
  for (var i = 0; i < toLB.length; i++) 
  {            
		if (toLB.options[i].value == "00000000-0000-0000-0000-000000000000") 
		{
			toLB.options[i] = null;
		}            
	}  
	
	//copy selected items
  for (var i = 0; i < fromLB.length; i++) 
  {            
    if (toLB.length < maxDestinationItems)
    {
		  if (fromLB.options[i].selected) {
			  if (!(listItem(fromLB.options[i].value, toLB)))
			  {
				  var oOption = document.createElement("OPTION");
				  oOption.text = fromLB.options[i].text;
				  oOption.value = fromLB.options[i].value;
				  toLB.options.add(oOption);
			  }
		  }            
	  }
	  else
	  {
	    //Max reached show error msg
	    if (errorMsgControl != '')
	    {
	      var errMsgControl = document.getElementById(errorMsgControl);
	      if (errMsgControl)
	        errMsgControl.style.display = '';
	    }
	  }
  }                
}

//remove selected item from the select element
function removeItem(listBoxID, maxDestinationItems, minItemsLeft, errorMsgControl, errorMinMsgControl)
{
	var targetLB = document.getElementById(listBoxID);

  if (targetLB.length <= minItemsLeft)
  {
    //Min reached show error msg
    if (errorMinMsgControl != '')
    {
      var errMsgControl = document.getElementById(errorMinMsgControl);
      if (errMsgControl)
	      errMsgControl.style.display = 'block';
	    return;
    }
  }
  for (var i = -1 + targetLB.length; i > -1; i--) {            
		if (targetLB.options[i].selected) {
			targetLB.options[i] = null;
		}            
	}  
	
	if (targetLB.length < maxDestinationItems)
	{
	    //Max reached hide error msg
	    if (errorMsgControl != '')
	    {
	      var errMsgControl = document.getElementById(errorMsgControl);
	      if (errMsgControl)
  	      errMsgControl.style.display = 'none';
	    }
	}

}

//get the option element from the select element given the value property
function listItem(val, targetElement)
{
	var result = null;
  for (var i = 0; i < targetElement.length; i++) {            
		if (targetElement.options[i].value == val) {                
			result = targetElement.options[i];
			break;
		}            
	}   
	return result;
}

//select all <options> in <select>
function selectAll(targetID)
{
	var target = document.getElementById(targetID);

  for (var i = 0; i < target.length; i++) {            
		target.options[i].selected = true;
	}   
}