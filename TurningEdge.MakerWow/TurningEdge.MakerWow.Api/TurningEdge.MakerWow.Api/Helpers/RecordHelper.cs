﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Helpers
{
    public static class RecordHelper
    {
        public static T[] Parse<T>(this object rawRecords)
            where T : JsonObject
        {
            var listRecords = rawRecords as List<object>;
            T[] records = new T[listRecords.Count];
            var factory = new Factory<JsonObject>();
            int i = 0;
            foreach(object record in listRecords)
            {
                records[i] = factory.Create<T>(record) as T;
                i++;
            }
            return records;
        }

        public static T ParseSingle<T>(this object rawRecord)
            where T : JsonObject
        {
            var factory = new Factory<JsonObject>();
            return factory.Create<T>(rawRecord) as T;
        }

        public static string SerializeJson<T>(this T[] records)
                where T : JsonObject
        {
            var listRecord = new StringBuilder();
            listRecord.Append("[");
            int i = 0;
            foreach (var record in records)
            {
                listRecord.Append(record.SerializeJson() + ((i < records.Length - 1)?",": string.Empty));
                i++;
            }
            listRecord.Append("]");
            return listRecord.ToString();
        }
    }
}
