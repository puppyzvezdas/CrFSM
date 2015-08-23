using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common.model
{
    [Serializable]
    [DataContract]
    public class User : IdentifiedObject
    {
        private string username;
        private string password;
        private UserType userType;

        [DataMember]
        [XmlElement("UserType")]
        public UserType UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        [DataMember]
        [XmlElement("Username")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        [XmlElement("Password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public User(string _username, string _password, UserType _usertype)
            : base(EntityType.User)
        {
            this.username = _username;
            this.password = _password;
            this.userType = _usertype;
        }

        public User():base(EntityType.User) { }
    }
}
