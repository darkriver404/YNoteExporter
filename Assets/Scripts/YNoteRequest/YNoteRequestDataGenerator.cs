using System;
using System.Collections.Generic;

public class YNoteRequestDataGenerator
{
    #region URL
    #region OAuth
    //OAuth 1.0a
    private static readonly string OAUTH1_SERVER_TIME = "http://[baseURL]/oauth/time";
    private static readonly string OAUTH1_REQUEST_TOKEN = "http://[baseURL]/oauth/request_token";
    private static readonly string OAUTH1_USER_LOGIN = "http://[baseURL]/oauth/authorize";
    private static readonly string OAUTH1_ACCESS_TOKEN = "http://[baseURL]/oauth/access_token";
    //OAuth 2.0
    private static readonly string OAUTH2_USER_LOGIN = "https://[baseURL]/oauth/authorize2";
    private static readonly string OAUTH2_ACCESS_TOKEN = "https://[baseURL]/oauth/access2";
    private static readonly string OAUTH2_ACCESS_TOKEN_REPLACE = "https://[baseURL]/oauth/replace";
    #endregion
    #region OpenAPI
    //用户操作
    private static readonly string API_USER_INFO = "https://[baseURL]/yws/open/user/get.json";
    //笔记本
    private static readonly string API_ALL_NOTEBOOK = "https://[baseURL]/yws/open/notebook/all.json";
    private static readonly string API_LIST_NOTES = "https://[baseURL]/yws/open/notebook/list.json";
    private static readonly string API_NEW_NOTEBOOK = "https://[baseURL]/yws/open/notebook/create.json";
    private static readonly string API_DEL_NOTEBOOK = "https://[baseURL]/yws/open/notebook/delete.json";
    //笔记
    private static readonly string API_NEW_NOTE = "https://[baseURL]/yws/open/note/create.json";
    private static readonly string API_GET_NOTE = "https://[baseURL]/yws/open/note/get.json";
    private static readonly string API_MODIFY_NOTE = "https://[baseURL]/yws/open/note/update.json";
    private static readonly string API_MOVE_NOTE = "https://[baseURL]/yws/open/note/move.json";
    private static readonly string API_DEL_NOTE = "https://[baseURL]/yws/open/note/delete.json";
    //分享
    private static readonly string API_PUBLISH = "https://[baseURL]/yws/open/share/publish.json";
    //附件
    private static readonly string API_UP_RESOURCE = "https://[baseURL]/yws/open/resource/upload.json";
    private static readonly string API_DOWN_RESOURCE = "https://[baseURL]/yws/open/resource/download/";
    #endregion
    #endregion

    private static readonly string OAUTH_CALLBACK = "oob";
    private static readonly string OAUTH_SHA1 = "HMAC-SHA1";
    private static readonly string OAUTH_VER = "1.0";

    public static YNoteRequestData GenServerTime(Action<string> cb)
    {
        YNoteRequestData data = new YNoteRequestData(cb);
        data.httpVerb = HTTPVerb.GET;
        data.url = YNoteUtil.GetURL(OAUTH1_SERVER_TIME);
        return data;
    }

    public static YNoteRequestData GenRequestToken(Action<string> cb)
    {
        YNoteRequestData data = new YNoteRequestData(cb);
        data.httpVerb = HTTPVerb.POST;
        data.url = YNoteUtil.GetURL(OAUTH1_REQUEST_TOKEN);

        string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
        string callback = OAUTH_CALLBACK;
        string method = OAUTH_SHA1;
        string timeStamp = YNoteOAuthUtil.GenerateTimeStampSec();
        string nonce = YNoteOAuthUtil.GenerateNonce();
        string ver = OAUTH_VER;
        string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, YNoteUtil.consumerKey, YNoteUtil.consumerSecret, callback, method, timeStamp, nonce, ver);

