using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TurningEdge.Helpers;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models;

namespace TurningEdge.MakerWow.Api.Models
{
    public class UserJsonObject : User, IJsonObject
    {
        public UserJsonObject(int id, string fName, string lName, string email, DateTime registerDate, RegistrationStatus status)
            : base(id, fName, lName, email, registerDate, status)
        {
        }

        public UserJsonObject(object result)
        {
            ParseJson(result as Dictionary<string, object>);
        }

        public void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _fName = (string)record["fname"];
            _lName = (string)record["lname"];
            _email = (string)record["email"];
            _registerDate = DateTime.ParseExact((string)record["register_date"],
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            _status = EnumHelper.ParseEnum<RegistrationStatus>(((string)record["status"]));
        }

        public string SerializeJson()
        {
            var record = new StringBuilder();

            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
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
