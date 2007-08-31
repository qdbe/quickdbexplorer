using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// SQLのバージョンによる実装の違いからくるSQL文などを生成する為のクラス
	/// ここでは Interface として定義し、実装は個別に行う
	/// </summary>
	public interface ISqlInterface
	{
		/// <summary>
		/// SQLServerに対するコネクション情報を管理する
		/// </summary>
		/// <param name="sqlConnection1"></param>
		void SetConnection(SqlConnection sqlConnection1);

		/// <summary>
		/// DBの一覧表示を取得するSQL文を返す
		/// </summary>
		/// <returns></returns>
		string GetDBSelect();

		/// <summary>
		/// DBオーナーのラベルを返す
		/// </summary>
		/// <returns></returns>
		string GetOwnerLabel1();
		/// <summary>
		/// ラジオボタンのラベルを返す
		/// </summary>
		/// <returns></returns>
		string GetOwnerLabel2();
		/// <summary>
		/// フィールド一覧の取得用SQL文を返す
		/// </summary>
		/// <param name="tbname"></param>
		/// <param name="tbnamelist"></param>
		/// <returns></returns>
		string GetFieldListSelect(string tbname, string []tbnamelist);
	}
}
