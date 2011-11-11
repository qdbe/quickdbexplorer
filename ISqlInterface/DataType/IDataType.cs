using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace quickDBExplorer.DataType
{
    /// <summary>
    /// SQLデータタイプ
    /// </summary>
    public interface IDataType
    {
        // DB データ型と .NET 型のマッピングは            
        // http://msdn.microsoft.com/ja-jp/library/cc716729.aspx
        // を参考に

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
        string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo);

        /// <summary>
        /// データ受け入れ用のチェック
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        string CheckForInput(string data, DBFieldInfo fieldInfo);
    }
}
