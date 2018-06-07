using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TurningEdge.Helpers;
using TurningEdge.MakerWow.Api.DataTypes;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Models
{
    public class User : JsonObject
    {
        private int _id;
        private string _fName;
        private string _lName;
        private string _email;
        private DateTime _registerDate;
        private RegistrationStatus _status;

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

        public User(
            int id,
            string fName,
            string lName,
            string email,
            DateTime registerDate,
            RegistrationStatus status)
            : base()
        {
            _id = id;
            _fName = fName;
            _lName = lName;
            _email = email;
            _registerDate = registerDate;
            _status = status;
        }

        public User(object record) 
            : base(record)
        {
        }

        protected override void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _fName = (string)record["fname"];
            _lName = (string)record["lname"];
            _email = (string)record["email"];
            _registerDate = DateTime.ParseExact((string)record["register_date"],
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            _status = EnumHelper.ParseEnum<RegistrationStatus>(((string)record["status"]));
        }

        public override string SerializeJson()
        {
            var record = new StringBuilder();

            record.Append("{");

            //record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "fname" + "\"" + " : " + "\"" + _fName + "\"" + ",");
            record.Append("\"" + "lname" + "\"" + " : " + "\"" + _lName + "\"" + ",");
            record.Append("\"" + "email" + "\"" + " : " + "\"" + _email + "\"" + ",");
            record.Append("\"" + "register_date" + "\"" + " : " + "\"" + _registerDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"" + ",");
            record.Append("\"" + "status" + "\"" + " : " + "\"" + _status.ToString().ToUpper() + "\"");

            record.Append("}");

            return record.ToString();
        }
    }
}
