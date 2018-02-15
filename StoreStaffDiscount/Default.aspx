<%@ Page  Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>  
     
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />  

    <script language="jscript" src="JScript.js" type="text/jscript" >
    </script>   

    <style type="text/css">       
         .RowSpacer {
            height:25px;
        }
        .cssStoreAddress {
            text-transform:capitalize;
        }
        .cssEmpNo {
            /*display:none;*/
        }       
         #cssEmpNoHdr, #cssDiscNoHdr{
           padding:5px 5px 5px 5px;
           font-weight:bold; 
           width:120px;
           text-align:right;
        }       
         #txtEmpNo {
           width:80px;
        }       
         #cssDiscField, #rblDiscRate, .cssFormFields{
           width:100px;
           text-align:center;
           padding:5px 5px 5px 5px;
        }       
         #tblEmpRate {
           width:230px;            
           border-spacing:0px;
           /*border:solid black 1px;*/
        }
        .Address{
            font-size:10pt;
            font-family:Tahoma;
            text-align:left;
            text-transform:capitalize;
        }
        .cssNote {
             font-size:8pt;
            font-family:Tahoma;
            text-align:left;
            vertical-align:top;
        }
        #tblAddress {
            display:none;
        }
    </style>
    
    <script type="text/javascript">    
        function NoEnter() {
            return !(window.event && window.event.keyCode == 13);
        }
               
    </script>  
     <script type="text/javascript" language="javascript" >
         function validate() {
             if (Page_ClientValidate())
                 return confirm('Are you sure want to Submit?');
         }
        </script> 
     
</head>

<body>
 <form id="StaffDiscForm" runat="server">
    <asp:Panel ID="pnForm" runat="server" >
  
        <asp:ValidationSummary ID="valSum" 
        runat="server" 
        ForeColor="Red" 
        Font-Size="10pt" 
        DisplayMode="BulletList" 
        EnableClientScript="true" 
        ShowSummary="false" 
        ShowMessageBox="true"  
        ValidationGroup="Depts" />
    
    <table class="MainBorderPrint" cellpadding=0 cellspacing=0 align="center">
    <tr>
    <td class="ImageCell"><img alt=""  src="images/banner.jpg" border="0" />        
    </td>
    </tr>
	<tr><td align="center"><span style="font-size:14.0pt;color:red">(<u>Ireland &amp; Airport Stores Only)</u></span></td></tr>
     <tr><td align="center">
        <table cellpadding=0 cellspacing=0 border=0>
     <tr><td class="RowSpacer"></td></tr>
     <tr><td class="RowSpacer"></td></tr>
    <tr><td align="center">
    
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" CssClass="GridStyle" 
         RowStyle-BorderColor="#dbdddc" RowStyle-BorderWidth="1px" CellPadding="0" CellSpacing="0" GridLines="both">
            <Columns>
                <asp:TemplateField HeaderText="Code" ItemStyle-Width="100px" >
                    <ItemTemplate >
                    
                <asp:textbox runat="server" ID="txtCode" Text='<%# Eval("Code") %>' CssClass="txtCode" />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="300px" >
                    <ItemTemplate>
                        <asp:textbox runat="server" ID="txtItemDescription" Text='<%# Eval("Item Description") %>' CssClass="txtItemDescription" />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Colour" ItemStyle-Width="100px" >
                    <ItemTemplate>
                        <asp:textbox runat="server" ID="txtColour" Text='<%# Eval("Colour") %>' CssClass="txtColour" />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Size" ItemStyle-Width="50px" >
                    <ItemTemplate>
                        <asp:textbox runat="server" ID="txtSize" Text='<%# Eval("Size") %>' CssClass="txtSize" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Qty" ItemStyle-Width="50px" >
                    <ItemTemplate>
                        <asp:textbox  runat="server" ID="txtQty" Text='<%# Eval("Qty") %>' CssClass="txtQty" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FULL PRICE AMOUNT" ItemStyle-Width="50px" >
                    <ItemTemplate>
                        <asp:textbox runat="server" ID="txtPrice" Text='<%# Eval("Price") %>' CssClass="txtPrice" />
                    </ItemTemplate>
                </asp:TemplateField>               
            </Columns>
        </asp:GridView>

        </td></tr>     
    <tr><td class="RowSpacer"></td></tr>
<tr>
    <td align="right" ><table cellspacing="10" cellpadding="0" border="0" >
                    <tr><td>Gift Card Voucher</td><td>
                        <asp:TextBox ID="txGiftCardVoucher" Width="100px" runat="server"></asp:TextBox></td></tr>
                 </table></td>
