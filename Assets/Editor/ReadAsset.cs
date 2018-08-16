using UnityEngine;
using UnityEditor;
using OrgDay.Util;

public class ReadAsset : Editor
{
    [MenuItem("Tools/ReadAsset/Config/AppConfig")]
    static void ReadAppConfig()
    {
        Read<AppConfig>("Config");
    }

    [MenuItem("Tools/ReadAsset/Config/RuntimeConfig")]
    static void ReadRuntimeConfig()
    {
        Read<RuntimeConfig>("Config");
    }

    static void Read<T>(string folder) where T : ScriptableObject
    {
        string dir = string.Format("{0}/Resources/Assets/{1}", Application.dataPath, folder);
        string path = EditorUtility.OpenFilePanel("读取Asset", dir, "asset");
        if (!string.IsNullOrEmpty(path))
        {
            int index = path.IndexOf("Assets/Resources/Assets");
            if (index != -1)
            {
                string assetPath = path.Substring(index);
                T role = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                string typeName = typeof(T).Name;
                Log.d(typeName, role.ToString());
            }
        }
    }
}
