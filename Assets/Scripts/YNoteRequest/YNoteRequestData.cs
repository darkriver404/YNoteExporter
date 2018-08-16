using System.Collections.Generic;
using OrgDay.Util;

namespace YNote.Util
{
    public enum HTTPVerb
    {
        GET, POST
    }

    public enum HTTPContentType
    {
        DEFAULT, APPLICATION, MULTIPART
    }

    public class YNoteRequestData
    {
        public HTTPVerb httpVerb { get; set; }

        public string url { get; set; }

        public bool needOpenUrl { get; set; }

        public HTTPContentType contentType { get; set; }

        public Dictionary<string, string> content { get; set; }

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "httpVerb", httpVerb,
                "url", url,
                "needOpenUrl", needOpenUrl,
                "contentType", contentType,
                "content", StringUtil.GetString(content));
        }
    }
}