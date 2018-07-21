using UnityEngine;

namespace OrgDay.Util
{
    /// <summary>
    /// Log工具 内核
    /// </summary>
    public partial class Log
    {
        public static void v(string tag, object message)
        {
            m("white", tag, message);
        }

        public static void d(string tag, object message)
        {
            m("green", tag, message);
        }

        public static void m(string color, string tag, object message)
        {
            Debug.Log(string.Format("<color={0}>{1}</color> {2}", color, tag, message));
        }

        public static void w(params object[] objs)
        {
            Debug.LogWarning(StringUtil.GetString(objs));
        }

        public static void e(params object[] objs)
        {
            Debug.LogError(StringUtil.GetString(objs));
        }
    }
}