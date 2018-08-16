using System;
using UnityEngine;
using OrgDay.Util;

public class BaseAsset : ScriptableObject
{
    public override string ToString()
    {
        return "BaseAsset";
    }
}

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

[Serializable]
public class AppConfig : BaseAsset
{
    /// <summary>
    /// 测试环境
    /// </summary>
    public string baseUrl_test;
    /// <summary>
    /// 线上环境
    /// </summary>
    public string baseUrl_online;
    /// <summary>
    /// 开发者
    /// </summary>
    public string ownerId;
    /// <summary>
    /// 应用名
    /// </summary>
    public string consumerName;
    /// <summary>
    /// consumerKey
    /// </summary>
    public string consumerKey;
    /// <summary>
    /// consumerSecret
    /// </summary>
    public string consumerSecret;
    /// <summary>
    /// oauth版本
    /// </summary>
    public OAuthVersion oauthVersion;
    /// <summary>
    /// 环境
    /// </summary>
    public EnvironmentType environment;

    public override string ToString()
    {
        return StringUtil.CombineKVP(
            "baseUrl_test", baseUrl_test,
            "baseUrl_online", baseUrl_online,
            "ownerId", ownerId,
            "consumerName", consumerName,
            "consumerKey", consumerKey,
            "consumerSecret", consumerSecret,
            "oauthVersion", oauthVersion,
            "environment", environment
            );
    }
}
