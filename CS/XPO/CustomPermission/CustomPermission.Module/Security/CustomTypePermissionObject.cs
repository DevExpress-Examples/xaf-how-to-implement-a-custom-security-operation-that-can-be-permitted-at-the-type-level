using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPermission.Module.Security {
    [XafDisplayName("Type Operation Permissions")]
    public class CustomTypePermissionObject : PermissionPolicyTypePermissionObject {
        public CustomTypePermissionObject(Session session)
            : base(session) {
        }
        [XafDisplayName("Export")]
        public SecurityPermissionState? ExportState {
            get {
                return GetPropertyValue<SecurityPermissionState?>(nameof(ExportState));
            }
            set {
                SetPropertyValue(nameof(ExportState), value);
            }
        }
    }
}
