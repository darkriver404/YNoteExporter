using System;
using System.Text;
using System.Collections;
using UnityEngine.Networking;
using System.Security.Cryptography;
using OrgDay.Util;

public class YNoteOAuthUtil
{
    public static string TimeInSecondsSince1970
    {
        get { return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString(); }
    }

    public static string GenerateTimeStampSec()
    {
        return TimeInSecondsSince1970;
    }

    public static string TimeInMillisecondsSince1970
    {
        get { return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(); }
    }

    public static string GenerateTimeStampMS()
    {
        return TimeInMillisecondsSince1970;
    }

    public static string GenerateNonce()
    {
        //System.Guid myGUID = System.Guid.NewGuid();
        //return myGUID.ToString();
        string nonce = System.Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(TimeInSecondsSince1970 + TimeInSecondsSince1970 + TimeInSecondsSince1970));
        return nonce;
    }

    public static string GenerateOAuthSignature(string http, string consumerKey, string consumerSecret, string callback, string method, string timeStamp, string nonce, string ver)
    {
        string baseString = BuildBaseString(http, consumerKey, callback, method, timeStamp, nonce, ver);
        //Log.d("baseString", baseString);
        string key = BuildKey(consumerSecret);
        //Log.d("key", key);
        string signature = BuildSignature(key, baseString);
        //Log.d("signature", signature);
        return signature;
    }

    /// <summary>
    /// 1 构造源串
    /// HTTP请求方式 & urlencode(uri) & urlencode(a=x&b=y&...)
    /// </summary>
    /// <param name="http"></param>
    /// <param name="consumerKey"></param>
    /// <param name="callback"></param>
    /// <param name="method"></param>
    /// <param name="timeStamp"></param>
    /// <param name="nonce"></param>
    /// <param name="ver"></param>
    /// <returns></returns>
    public static string BuildBaseString(string http, string consumerKey, string callback, string method, string timeStamp, string nonce, string ver)
    {
        StringBuilder header = new StringBuilder();

        //1 大写 HTTP请求方式
        header.Append(http.ToUpper());
        header.Append("&");

        //2 URI路径进行URL编码
        string url = YNoteUtil.GetURL("http://[baseURL]/oauth/request_token");
        //Log.d("url", url);
        string encoded_url = Uri.EscapeDataString(url);
        //Log.d("encoded_url", encoded_url);
        header.Append(encoded_url);
        header.Append("&");

        //3 除 oauth_signature 外的所有参数按key进行字典升序排列
        //  用&拼接起来，并进行URL编码
        StringBuilder param = new StringBuilder();
        param.Append("oauth_callback=").Append(callback);
        param.Append("&").Append("oauth_consumer_key=").Append(consumerKey);
        param.Append("&").Append("oauth_nonce=").Append(nonce);
        param.Append("&").Append("oauth_signature_method=").Append(method);
        param.Append("&").Append("oauth_timestamp=").Append(timeStamp);
        param.Append("&").Append("oauth_version=").Append(ver);

        string paramStr = param.ToString();
        string encoded_param = Uri.EscapeDataString(paramStr);
        //Log.d("paramStr", paramStr);
        //Log.d("encoded_param", encoded_param);

        //4 连接起来
        header.Append(encoded_param);
        return header.ToString();
    }

    /// <summary>
    /// 2 构造密钥
    /// oauth_consumer_secret & oauth_token_secret
    /// </summary>
    /// <param name="consumerSecret"></param>
    /// <returns></returns>
    public static string BuildKey(string consumerSecret, string oauth_token_secret = "")
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(consumerSecret);
        builder.Append("&");
        if (!string.IsNullOrEmpty(oauth_token_secret))
        {
            builder.Append(oauth_token_secret);
        }
        return builder.ToString();
    }

    /// <summary>
    /// 3 生成签名值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="baseString"></param>
    /// <returns></returns>
    public static string BuildSignature(string key, string baseString)
    {
        //1. 使用HMAC-SHA1加密算法，将Step1中的到的源串以及Step2中得到的密钥进行加密。
        byte[] hashed = GetSha1Hash(key, baseString);
        //2.然后将加密后的字符串经过Base64编码，即得到oauth_signature签名参数的值。
        return Convert.ToBase64String(hashed);
    }

    /// <summary>
    /// HMAC-SHA1加密
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static byte[] GetSha1Hash(string key, string message)
    {
        var encoding = new System.Text.ASCIIEncoding();

        byte[] keyBytes = encoding.GetBytes(key);
        byte[] messageBytes = encoding.GetBytes(message);

        HMACSHA1 SHA1 = new HMACSHA1(keyBytes);
        byte[] Hashed = SHA1.ComputeHash(messageBytes);
        return Hashed;
    }

    public static string GenerateOAuthSignature2(string http, string consumerKey, string consumerSecret, string oauth_token, string oauth_verifier, string oauth_token_secret, string method, string timeStamp, string nonce, string ver)
    {
        string baseString = BuildBaseString2(http, consumerKey, oauth_token, oauth_verifier, method, timeStamp, nonce, ver);
        //Log.d("baseString", baseString);
        string key = BuildKey(consumerSecret, oauth_token_secret);
        //Log.d("key", key);
        string signature = BuildSignature(key, baseString);
        //Log.d("signature", signature);
        return signature;
    }

    /// <summary>
    /// 1 构造源串
    /// HTTP请求方式 & urlencode(uri) & urlencode(a=x&b=y&...)
    /// </summary>
    /// <param name="http"></param>
    /// <param name="consumerKey"></param>
    /// <param name="callback"></param>
    /// <param name="method"></param>
    /// <param name="timeStamp"></param>
    /// <param name="nonce"></param>
    /// <param name="ver"></param>
    /// <returns></returns>
    public static string BuildBaseString2(string http, string consumerKey, string oauth_token, string oauth_verifier, string method, string timeStamp, string nonce, string ver)
    {
        StringBuilder header = new StringBuilder();

        //1 大写 HTTP请求方式
        header.Append(http.ToUpper());
        header.Append("&");

        //2 URI路径进行URL编码
        string url = YNoteUtil.GetURL("http://[baseURL]/oauth/access_token");
        //Log.d("url", url);
        string encoded_url = Uri.EscapeDataString(url);
        //Log.d("encoded_url", encoded_url);
        header.Append(encoded_url);
        header.Append("&");

        //3 除 oauth_signature 外的所有参数按key进行字典升序排列
        //  用&拼接起来，并进行URL编码
        StringBuilder param = new StringBuilder();
        param.Append("oauth_consumer_key=").Append(consumerKey);
        param.Append("&").Append("oauth_nonce=").Append(nonce);
        param.Append("&").Append("oauth_signature_method=").Append(method);
        param.Append("&").Append("oauth_timestamp=").Append(timeStamp);
        param.Append("&").Append("oauth_token=").Append(oauth_token);
        param.Append("&").Append("oauth_verifier=").Append(oauth_verifier);
        param.Append("&").Append("oauth_version=").Append(ver);

        string paramStr = param.ToString();
        string encoded_param = Uri.EscapeDataString(paramStr);
        //Log.d("paramStr", paramStr);
        //Log.d("encoded_param", encoded_param);

        //4 连接起来
        header.Append(encoded_param);
        return header.ToString();
    }

    public static string GetHttpVerbName(HTTPVerb verb)
    {
        switch (verb)
        {
            default:
            case HTTPVerb.GET: return UnityWebRequest.kHttpVerbGET;
            case HTTPVerb.POST: return UnityWebRequest.kHttpVerbPOST;
        }
    }
}