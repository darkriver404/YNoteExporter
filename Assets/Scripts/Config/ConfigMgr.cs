using UnityEngine;
using OrgDay.Util;

public class ConfigMgr : MonoBehaviour
{
    public static ConfigMgr Inst;
    public AppConfig appConfig;
    public RuntimeConfig runtimeConfig;

    void Awake()
    {
        Inst = this;
        Log.saveToFile = runtimeConfig.saveToFile;
    }
}