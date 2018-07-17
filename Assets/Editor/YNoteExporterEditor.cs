using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(YNoteExporter))]
public class YNoteExporterEditor : Editor
{
    public YNoteExporter Target
    {
        get { return (YNoteExporter)target; }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        this.serializedObject.Update();

        if (Target == null)
        {
            return;
        }
        Target.UpdateEnvironment();

        if (GUILayout.Button("GetServerTime"))
        {
            Target.GetServerTime();
        }
        if (GUILayout.Button("RequestToken"))
        {
            Target.RequestToken();
        }
        if (GUILayout.Button("RequestUserLogin"))
        {
            Target.RequestUserLogin();
        }
        if (GUILayout.Button("RequestAccessToken"))
        {
            Target.RequestAccessToken();
        }

        if (GUILayout.Button("RequestUserInfo"))
        {
            Target.RequestUserInfo();
        }
    }
}
