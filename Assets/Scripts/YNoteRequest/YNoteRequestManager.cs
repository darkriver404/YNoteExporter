using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using OrgDay.Util;
using YNote.Util;

public class YNoteRequestManager : MonoBehaviour
{
    public static YNoteRequestManager Instance;

    public Action<YNoteResponseData> defaultCB;

    private void Awake()
    {
        Instance = this;
    }

    public void ParseRequest<T>(YNoteRequestData data, Action<T> cb)
    {
        if (data.needOpenUrl)
        {
            Application.OpenURL(data.url);
        }
        StartCoroutine(SendRequest(data, cb));
    }

    public IEnumerator SendRequest<T>(YNoteRequestData data, Action<T> cb)
    {
        Log.send(data.ToString());
        Log.SaveToFile("send", data.ToString());

        UnityWebRequest request;
        switch (data.httpVerb)
        {
            default:
            case HTTPVerb.GET:
                request = UnityWebRequest.Get(data.url);
                break;
            case HTTPVerb.POST:
                {
                    switch(data.contentType)
                    {
                        default:
                        case HTTPContentType.DEFAULT:
                        case HTTPContentType.APPLICATION:
                            request = UnityWebRequest.Post(data.url, data.content);
                            break;
                        case HTTPContentType.MULTIPART:
                            {
                                List<IMultipartFormSection> multipartFormSections = new List<IMultipartFormSection>();
                                foreach (var kvp in data.content)
                                {
                                    multipartFormSections.Add(new MultipartFormDataSection(kvp.Key, kvp.Value, YNoteOAuthUtil.GetHttpContentType(data.contentType)));
                                }
                                request = UnityWebRequest.Post(data.url, multipartFormSections);
                                request.uploadHandler.contentType = YNoteOAuthUtil.GetHttpContentType(data.contentType);
                            }
                            break;
                    }
                }
                break;
        }
        yield return request.Send();

        YNoteResponseData response = new YNoteResponseData();
        response.errorMsg = request.error;
        response.httpResponseCode = request.responseCode;

        if (request.isNetworkError)
        {
            response.type = ResponseType.Error;
            Log.e("NetworkError", request.error);
        }
        else
        {
            response.type = ResponseType.Success;
            
            Log.d("HttpCode", request.responseCode);
            string text = request.downloadHandler.text;
            byte[] results = request.downloadHandler.data;
            response.text = text;

            switch (request.responseCode)
            {
                case 200:
                    {
                        if(StringUtil.IsHtmlStrSimple(text))
                        {
                            Log.w("recv", "is html!");
                            break;
                        }
                        if(StringUtil.IsOAuthStrSimple(text))
                        {
                            text = StringUtil.OAuthStr2JsonStr(text);
                        }
                        if (StringUtil.IsJsonStrSimple(text))
                        {
                            T result = JsonConvert.DeserializeObject<T>(text);
                            if (cb != null)
                            {
                                cb(result);
                            }
                        }
                    }
                    break;
            }
        }

        Log.recv(response.ToString());
        Log.SaveToFile("recv", response.ToString());

        if (defaultCB != null)
        {
            defaultCB(response);
        }
    }
}
