using System;
using System.Collections.Generic;
using System.Text;
using quickDBExplorer;

namespace quickDBExplorer.ICommand
{
    public class CommandArg
    {
        public string DataBaseName{get;private set;}

        public IEnumerable<string> Schema { get; private set; }

        public bool ShowSystemUser { get; private set; }

        public bool ShowView { get; private set; }

        public bool SortOrder { get; private set; }

        public OutputTarget Target { get; private set; }

        public string OutputTargetFileInfo { get; private set; }

        public OutputEncode Encode { get; private set; }

        public string Where { get; private set; }

        public string OrderBy { get; private set; }

        public string Alias { get; private set; }

        public bool ShowGrid { get; private set; }

        public int MaxDataCount { get; private set; }

        public IEnumerable<DBObjectInfo> SelectedObjects { get; private set;}

        public bool ShowFieldAttribute { get; private set; }
    }

    public enum OutputTarget
    {
        OUTPUT_CLIPBOARD,
        OUTPUT_SINGLEFILE,
        OUTPUT_MULTIFILE
    }

    public enum OutputEncode
    {
        ENCODE_SJIS,
        ENCODE_UNICODE,
        ENCODE_UTF8
    }
}