        Dictionary<string, string> content = new Dictionary<string, string>();
        content.Add("oauth_callback", callback); // 回调 url
        content.Add("oauth_consumer_key", YNoteUtil.consumerKey); // consumerKey
        content.Add("oauth_nonce", nonce); // 随机串
        content.Add("oauth_signature_method", method); // 签名方法
        content.Add("oauth_timestamp", timeStamp); // 时间戳
        content.Add("oauth_version", ver); // oauth 版本
        content.Add("oauth_signature", signature); // 签名
        data.content = content;

        return data;
    }

    public static YNoteRequestData GenUserLogin(Action<string> cb)
    {
        YNoteRequestData data = new YNoteRequestData(null);
        data.httpVerb = HTTPVerb.GET;
        data.url = string.Format("{0}?oauth_token={1}", YNoteUtil.GetURL(OAUTH1_USER_LOGIN), YNoteUtil.oauth_token);
        data.needOpenUrl = true;
        return data;
    }

    public static YNoteRequestData GenAccessToken(Action<string> cb)
    {
        YNoteRequestData data = new YNoteRequestData(cb);
        data.httpVerb = HTTPVerb.POST;
        data.url = YNoteUtil.GetURL(OAUTH1_ACCESS_TOKEN);

        string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
        string callback = OAUTH_CALLBACK;
        string method = OAUTH_SHA1;
        string timeStamp = YNoteOAuthUtil.GenerateTimeStampSec();
        string nonce = YNoteOAuthUtil.GenerateNonce();
        string ver = OAUTH_VER;
        string oauth_token = YNoteUtil.oauth_token;
        string oauth_token_secret = YNoteUtil.oauth_token_secret;
        string oauth_verifier = YNoteUtil.oauth_verifier;
        string signature = YNoteOAuthUtil.GenerateOAuthSignature2(http, YNoteUtil.consumerKey, YNoteUtil.consumerSecret, oauth_token, oauth_verifier, oauth_token_secret, method, timeStamp, nonce, ver);

        Dictionary<string, string> content = new Dictionary<string, string>();
        content.Add("oauth_consumer_key", YNoteUtil.consumerKey); // consumerKey
        content.Add("oauth_token", oauth_token); // 请求 request_token 时返回的 oauth_token
        content.Add("oauth_verifier", oauth_verifier); // 授权码
        content.Add("oauth_signature_method", method); // 签名方法
        content.Add("oauth_timestamp", timeStamp); // 时间戳
        content.Add("oauth_nonce", nonce); // 随机串
        content.Add("oauth_version", ver); // oauth 版本
        content.Add("oauth_signature", signature); // 签名
        data.content = content;
        return data;
    }

    public static YNoteRequestData GenUserInfo(Action<string> cb)
    {
        YNoteRequestData data = new YNoteRequestData(cb);
        data.httpVerb = HTTPVerb.POST;
        data.url = YNoteUtil.GetURL(API_USER_INFO);

        string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
        string method = OAUTH_SHA1;
        string timeStamp = YNoteOAuthUtil.GenerateTimeStampSec();
        string nonce = YNoteOAuthUtil.GenerateNonce();
        string ver = OAUTH_VER;
        string oauth_token = YNoteUtil.oauth_token;
        string oauth_token_secret = YNoteUtil.oauth_token_secret;
        string oauth_verifier = YNoteUtil.oauth_verifier;
        string signature = YNoteOAuthUtil.GenerateOAuthSignature2(http, YNoteUtil.consumerKey, YNoteUtil.consumerSecret, oauth_token, oauth_verifier, oauth_token_secret, method, timeStamp, nonce, ver);

        Dictionary<string, string> content = new Dictionary<string, string>();
        content.Add("oauth_consumer_key", YNoteUtil.consumerKey); // consumerKey
        content.Add("oauth_token", oauth_token); // 请求 request_token 时返回的 oauth_token
        content.Add("oauth_verifier", oauth_verifier); // 授权码
        content.Add("oauth_signature_method", method); // 签名方法
        content.Add("oauth_timestamp", timeStamp); // 时间戳
        content.Add("oauth_nonce", nonce); // 随机串
        content.Add("oauth_version", ver); // oauth 版本
        content.Add("oauth_signature", signature); // 签名
        data.content = content;
        return data;
    }
}
