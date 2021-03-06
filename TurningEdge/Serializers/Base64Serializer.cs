﻿using System;
using System.Collections.Generic;

namespace TurningEdge.Serializers
{
    public static class Base64Serializer
    {
        public static string Encode(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] Decode(this string base64EncodedData)
        {
            return Convert.FromBase64String(base64EncodedData);
        }
    }
}
