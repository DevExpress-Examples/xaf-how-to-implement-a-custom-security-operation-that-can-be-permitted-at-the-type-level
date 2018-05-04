using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace CustomSecurityOperation.Module {
    public class ExportPermission : IOperationPermission {
        public ExportPermission(Type objectType, SecurityPermissionState state) {
            ObjectType = objectType;
            State = state;
        }
        public Type ObjectType { get; private set; }

        public string Operation {
            get {
                return "Export";
            }
        }
        public SecurityPermissionState State { get; set; }
    }
    public class ExportPermissionRequest : IPermissionRequest {
        public ExportPermissionRequest(Type objectType) {
            ObjectType = objectType;
        }
        public Type ObjectType { get; private set; }
        public object GetHashObject() {
            return this.GetType().FullName;
        }
    }

    public class ExportPermissionRequestProcessor : PermissionRequestProcessorBase<ExportPermissionRequest> {
        private IPermissionDictionary permissionDictionary;
        public ExportPermissionRequestProcessor(IPermissionDictionary permissionDictionary) {
            this.permissionDictionary = permissionDictionary;
        }
        public override bool IsGranted(ExportPermissionRequest permissionRequest) {
            IEnumerable<ExportPermission> exportPermissions = permissionDictionary.GetPermissions<ExportPermission>().Where(p => p.ObjectType == permissionRequest.ObjectType);
            if (exportPermissions.Count() == 0) {
                return IsGrantedByPolicy(permissionDictionary);
            }
            else {
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
            List<SecurityPermissionPolicy> permissionPolicies = permissionDictionary.GetPermissions<PermissionPolicy>().Select(p => p.SecurityPermissionPolicy).ToList();
            if (permissionPolicies != null && permissionPolicies.Count != 0) {
                if (permissionPolicies.Any(p => p == SecurityPermissionPolicy.AllowAllByDefault)) {
                    result = SecurityPermissionPolicy.AllowAllByDefault;
                }
                else {
                    if (permissionPolicies.Any(p => p == SecurityPermissionPolicy.ReadOnlyAllByDefault)) {
                        result = SecurityPermissionPolicy.ReadOnlyAllByDefault;
                    }
                }
            }
            return result;
        }
    }
    [XafDisplayName("Type Operation Permissions")]
    public class CustomTypePermissionObject : PermissionPolicyTypePermissionObject {
        public CustomTypePermissionObject(Session session)
            : base(session) {
        }
        [XafDisplayName("Export")]
        public SecurityPermissionState? ExportState {
            get {
                return GetPropertyValue<SecurityPermissionState?>("ExportState");
            }
            set {
                SetPropertyValue("ExportState", value);
            }
        }
    }
}
