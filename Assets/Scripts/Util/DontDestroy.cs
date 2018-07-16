using UnityEngine;

namespace OrgDay.Util
{
    /// <summary>
    /// 加载时不销毁 
    /// </summary>
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}