using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using OrgDay.Util;

public class YNoteRequestManager : MonoBehaviour
{
    public static YNoteRequestManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SendRequest(YNoteRequestData data)
    {
        StartCoroutine(YieldSendRequest(data));
    }

    public IEnumerator YieldSendRequest(YNoteRequestData data)
    {
        if (data.needOpenUrl)
        {
            Application.OpenURL(data.url);
        }
        Log.send(data.url, data.content);
        UnityWebRequest www;
        switch (data.httpVerb)
        {
            default:
            case HTTPVerb.GET:
                www = UnityWebRequest.Get(data.url);
                break;
            case HTTPVerb.POST:
                www = UnityWebRequest.Post(data.url, data.content);
                break;
        }
        yield return www.Send();

        string resultContent = string.Empty;
        if (!www.isNetworkError)
        {
            resultContent = www.downloadHandler.text;
        }
        Log.recv(resultContent);
        if (data.cb != null)
        {
            data.cb(resultContent);
        }
    }
}
