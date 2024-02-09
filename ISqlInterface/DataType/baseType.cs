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
        /// 型が存在するか否かをチェックする場合の文字列
        /// </summary>
        public string DefalutParseString { get; protected set; }

        /// <summary>
        /// C# の型を返す
        /// </summary>
        public abstract Type Type{ get; }

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
            this.DefalutParseString = "1";
        }

        /// <summary>
        /// バイナリか否か
        /// </summary>
        public virtual bool IsBinary
        {
            get { return false; }
        }

        /// <summary>
        /// 文字データを読み込むことができるか否か
        /// </summary>
        /// <returns></returns>
        public abstract bool CanLoadData();

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
        /// EXCEL出力用のカラムの方情報を出力する
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="length"></param>
        /// <param name="prec"></param>
        /// <param name="xscale"></param>
        /// <param name="isComma"></param>
        /// <returns></returns>
        public abstract string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma);

        /// <summary>
        /// 区切り文字を出力する（カンマ、タブ）
        /// </summary>
        /// <param name="isComma"></param>
        /// <returns></returns>
        public virtual string GetSeparator(bool isComma)
        {
            if (isComma)
            {
                return ",";
            }
            else
            {
                return "\t";
            }
        }

        /// <summary>
        /// C# の型名を返す
        /// </summary>
        /// <returns></returns>
        public virtual string GetCSharpTypeString()
        {
            return this.Type.Name;
        }

        //public abstract bool CanBeNullable();


        /// <summary>
        /// データ受け入れ用のチェック
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="isEmptyAsNull"></param>
        /// <param name="result"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public abstract bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg);

        /// <summary>
        /// 文字列をパースする
        /// </summary>
        /// <param name="data"></param>
        /// <param name="t"></param>
        /// <param name="defaultErrorMessage"></param>
        /// <param name="isEmptyAsNull"></param>
        /// <param name="result"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        protected bool TryParse(string data, Type t, string defaultErrorMessage, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            errmsg = null;
            try
            {
                if (string.IsNullOrEmpty(data.Trim()))
                {
                    if (isEmptyAsNull == 0)
                    {
                        result = DBNull.Value;
                    }
                    else if( result is string )
                    {
                        result = string.Empty;
                    }
                    else
                    {
                        errmsg = defaultErrorMessage;
                        return false;
                    }
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

        /// <summary>
        /// 値が正しいかどうかチェックする
        /// </summary>
        /// <param name="val"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="isEmptyAsNull"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public virtual bool CheckValue(object val, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, out string errmsg)
        {
            errmsg = "";
            if (val != null && val != DBNull.Value)
            {
                if (val.GetType() != this.Type)
                {
                    errmsg = "型が違います";
                    return false;
                }
            }
            bool result = this.TryCheckObject(val, fieldInfo, isEmptyAsNull, ref errmsg);

            return result;
        }

        /// <summary>
        /// 値をチェックする
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="isEmptyAsNull"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public virtual bool TryCheckObject(object data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref string errmsg)
        {
            object result = null;
            return TryParse(data.ToString(), fieldInfo, isEmptyAsNull, ref result, ref errmsg);
        }
    }
}
