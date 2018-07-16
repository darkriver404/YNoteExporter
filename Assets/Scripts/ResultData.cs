using System;
using UnityEngine;

public class ResultData
{
    public virtual void Log()
    {
    }

    public static T Create<T>(string result) where T : ResultData
    {
        if (string.IsNullOrEmpty(result))
        {
            Debug.LogError("result IsNullOrEmpty!");
            return default(T);
        }

        string json = result;
        if (!YNoteUtil.IsJsonStr(result))
        {
            json = YNoteUtil.ResultStr2JsonStr(result);
        }
        T t = JsonUtility.FromJson<T>(json);
        t.Log();
        return t;
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
    public int oauth_timestamp;

    public override void Log()
    {
        Debug.Log("unit" + ":" + unit);
        Debug.Log("oauth_timestamp" + ":" + oauth_timestamp);
    }
}

[Serializable]
public class TokenData : ResultData
{
    public string oauth_token;
    public string oauth_token_secret;
    public string oauth_callback_confirmed;

    public static TokenData CreateFromJSON(string jsonString)
    {
        if (!YNoteUtil.IsJsonStr(jsonString))
        {
            jsonString = YNoteUtil.ResultStr2JsonStr(jsonString);
        }
        return JsonUtility.FromJson<TokenData>(jsonString);
    }

    public override void Log()
    {
        Debug.Log("oauth_token" + ":" + oauth_token);
        Debug.Log("oauth_token_secret" + ":" + oauth_token_secret);
        Debug.Log("oauth_callback_confirmed" + ":" + oauth_callback_confirmed);

    }
}

[Serializable]
public class TokenErrorData : ResultData
{
    public string oauth_problem;
    public string error;
    public string message;

    public override void Log()
    {
        Debug.Log("oauth_problem" + ":" + oauth_problem);
        Debug.Log("error" + ":" + error);
        Debug.Log("message" + ":" + message);
    }

    public static readonly string ErrorMark = "oauth_problem";
}

[Serializable]
public class UserLoginData : ResultData
{
    public string oauth_token;
    public string oauth_verifier;

    public override void Log()
    {
        Debug.Log("oauth_token" + ":" + oauth_token);
        Debug.Log("oauth_verifier" + ":" + oauth_verifier);
    }
}

[Serializable]
public class AccessTokenData : ResultData
{
    public string oauth_token;
    public string oauth_token_secret;

    public override void Log()
    {
        Debug.Log("oauth_token" + ":" + oauth_token);
        Debug.Log("oauth_token_secret" + ":" + oauth_token_secret);
    }
}