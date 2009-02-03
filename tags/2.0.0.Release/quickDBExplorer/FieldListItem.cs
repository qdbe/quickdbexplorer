using System;

namespace quickDBExplorer
{
	/// <summary>
	/// FieldListに表示する項目の詳細
	/// </summary>
	public class FieldListItem
	{
		// 表示文字列
		private string dispText = "";
		// 関連オブジェクト
		private object pBackObj = null;

		/// <summary>
		/// 関連オブジェクトを返す
		/// </summary>
		public object BackObj
		{
			get { return this.pBackObj; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="text"></param>
		/// <param name="refObj"></param>
		public FieldListItem(string text, object refObj)
		{
			this.dispText = text;
			this.pBackObj = refObj;
		}

		/// <summary>
		/// 通常は表示文字のみを返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.dispText;
		}
	}
}
