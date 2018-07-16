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

    private IOAuthUtil util;

    private int oauth_timestamp;
    public string oauth_token;
    public string oauth_token_secret;
    public string oauth_verifier;

    void Start()
    {
        switch (oauthVersion)
        {
            case OAuthVersion.oauth_1_0_a:
                util = new YNoteOAuthUtil();
                break;
            case OAuthVersion.oauth_2_0:
                util = new YNoteOAuthV2Util();
                break;
        }
    }

    void Update()
    {

    }

    public void UpdateEnvironment()
    {
        YNoteUtil.CurrentEnvironment = environment;
    }

    public void GetServerTime()
    {
        StartCoroutine(util.GetServerTime(ParseServerTime));
    }

    public void RequestToken()
    {
        StartCoroutine(util.GetRequestToken(ParseRequestToken));
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
            }
        }
        else
        {
            if (error != null)
            {

            }
        }
    }

    public void RequestUserLogin()
    {
        StartCoroutine(util.RequestUserLogin(oauth_token, ParseUserLogin));
    }

    void ParseUserLogin(string result)
    {
        UserLoginData data = ResultData.Create<UserLoginData>(result);
        if (data != null)
        {
        }
    }

    public void RequestAccessToken()
    {
        StartCoroutine(util.RequestAccessToken(oauth_token, oauth_verifier, oauth_token_secret, ParseAccessToken));
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
}
