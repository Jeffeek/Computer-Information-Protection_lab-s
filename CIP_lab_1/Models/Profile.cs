using System;
using System.Runtime.Serialization;

namespace CIP_lab_1.Models
{
    [DataContract]
    public class Profile : IEquatable<Profile>
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string SecretWord { get; set; }


        public bool Equals(Profile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(FullName, other.FullName) &&
                   string.Equals(Login, other.Login) &&
                   string.Equals(Password, other.Password) && 
                   string.Equals(SecretWord, other.SecretWord);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Profile) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (FullName != null ? FullName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Login != null ? Login.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SecretWord != null ? SecretWord.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
} 