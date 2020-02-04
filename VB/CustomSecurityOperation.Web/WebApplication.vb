Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Xpo

Namespace CustomSecurityOperation.Web
	Partial Public Class CustomSecurityOperationAspNetApplication
		Inherits WebApplication

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
		Private module3 As CustomSecurityOperation.Module.CustomSecurityOperationModule
		Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
		Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
		Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
		Private scriptRecorderAspNetModule1 As DevExpress.ExpressApp.ScriptRecorder.Web.ScriptRecorderAspNetModule
		Private sqlConnection1 As System.Data.SqlClient.SqlConnection

		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection)
		End Sub

		Private Sub CustomSecurityOperationAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
			e.Updater.Update()
			e.Handled = True
		End Sub

		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
			Me.module3 = New CustomSecurityOperation.Module.CustomSecurityOperationModule()
			Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
			Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
			Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
			Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
			Me.scriptRecorderAspNetModule1 = New DevExpress.ExpressApp.ScriptRecorder.Web.ScriptRecorderAspNetModule()
			DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' sqlConnection1
			' 
			Me.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.\SQLEXPRESS;Initial Catalog=C" & "ustomSecurityOperation"
			Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
			' 
			' securityStrategyComplex1
			' 
			Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
			Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
			Me.securityStrategyComplex1.UserType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser)
			' 
			' authenticationStandard1
			' 
			Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
			' 
			' CustomSecurityOperationAspNetApplication
			' 
			Me.ApplicationName = "CustomSecurityOperation"
			Me.Connection = Me.sqlConnection1
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module3)
			Me.Modules.Add(Me.securityModule1)
			Me.Security = Me.securityStrategyComplex1
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.CustomSecurityOperationAspNetApplication_DatabaseVersionMismatch);
			DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub
	End Class
End Namespace
