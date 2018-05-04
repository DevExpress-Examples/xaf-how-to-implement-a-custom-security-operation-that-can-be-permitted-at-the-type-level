using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security;

namespace CustomSecurityOperation.Module.Controllers {
    public class SecuredExportController : ObjectViewController {
        private ExportController exportController;
        protected override void OnActivated() {
            base.OnActivated();
            exportController = Frame.GetController<ExportController>();
            exportController.ExportAction.Executing += ExportAction_Executing;
            if (SecuritySystem.Instance is IRequestSecurity) {
                exportController.Active.SetItemValue("Security",
                    SecuritySystem.IsGranted(new ExportPermissionRequest(View.ObjectTypeInfo.Type)));
            }
        }
        void ExportAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
            SecuritySystem.Demand(new ExportPermissionRequest(View.ObjectTypeInfo.Type));
        }
    }
}
