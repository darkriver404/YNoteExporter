using System.IO;
using UnityEngine;
using UnityEditor;

public class CreateAsset : Editor
{
    [MenuItem("Tools/CreateAsset/Config/AppConfig")]
    static void CreateAppConfig()
    {
        Create<AppConfig>("Config");
    }

    [MenuItem("Tools/CreateAsset/Config/RuntimeConfig")]
    static void CreateRuntimeConfig()
    {
        Create<RuntimeConfig>("Config");
    }

    static void Create<T>(string folder) where T : ScriptableObject
    {
        string typeName = typeof(T).Name;
        ScriptableObject obj = ScriptableObject.CreateInstance<T>();

        if (!obj)
        {
            Debug.LogWarning("Asset type not found! " + typeName);
            return;
        }

        string subPath = string.Format("Resources/Assets/{0}", folder);
        string path = string.Format("{0}/{1}", Application.dataPath, subPath);
        if (!Directory.Exists(path))
        {
            Debug.Log(path);
            Directory.CreateDirectory(path);
        }

        string assetPath = string.Format("Assets/{0}/{1}_new.asset", subPath, typeName);
        AssetDatabase.CreateAsset(obj, assetPath);
    }
}
