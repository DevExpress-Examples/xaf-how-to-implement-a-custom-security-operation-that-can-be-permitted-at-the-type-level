Imports System

Namespace CustomSecurityOperation.Win
	Partial Public Class CustomSecurityOperationWindowsFormsApplication
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
			Me.module3 = New CustomSecurityOperation.Module.CustomSecurityOperationModule()
			Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
			Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
			Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
			Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
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
			' CustomSecurityOperationWindowsFormsApplication
			' 
			Me.ApplicationName = "CustomSecurityOperation"
			Me.Connection = Me.sqlConnection1
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module3)
			Me.Modules.Add(Me.securityModule1)
			Me.Security = Me.securityStrategyComplex1
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.CustomSecurityOperationWindowsFormsApplication_CustomizeLanguagesList);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.CustomSecurityOperationWindowsFormsApplication_DatabaseVersionMismatch);
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
		Private module3 As CustomSecurityOperation.Module.CustomSecurityOperationModule
		Private sqlConnection1 As System.Data.SqlClient.SqlConnection
		Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
		Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
		Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
	End Class
End Namespace
