using UnityEngine;
using LitJson;
using System;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace _World.Tools
{
    public class Util
    {
        /// <summary>
        /// json to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T StringToObject<T>(string str) => JsonMapper.ToObject<T>(str);
        /// <summary>
        /// object to json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToString<T>(T t) => JsonMapper.ToJson(t);
        /// <summary>
        /// serialize object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public T DeSerializeObject<T>(string str) => JsonConvert.DeserializeObject<T>(str);
        /// <summary>
        /// deserialize object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string SerializaObject<T>(T t) => JsonConvert.SerializeObject(t);
        /// <summary>
        /// bytes to str.
        /// </summary>
        /// <param name="bts"></param>
        /// <returns></returns>
        public string BytesToStr(byte[] bts) => Encoding.UTF8.GetString(bts);
        /// <summary>
        /// str to bytes.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public byte[] StrToBytes(string str) => Encoding.UTF8.GetBytes(str);
    }
    
}

