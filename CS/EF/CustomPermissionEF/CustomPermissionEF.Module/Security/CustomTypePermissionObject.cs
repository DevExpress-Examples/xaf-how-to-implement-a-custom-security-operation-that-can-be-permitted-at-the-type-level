using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPermission.Module.Security {
    [XafDisplayName("Type Operation Permissions")]
    public class CustomTypePermissionObject : PermissionPolicyTypePermissionObject {
        [XafDisplayName("Export")]
        public virtual SecurityPermissionState? ExportState { get; set; }
    }
}
