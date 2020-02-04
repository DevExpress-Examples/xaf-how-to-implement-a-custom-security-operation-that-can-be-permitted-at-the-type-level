Imports System

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.Persistent.Base

Namespace CustomSecurityOperation.Module.DatabaseUpdate
	Public Class Updater
		Inherits ModuleUpdater

		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim admin As PermissionPolicyUser = ObjectSpace.FindObject(Of PermissionPolicyUser)(New BinaryOperator("UserName", "Admin"))
			If admin Is Nothing Then
				admin = ObjectSpace.CreateObject(Of PermissionPolicyUser)()
				admin.UserName = "Admin"
				admin.SetPassword("")
				Dim adminRole As PermissionPolicyRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
				adminRole.Name = "Administrator Role"
				adminRole.IsAdministrative = True
				admin.Roles.Add(adminRole)
			End If
			Dim user As PermissionPolicyUser = ObjectSpace.FindObject(Of PermissionPolicyUser)(New BinaryOperator("UserName", "User"))
			If user Is Nothing Then
				user = ObjectSpace.CreateObject(Of PermissionPolicyUser)()
				user.UserName = "User"
				user.SetPassword("")
				Dim userRole As PermissionPolicyRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
				userRole.Name = "User Role"
				Dim taskTypePermission As CustomTypePermissionObject = ObjectSpace.CreateObject(Of CustomTypePermissionObject)()
				taskTypePermission.TargetType = GetType(Task)
				taskTypePermission.CreateState = SecurityPermissionState.Allow
				taskTypePermission.DeleteState = SecurityPermissionState.Allow
				taskTypePermission.NavigateState = SecurityPermissionState.Allow
				taskTypePermission.ReadState = SecurityPermissionState.Allow
				taskTypePermission.WriteState = SecurityPermissionState.Allow
				taskTypePermission.ExportState = SecurityPermissionState.Allow
				Dim userTypePermission As CustomTypePermissionObject = ObjectSpace.CreateObject(Of CustomTypePermissionObject)()
				userTypePermission.TargetType = GetType(PermissionPolicyUser)
				userTypePermission.NavigateState = SecurityPermissionState.Allow
				userTypePermission.ReadState = SecurityPermissionState.Allow
				userRole.TypePermissions.Add(taskTypePermission)
				userRole.TypePermissions.Add(userTypePermission)
				user.Roles.Add(userRole)
			End If
			ObjectSpace.CommitChanges()
			For i As Integer = 1 To 10
				Dim subject As String = String.Format("Task {0}", i)
				Dim task As Task = ObjectSpace.FindObject(Of Task)(New BinaryOperator("Subject", subject))
				If task Is Nothing Then
					task = ObjectSpace.CreateObject(Of Task)()
					task.Subject = subject
					task.DueDate = Date.Today
					task.Save()
				End If
			Next i
			ObjectSpace.CommitChanges()
		End Sub
	End Class
End Namespace
