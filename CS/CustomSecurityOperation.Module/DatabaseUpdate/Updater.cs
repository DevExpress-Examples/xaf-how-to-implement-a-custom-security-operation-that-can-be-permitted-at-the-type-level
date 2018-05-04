using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Base;

namespace CustomSecurityOperation.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            PermissionPolicyUser admin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Admin"));
            if (admin == null) {
                admin = ObjectSpace.CreateObject<PermissionPolicyUser>();
                admin.UserName = "Admin";
                admin.SetPassword("");
                PermissionPolicyRole adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrator Role";
                adminRole.IsAdministrative = true;
                admin.Roles.Add(adminRole);
            }
            PermissionPolicyUser user = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "User"));
            if (user == null) {
                user = ObjectSpace.CreateObject<PermissionPolicyUser>();
                user.UserName = "User";
                user.SetPassword("");
                PermissionPolicyRole userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                userRole.Name = "User Role";
                CustomTypePermissionObject taskTypePermission = ObjectSpace.CreateObject<CustomTypePermissionObject>();
                taskTypePermission.TargetType = typeof(Task);
                taskTypePermission.CreateState = SecurityPermissionState.Allow;
                taskTypePermission.DeleteState = SecurityPermissionState.Allow;
                taskTypePermission.NavigateState = SecurityPermissionState.Allow;
                taskTypePermission.ReadState = SecurityPermissionState.Allow;
                taskTypePermission.WriteState = SecurityPermissionState.Allow;
                taskTypePermission.ExportState = SecurityPermissionState.Allow;
                CustomTypePermissionObject userTypePermission = ObjectSpace.CreateObject<CustomTypePermissionObject>();
                userTypePermission.TargetType = typeof(PermissionPolicyUser);
                userTypePermission.NavigateState = SecurityPermissionState.Allow;
                userTypePermission.ReadState = SecurityPermissionState.Allow;
                userRole.TypePermissions.Add(taskTypePermission);
                userRole.TypePermissions.Add(userTypePermission);
                user.Roles.Add(userRole);
            }
            ObjectSpace.CommitChanges();
            for (int i = 1; i <= 10; i++) {
                string subject = string.Format("Task {0}", i);
                Task task = ObjectSpace.FindObject<Task>(new BinaryOperator("Subject", subject));
                if (task == null) {
                    task = ObjectSpace.CreateObject<Task>();
                    task.Subject = subject;
                    task.DueDate = DateTime.Today;
                    task.Save();
                }
            }
            ObjectSpace.CommitChanges();
        }
    }
}
