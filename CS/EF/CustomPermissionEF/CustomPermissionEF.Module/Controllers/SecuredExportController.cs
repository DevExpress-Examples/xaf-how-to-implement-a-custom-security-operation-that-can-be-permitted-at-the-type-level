using CustomPermission.Module.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;

namespace CustomPermission.Module.Controllers {
    public class SecuredExportController : ObjectViewController {
        private ExportController exportController;
        protected override void OnActivated() {
            base.OnActivated();
            exportController = Frame.GetController<ExportController>();

            if (exportController != null) {
                // exportController.ExportAction.Executing += ExportAction_Executing; // Uncomment this line and comment the next one to show the error when a user performs the forbidden action
                exportController.ExportAction.Active.SetItemValue("Security", Application.GetSecurityStrategy().IsGranted(new ExportPermissionRequest(View.ObjectTypeInfo.Type)));
            }
        }
        void ExportAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (!Application.GetSecurityStrategy().IsGranted(new ExportPermissionRequest(View.ObjectTypeInfo.Type))) {
                throw new UserFriendlyException("Export operation is prohibited.");
            }
        }

    }
}
