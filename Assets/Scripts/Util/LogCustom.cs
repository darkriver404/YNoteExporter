using System;
using System.Text;
using System.Collections.Generic;

namespace OrgDay.Util
{
    /// <summary>
    /// Log工具
    /// </summary>
    public partial class Log
    {
        public static bool saveToFile = false;

        public static void kvp(string key, object value)
        {
            v("", string.Format("{0} : {1}", key, value));
        }

        public static void send(string url, Dictionary<string, string> content = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("url:").Append(url);
            string temp = StringUtil.GetString(content);
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append("\n").Append(temp);
            }
            send(sb.ToString());
        }

        public static void send(string message)
        {
            m("cyan", "send", message);
        }

        public static void recv(string message)
        {
            m("cyan", "receive", message);
        }

        public static void SaveToFile(string tag, string content)
        {
            if (!saveToFile)
                return;
            string fileName = string.Format("{0}_{1}", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"), tag);
            System.IO.File.WriteAllText(string.Format("{0}\\Output\\{1}.txt", System.Environment.CurrentDirectory, fileName), content);
        }
    }
}