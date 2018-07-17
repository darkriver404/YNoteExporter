using System;
using System.Collections.Generic;
using OrgDay.Util;

public enum HTTPVerb
{
    GET, POST
}

public class YNoteRequestData
{
    public string url;
    public HTTPVerb httpVerb;
    public Dictionary<string, string> content;
    public Action<string> cb;
    public bool needOpenUrl;

    public YNoteRequestData(Action<string> action)
    {
        cb = action;
    }

    public void LogDebugInfo()
    {
        Log.send(url, content);
    }
}