Imports System
Imports System.Configuration
Imports System.Windows.Forms

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports CustomSecurityOperation.Module
Imports System.Collections.Generic

Namespace CustomSecurityOperation.Win
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Sub Main()
#If EASYTEST Then
			DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register()
#End If
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
			Dim winApplication As New CustomSecurityOperationWindowsFormsApplication()
			InMemoryDataStoreProvider.Register()
			winApplication.ConnectionString = InMemoryDataStoreProvider.ConnectionString
			AddHandler DirectCast(winApplication.Security, SecurityStrategy).CustomizeRequestProcessors, AddressOf CustomizeRequestProcessors
			Try
				winApplication.Setup()
				winApplication.Start()
			Catch e As Exception
				winApplication.HandleException(e)
			End Try
		End Sub
		Private Sub CustomizeRequestProcessors(ByVal sender As Object, ByVal e As CustomizeRequestProcessorsEventArgs)
			Dim result As New List(Of IOperationPermission)()
			Dim security As SecurityStrategyComplex = TryCast(sender, SecurityStrategyComplex)
			If security IsNot Nothing Then
				Dim user As PermissionPolicyUser = TryCast(security.User, PermissionPolicyUser)
				If user IsNot Nothing Then
					For Each role As PermissionPolicyRole In user.Roles
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
	End Module
End Namespace
