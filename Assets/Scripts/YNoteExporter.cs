using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using OrgDay.Util;
using YNote;

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
    public string notebookPath = @"\5FAD6B8F6CF949B6B9691D3E4C1CA4CE";
    public string notebookName = "new_notebook";

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

    public void ListAllNotes()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenListAllNotes(SafeNotebookPath(notebookPath), ParseListAllNotes);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    public void CreateNotebook()
    {
        YNoteRequestData data = YNoteRequestDataGenerator.GenCreateNotebook(SafeNotebookName(notebookName), ParseCreateNotebook);
        YNoteRequestManager.Instance.SendRequest(data);
    }

    void ParseServerTime(string result)
    {
        ServerTimeData data = ResultData.Create<ServerTimeData>(result);
        if (data != null)
        {
            Log.d("server_time", data.ToString());
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
                Log.d("request_token", data.ToString());
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
                Log.d("error", error.ToString());
            }
        }
    }
    
    void ParseUserLogin(string result)
    {
        UserLoginData data = ResultData.Create<UserLoginData>(result);
        if (data != null)
        {
            Log.d("user_login", data.ToString());
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
                Log.d("access_token", data.ToString());
                YNoteUtil.access_token = data.oauth_token;
                YNoteUtil.access_token_secret = data.oauth_token_secret;
            }
        }
        else
        {
            if (error != null)
            {
                Log.d("error", error.ToString());
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
                Log.d("user", data.ToString());
            }
        }
        else
        {
            if (error != null)
            {
                Log.d("error", error.ToString());
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
                Log.d("all_notebooks", list[i].ToString());
            }
        }
    }

    void ParseListAllNotes(string result)
    {
        SaveToFile("notes_"+ SafeNotebookPath(notebookPath), result);
        List<string> list = JsonConvert.DeserializeObject<List<string>>(result);
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Log.d(i.ToString(),list[i]);
            }
        }
    }

    void ParseCreateNotebook(string result)
    {
        SaveToFile("create_notebook_" + SafeNotebookName(notebookName), result);
        CreateNotebook data;
        UserInfoErrorData error;
        if (ResultData.Create(result, UserInfoErrorData.ErrorMark, out data, out error))
        {
            if (data != null)
            {
                Log.d("notebook", data.ToString());
            }
        }
        else
        {
            if (error != null)
            {
                Log.d("error", error.ToString());
            }
        }
    }

    void SaveToFile(string fileName, string text)
    {
        System.IO.File.WriteAllText( string.Format("{0}\\Output\\{1}.txt", System.Environment.CurrentDirectory, fileName), text);
    }

    string SafeNotebookPath(string path)
    {
        if(path.StartsWith("\\"))
        {
            return path.Substring(1);
        }
        return path;
    }

    string SafeNotebookName(string name)
    {
        return name.Replace(" ", "_");
    }
}
