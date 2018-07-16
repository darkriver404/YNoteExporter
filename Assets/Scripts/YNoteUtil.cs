using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class YNoteUtil
{
    /// <summary>
    /// 有道云笔记的 domain
    /// </summary>
    public static readonly string baseUrl_test = "notesandbox.youdao.com"; //测试环境
    public static readonly string baseUrl_online = "note.youdao.com"; //线上环境

    public static readonly string ownerId = "李晓天";
    public static readonly string consumerName = "OrgDay";
    public static readonly string consumerKey = "ba90f499d25a0c358f17fe937027c28b";
    public static readonly string consumerSecret = "162d6ffe283111467de97f13f1e2e4a2";

    public static EnvironmentType CurrentEnvironment;

    public static string GetURL(string url)
    {
        string baseUrl;
        switch (CurrentEnvironment)
        {
            default:
            case EnvironmentType.Test:
                baseUrl = baseUrl_test;
                break;
            case EnvironmentType.Online:
                baseUrl = baseUrl_online;
                break;
        }
        return url.Replace("[baseURL]", baseUrl);
    }
}