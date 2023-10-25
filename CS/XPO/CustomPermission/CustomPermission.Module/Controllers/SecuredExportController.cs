using CustomPermission.Module.Security;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CustomPermission.Module.Controllers {
    public class SecuredExportController : ObjectViewController {
        private ExportController exportController;
        protected override void OnActivated() {
            base.OnActivated();
            exportController = Frame.GetController<ExportController>();
            if (exportController != null) {
                exportController.ExportAction.Executing += ExportAction_Executing;
                if (SecuritySystem.Instance is IRequestSecurity) {
                    exportController.Active.SetItemValue("Security",
                        Application.GetSecurityStrategy().IsGranted(new ExportPermissionRequest(View.ObjectTypeInfo.Type)));
                }
            }
        }
        void ExportAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (!Application.GetSecurityStrategy().IsGranted(new ExportPermissionRequest(View.ObjectTypeInfo.Type))) {
                throw new UserFriendlyException("Export operation is prohibited.");
            }
        }
    }
}
