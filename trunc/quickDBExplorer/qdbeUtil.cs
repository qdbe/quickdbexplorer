using System;
using System.Data;

namespace quickDBExplorer
{
	/// <summary>
	/// 各種ユーティリティ関数
	/// Static メソッドのみ保持している
	/// </summary>
	public sealed class qdbeUtil
	{

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private qdbeUtil()
		{
		}

		/// <summary>
		/// テーブル名を解析し、[owner]、[tablename]に分割する
		/// 基の名前は[]が付いていないことが前提
		/// </summary>
		/// <param name="tbname">テーブル名(owner.tablename形式)</param>
		/// <returns>[owner]と[tablename]に分割された文字配列</returns>
		public static string[] SplitTbname(string tbname)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			string []retstr = new string[2];
			retstr[0] = string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}]",str[0]);
			retstr[1] = string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}]",str[1]);
			return retstr;
		}

		/// <summary>
		/// テーブル名を解析し、[owner].[tablename]形式に変更する
		/// 基の名前は[]が付いていないことが前提
		/// </summary>
		/// <param name="tbname">テーブル名(owner.tablename形式)</param>
		/// <returns></returns>
		public static string GetTbname(string tbname)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}]",str[0],str[1]);
		}

		/// <summary>
		/// テーブル名を解析し、[owner].[tablename]形式に変更する。
		/// その際、指定された文字列をテーブル名称に付加した結果を返す
		/// 基の名前は[]が付いていないことが前提
		/// </summary>
		/// <param name="tbname">テーブル名(owner.tablename形式)</param>
		/// <param name="addstr">テーブル名に付加する文字列</param>
		/// <returns>解析後のテーブル名([owner].[tabblname]形式)</returns>
		public static string GetTbnameAdd(string tbname,string addstr)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}_{2}]",str[0], str[1], addstr);
		}

		/// <summary>
		/// 各種履歴情報への項目の追加を行う
		/// </summary>
		/// <param name="key">テーブル名称</param>
		/// <param name="hvalue">追加する履歴項目</param>
		/// <param name="tdata">追加する先の履歴情報</param>
		static public void SetNewHistory(string key, string hvalue, TextHistoryDataSet tdata)
		{
			if( hvalue == null || hvalue == "" )
			{
				return;
			}

			DataRow []drl = tdata.TextHistoryDataSets.Select( string.Format(System.Globalization.CultureInfo.CurrentCulture,"KeyValue = '{0}'", key ) );

			TextHistoryDataSet.TextHistoryDataSetsRow dr = null;
			int maxKeyNo = 0;
			for( int i = 0; i < tdata.TextHistoryDataSets.Rows.Count; i++ )
			{
				dr = tdata.TextHistoryDataSets[i];
				if( dr.KeyValue == key &&
					dr.DataValue == hvalue )
				{
					// 同じテーブルに対し、既に同じ履歴が登録されているため、キーNOだけ新しくするため、
					// 過去のデータは一旦削除し新しく追加しなおす
					dr.Delete();
					continue;
				}
				if( dr.IsKeyNoNull() == true )
				{
					dr.KeyNo = i;
				}
				if( maxKeyNo < dr.KeyNo )
				{
					maxKeyNo = dr.KeyNo;
				}
			}
			maxKeyNo++;
			TextHistoryDataSet.TextHistoryDataSetsRow ndr = tdata.TextHistoryDataSets.NewTextHistoryDataSetsRow();
			ndr.KeyValue = key;
			ndr.DataValue = hvalue;
			ndr.KeyNo = maxKeyNo;
			tdata.TextHistoryDataSets.Rows.Add(ndr);
			tdata.TextHistoryDataSets.AcceptChanges();
		}

	}
}
