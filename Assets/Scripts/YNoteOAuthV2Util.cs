using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using OrgDay.Util;

public class YNoteOAuthV2Util : YNoteUtil, IOAuthUtil
{
    public IEnumerator GetServerTime(Action<string> result)
    {
        yield return null;
    }

    public IEnumerator GetRequestToken(Action<string> result)
    {
        Dictionary<string, string> content = new Dictionary<string, string>();
        content.Add("client_id", "a82cf830df2780731c65783ddb82b207");//consumerKey
        content.Add("response_type", "code");// code
        content.Add("redirect_uri", "");// domains //http://note.youdao.com/redirect
        content.Add("state", "");// state
        content.Add("display", "mobile");// display

        string url = GetURL("https://[baseURL]/oauth/authorize2");
        Log.send(url, content);
        UnityWebRequest www = UnityWebRequest.Post(url, content);
        yield return www.Send();

        string resultContent = string.Empty;
        if (!www.isNetworkError)
        {
            resultContent = www.downloadHandler.text;
        }
        Log.recv(resultContent);
        result(resultContent);
    }

    public IEnumerator RequestUserLogin(string oauth_token, Action<string> result)
    {
        yield return null;
    }

    public IEnumerator RequestAccessToken(string oauth_token, string oauth_verifier, string oauth_token_secret, Action<string> result)
    {
        yield return null;
    }
}
