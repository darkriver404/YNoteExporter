using System;
using System.Collections.Generic;
using UnityEngine;
using OrgDay.Util;

public class ResultData
{
    public virtual void LogDebugInfo()
    {
    }

    public static T Create<T>(string result)
    {
        if (string.IsNullOrEmpty(result))
        {
            Log.e("result IsNullOrEmpty!");
            return default(T);
        }

        string json = result;
        if (!StringUtil.IsJsonStrSimple(result))
        {
            json = StringUtil.OAuthStr2JsonStr(result);
        }
        try
        {
            T t = JsonUtility.FromJson<T>(json);
            return t;
        }
        catch (Exception e)
        {
            Log.e(e.Message);
        }
        return default(T);
    }

    public static bool Create<T1, T2>(string result, string errorStr, out T1 t1, out T2 t2) 
        where T1: ResultData
        where T2 : ResultData
    {
        t1 = default(T1);
        t2 = default(T2);

        if (result.Contains(errorStr))
        {
            t2 = Create<T2>(result);
            return false;
        }
        t1 = Create<T1>(result);
        return true;
    }
}

[Serializable]
public class ServerTimeData : ResultData
{
    public string unit;
    public long oauth_timestamp;

    public override void LogDebugInfo()
    {
        Log.kvp("unit", unit);
        Log.kvp("oauth_timestamp" , oauth_timestamp);
    }
}

[Serializable]
public class TokenData : ResultData
{
    public string oauth_token;
    public string oauth_token_secret;
    public string oauth_callback_confirmed;

    public override void LogDebugInfo()
    {
        Log.kvp("oauth_token" , oauth_token);
        Log.kvp("oauth_token_secret" , oauth_token_secret);
        Log.kvp("oauth_callback_confirmed" , oauth_callback_confirmed);
    }
}

[Serializable]
public class TokenErrorData : ResultData
{
    public string oauth_problem;
    public int error;
    public string message;

    public override void LogDebugInfo()
    {
        Log.kvp("oauth_problem" , oauth_problem);
        Log.kvp("error" , error);
        Log.kvp("message" , message);
    }

    public static readonly string ErrorMark = "oauth_problem";
}

[Serializable]
public class UserLoginData : ResultData
{
    public string oauth_token;
    public string oauth_verifier;

    public override void LogDebugInfo()
    {
        Log.kvp("oauth_token" , oauth_token);
        Log.kvp("oauth_verifier" , oauth_verifier);
    }
}

[Serializable]
public class AccessTokenData : ResultData
{
    public string oauth_token;
    public string oauth_token_secret;

    public override void LogDebugInfo()
    {
        Log.kvp("oauth_token" , oauth_token);
        Log.kvp("oauth_token_secret" , oauth_token_secret);
    }
}

[Serializable]
public class UserInfoData : ResultData
{
    public string id;       //用户ID
    public string user;     //用户名（部分隐藏）
    public long total_size; //字节
    public long used_size;  //字节
    public long register_time; //ms
    public long last_login_time; //ms
    public long last_modify_time; //ms
    public string default_notebook; //默认笔记本 path
    public bool is_multilevel;

    public override void LogDebugInfo()
    {
        Log.kvp("用户ID", id);
        Log.kvp("用户名", user);
        Log.kvp("总空间大小", CommonUtil.ShowProperSize(total_size));
        Log.kvp("已使用空间大小", CommonUtil.ShowProperSize(used_size));
        Log.kvp("注册时间", CommonUtil.ShowFormatMS(register_time));
        Log.kvp("最后登录时间", CommonUtil.ShowFormatMS(last_login_time));
        Log.kvp("最后修改时间", CommonUtil.ShowFormatMS(last_modify_time));
        Log.kvp("默认笔记本", default_notebook);
        Log.kvp("是否多层级", is_multilevel);
    }
}

[Serializable]
public class UserInfoErrorData : ResultData
{
    public bool canTryAgain;
    public string scope;
    public int error;
    public string message;
    public string objectUser;

    public override void LogDebugInfo()
    {
        Log.kvp("canTryAgain", canTryAgain);
        Log.kvp("scope", scope);
        Log.kvp("error", error);
        Log.kvp("message", message);
        Log.kvp("objectUser", objectUser);
    }

    public static readonly string ErrorMark = "error";
}

[Serializable]
public class NotebookData : ResultData
{
    public string path;
    public string name;
    public int notes_num;
    public string group;//已废弃的笔记本组字段
    public long create_time;
    public long modify_time;

    public override void LogDebugInfo()
    {
        //Log.kvp("path", path);
        Log.kvp("name", name);
        Log.kvp("notes_num", notes_num);
        //Log.kvp("group", group);
        Log.kvp("创建时间", CommonUtil.ShowFormatSec(create_time));
        Log.kvp("修改时间", CommonUtil.ShowFormatSec(modify_time));
    }
}

[Serializable]
public class CreateNotebook : ResultData
{
    public string path;
    public long create_time;
    public long modify_time;
    public string name;
    public int notes_num;

    public override void LogDebugInfo()
    {
        Log.kvp("path", path);
        Log.kvp("name", name);
        Log.kvp("创建时间", CommonUtil.ShowFormatSec(create_time));
    }
}

[Serializable]
public class DeleteNotebook : ResultData
{
    public override void LogDebugInfo()
    {
    }
}