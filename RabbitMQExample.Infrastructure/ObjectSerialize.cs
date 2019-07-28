﻿

using Newtonsoft.Json;
using System;
using System.Text;

namespace RabbitMQExample.Model
{
    public static class ObjectSerialize
    {
        public static byte[] Serialize(this object obj)
        {
            if(obj == null) return null;
            var json = JsonConvert.SerializeObject(obj);
            return Encoding.ASCII.GetBytes(json);
        }

        public static object DeSerialize(this byte[] arrBytes, Type type)
        {
            var json = Encoding.Default.GetString(arrBytes);
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}
