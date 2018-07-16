using UnityEngine;

namespace OrgDay.Util
{
    /// <summary>
    /// YieldInstructions 类
    /// 复用 Waitxxx 对象 防止每次分配
    /// </summary>
    public class Yields
    {
        public static WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        public static WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
        public static WaitForSeconds WaitFor01Sec = new WaitForSeconds(0.1f);
        public static WaitForSeconds WaitFor05Sec = new WaitForSeconds(0.5f);
        public static WaitForSeconds WaitFor1Sec = new WaitForSeconds(1);
        public static WaitForSeconds WaitFor3Sec = new WaitForSeconds(3);
        public static WaitForSeconds WaitFor5Sec = new WaitForSeconds(5);
        public static WaitForSeconds WaitFor10Sec = new WaitForSeconds(10);
    }
}
