Imports SQLAccess = Application.Data.SqlAccess
Imports EmailEx = Application.Exception.ExceptionEmails
Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports gvh = Application.Controls.GridView.GridViewHelper
Imports ep = Application.Email
Imports em = Application.Email.EmailHelper
Imports uCtrls = Application.Controls.UserControls
Imports System.IO
Imports System.Web.Services

Partial Class _Default
    Inherits System.Web.UI.Page
    Private m_DeptNo As Integer = 0
    Private m_PayrollID As Integer = -1
    Private m_Name As String = ""
    Private m_DeptName As String = ""
    Private m_Address As String = ""
    Private m_BillAddress As String = ""

    Public Property BillAddress() As String
        Get
            Return m_BillAddress
        End Get
        Set(value As String)
            m_BillAddress = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return m_Address
        End Get
        Set(value As String)
            m_Address = value
        End Set
    End Property

    Public Property DeptName() As String
        Get
            Return m_DeptName
        End Get
        Set(ByVal value As String)
            m_DeptName = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    Public Property PayrollID() As Integer
        Get
            Return m_PayrollID
        End Get
        Set(ByVal value As Integer)
            m_PayrollID = value
        End Set
    End Property

    Public Property DeptNo() As Integer
        Get
            Return m_DeptNo
        End Get
        Set(ByVal value As Integer)
            m_DeptNo = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                pnForm.Visible = True
                pnSummary.Visible = False
                DataBindGridView()
                drp_Depts()
            Else
                If Not Request.Form("txtStoreAddress") Is Nothing Then
                    Address = Request.Form("txtStoreAddress").ToString().Replace(Chr(10), "<BR>")
                Else
                    Address = ""
                End If

                If Not txtBillAddress Is Nothing Then
                    BillAddress = txtBillAddress.Text.ToString().Replace(Chr(10), "<BR>")
                Else
                    BillAddress = ""
                End If


                If Not txtEmpNo Is Nothing Then
                    If IsNumeric(txtEmpNo.Text) Then
                        PayrollID = CType(txtEmpNo.Text, Integer)
                        GetName()
                    End If
                End If


            End If
        Catch ex As Exception
            EmailEx.PublishException(ex)
        End Try
    End Sub

    Private Sub DataBindGridView()
        Dim dt As DataTable = Nothing
        Dim StaffDiscTable As BuildCustomDataTable = Nothing

        Try

            StaffDiscTable = New BuildCustomDataTable(New String() {"Code", "Item Description", "Colour", "Size", "Qty", "Price"}, 11)
            dt = StaffDiscTable.CustomDataTable

            If Not IsNothing(dt) Then
                gv.DataSource = dt
                gv.DataBind()
            End If

            'txtNum.Attributes.Add("OnKeyPress", "return NoEnter()")

        Catch ex As Exception
            Throw ex
        Finally
            StaffDiscTable = Nothing
        End Try
    End Sub

    Private Function GetGiftCardVoucher(ByVal Value As String) As String
        Dim ReturnValue As String = ""
        Try
            If Value.Length > 0 Then
                ReturnValue = "Gift Voucher Number " & Value.ToString()
            End If

        Catch ex As Exception
            Throw ex
            ReturnValue = ""
        Finally
            GetGiftCardVoucher = ReturnValue
        End Try
    End Function

    Private Sub SubmitStaffDisc()
        Dim gvr As GridViewRow = Nothing
        Dim HTMLOrderTable As String = ""
        Try
            GetFormNames(Request)

            HTMLOrderTable = HTMLOrderTable & _
            "<html>" & _
            "<head>" & _
            "<style>" & _
            ".SectionTitle{font-weight:bold;font-size:10pt;font-family:Tahoma;}" & _
            ".MainBorderPrint{BORDER:#dbdddc 4px solid;width:640px;}" & _
            ".GridStyle{BORDER: #dbdddc 1px solid;font-size:10pt;font-family:Tahoma;text-transform:capitalize;}" & _
            ".BodyText{font-size:10pt;font-family:Tahoma;text-align:left;}" & _
            ".Address{font-size:10pt;font-family:Tahoma;text-align:left;text-transform:capitalize;}" & _
            ".GridHeader{BORDER: #dbdddc 1px solid;font-weight:bold;font-size:10pt;font-family:Tahoma;text-align:center;}" & _
            ".tblHeader{BORDER: #dbdddc 1px solid;font-weight:bold;font-size:10pt;font-family:Tahoma;text-align:left;}" & _
            ".RowSpacer{height:10px;}" & _
            ".SummaryHeader{font-weight:bold;font-size:10pt;font-family:Tahoma;text-align:center;}" & _
            "</style>" & _
            "<head>" & _
            "<body>"

            HTMLOrderTable = HTMLOrderTable & "<table cellpadding='0' align='center' cellspacing='3' border='0' class='MainBorderPrint' >"

            HTMLOrderTable = HTMLOrderTable & "<tr>"

            HTMLOrderTable = HTMLOrderTable & "<td><img  src='cid:HeaderID' alt='Staff Discount' border='0' />"

            HTMLOrderTable = HTMLOrderTable & "<td>"

            HTMLOrderTable = HTMLOrderTable & "</tr>"

	    HTMLOrderTable = HTMLOrderTable & "<tr><td align='center'><span style='font-size:14.0pt;color:red'>(<u>Ireland &amp; Airport Stores Only)</u></span></td></tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer'></td></tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr><td align='center' colspan='2'>"

            HTMLOrderTable = HTMLOrderTable & "<table cellpadding='3' cellspacing='0' border='0' width='650px'>"

            HTMLOrderTable = HTMLOrderTable & "<tr>"

            HTMLOrderTable = HTMLOrderTable & "<td align='center' colspan='2'>"

            HTMLOrderTable = HTMLOrderTable & "<table cellpadding='3' cellspacing='0' border='0' width='650px'>"

            HTMLOrderTable = HTMLOrderTable & "<tr>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridHeader' width='100px'>Code</td>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridHeader' width='300px'>Item Description</td>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridHeader' width='100px'>Colour</td>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridHeader' width='50px'>Size</td>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridHeader' width='50px'>Qty</td>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridHeader' width='50px'>Price</td>"

            HTMLOrderTable = HTMLOrderTable & "</tr>"

            For Each gvr In gv.Rows

                If gvr.RowType = DataControlRowType.DataRow Then

                    HTMLOrderTable = HTMLOrderTable & "<tr>"

                    HTMLOrderTable = HTMLOrderTable & "<td class='GridStyle'>" & IsValueNothing(gvh.GetTextboxValue(gvr, "txtCode")) & "</td>"

                    HTMLOrderTable = HTMLOrderTable & "<td class='GridStyle'>" & IsValueNothing(gvh.GetTextboxValue(gvr, "txtItemDescription")) & "</td>"

                    HTMLOrderTable = HTMLOrderTable & "<td class='GridStyle'>" & IsValueNothing(gvh.GetTextboxValue(gvr, "txtColour")) & "</td>"

                    HTMLOrderTable = HTMLOrderTable & "<td class='GridStyle' align='center'>" & IsValueNothing(gvh.GetTextboxValue(gvr, "txtSize")) & "</td>"

                    HTMLOrderTable = HTMLOrderTable & "<td class='GridStyle' align='center'>" & IsValueNothing(gvh.GetTextboxValue(gvr, "txtQty")) & "</td>"

                    HTMLOrderTable = HTMLOrderTable & "<td class='GridStyle'>" & IsValueNothing(gvh.GetTextboxValue(gvr, "txtPrice")) & "</td>"

                    HTMLOrderTable = HTMLOrderTable & "</tr>"

                End If

            Next

            HTMLOrderTable = HTMLOrderTable & "</table>"

            HTMLOrderTable = HTMLOrderTable & "</td>"

            HTMLOrderTable = HTMLOrderTable & "</tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer' valign='top' align='left'>" & GetGiftCardVoucher(txGiftCardVoucher.Text) & "</td></tr>"
            HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer'></td></tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr>"
            HTMLOrderTable = HTMLOrderTable & "<td colspan='2'>"

            HTMLOrderTable = HTMLOrderTable & "<table border='0' cellpadding='0' cellspacing='0' width='150' ><tr>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridHeader'>Discount Rate"

            HTMLOrderTable = HTMLOrderTable & "</td>"

            HTMLOrderTable = HTMLOrderTable & "<td class='GridStyle'>&nbsp;&nbsp;"

            HTMLOrderTable = HTMLOrderTable & rblDiscRate.SelectedValue.ToString() & "%"

            HTMLOrderTable = HTMLOrderTable & "</td>"

            HTMLOrderTable = HTMLOrderTable & "</tr></table>"


            HTMLOrderTable = HTMLOrderTable & "</td>"
            HTMLOrderTable = HTMLOrderTable & "</tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr>"

            HTMLOrderTable = HTMLOrderTable & "<td align='left' colspan='2'>"

            HTMLOrderTable = HTMLOrderTable & "<table cellpadding='5' cellspacing='0' border='0' ><tr><td align='left'  class='tblHeader' >Payroll No</td><td align='left'  class='tblHeader' >Department</td></tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr><td align='left' class='GridStyle'>" & txtEmpNo.Text & "</td><td align='left' class='GridStyle'>" & uCtrls.GetDropDownListText(drpDept) & "</td></tr></table>"

            HTMLOrderTable = HTMLOrderTable & "</td>"

            HTMLOrderTable = HTMLOrderTable & "</tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr>"

            HTMLOrderTable = HTMLOrderTable & "<td valign='top' class='SectionTitle'  align='left'>Delivery Address"

            HTMLOrderTable = HTMLOrderTable & "</td>"

            HTMLOrderTable = HTMLOrderTable & "<td valign='top' class='SectionTitle'  align='left'>Billing Address"

            HTMLOrderTable = HTMLOrderTable & "</tr>"

            'HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer'></td></tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr>"

            HTMLOrderTable = HTMLOrderTable & "<td  class='Address'>" & Name.ToLower() & "<br/>" & IsValueNothing(Address)

            HTMLOrderTable = HTMLOrderTable & "<td  class='Address'>" & Name.ToLower() & "<br/>" & IsValueNothing(BillAddress)

            HTMLOrderTable = HTMLOrderTable & "</td>"

            HTMLOrderTable = HTMLOrderTable & "</tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer'></td></tr>"
            HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer'></td></tr>"

            HTMLOrderTable = HTMLOrderTable & "</table>"

            HTMLOrderTable = HTMLOrderTable & "</td></tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer'></td></tr>"
            HTMLOrderTable = HTMLOrderTable & "<tr><td class='RowSpacer'></td></tr>"

            HTMLOrderTable = HTMLOrderTable & "<tr>"

            HTMLOrderTable = HTMLOrderTable & "<td><img  src='cid:FooterID' border='0' />"

            HTMLOrderTable = HTMLOrderTable & "<td>"

            HTMLOrderTable = HTMLOrderTable & "</tr>"

            HTMLOrderTable = HTMLOrderTable & "</table>"

            HTMLOrderTable = HTMLOrderTable & _
            "</body>" & _
            "</html>"


            SendStaffOrder(HTMLOrderTable)

            pnForm.Visible = False
            pnSummary.Visible = True
            lblSummary.Text = HTMLOrderTable.Replace("cid:HeaderID", "images/banner.jpg").Replace("cid:FooterID", "images/Banner.jpg")

        Catch ex As Exception
            EmailEx.PublishException(ex)
        End Try

    End Sub

    Private Sub GetFormNames(ByRef Request As HttpRequest)
        Dim o As Object = Nothing
        Dim dt As DataTable = Nothing

        Try
            If Not Request.Form Is Nothing Then

                If Not Request.Form("drpDept") Is Nothing Then
                    If IsNumeric(Request.Form("drpDept")) Then
                        DeptNo = CType(Request.Form("drpDept"), Integer)
                    Else
                        DeptNo = 0
                    End If
                End If

                If Not Request.Form("drpNames") Is Nothing Then
                    PayrollID = CType(Request.Form("drpNames"), Integer)
                End If

            End If

            dt = SQLAccess.GetRecords(WebConfigurationManager.ConnectionStrings("SqlConnectionString").ToString(), _
            WebConfigurationManager.AppSettings("GetNamesList").ToString(), _
            New SqlParameter() {New SqlParameter("@PayrollID", PayrollID)})

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("DeptName")) Then
                        DeptName = CType(dt.Rows(0)("DeptName"), String)
                    End If

                    If Not IsDBNull(dt.Rows(0)("Name")) Then
                        Name = CType(dt.Rows(0)("Name"), String)
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SendStaffOrder(ByVal EmailBody As String)
        Dim EmailParams As ep.EmailParameters = Nothing
        Dim ImageFolder As String = ""
        Dim ImageNV As NameValueCollection = Nothing
        Try
            EmailParams = New ep.EmailParameters

            EmailParams.EmailFrom = WebConfigurationManager.AppSettings("StaffPurchaseEmailFrom").ToString()
            EmailParams.EmailTo = WebConfigurationManager.AppSettings("StaffPurchaseEmailTo").ToString()
            EmailParams.EmailCc = GetSendersEmailAddress()
            EmailParams.EmailSubject = WebConfigurationManager.AppSettings("StaffPurchaseEmailSubject").ToString()
            EmailParams.EmailBody = EmailBody
            EmailParams.EmailServer = WebConfigurationManager.AppSettings("ExchServer").ToString()

            ImageFolder = Server.MapPath(".").ToString()
            ImageNV = New NameValueCollection

            ImageNV.Add("HeaderID", ImageFolder & "\images\Banner.jpg")
            ImageNV.Add("FooterID", ImageFolder & "\images\Banner.jpg")

            em.HTMLEmailImgEmbed(EmailParams, ImageNV)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetSendersEmailAddress() As String
        Dim Username As String = ""
        Try
            Username = HttpContext.Current.User.Identity.Name.ToString.ToLower.Replace("ffdomain\", "") & _
                        WebConfigurationManager.AppSettings("StaffPurchaseEmailExtn").ToString()
        Catch ex As Exception
            Throw ex
            Username = ""
        Finally
            GetSendersEmailAddress = Username
        End Try
    End Function

    Private Function IsValueNothing(ByVal value As String) As String
        Try
            If value = "" Then
                value = "&nbsp;"
            End If
        Catch ex As Exception
            Throw ex
        Finally
            IsValueNothing = value
        End Try
    End Function

    Protected Sub gv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv.RowDataBound
        Try

            ConfigOnKeyPress(gvh.GetTextBox(e.Row, "txtCode"))
            ConfigOnKeyPress(gvh.GetTextBox(e.Row, "txtItemDescription"))
            ConfigOnKeyPress(gvh.GetTextBox(e.Row, "txtColour"))
            ConfigOnKeyPress(gvh.GetTextBox(e.Row, "txtSize"))
            ConfigOnKeyPress(gvh.GetTextBox(e.Row, "txtQty"))
            ConfigOnKeyPress(gvh.GetTextBox(e.Row, "txtPrice"))

        Catch ex As Exception
            EmailEx.PublishException(ex)
        End Try
    End Sub

    Private Sub ConfigOnKeyPress(ByRef txt As Object)
        Try

            If Not txt Is Nothing Then
                If TypeOf txt Is TextBox Then

                    CType(txt, TextBox).Attributes.Add("onKeyPress", "return NoEnter()")

                End If
            Else

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub drp_Depts()

        Try

            uCtrls.DropDownList_SQLDataBind(drpDept, _
                                            WebConfigurationManager.ConnectionStrings("SqlConnectionString").ToString(), _
                                            WebConfigurationManager.AppSettings("GetDeptList").ToString(), _
                                            "Store Name", _
                                            "DeptNo", _
                                            "Select a Department", _
                                            DeptNo)

            drpDept.Attributes.Add("onChange", "Department_OnSelChange()")

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    <WebMethod()> Public Shared Function txt_StoreAddress(ByVal DeptNo As String) As String
        Dim Address As Object = Nothing
        Dim txt As TextBox = Nothing
        Dim ReturnValue As String = ""
        Dim sw As StringWriter = Nothing
        Dim hw As HtmlTextWriter = Nothing

        Try

            If IsNumeric(DeptNo) Then

                Address = SQLAccess.GetScalarValue(WebConfigurationManager.ConnectionStrings("SqlConnectionString").ToString(), _
                                               WebConfigurationManager.AppSettings("StoreAddress").ToString(), _
                                               New SqlParameter() {New SqlParameter("@DeptNo", CType(DeptNo, Integer))})

                If Not IsDBNull(Address) Then

                    ReturnValue = CType(Address, String)

                    txt = New TextBox

                    txt.ID = "txtStoreAddress"
                    txt.CssClass = "cssStoreAddress"
                    txt.TextMode = TextBoxMode.MultiLine
                    txt.Height = "100"
                    txt.Width = "321"
                    txt.ReadOnly = True

                    txt.Text = Address

                    sw = New StringWriter
                    hw = New HtmlTextWriter(sw)

                    txt.RenderControl(hw)

                    ReturnValue = sw.ToString()

                Else

                    ReturnValue = ""

                End If

            End If

        Catch ex As Exception
            Throw ex
        Finally
            txt_StoreAddress = ReturnValue
        End Try
    End Function

    <WebMethod()> Public Shared Function drp_Names(ByVal DeptNo As String, ByVal PayrollNo As String) As String
        Dim drpNames As DropDownList
        Dim sw As StringWriter = Nothing
        Dim hw As HtmlTextWriter = Nothing
        Dim ReturnValue As String = ""

        Try

            If IsNumeric(DeptNo) Then
                drpNames = New DropDownList
                uCtrls.DropDownList_SQLDataBind(drpNames, _
                                               WebConfigurationManager.ConnectionStrings("SqlConnectionString").ToString(), _
                                               WebConfigurationManager.AppSettings("GetNamesList").ToString(), _
                                               "Name", _
                                               "Payroll_ID", _
                                               "Select a Name", _
                                               PayrollNo, _
                                               New SqlParameter() {New SqlParameter("@DeptNo", CType(DeptNo, Integer))})

                'drpNames.Attributes.Add("onChange", "EmpNames_OnSelChange()")

                drpNames.ID = "drpNames"
                drpNames.CssClass = "drp"
                sw = New StringWriter
                hw = New HtmlTextWriter(sw)
                drpNames.RenderControl(hw)
                ReturnValue = sw.ToString()

            Else
                ReturnValue = ""
            End If

        Catch ex As Exception
            Throw ex
        Finally
            drp_Names = ReturnValue
        End Try

    End Function

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            SubmitStaffDisc()
        Catch ex As Exception
            EmailEx.PublishException(ex)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Private Sub GetName()
        Dim dt As DataTable = Nothing

        Try
            dt = SQLAccess.GetRecords(WebConfigurationManager.ConnectionStrings("SqlConnectionString").ToString(), _
                                            WebConfigurationManager.AppSettings("GetNamesList").ToString(), _
                                            New SqlParameter() {New SqlParameter("@DeptNo", CType(PayrollID, Integer))})
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Name = CType(SQLAccess.CheckDbNull(dt.Rows(0)("Name")), String)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class

Public Class BuildCustomDataTable
    Private m_CustomDataTable As DataTable

    Public Property CustomDataTable() As DataTable
        Get
            Return m_CustomDataTable
        End Get
        Set(ByVal value As DataTable)
            m_CustomDataTable = value
        End Set
    End Property

    Public Sub New(ByVal ColNames() As String, ByVal NoOfRows As Integer)
        Try
            CustomDataTable = New DataTable
            CreateIdColumn("ID", 0, 1)

            For Each c As String In ColNames
                CustomDataTable.Columns.Add(New DataColumn(c, GetType(String)))
            Next

            CreateRows(NoOfRows)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateIdColumn(ByVal Name As String, ByVal Start As Integer, ByVal Increments As Integer)
        Try
            CustomDataTable.Columns.Add(New DataColumn(Name, GetType(Integer)))
            CustomDataTable.Columns(Name).AutoIncrement = True
            CustomDataTable.Columns(Name).AutoIncrementSeed = Start
            CustomDataTable.Columns(Name).AutoIncrementStep = Increments
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateRows(ByVal NoOfRows As Integer)
        Dim i As Integer = 0
        Try

            For i = 0 To NoOfRows - 1
                CustomDataTable.Rows.Add(CustomDataTable.NewRow())
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class

