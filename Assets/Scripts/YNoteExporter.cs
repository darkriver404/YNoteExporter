using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrgDay.Util;

/// <summary>
/// OAuth 版本
/// </summary>
public enum OAuthVersion
{
    oauth_1_0_a,
    oauth_2_0,
}

/// <summary>
/// 环境
/// </summary>
public enum EnvironmentType
{
    Test,   // 测试沙箱
    Online, // 正式线上环境
}

public class YNoteExporter : MonoBehaviour
{
    public OAuthVersion oauthVersion;
    public EnvironmentType environment;

    //private IOAuthUtil util;

    private int oauth_timestamp;
    public string oauth_token;
    public string oauth_token_secret;
    public string oauth_verifier;

    void Start()
    {
        //switch (oauthVersion)
        //{
        //    case OAuthVersion.oauth_1_0_a:
        //        util = new YNoteOAuthUtil();
        //        break;
        //    case OAuthVersion.oauth_2_0:
        //        util = new YNoteOAuthV2Util();
        //        break;
        //}
    }

    void Update()
    {
        YNoteUtil.oauth_verifier = oauth_verifier;
    }

    public void UpdateEnvironment()
    {
        YNoteUtil.CurrentEnvironment = environment;
    }

    public void GetServerTime()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenServerTime(ParseServerTime);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    public void RequestToken()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenRequestToken(ParseRequestToken);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    public void RequestUserLogin()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenUserLogin(ParseUserLogin);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    public void RequestAccessToken()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenAccessToken(ParseAccessToken);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    public void RequestUserInfo()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenUserInfo(ParseUserInfo);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    void ParseServerTime(string result)
    {
        ServerTimeData data = ResultData.Create<ServerTimeData>(result);
        if (data != null)
        {
            oauth_timestamp = data.oauth_timestamp;
        }
    }

    void ParseRequestToken(string result)
    {
        TokenData data;
        TokenErrorData error;
        if (ResultData.Create(result, TokenErrorData.ErrorMark, out data, out error))
        {
            if (data != null)
            {
                oauth_token = data.oauth_token;
                oauth_token_secret = data.oauth_token_secret;
                //Log.d("oauth_token", oauth_token);

                YNoteUtil.oauth_token = data.oauth_token;
                YNoteUtil.oauth_token_secret = data.oauth_token_secret;
            }
        }
        else
        {
            if (error != null)
            {

            }
        }
    }
    
    void ParseUserLogin(string result)
    {
        UserLoginData data = ResultData.Create<UserLoginData>(result);
        if (data != null)
        {
        }
    }

    void ParseAccessToken(string result)
    {
        AccessTokenData data;
        TokenErrorData error;
        if (ResultData.Create(result, TokenErrorData.ErrorMark, out data, out error))
        {
            if (data != null)
            {
            }
        }
        else
        {
            if (error != null)
            {

            }
        }
    }

    void ParseUserInfo(string result)
    {
        AccessTokenData data;
        TokenErrorData error;
        if (ResultData.Create(result, TokenErrorData.ErrorMark, out data, out error))
        {
            if (data != null)
            {
            }
        }
        else
        {
            if (error != null)
            {

            }
        }
    }
}
