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

        if (GUILayout.Button("ServerTime"))
        {
            Target.GetServerTime();
        }
        //OAuth
        GUILayout.Label("登录");
        if (GUILayout.Button("RequestToken"))
        {
            Target.RequestToken();
        }
        if (GUILayout.Button("UserLogin"))
        {
            Target.RequestUserLogin();
        }
        if (GUILayout.Button("AccessToken"))
        {
            Target.RequestAccessToken();
        }
        //API
        GUILayout.Label("用户");
        if (GUILayout.Button("UserInfo"))
        {
            Target.RequestUserInfo();
        }
        GUILayout.Label("笔记本");
        if (GUILayout.Button("AllNotebook"))
        {
            Target.RequestAllNotebook();
        }
        if (GUILayout.Button("ListAllNotes"))
        {
            Target.ListAllNotes();
        }
        if (GUILayout.Button("CreateNotebook"))
        {
            Target.CreateNotebook();
        }
        GUILayout.Label("笔记");
        if (GUILayout.Button("CreateNote"))
        {
            Target.CreateNote();
        }
        if (GUILayout.Button("GetNote"))
        {
            Target.GetNote();
        }
        if (GUILayout.Button("ModifyNote"))
        {
            Target.ModifyNote();
        }
        if (GUILayout.Button("MoveNote"))
        {
            Target.MoveNote();
        }
        if (GUILayout.Button("ShareNote"))
        {
            Target.ShareNote();
        }
        GUILayout.Label("附件");
    }
}
