using System;
using System.Collections.Generic;
using YNote.Data;
namespace YNote.Util
{
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
        private static readonly string API_USER_INFO = "http://[baseURL]/yws/open/user/get.json";
        //笔记本
        private static readonly string API_ALL_NOTEBOOK = "http://[baseURL]/yws/open/notebook/all.json";
        private static readonly string API_LIST_NOTES = "http://[baseURL]/yws/open/notebook/list.json";
        private static readonly string API_NEW_NOTEBOOK = "http://[baseURL]/yws/open/notebook/create.json";
        private static readonly string API_DEL_NOTEBOOK = "http://[baseURL]/yws/open/notebook/delete.json";
        //笔记
        private static readonly string API_NEW_NOTE = "http://[baseURL]/yws/open/note/create.json";
        private static readonly string API_GET_NOTE = "http://[baseURL]/yws/open/note/get.json";
        private static readonly string API_MODIFY_NOTE = "http://[baseURL]/yws/open/note/update.json";
        private static readonly string API_MOVE_NOTE = "http://[baseURL]/yws/open/note/move.json";
        private static readonly string API_DEL_NOTE = "http://[baseURL]/yws/open/note/delete.json";
        //分享
        private static readonly string API_PUBLISH = "http://[baseURL]/yws/open/share/publish.json";
        //附件
        private static readonly string API_UP_RESOURCE = "http://[baseURL]/yws/open/resource/upload.json";
        private static readonly string API_DOWN_RESOURCE = "http://[baseURL]/yws/open/resource/download/";
        #endregion
        #endregion

        private static readonly string OAUTH_CALLBACK = "oob";
        private static readonly string OAUTH_SHA1 = "HMAC-SHA1";
        private static readonly string OAUTH_VER = "1.0";

        public static void GetServerTime<T>(Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.GET;
            data.url = YNoteUtil.GetURL(OAUTH1_SERVER_TIME);
            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void GetRequestToken<T>(Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(OAUTH1_REQUEST_TOKEN);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string callback = OAUTH_CALLBACK;
            string method = OAUTH_SHA1;
            string timeStamp = YNoteOAuthUtil.GenerateTimeStampSec();
            string nonce = YNoteOAuthUtil.GenerateNonce();
            string ver = OAUTH_VER;

            data.content = new Dictionary<string, string>();
            data.content.Add("oauth_callback", callback); // 回调 url
            data.content.Add("oauth_consumer_key", YNoteUtil.consumerKey); // consumerKey
            data.content.Add("oauth_nonce", nonce); // 随机串
            data.content.Add("oauth_signature_method", method); // 签名方法
            data.content.Add("oauth_timestamp", timeStamp); // 时间戳
            data.content.Add("oauth_version", ver); // oauth 版本

            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void UserLogin<T>(Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.GET;
            data.url = string.Format("{0}?oauth_token={1}", YNoteUtil.GetURL(OAUTH1_USER_LOGIN), YNoteUtil.oauth_token);
            data.needOpenUrl = true;
            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void GetAccessToken<T>(Action<T> cb)
        {
            var data = new YNoteRequestData();
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

            data.content = new Dictionary<string, string>();
            data.content.Add("oauth_consumer_key", YNoteUtil.consumerKey); // consumerKey
            data.content.Add("oauth_token", oauth_token); // 请求 request_token 时返回的 oauth_token
            data.content.Add("oauth_verifier", oauth_verifier); // 授权码
            data.content.Add("oauth_signature_method", method); // 签名方法
            data.content.Add("oauth_timestamp", timeStamp); // 时间戳
            data.content.Add("oauth_nonce", nonce); // 随机串
            data.content.Add("oauth_version", ver); // oauth 版本

            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        private static void AppendOAuthContent(ref YNoteRequestData data)
        {
            string method = OAUTH_SHA1;
            string timeStamp = YNoteOAuthUtil.GenerateTimeStampSec();
            string nonce = YNoteOAuthUtil.GenerateNonce();
            string ver = OAUTH_VER;
            string oauth_token = YNoteUtil.access_token;

            data.content = new Dictionary<string, string>();
            data.content.Add("oauth_consumer_key", YNoteUtil.consumerKey); // consumerKey
            data.content.Add("oauth_token", oauth_token); //  oauth_token
            data.content.Add("oauth_signature_method", method); // 签名方法
            data.content.Add("oauth_timestamp", timeStamp); // 时间戳
            data.content.Add("oauth_nonce", nonce); // 随机串
            data.content.Add("oauth_version", ver); // oauth 版本
        }

        public static void GetUserInfo<T>(Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.GET;
            data.url = YNoteUtil.GetURL(API_USER_INFO);

            AppendOAuthContent(ref data);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名
            data.url = YNoteOAuthUtil.GenerateQueryString(data.url, data.content);

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void GetAllNotebook<T>(Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_ALL_NOTEBOOK);

            AppendOAuthContent(ref data);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void ListAllNotes<T>(string notebookPath, Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_LIST_NOTES);

            AppendOAuthContent(ref data);
            data.content.Add("notebook", notebookPath);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void CreateNotebook<T>(string notebookName, Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_NEW_NOTEBOOK);

            AppendOAuthContent(ref data);
            data.content.Add("name", notebookName);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void CreateNote<T>(string source, string author, string title, string content, Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_NEW_NOTE);
            data.contentType = HTTPContentType.MULTIPART;

            AppendOAuthContent(ref data);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            data.content.Add("source", source);
            data.content.Add("author", author);
            data.content.Add("title", title);
            data.content.Add("content", content);

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void GetNote<T>(string notePath, Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_GET_NOTE);

            AppendOAuthContent(ref data);
            data.content.Add("path", notePath);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void ModifyNote<T>(string notePath, string source, string author, string title, string content, Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_MODIFY_NOTE);
            data.contentType = HTTPContentType.MULTIPART;

            AppendOAuthContent(ref data);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            data.content.Add("path", notePath);
            data.content.Add("source", source);
            data.content.Add("author", author);
            data.content.Add("title", title);
            data.content.Add("content", content);

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }


        public static void MoveNote<T>(string notePath, string targetNotebook, Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_MOVE_NOTE);

            AppendOAuthContent(ref data);
            data.content.Add("path", notePath);
            data.content.Add("notebook", targetNotebook);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }

        public static void PublishNote<T>(string notePath, Action<T> cb)
        {
            var data = new YNoteRequestData();
            data.httpVerb = HTTPVerb.POST;
            data.url = YNoteUtil.GetURL(API_PUBLISH);

            AppendOAuthContent(ref data);
            data.content.Add("path", notePath);

            string http = YNoteOAuthUtil.GetHttpVerbName(data.httpVerb);
            string oauth_token_secret = YNoteUtil.access_token_secret;
            string signature = YNoteOAuthUtil.GenerateOAuthSignature(http, data.url, data.content, YNoteUtil.consumerSecret, oauth_token_secret);
            data.content.Add("oauth_signature", signature); // 签名

            YNoteRequestManager.Instance.ParseRequest(data, cb);
        }
    }
}