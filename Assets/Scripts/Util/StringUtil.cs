using UnityEngine;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OrgDay.Util
{
    /// <summary>
    /// 字符串工具
    /// </summary>
    public class StringUtil
    {
        public static string GetLogString(string tag, params object[] objs)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(tag).Append(" ").Append(GetString(objs));
            return builder.ToString();
        }

        public static string GetString(params object[] objs)
        {
            StringBuilder builder = new StringBuilder();
            if (objs != null && objs.Length != 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    builder.Append(objs[i]).Append(" ");
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 简单判断是否为json字符串
        /// </summary>
        public static bool IsJsonStrSimple(string str)
        {
            return str.StartsWith("{") && str.EndsWith("}") && str.Contains("\"");
        }

        /// <summary>
        /// 用正则判断是否为json字符串
        /// </summary>
        public static bool IsJsonStr(string str)
        {
            //TODO
            throw Exceptions.NotImplement;
        }

        /// <summary>
        /// 简单判断是否为oauth字符串
        /// </summary>
        public static bool IsOAuthStrSimple(string str)
        {
            return !str.StartsWith("{") && !str.EndsWith("}") && !str.Contains("\"") && str.Contains("=") && str.Contains("&");
        }

        /// <summary>
        /// oauth字符串转化为json字符串
        /// </summary>
        public static string OAuthStr2JsonStr(string str)
        {
            string json = str.Replace("=", "\":\"");
            json = json.Replace("&", "\",\"");

            StringBuilder builder = new StringBuilder();
            builder.Append("{\"");
            builder.Append(json);
            builder.Append("\"}");
            return builder.ToString();
        }
    }
}