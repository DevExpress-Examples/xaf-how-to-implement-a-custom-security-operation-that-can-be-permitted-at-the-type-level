Imports System
Imports System.Collections.Generic

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security.Strategy


Namespace CustomSecurityOperation.Module
	Public NotInheritable Partial Class CustomSecurityOperationModule
		Inherits ModuleBase

		Public Sub New()
			InitializeComponent()
		End Sub
		Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
			Dim updater As ModuleUpdater = New DatabaseUpdate.Updater(objectSpace, versionFromDB)
			Return New ModuleUpdater() { updater }
		End Function
	End Class
End Namespace
