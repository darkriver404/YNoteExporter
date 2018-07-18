using System;

namespace OrgDay.Util
{
    public class CommonUtil
    {
        public static string ShowProperSize(long size_byte)
        {
            string str = "";
            long total_byte = size_byte;
            long total_kb = total_byte / 1024;
            total_byte = total_byte % 1024;
            str = string.Format("{0}字节",total_byte);

            if (total_kb != 0)
            {
                long total_mb = total_kb / 1024;
                total_kb = total_kb % 1024;
                str = string.Format("{0}KB {1}", total_kb, str);

                if (total_mb != 0)
                {
                    long total_gb = total_mb / 1024;
                    total_mb = total_mb % 1024;
                    str = string.Format("{0}MB {1}", total_mb, str);

                    if (total_gb != 0)
                    {
                        str = string.Format("{0}G {1}", total_gb, str);
                    }
                }
            }
            return str;
        }

        public static string ShowFormatMS(long time_ms)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(time_ms);
            return dt.ToString("yyyy/MM/dd HH:mm:ss:ffff");
        }

        public static string ShowFormatSec(long time_sec)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(time_sec);
            return dt.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}