</tr>


        <tr><td align="left" > 
            <table id="tblEmpRate" cellspacing="0" cellpadding="5" border="0">               
                <tr><td id="cssDiscNoHdr">Discount Rate:</td>
                    <td id="cssDiscField"><asp:RadioButtonList ID="rblDiscRate" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="70%" Value="70" Selected="True" />
                            <asp:ListItem Text="40%" Value="40" />                
                        </asp:RadioButtonList></td></tr>    
                 <tr id="trEmpNo" ><td id="cssEmpNoHdr">Employee No:</td>                         
                         <td class="cssFormFields">
                         <asp:TextBox ID="txtEmpNo" CssClass="cssEmpNo" runat="server" ></asp:TextBox></td>

                     <td><asp:RequiredFieldValidator ID="valTxtEmpNo"
                                                        ControlToValidate="txtEmpNo"
                                                        Text="*"  ValidationGroup="Depts" 
                                                        ErrorMessage="Enter the Payroll No"
                                                        runat="server" />
</td>
                 </tr>                
                 <!--   <tr ><td id="cssEmpNoHdr">Employee No:</td><td class="cssFormFields"><div id="divEmpNo" ></div></td></tr> -->
                </table>
            </td>

        </tr>           
        <tr>
            <td class="RowSpacer" align="left" >
                <table cellpadding="5" cellspacing="0"  border="0"> <!--  style="border-spacing:0px;" -->
                     
                     <tr>
                        <td>
                            <asp:RequiredFieldValidator ID="valReqDepts" runat="server"  ValidationGroup="Depts" 
                                InitialValue="Select a Department" ControlToValidate="drpDept" 
                                ErrorMessage="Select a department" Text="*"></asp:RequiredFieldValidator>
                        </td>
                          <td  ><!-- style="border:solid 1px black; padding:5px 5px 5px 5px;width:218px; text-align:center;"-->
                            <asp:DropDownList ID="drpDept" ValidationGroup="Depts" runat="server"  >
                            </asp:DropDownList>
                        </td>
                       
                        <td id="tdDeptEmpNames" >&nbsp;
                          <%-- <div id="DivDeptEmpNames" runat="server" visible="true" >
                           </div>--%>
                        </td>
                         <td></td>
                        <td>
                            
                        </td>
                    </tr> 
                </table>
            </td>
        </tr>   
        <tr>
            <td>
               <!-- <asp:Label ID="lblStoreAddress" runat="server" CssClass="" Text="" /> -->
            </td>

        </tr>       
    <tr><td class="RowSpacer">&nbsp;</td></tr>
        <tr><td align="left">
            <table cellpadding="5" cellspacing="0"  border="0">
                
                 <tr id="trDepts">
                    <td valign="top"  align="left">
                        <table id="tblAddress">
                            <tr>
                    <td valign="top" class="SectionTitle"  align="left">
                        Delivery Address</td>  <td valign="top" class="SectionTitle"  align="left">Billing Address</td>
                 </tr>
                            <tr><td class="cssNote">By default this is your store. You may change this to any address.
                                      </td><td>&nbsp;</td></tr>
                            <tr><td>
                                 <div id="divStoreAddress" runat="server" visible="true" >
                           </div>
                                </td> 
                                <td>
                                    <asp:TextBox ID="txtBillAddress" TextMode="MultiLine" Height="100" Width="321" runat="server"></asp:TextBox></td>
                               </tr>
                            <tr>
                                <td valign="bottom" align="right" >
                                    <input style="font-face:tahoma;font-size:8pt;height:20px;width:150px;display:none;" 
                                        type="button" name="inpRefresh" id="inpRefresh" onclick="Department_OnSelChange();" 
                                        value="Refresh Store Address" />
                                     </td><td>&nbsp;</td>
                            </tr>

                        </table>
                        <!--
                            <asp:TextBox CssClass="txtAddress" ID="txtAddress" TextMode="MultiLine" runat="server" Height="100px" 
                            width="321px"></asp:TextBox>
                        -->
                    </td>
                </tr>
            </table>
            </td></tr>
             <tr><td class="RowSpacer"></td></tr> 
        <tr><td align="right">
            <asp:Button ID="btnSubmit" 
                runat="server" 
                Text="Submit"  
                OnClientClick="return validate();" 
                ValidationGroup="Depts" />
            </td></tr>
           </table>    
    </td></tr>      
    <tr><td class="RowSpacer"></td></tr>
    <tr><td><img alt="Staff Orders" src="images/Banner.jpg" border="0" /></td></tr>
    </table>
    
    <asp:ScriptManager 
        ID="SM1" 
        runat="server" 
        EnablePageMethods="true"
        ScriptMode="Release" 
        LoadScriptsBeforeUI="true">
     <Scripts>
            <asp:ScriptReference Path="~/JScript.js" />
        </Scripts>
    </asp:ScriptManager>           
     </asp:Panel>  
         
     <asp:Panel ID="pnSummary" runat="server">
        <table cellpadding=10 cellspacing=10 border=0 align="center">
        <tr>
        <td align="center" class="SummaryHeader">
        Thank You for your Order. <br />Please find below your order summary. 
        </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSummary" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        </table>            
     </asp:Panel>
    
    </form>
</body>
</html>
