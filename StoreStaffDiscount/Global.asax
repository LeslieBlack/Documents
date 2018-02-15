<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
        If HttpContext.Current.Response.Status.Substring(0, 3) = "401" Then
            HttpContext.Current.Response.ClearContent()
            HttpContext.Current.Response.Redirect("accessdenied.htm")
        End If
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
    
    Protected Sub Application_EndRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        If HttpContext.Current.Response.Status.Substring(0, 3) = "401" Then
            HttpContext.Current.Response.ClearContent()
            'Server.Transfer("UnAuthorisedAccess.html", False)
            '' HttpContext.Current.Response.Redirect("UnAuthorisedAccess.html", False)
        End If
    End Sub
       
</script>