using System;
using System.Data;

namespace quickDBExplorer
{
	/// <summary>
	/// 履歴を管理する為のクラス
	/// </summary>
	public class textHistory : DataSet
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public textHistory()
		{
			this.CaseSensitive = true;
			this.Columns.Add("KeyValue");
			this.Columns.Add("DataValue");

			DataColumn[] pkeys = new DataColumn[1];
			pkeys[0] = this.Columns["KeyValue"];

			this.PrimaryKey = pkey;
		}
	}
}
