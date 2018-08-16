using UnityEngine;

public class ConfigMgr : MonoBehaviour
{
    public static ConfigMgr Inst;
    public AppConfig appConfig;

    void Awake()
    {
        Inst = this;
    }
}