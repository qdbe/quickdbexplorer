using System;
using System.Data;

namespace quickDBExplorer
{
	/// <summary>
	/// �������Ǘ�����ׂ̃N���X
	/// </summary>
	public class textHistory : DataSet
	{
		/// <summary>
		/// �R���X�g���N�^
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
