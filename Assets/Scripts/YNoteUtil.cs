﻿using System.Text;
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

    public static bool IsJsonStr(string result)
    {
        return result.Contains("{") && result.Contains("}") && result.Contains("\"");
    }

    public static string ResultStr2JsonStr(string result)
    {
        string json = result.Replace("=", "\":\"");
        json = json.Replace("&", "\",\"");

        StringBuilder builder = new StringBuilder();
        builder.Append("{\"");
        builder.Append(json);
        builder.Append("\"}");
        return builder.ToString();
    }

    public static void LogSend(string url, Dictionary<string, string> content = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("url:").Append(url);
        if (content != null && content.Count != 0)
        {
            sb.Append("\n");
            foreach (var kvp in content)
            {
                sb.Append(kvp.Key).Append(":").Append(kvp.Value).Append("\n");
            }
        }
        LogSend(sb.ToString());
    }

    public static void LogSend(string message)
    {
        LogColor("cyan", "send", message);
    }

    public static void LogRecv(string message)
    {
        LogColor("cyan", "receive", message);
    }

    public static void LogMark(string message)
    {
        LogColor("green", "mark", message);
    }

    public static void Log(string tag, string message)
    {
        LogColor("green", tag, message);
    }

    public static void LogColor(string color, string tag, string message)
    {
        Debug.Log(string.Format("<color={0}>{1}</color> {2}", color, tag, message));
    }
}