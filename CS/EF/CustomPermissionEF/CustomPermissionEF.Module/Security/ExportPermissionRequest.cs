using DevExpress.ExpressApp.Security;

namespace CustomPermission.Module.Security {
    public class ExportPermissionRequest : IPermissionRequest {
        public ExportPermissionRequest(Type objectType) {
            ObjectType = objectType;
        }
        public Type ObjectType { get; private set; }
        public object GetHashObject() {
            return ObjectType.FullName;
        }
    }
}
