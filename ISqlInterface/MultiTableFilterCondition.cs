using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace quickDBExplorer
{
    [Obsolete("no uses",true)]
    /// <summary>
    /// オブジェクト検索の条件を管理
    /// </summary>
    public class MultiTableFilterCondition
    {
        /// <summary>
        /// フィールド名を検索するか否か
        /// </summary>
        public bool IsSearchField
        {
            get;
            set;
        }

        /// <summary>
        /// テーブル名を検索するか否か
        /// </summary>
        public bool IsSearchTable
        {
            get;
            set;
        }

        /// <summary>
        /// View名を検索するか否か
        /// </summary>
        public bool IsSearchView
        {
            get;
            set;
        }

        /// <summary>
        /// Synonym名を検索するか否か
        /// </summary>
        public bool IsSearchSynonym
        {
            get;
            set;
        }

        /// <summary>
        /// ストアドプロシージャーを検索するか否か
        /// </summary>
        public bool IsSearchProcedure
        {
            get;
            set;
        }

        /// <summary>
        /// 関数を検索するか否か
        /// </summary>
        public bool IsSearchFunction
        {
            get;
            set;
        }

        /// <summary>
        /// フィールド検索にテーブルを対象に含めるか
        /// </summary>
        public bool IsFieldTable
        {
            get;
            set;
        }
        /// <summary>
        /// フィールド検索にVIEWを対象に含めるか
        /// </summary>
        public bool IsFieldView
        {
            get;
            set;
        }

        /// <summary>
        /// フィールド検索にSynonymを対象に含めるか
        /// </summary>
        public bool IsFieldSynonym
        {
            get;
            set;
        }


        /// <summary>
        /// 検索結果を元にオブジェクトを選択するか否か
        /// </summary>
        public bool IsShowTableSelect
        {
            get;
            set;
        }


        /// <summary>
        /// 検索結果を元にオブジェクトを絞り込むか否か
        /// </summary>
        public bool IsTableFilter
        {
            get;
            set;
        }

        /// <summary>
        /// 大文字・小文字を区別するか否か
        /// </summary>
        public bool IsCaseSensitive
        {
            get;
            set;
        }

        /// <summary>
        /// 現在表示中のスキーマのみ対象とするか否か
        /// </summary>
        public bool IsSchemaOnly
        {
            get;
            set;
        }

        public string ExcludeField{get;set;}

        public string ExcludeFieldName { get; set; }


        public string ExcludeObjName { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MultiTableFilterCondition()
        {
            this.IsCaseSensitive = true;
            this.ExcludeField = "";
            this.ExcludeFieldName = "";
            this.ExcludeObjName = "";
        }
    }
}
