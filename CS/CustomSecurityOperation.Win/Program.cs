using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using CustomSecurityOperation.Module;
using System.Collections.Generic;

namespace CustomSecurityOperation.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
#if EASYTEST
			DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            CustomSecurityOperationWindowsFormsApplication winApplication = new CustomSecurityOperationWindowsFormsApplication();
            InMemoryDataStoreProvider.Register();
            winApplication.ConnectionString = InMemoryDataStoreProvider.ConnectionString;
            ((SecurityStrategy)winApplication.Security).CustomizeRequestProcessors += CustomizeRequestProcessors;
            try {
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e) {
                winApplication.HandleException(e);
            }
        }
        private static void CustomizeRequestProcessors(object sender, CustomizeRequestProcessorsEventArgs e) {
            List<IOperationPermission> result = new List<IOperationPermission>();
            SecurityStrategyComplex security = sender as SecurityStrategyComplex;
            if (security != null) {
                PermissionPolicyUser user = security.User as PermissionPolicyUser;
                if (user != null) {
                    foreach (PermissionPolicyRole role in user.Roles) {
                        foreach (PermissionPolicyTypePermissionObject persistentPermission in role.TypePermissions) {
                            CustomTypePermissionObject customPermission = persistentPermission as CustomTypePermissionObject;
                            if (customPermission != null && customPermission.ExportState != null) {
                                SecurityPermissionState state = (SecurityPermissionState)customPermission.ExportState;
                                result.Add(new ExportPermission(customPermission.TargetType, state));
                            }
                        }
                    }
                }
            }
            IPermissionDictionary permissionDictionary = new PermissionDictionary(result);
            e.Processors.Add(typeof(ExportPermissionRequest), new ExportPermissionRequestProcessor(permissionDictionary));
        }
    }
}
