using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.Forms
{
    /// <summary>
    /// 処理開始時の情報を記憶する為のクラス
    /// </summary>
    public class ProcCondition
    {
        /// <summary>
        /// 出力先の指定
        /// </summary>
        public int OutputDestination { get; set; }

        /// <summary>
        /// 出力先ファイルの指定
        /// </summary>
        public string OutputFile { get; set; }

        /// <summary>
        /// 文字エンコーディングの指定
        /// </summary>
        public System.Text.Encoding Encoding { get; set; }

        /// <summary>
        /// 選択されたオブジェクトの一覧
        /// </summary>
        private List<string> pObjects;

        /// <summary>
        /// 選択されたオブジェクトの一覧
        /// </summary>
        public IEnumerable<string> TargetObjects
        {
            get { return pObjects; }
        }

        /// <summary>
        /// 選択されたオブジェクトの一覧に追加する
        /// </summary>
        /// <param name="tbname"></param>
        public void AddObject(string tbname)
        {
            this.pObjects.Add(tbname);
        }

        /// <summary>
        /// where 句の指定
        /// </summary>
        private string pWhereStr;
        /// <summary>
        /// where 句の指定
        /// </summary>
        public string WhereStr
        {
            get { return this.pWhereStr; }
            set
            {
                if (value != null)
                {
                    this.pWhereStr = value.Trim();
                }
                else
                {
                    this.pWhereStr = value;
                }
            }
        }
        /// <summary>
        /// Order by 句の指定
        /// </summary>
        private string pOrderStr;
        /// <summary>
        /// Order by 句の指定
        /// </summary>
        public string OrderStr
        {
            get { return this.pOrderStr; }
            set
            {
                if (value != null)
                {
                    this.pOrderStr = value.Trim();
                }
                else
                {
                    this.pOrderStr = value;
                }
            }
        }

        /// <summary>
        /// 別名の設定
        /// </summary>
        public string AliasStr { get; set; }

        /// <summary>
        /// 最大件数の指定
        /// </summary>
        public string MaxStr { get; set; }

        /// <summary>
        /// 最大件数の獲得
        /// 空白の場合は0を返す
        /// </summary>
        public int MaxCount
        {
            get
            {
                if (this.MaxStr == null ||
                    this.MaxStr == string.Empty)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(this.MaxStr, System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }
        /// <summary>
        /// グリッドにデータを表示するか否かの指定
        /// </summary>
        public bool IsDisp { get; set; }

        /// <summary>
        /// グリッドにデータを全て表示するか否かの指定
        /// </summary>
        public bool IsAllDisp { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProcCondition()
        {
            this.AliasStr = string.Empty;
            this.Encoding = System.Text.Encoding.Unicode;
            this.IsAllDisp = false;
            this.IsDisp = true;
            this.MaxStr = string.Empty;
            this.OrderStr = string.Empty;
            this.OutputDestination = 1;
            this.OutputFile = string.Empty;
            this.pObjects = new List<string>();
            this.WhereStr = string.Empty;
        }
    }
}
