Imports System
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Web

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Web
Imports DevExpress.Web
Imports DevExpress.ExpressApp.Xpo
Imports System.Collections.Generic
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports CustomSecurityOperation.Module

Namespace CustomSecurityOperation.Web
	Public Class [Global]
		Inherits System.Web.HttpApplication

		Public Sub New()
			InitializeComponent()
		End Sub
		Protected Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
			AddHandler ASPxWebControl.CallbackError, AddressOf Application_Error

#If EASYTEST Then
			DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = True
#End If

		End Sub
		Protected Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
			WebApplication.SetInstance(Session, New CustomSecurityOperationAspNetApplication())
			InMemoryDataStoreProvider.Register()
			WebApplication.Instance.ConnectionString = InMemoryDataStoreProvider.ConnectionString
			AddHandler DirectCast(WebApplication.Instance.Security, SecurityStrategy).CustomizeRequestProcessors, AddressOf CustomizeRequestProcessors
			WebApplication.Instance.SwitchToNewStyle()
			WebApplication.Instance.Setup()
			WebApplication.Instance.Start()
		End Sub
		Private Shared Sub CustomizeRequestProcessors(ByVal sender As Object, ByVal e As CustomizeRequestProcessorsEventArgs)
			Dim result As New List(Of IOperationPermission)()
			Dim security As SecurityStrategyComplex = TryCast(sender, SecurityStrategyComplex)
			If security IsNot Nothing Then
'INSTANT VB NOTE: The variable user was renamed since Visual Basic does not handle local variables named the same as class members well:
				Dim user_Renamed As PermissionPolicyUser = TryCast(security.User, PermissionPolicyUser)
				If user_Renamed IsNot Nothing Then
					For Each role As PermissionPolicyRole In user_Renamed.Roles
						For Each persistentPermission As PermissionPolicyTypePermissionObject In role.TypePermissions
							Dim customPermission As CustomTypePermissionObject = TryCast(persistentPermission, CustomTypePermissionObject)
							If customPermission IsNot Nothing AndAlso customPermission.ExportState IsNot Nothing Then
								Dim state As SecurityPermissionState = CType(customPermission.ExportState, SecurityPermissionState)
								result.Add(New ExportPermission(customPermission.TargetType, state))
							End If
						Next persistentPermission
					Next role
				End If
			End If
			Dim permissionDictionary As IPermissionDictionary = New PermissionDictionary(result)
			e.Processors.Add(GetType(ExportPermissionRequest), New ExportPermissionRequestProcessor(permissionDictionary))
		End Sub
		Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
			Dim filePath As String = HttpContext.Current.Request.PhysicalPath
			If Not String.IsNullOrEmpty(filePath) AndAlso (filePath.IndexOf("Images") >= 0) AndAlso Not System.IO.File.Exists(filePath) Then
				HttpContext.Current.Response.End()
			End If
		End Sub
		Protected Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
		End Sub
		Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
		End Sub
		Protected Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
			ErrorHandling.Instance.ProcessApplicationError()
		End Sub
		Protected Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
			WebApplication.LogOff(Session)
			WebApplication.DisposeInstance(Session)
		End Sub
		Protected Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
		End Sub
		#Region "Web Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
		End Sub
		#End Region
	End Class
End Namespace
