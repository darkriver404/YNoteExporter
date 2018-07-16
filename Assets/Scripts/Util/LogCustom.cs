using System.Text;
using System.Collections.Generic;

namespace OrgDay.Util
{
    /// <summary>
    /// Log工具
    /// </summary>
    public partial class Log
    {
        public static void kvp(string key, object value)
        {
            v("", string.Format("{0} : {1}", key, value));
        }

        public static void send(string url, Dictionary<string, string> content = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("url:").Append(url);
            if (content != null && content.Count != 0)
            {
                sb.Append("\n");
                foreach (var kvp in content)
                {
                    sb.Append(kvp.Key).Append(":").Append(kvp.Value).Append("\n");
                }
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
    }
}