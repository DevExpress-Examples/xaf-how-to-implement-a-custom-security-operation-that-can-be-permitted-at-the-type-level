Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo

Namespace CustomSecurityOperation.Win
	Partial Public Class CustomSecurityOperationWindowsFormsApplication
		Inherits WinApplication

		Public Sub New()
			InitializeComponent()
			DelayedViewItemsInitialization = True
		End Sub

		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection)
		End Sub
		Private Sub CustomSecurityOperationWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
			e.Updater.Update()
			e.Handled = True
		End Sub
		Private Sub CustomSecurityOperationWindowsFormsApplication_CustomizeLanguagesList(ByVal sender As Object, ByVal e As CustomizeLanguagesListEventArgs) Handles Me.CustomizeLanguagesList
			Dim userLanguageName As String = System.Threading.Thread.CurrentThread.CurrentUICulture.Name
			If userLanguageName <> "en-US" AndAlso e.Languages.IndexOf(userLanguageName) = -1 Then
				e.Languages.Add(userLanguageName)
			End If
		End Sub
	End Class
End Namespace
