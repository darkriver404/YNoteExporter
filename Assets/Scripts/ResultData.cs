using System;
using UnityEngine;
using OrgDay.Util;

public class ResultData
{
    public virtual void LogDebugInfo()
    {
    }

    public static T Create<T>(string result) where T : ResultData
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
            t.LogDebugInfo();
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
    public string user;
    public long total_size;
    public long used_size;
    public long register_time;
    public long last_login_time;
    public long last_modify_time;
    public string default_notebook;

    public override void LogDebugInfo()
    {
        Log.kvp("user", user);
        Log.kvp("total_size", total_size);
        Log.kvp("used_size", used_size);
        Log.kvp("register_time", register_time);
        Log.kvp("last_login_time", last_login_time);
        Log.kvp("last_modify_time", last_modify_time);
        Log.kvp("default_notebook", default_notebook);
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
}