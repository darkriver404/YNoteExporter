using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        }
    }

    void ParseRequestToken(string result)
    {
        TokenData data;
        TokenErrorData error;
        if (ResultData.Create<TokenData, TokenErrorData>(result, TokenErrorData.ErrorMark, out data, out error))
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
