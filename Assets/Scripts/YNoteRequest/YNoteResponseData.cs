using OrgDay.Util;

namespace YNote.Util
{
    public enum ResponseType
    {
        Success,
        Error
    }

    public class YNoteResponseData
    {
        public ResponseType type { get; set; }

        public long httpResponseCode { get; set; }

        public string errorMsg { get; set; }
        public string text { get; set; }

        public override string ToString()
        {
            return StringUtil.CombineKVP(
                "result", type,
                "code", httpResponseCode,
                "msg", errorMsg,
                "text", text);
        }
    }
}