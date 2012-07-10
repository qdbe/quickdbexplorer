using System;
using System.Collections.Generic;
using System.Text;
using quickDBExplorer;

namespace quickDBExplorer.ICommand
{
    /// <summary>
    /// プラグイン用引数(画面条件)
    /// </summary>
    public class CommandArg
    {
        /// <summary>
        /// データベース名
        /// </summary>
        public string DataBaseName{get;private set;}

        private List<string> pSchema { get; set; }
        /// <summary>
        /// 選択状態のスキーマ
        /// </summary>
        public IEnumerable<string> Schema { get { return this.pSchema; } }

        /// <summary>
        /// システムユーザーを表示するか否か
        /// </summary>
        public bool ShowSystemUser { get; private set; }

        /// <summary>
        /// Viewを表示するか否か
        /// </summary>
        public bool ShowView { get; private set; }

        /// <summary>
        /// オブジェクトの表示順
        /// </summary>
        public bool SortOrder { get; private set; }

        /// <summary>
        /// 出力先
        /// </summary>
        public OutputTarget Target { get; private set; }

        /// <summary>
        /// 出力先ファイル情報
        /// </summary>
        public string OutputTargetFileInfo { get; private set; }

        /// <summary>
        /// 出力文字エンコード
        /// </summary>
        public OutputEncode Encode { get; private set; }

        /// <summary>
        /// where 条件
        /// </summary>
        public string Where { get; private set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public string OrderBy { get; private set; }

        /// <summary>
        /// テーブルAlias
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// データを表示するか否か
        /// </summary>
        public bool ShowGrid { get; private set; }

        /// <summary>
        /// データ表示の最大件数
        /// </summary>
        public int MaxDataCount { get; private set; }

        private List<DBObjectInfo> pSelectedObjects { get; set; }
        /// <summary>
        /// 選択状態にあるオブジェクト
        /// </summary>
        public IEnumerable<DBObjectInfo> SelectedObjects { get {return pSelectedObjects;} }

        /// <summary>
        /// 属性を表示するか否か
        /// </summary>
        public bool ShowFieldAttribute { get; private set; }
    }

    /// <summary>
    /// 出力先
    /// </summary>
    public enum OutputTarget
    {
        /// <summary>
        /// クリップボード
        /// </summary>
        OUTPUT_CLIPBOARD,
        /// <summary>
        /// 単一ファイル
        /// </summary>
        OUTPUT_SINGLEFILE,
        /// <summary>
        /// 複数ファイル
        /// </summary>
        OUTPUT_MULTIFILE
    }

    /// <summary>
    /// 文字エンコーディング種類
    /// </summary>
    public enum OutputEncode
    {
        /// <summary>
        /// SHIFTJIS
        /// </summary>
        ENCODE_SJIS,
        /// <summary>
        /// UNICODE(UTF-16LE)
        /// </summary>
        ENCODE_UNICODE,
        /// <summary>
        /// UTF-8(LE)
        /// </summary>
        ENCODE_UTF8
    }
}
