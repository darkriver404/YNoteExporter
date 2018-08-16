using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using OrgDay.Util;
using YNote.Data;
using YNote.Util;


public class YNoteExporter : MonoBehaviour
{
    private long oauth_timestamp;
    public string oauth_token;
    public string oauth_token_secret;
    public string oauth_verifier;
    public string notebookPath = @"\5FAD6B8F6CF949B6B9691D3E4C1CA4CE";
    public string notebookName = "new_notebook";
    public string noteContent = "default content";
    public string notePath;
    public string noteSource;
    public string noteAuthor;
    public string noteTitle;

    void Start()
    {
    }

    void Update()
    {
        YNoteUtil.oauth_verifier = oauth_verifier;
    }

    public void GetServerTime()
    {
        YNoteRequestDataGenerator.GetServerTime<ServerTimeData>(ParseServerTime);
    }

    public void RequestToken()
    {
        YNoteRequestDataGenerator.GetRequestToken<TokenData>(ParseRequestToken);
    }

    public void RequestUserLogin()
    {
        YNoteRequestDataGenerator.UserLogin<UserLoginData>(ParseUserLogin);
    }

    public void RequestAccessToken()
    {
        YNoteRequestDataGenerator.GetAccessToken<AccessTokenData>(ParseAccessToken);
    }

    public void RequestUserInfo()
    {
        YNoteRequestDataGenerator.GetUserInfo<UserInfoData>(ParseUserInfo);
    }

    public void RequestAllNotebook()
    {
        YNoteRequestDataGenerator.GetAllNotebook<List<NotebookData>>(ParseAllNotebook);
    }

    public void ListAllNotes()
    {
        YNoteRequestDataGenerator.ListAllNotes<List<string>>(SafeNotebookPath(notebookPath), ParseListAllNotes);
    }

    public void CreateNotebook()
    {
        YNoteRequestDataGenerator.CreateNotebook<CreateNotebook>(SafeNotebookName(notebookName), ParseCreateNotebook);
    }

    public void CreateNote()
    {
        YNoteRequestDataGenerator.CreateNote<CreateNote>(noteSource, noteAuthor, noteTitle, noteContent, ParseCreateNote);
    }

    public void GetNote()
    {
        YNoteRequestDataGenerator.GetNote<GetNote>(notePath, ParseGetNote);
    }

    public void ModifyNote()
    {
        YNoteRequestDataGenerator.ModifyNote<ModifyNote>(notePath, noteSource, noteAuthor, noteTitle, noteContent, ParseModifyNote);
    }

    public void MoveNote()
    {
        YNoteRequestDataGenerator.MoveNote<MoveNote>(notePath, SafeNotebookPath(notebookPath), ParseMoveNote);
    }

    public void ShareNote()
    {
        YNoteRequestDataGenerator.PublishNote<ShareNote>(notePath, ParseShareNote);
    }

    void ParseServerTime(ServerTimeData data)
    {
        if (data != null)
        {
            Log.d("server_time", data.ToString());
            oauth_timestamp = data.oauth_timestamp;
        }
    }

    void ParseRequestToken(TokenData data)
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
    
    void ParseUserLogin(UserLoginData data)
    {
        if (data != null)
        {
            Log.d("user_login", data.ToString());
        }
    }

    void ParseAccessToken(AccessTokenData data)
    {
        if (data != null)
        {
            Log.d("access_token", data.ToString());
            YNoteUtil.access_token = data.oauth_token;
            YNoteUtil.access_token_secret = data.oauth_token_secret;
        }
    }

    void ParseUserInfo(UserInfoData data)
    {
        //UserInfoData data;
        //UserInfoErrorData error;
        //if (ResultData.Create(result, UserInfoErrorData.ErrorMark, out data, out error))
        //{
            if (data != null)
            {
                Log.d("user", data.ToString());
            }
        //}
        //else
        //{
        //    if (error != null)
        //    {
        //        Log.d("error", error.ToString());
        //    }
        //}
    }

    void ParseAllNotebook(List<NotebookData> list)
    {
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Log.d("all_notebooks", list[i].ToString());
            }
        }
    }

    void ParseListAllNotes(List<string> list)
    {
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Log.d(i.ToString(),list[i]);
            }
        }
    }

    void ParseCreateNotebook(CreateNotebook data)
    {
        if (data != null)
        {
            Log.d("notebook", data.ToString());
        }
    }

    void ParseCreateNote(CreateNote data)
    {
        if (data != null)
        {
            Log.d("CreateNote", data.ToString());
        }
    }

    void ParseGetNote(GetNote data)
    {
        if (data != null)
        {
            Log.d("GetNote", data.ToString());
        }
    }

    void ParseModifyNote(ModifyNote data)
    {
        if (data != null)
        {
            Log.d("ModifyNote", data.ToString());
        }
    }

    void ParseMoveNote(MoveNote data)
    {
        if (data != null)
        {
            Log.d("MoveNote", data.ToString());
        }
    }

    void ParseShareNote(ShareNote data)
    {
        if (data != null)
        {
            Log.d("ShareNote", data.ToString());
        }
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
