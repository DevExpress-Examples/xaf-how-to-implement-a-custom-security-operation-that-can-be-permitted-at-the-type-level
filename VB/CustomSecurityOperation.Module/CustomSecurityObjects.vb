Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy

Namespace CustomSecurityOperation.Module
	Public Class ExportPermission
		Implements IOperationPermission
		Public Sub New(ByVal objectType As Type, ByVal state As SecurityPermissionState)
			Me.ObjectType = objectType
			Me.State = state
		End Sub
		Private privateObjectType As Type
		Public Property ObjectType() As Type
			Get
				Return privateObjectType
			End Get
			Private Set(ByVal value As Type)
				privateObjectType = value
			End Set
		End Property
		Public ReadOnly Property Operation() As String Implements IOperationPermission.Operation
			Get
				Return "Export"
			End Get
		End Property
		Public Property State() As SecurityPermissionState
	End Class
	Public Class ExportPermissionRequest
		Implements IPermissionRequest
		Public Sub New(ByVal objectType As Type)
			Me.ObjectType = objectType
		End Sub
		Private privateObjectType As Type
		Public Property ObjectType() As Type
			Get
				Return privateObjectType
			End Get
			Private Set(ByVal value As Type)
				privateObjectType = value
			End Set
		End Property
		Public Function GetHashObject() As Object Implements IPermissionRequest.GetHashObject
			Return ObjectType.FullName
		End Function
	End Class

	Public Class ExportPermissionRequestProcessor
		Inherits PermissionRequestProcessorBase(Of ExportPermissionRequest)
		Private permissionDictionary As IPermissionDictionary
		Public Sub New(ByVal permissionDictionary As IPermissionDictionary)
			Me.permissionDictionary = permissionDictionary
		End Sub
		Public Overrides Function IsGranted(ByVal permissionRequest As ExportPermissionRequest) As Boolean
			Dim exportPermissions As IEnumerable(Of ExportPermission) = permissionDictionary.GetPermissions(
		Of ExportPermission)().Where(Function(p) p.ObjectType = permissionRequest.ObjectType)
			If exportPermissions.Count() = 0 Then
				Return IsGrantedByPolicy(permissionDictionary)
			Else
				Return exportPermissions.Any(Function(p) p.State = SecurityPermissionState.Allow)
			End If
		End Function
		Private Function IsGrantedByPolicy(ByVal permissionDictionary As IPermissionDictionary) As Boolean
			If GetPermissionPolicy(permissionDictionary) = SecurityPermissionPolicy.AllowAllByDefault Then
				Return True
			End If
			Return False
		End Function
		Private Function GetPermissionPolicy(ByVal permissionDictionary As IPermissionDictionary) As SecurityPermissionPolicy
			Dim result As SecurityPermissionPolicy = SecurityPermissionPolicy.DenyAllByDefault
			Dim permissionPolicies As List(Of SecurityPermissionPolicy) = permissionDictionary.GetPermissions(
		Of PermissionPolicy)().Select(Function(p) p.SecurityPermissionPolicy).ToList()
			If permissionPolicies IsNot Nothing AndAlso permissionPolicies.Count <> 0 Then
				If permissionPolicies.Any(Function(p) p = SecurityPermissionPolicy.AllowAllByDefault) Then
					result = SecurityPermissionPolicy.AllowAllByDefault
				Else
					If permissionPolicies.Any(Function(p) p = SecurityPermissionPolicy.ReadOnlyAllByDefault) Then
						result = SecurityPermissionPolicy.ReadOnlyAllByDefault
					End If
				End If
			End If
			Return result
		End Function
	End Class
	<XafDisplayName("Type Operation Permissions")>
	Public Class CustomTypePermissionObject
		Inherits PermissionPolicyTypePermissionObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<XafDisplayName("Export")>
		Public Property ExportState() As SecurityPermissionState?
			Get
				Return GetPropertyValue(Of SecurityPermissionState?)(NameOf(ExportState))
			End Get
			Set(ByVal value? As SecurityPermissionState)
				SetPropertyValue(NameOf(ExportState), value)
			End Set
		End Property
	End Class
End Namespace
