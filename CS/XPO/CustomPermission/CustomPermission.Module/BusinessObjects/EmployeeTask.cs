using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace dxTestSolution.Module.BusinessObjects {
    [DefaultClassOptions]

    public class EmployeeTask : BaseObject {
        public EmployeeTask(Session session)
            : base(session) {
        }
     
        string _firstName;
        public string FirstName {
            get {
                return _firstName;
            }
            set {
                SetPropertyValue(nameof(FirstName), ref _firstName, value);
            }
        }
        string _lastName;
        public string LastName {
            get {
                return _lastName;
            }
            set {
                SetPropertyValue(nameof(LastName), ref _lastName, value);
            }
        }
        int _age;
        public int Age {
            get {
                return _age;
            }
            set {
                SetPropertyValue(nameof(Age), ref _age, value);
            }
        }
       
        [Association("Contact-Tasks")]
        public XPCollection<MyTask> Tasks {
            get {
                return GetCollection<MyTask>(nameof(Tasks));
            }
        }
    }
}