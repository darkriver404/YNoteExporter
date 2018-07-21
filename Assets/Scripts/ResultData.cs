using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using OrgDay.Util;

namespace YNote.Data
{
    [Serializable]
    public class ServerTimeData
    {
        public string unit;
        public long oauth_timestamp;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "unit", unit,
                "oauth_timestamp", oauth_timestamp);
        }
    }

    [Serializable]
    public class TokenData
    {
        public string oauth_token;
        public string oauth_token_secret;
        public string oauth_callback_confirmed;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "oauth_token", oauth_token,
                "oauth_token_secret", oauth_token_secret,
                "oauth_callback_confirmed", oauth_callback_confirmed);
        }
    }

    [Serializable]
    public class TokenErrorData
    {
        public string oauth_problem;
        public int error;
        public string message;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "oauth_problem", oauth_problem,
                "error", error,
                "message", message);
        }

        public static readonly string ErrorMark = "oauth_problem";
    }

    [Serializable]
    public class UserLoginData
    {
        public string oauth_token;
        public string oauth_verifier;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "oauth_token", oauth_token,
                "oauth_verifier", oauth_verifier);
        }
    }

    [Serializable]
    public class AccessTokenData
    {
        public string oauth_token;
        public string oauth_token_secret;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "oauth_token", oauth_token,
                "oauth_token_secret", oauth_token_secret);
        }
    }

    [Serializable]
    public class UserInfoData
    {
        public string id;       //用户ID
        public string user;     //用户名（部分隐藏）
        public long total_size; //字节
        public long used_size;  //字节
        public long register_time; //ms
        public long last_login_time; //ms
        public long last_modify_time; //ms
        public string default_notebook; //默认笔记本 path
        public bool is_multilevel;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "用户ID", id,
                "用户名", user,
                "总空间大小", CommonUtil.ShowProperSize(total_size),
                "已使用空间大小", CommonUtil.ShowProperSize(used_size),
                "注册时间", CommonUtil.ShowFormatMS(register_time),
                "最后登录时间", CommonUtil.ShowFormatMS(last_login_time),
                "最后修改时间", CommonUtil.ShowFormatMS(last_modify_time),
                "默认笔记本", default_notebook,
                "是否多层级", is_multilevel);
        }
    }

    [Serializable]
    public class UserInfoErrorData
    {
        public bool canTryAgain;
        public string scope;
        public int error;
        public string message;
        public string objectUser;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "canTryAgain", canTryAgain,
                "scope", scope,
                "error", error,
                "message", message,
                "objectUser", objectUser);
        }

        public static readonly string ErrorMark = "error";
    }

    [Serializable]
    public class NotebookData
    {
        public string path;
        public string name;
        public int notes_num;
        public string group;//已废弃的笔记本组字段
        public long create_time;
        public long modify_time;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                //"path", path,
                "name", name,
                "notes_num", notes_num,
                //"group", group,
                "创建时间", CommonUtil.ShowFormatSec(create_time),
                "修改时间", CommonUtil.ShowFormatSec(modify_time));
        }
    }

    [Serializable]
    public class CreateNotebook
    {
        public string path;
        public long create_time;
        public long modify_time;
        public string name;
        public int notes_num;

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "path", path,
                "name", name,
                "创建时间", CommonUtil.ShowFormatSec(create_time));
        }
    }

    [Serializable]
    public class DeleteNotebook
    {
    }
}