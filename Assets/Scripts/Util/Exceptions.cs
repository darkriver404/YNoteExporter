using System;

namespace OrgDay.Util
{
    /// <summary>
    /// Exception 类
    /// 复用异常对象 防止每次分配
    /// </summary>
    public class Exceptions
    {
        public static Exception NotImplement = new NotImplementedException("Not Implemented!");
    }
}