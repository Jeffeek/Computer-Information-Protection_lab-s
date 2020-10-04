using System.Runtime.Serialization;

namespace lab_1.Models
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