using CustomPermission.Module.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace CustomPermission.Module.Controllers {
    public class RemoveBaseTypePermissionNewActionItemController :
    ObjectViewController<ObjectView, PermissionPolicyTypePermissionObject> {
        protected override void OnFrameAssigned() {
            NewObjectViewController newObjectViewController = Frame.GetController<NewObjectViewController>();
            if (newObjectViewController != null) {
                newObjectViewController.CollectDescendantTypes += (s, e) => {
                    e.Types.Remove(typeof(PermissionPolicyTypePermissionObject));
                };
                newObjectViewController.ObjectCreating += (s, e) => {
                    if (e.ObjectType == typeof(PermissionPolicyTypePermissionObject)) {
                        e.NewObject = e.ObjectSpace.CreateObject(typeof(CustomTypePermissionObject));
                    }
                };
            }
            base.OnFrameAssigned();
        }
    }
}
