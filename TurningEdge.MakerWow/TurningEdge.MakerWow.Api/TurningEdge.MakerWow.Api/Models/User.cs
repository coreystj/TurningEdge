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


        public User(object record) 
            : base(record)
        {
        }

        protected override void ParseJson(object rawObject)
        {
            var record = rawObject as Dictionary<string, object>;

            _id = int.Parse((string)record["id"]);
            _fName = (string)record["fname"];
            _lName = (string)record["lname"];
            _email = (string)record["email"];
            _registerDate = DateTime.ParseExact((string)record["register_date"],
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            _status = EnumHelper.ParseEnum<RegistrationStatus>(((string)record["status"]));
        }
    }
}
