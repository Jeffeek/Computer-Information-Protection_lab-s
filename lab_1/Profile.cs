using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    [DataContract]
    public class Profile
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string SecretWord { get; set; }
    }
}
