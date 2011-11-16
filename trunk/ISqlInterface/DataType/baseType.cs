using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace quickDBExplorer.DataType
{
    /// <summary>
    /// SQLデータタイプ
    /// </summary>
    public abstract class baseType : IDataType
    {
        // DB データ型と .NET 型のマッピングは            
        // http://msdn.microsoft.com/ja-jp/library/cc716729.aspx
        // を参考に

        /// <summary>
        /// 型の表現に長さを含むか否か
        /// </summary>
        protected bool TypeHasSize
        {
            get;
            set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public baseType()
        {
            this.TypeHasSize = false;
        }

        /// <summary>
        /// フィールドのデータを出力用に変換する
        /// </summary>
        /// <param name="dr">データリーダー</param>
        /// <param name="col">カラム</param>
        /// <param name="addstr"></param>
        /// <param name="unichar"></param>
        /// <param name="outNull"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public abstract string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo);

        /// <summary>
        /// フィールドリストに表示するための文字列を取得する
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="length"></param>
        /// <param name="prec"></param>
        /// <param name="xscale"></param>
        /// <returns></returns>
        public virtual string GetFieldTypeString(string typename, int length, int prec, int xscale)
        {
            return typename;
        }

        /// <summary>
        /// データ受け入れ用のチェック
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="result"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public abstract bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg);

        /// <summary>
        /// 文字列をパースする
        /// </summary>
        /// <param name="data"></param>
        /// <param name="t"></param>
        /// <param name="defaultErrorMessage"></param>
        /// <param name="result"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        protected bool TryParse(string data, Type t, string defaultErrorMessage, ref object result, ref string errmsg)
        {
            errmsg = null;
            try
            {
                if (string.IsNullOrEmpty(data.Trim()))
                {
                    result = DBNull.Value;
                    return true;
                }
                else
                {
                    object localresult = null;
                    localresult = t.InvokeMember("Parse", BindingFlags.InvokeMethod, 
                        null, 
                        null,
                        new object[] { data } );
                    if( localresult != null )
                    {
                        result = localresult;
                        return true;
                    }
                    else
                    {
                        errmsg = defaultErrorMessage;
                        return false;
                    }
                }
            }
            catch
            {
                errmsg = defaultErrorMessage;
            }
            return false;
        }
    }
}
