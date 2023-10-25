using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;

namespace CustomPermission.Module.Security {
    public class ExportPermissionRequestProcessor : PermissionRequestProcessorBase<ExportPermissionRequest> {
        private IPermissionDictionary permissionDictionary;
        public ExportPermissionRequestProcessor(IPermissionDictionary permissionDictionary) {
            this.permissionDictionary = permissionDictionary;
        }
        public override bool IsGranted(ExportPermissionRequest permissionRequest) {
            IEnumerable<ExportPermission> exportPermissions =
                permissionDictionary.GetPermissions<ExportPermission>().Where(p => p.ObjectType == permissionRequest.ObjectType);
            if (exportPermissions.Count() == 0) {
                return IsGrantedByPolicy(permissionDictionary);
            } else {
                return exportPermissions.Any(p => p.State == SecurityPermissionState.Allow);
            }
        }
        private bool IsGrantedByPolicy(IPermissionDictionary permissionDictionary) {
            if (GetPermissionPolicy(permissionDictionary) == SecurityPermissionPolicy.AllowAllByDefault) {
                return true;
            }
            return false;
        }
        private SecurityPermissionPolicy GetPermissionPolicy(IPermissionDictionary permissionDictionary) {
            SecurityPermissionPolicy result = SecurityPermissionPolicy.DenyAllByDefault;
            List<SecurityPermissionPolicy> permissionPolicies =
                permissionDictionary.GetPermissions<PermissionPolicy>().Select(p => p.SecurityPermissionPolicy).ToList();
            if (permissionPolicies != null && permissionPolicies.Count != 0) {
                if (permissionPolicies.Any(p => p == SecurityPermissionPolicy.AllowAllByDefault)) {
                    result = SecurityPermissionPolicy.AllowAllByDefault;
                } else {
                    if (permissionPolicies.Any(p => p == SecurityPermissionPolicy.ReadOnlyAllByDefault)) {
                        result = SecurityPermissionPolicy.ReadOnlyAllByDefault;
                    }
                }
            }
            return result;
        }
    }
}
