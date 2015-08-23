using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common.model
{
    [Serializable]
    [DataContract(Name = "CrewMember")]
    public class CrewMember : Worker
    {
        #region Fields
        private string name;
        private string familyName;
        private string email;
        private string phone;
        private string username;
        private string password;
        private UserType userType = UserType.Worker;
        private string crew;
        private List<Skills> skills = new List<Skills>();
        private string shift;
        private List<string> workingDays = new List<string>();
        #endregion

        #region Properties
        [DataMember]
        [XmlElement("FirstName")]
        public string FirstName
        {
            get { return name; }
            set
            {
                name = value;
                Name = this.name + " " + this.familyName;
            }
        }
        [DataMember]
        [XmlElement("FamilyName")]
        public string FamilyName
        {
            get { return familyName; }
            set
            {
                familyName = value;
                Name = this.name + " " + this.familyName;
            }
        }
        [DataMember]
        [XmlElement("Email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        [DataMember]
        [XmlElement("Phone")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        [DataMember]
        [XmlIgnore]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        [XmlIgnore]
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        [DataMember]
        [XmlIgnore]
        public UserType UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        [DataMember]
        [XmlElement("Crew")]
        public string Crew
        {
            get { return crew; }
            set { crew = value; }
        }

        [DataMember]
        public List<Skills> Skills
        {
            get { return skills; }
            set { skills = value; }
        }

        [DataMember]
        public string Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        [DataMember]
        public List<string> WorkingDays
        {
            get { return workingDays; }
            set { workingDays = value; }
        } 
        #endregion

        #region Constructors
        public CrewMember() : base(EntityType.CrewMember) { }

        public CrewMember(string _name, string _familyName, string _email, string _phone)
            : base(EntityType.CrewMember)
        {
            this.name = _name;
            this.familyName = _familyName;
            this.email = _email;
            this.phone = _phone;
            // TODO: zastita od whitespace u imenu i prezimenu
            this.username = string.Format("{0}.{1}", _name.ToLower(), _familyName.ToLower());
        }

        public CrewMember(string _name, string _familyName, string _email, string _phone, string _username, string _password)
            : base(EntityType.CrewMember)
        {
            this.name = _name;
            this.familyName = _familyName;
            this.email = _email;
            this.phone = _phone;
            this.username = _username;
            this.password = _password;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Member: {4} {0} {1}, {2}, {3}", this.name, this.familyName, this.email, this.phone, this.GlobalId);
        }

        public string SkillsToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in skills)
                sb.Append(string.Format("{0}, ", item));

            return sb.ToString(0, sb.Length - 2);
        }
    }
}
