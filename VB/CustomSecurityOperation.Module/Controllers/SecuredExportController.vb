Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Security

Namespace CustomSecurityOperation.Module.Controllers
    Public Class SecuredExportController
        Inherits ObjectViewController
        Private exportController As ExportController
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            exportController = Frame.GetController(Of ExportController)()
            If exportController IsNot Nothing Then
                AddHandler exportController.ExportAction.Executing, AddressOf ExportAction_Executing
                If TypeOf SecuritySystem.Instance Is IRequestSecurity Then
                    exportController.Active.SetItemValue("Security", SecuritySystem.IsGranted(New ExportPermissionRequest(View.ObjectTypeInfo.Type)))
                End If
            End If
        End Sub
        Private Sub ExportAction_Executing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
            SecuritySystem.Demand(New ExportPermissionRequest(View.ObjectTypeInfo.Type))
        End Sub
    End Class
End Namespace
