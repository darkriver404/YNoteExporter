using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class YNoteUtil
{
    public static string consumerKey
    {
        get { return ConfigMgr.Inst.appConfig.consumerKey; }
    }

    public static string consumerSecret
    {
        get { return ConfigMgr.Inst.appConfig.consumerSecret; }
    }

    public static string baseUrl
    {
        get
        {
            switch (ConfigMgr.Inst.appConfig.environment)
            {
                default:
                case EnvironmentType.Test:
                    return ConfigMgr.Inst.appConfig.baseUrl_test;
                case EnvironmentType.Online:
                    return ConfigMgr.Inst.appConfig.baseUrl_online;
            }
        }
    }

    public static string GetURL(string url)
    {
        return url.Replace("[baseURL]", baseUrl);
    }

    public static string oauth_token;
    public static string oauth_token_secret;
    public static string oauth_verifier;

    public static string access_token;
    public static string access_token_secret;

}