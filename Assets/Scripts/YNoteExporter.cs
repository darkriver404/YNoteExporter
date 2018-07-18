using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
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

    private long oauth_timestamp;
    public string oauth_token;
    public string oauth_token_secret;
    public string oauth_verifier;

    void Start()
    {
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

    public void RequestAllNotebook()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenAllNotebook(ParseAllNotebook);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    void ParseServerTime(string result)
    {
        ServerTimeData data = ResultData.Create<ServerTimeData>(result);
        if (data != null)
        {
            data.LogDebugInfo();
            oauth_timestamp = data.oauth_timestamp;
        }
    }

    void ParseRequestToken(string result)
    {
        SaveToFile("request_token", result);
        TokenData data;
        TokenErrorData error;
        if (ResultData.Create(result, TokenErrorData.ErrorMark, out data, out error))
        {
            if (data != null)
            {
                data.LogDebugInfo();
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
                error.LogDebugInfo();
            }
        }
    }
    
    void ParseUserLogin(string result)
    {
        UserLoginData data = ResultData.Create<UserLoginData>(result);
        if (data != null)
        {
            data.LogDebugInfo();
        }
    }

    void ParseAccessToken(string result)
    {
        SaveToFile("access_token", result);
        AccessTokenData data;
        TokenErrorData error;
        if (ResultData.Create(result, TokenErrorData.ErrorMark, out data, out error))
        {
            if (data != null)
            {
                data.LogDebugInfo();
                YNoteUtil.access_token = data.oauth_token;
                YNoteUtil.access_token_secret = data.oauth_token_secret;
            }
        }
        else
        {
            if (error != null)
            {
                error.LogDebugInfo();
            }
        }
    }

    void ParseUserInfo(string result)
    {
        SaveToFile("user", result);
        UserInfoData data;
        UserInfoErrorData error;
        if (ResultData.Create(result, UserInfoErrorData.ErrorMark, out data, out error))
        {
            if (data != null)
            {
                data.LogDebugInfo();
            }
        }
        else
        {
            if (error != null)
            {
                error.LogDebugInfo();
            }
        }
    }

    void ParseAllNotebook(string result)
    {
        SaveToFile("notebooks", result);
        List<NotebookData> list = JsonConvert.DeserializeObject<List<NotebookData>>(result);
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].LogDebugInfo();
            }
        }
    }

    void SaveToFile(string fileName, string text)
    {
        System.IO.File.WriteAllText( string.Format("{0}\\Output\\{1}.txt", System.Environment.CurrentDirectory, fileName), text);
    }
}
