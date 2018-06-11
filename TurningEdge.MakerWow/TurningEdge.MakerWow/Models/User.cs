using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.DataTypes;

namespace TurningEdge.MakerWow.Models
{
    [Serializable]
    public class User : ICloneable
    {
        protected int _id;
        protected string _fName;
        protected string _lName;
        protected string _email;
        protected DateTime _registerDate;
        protected RegistrationStatus _status;

        public int Id
        {
            get
            {
                return _id;
            }
        }
        public string FName
        {
            get
            {
                return _fName;
            }
        }
        public string LName
        {
            get
            {
                return _lName;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
        }
        public DateTime RegisterDate
        {
            get
            {
                return _registerDate;
            }
        }
        public RegistrationStatus Status
        {
            get
            {
                return _status;
            }
        }

        public User()
        {

        }

        public User(
            int id,
            string fName,
            string lName,
            string email,
            DateTime registerDate,
            RegistrationStatus status)
        {
            _id = id;
            _fName = fName;
            _lName = lName;
            _email = email;
            _registerDate = registerDate;
            _status = status;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as User;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }

        public object Clone()
        {
            return new User(_id,
                _fName,
                _lName,
                _email,
                _registerDate,
                _status);
        }
    }
}
