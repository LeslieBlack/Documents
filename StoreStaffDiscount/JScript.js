
/****************************************************************
    
                        AJAX PAGE METHOD
    
****************************************************************/

function onCallbackComplete(PageMethodReturnValue) {
    //What to do on successful completion of the callback.
    //The input parameter is the return value from the page method.
    var obj = document.getElementById("DivDeptEmpNames");
    obj.innerHTML = PageMethodReturnValue;
    var Dept;

    Dept = GetDept();
}

function onCallbackError() {
    //What to do in the event of an error during the callback
    alert("Error");
}

function GetDept() {
    var obj;
    var Dept;

    obj = document.getElementById("drpDept");
   
    if (obj.selectedIndex != 0) {
        Dept = obj.options[obj.selectedIndex].value;
        DisplayStoreAddress(Dept);
    }
    else {
        Dept = "";
       // document.getElementById("txtEmpNo").style.display = "none";
        //document.getElementById("inpRefresh").style.display = "none";
        document.getElementById("divStoreAddress").innerHTML = ""
    }
    return Dept;
}

function Department_OnSelChange() {
    //Event handler for the control.
    //Invoking the WebMethod from the Code Behind Page.   
    var Dept;
    Dept = GetDept();
   
   
    // Call the server side page method.   
    //PageMethods.drp_Names(Dept,null,
    //onCallbackComplete,
    //onCallbackError);

}

function DisplayStoreAddress(Dept) {
    // Call the server side page method.   
    PageMethods.txt_StoreAddress(Dept,
        onStoreAddressSuccess,
        onStoreAddressFailed);

    //var obj;
    //obj = document.getElementById("inpRefresh");
    //obj.style.display = "block";
}

function onStoreAddressSuccess(PageMethodReturnValue) {
    //What to do on successful completion of the callback.
    //The input parameter is the return value from the page method.
    document.getElementById("tblAddress").style.display = "block";
    var obj = document.getElementById("divStoreAddress");
    obj.innerHTML = PageMethodReturnValue;
}

function onStoreAddressFailed() {
    //What to do in the event of an error during the callback
    alert("Error");
}

/************************************************

           Javascript Methods

************************************************/

function Form_Submit() {
   
    var ans;
    ans = Page_ClientValidate("Depts"); 
    //alert("2");
    if (ans == true) {
       // alert("3");
        var obj;
        obj = document.getElementById("drpNames");
        if (obj != null) {
           // alert("4");
            if (obj.selectedIndex == 0) {
                alert("You must select a name.");
            }
            else {
                StaffDiscForm.submit();
                return Page_IsValid;
            }
        }       
    }
    //alert("End");
}

function GetPayrollID(){
    var obj;
    var PayrollID;

    obj = document.getElementById("drpNames");

    if (obj.selectedIndex != 0) {
        PayrollID = obj.options[obj.selectedIndex].value;
    }
    else {
        PayrollID = -1;
    }
    return PayrollID;
}