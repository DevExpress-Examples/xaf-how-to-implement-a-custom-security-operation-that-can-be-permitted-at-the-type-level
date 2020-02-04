Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy

Namespace CustomSecurityOperation.Module.Controllers
    Public Class RemoveBaseTypePermissionNewActionItemController
        Inherits ObjectViewController(Of ObjectView, PermissionPolicyTypePermissionObject)
        Protected Overrides Sub OnFrameAssigned()
            Dim _newObjectViewController As NewObjectViewController = Frame.GetController(Of NewObjectViewController)()
            If _newObjectViewController IsNot Nothing Then
                AddHandler _newObjectViewController.CollectDescendantTypes, Sub(s, e)
                                                                                e.Types.Remove(GetType(PermissionPolicyTypePermissionObject))
                                                                            End Sub

                AddHandler _newObjectViewController.ObjectCreating, Sub(s, e)
                                                                        If e.ObjectType = GetType(PermissionPolicyTypePermissionObject) Then
                                                                            e.NewObject = e.ObjectSpace.CreateObject(GetType(CustomTypePermissionObject))
                                                                        End If
                                                                    End Sub
            End If
            MyBase.OnFrameAssigned()
        End Sub
    End Class
End Namespace
