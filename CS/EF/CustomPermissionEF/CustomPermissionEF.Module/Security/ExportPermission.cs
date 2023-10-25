using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;

namespace CustomPermission.Module.Security {
    [DomainComponent]
    public class ExportPermission : IOperationPermission {
        public ExportPermission(Type objectType, SecurityPermissionState state) {
            ObjectType = objectType;
            State = state;
        }
        public Type ObjectType { get; private set; }
        public string Operation { get { return "Export"; } }
        public SecurityPermissionState State { get; set; }
    }
}
