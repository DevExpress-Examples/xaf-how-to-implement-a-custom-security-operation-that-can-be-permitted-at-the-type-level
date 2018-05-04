using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using DevExpress.ExpressApp.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using CustomSecurityOperation.Module;

namespace CustomSecurityOperation.Web {
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
        }
        protected void Application_Start(Object sender, EventArgs e) {
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);

#if EASYTEST
			DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif

        }
        protected void Session_Start(Object sender, EventArgs e) {
            WebApplication.SetInstance(Session, new CustomSecurityOperationAspNetApplication());
            InMemoryDataStoreProvider.Register();
            WebApplication.Instance.ConnectionString = InMemoryDataStoreProvider.ConnectionString;
            ((SecurityStrategy)WebApplication.Instance.Security).CustomizeRequestProcessors += CustomizeRequestProcessors;
            WebApplication.Instance.SwitchToNewStyle();
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
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
        protected void Application_BeginRequest(Object sender, EventArgs e) {
            string filePath = HttpContext.Current.Request.PhysicalPath;
            if (!string.IsNullOrEmpty(filePath)
                && (filePath.IndexOf("Images") >= 0) && !System.IO.File.Exists(filePath)) {
                HttpContext.Current.Response.End();
            }
        }
        protected void Application_EndRequest(Object sender, EventArgs e) {
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
        }
        protected void Application_Error(Object sender, EventArgs e) {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(Object sender, EventArgs e) {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e) {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
