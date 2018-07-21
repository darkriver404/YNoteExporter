using System;
using System.Collections.Generic;

namespace YNote.Util
{
    public enum HTTPVerb
    {
        GET, POST
    }

    public class YNoteRequestData
    {
        public HTTPVerb httpVerb { get; set; }

        public string url { get; set; }

        public Dictionary<string, string> content { get; set; }

        public bool needOpenUrl { get; set; }
    }
}