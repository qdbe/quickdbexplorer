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
		/// テーブル一覧のカラムヘッダの表示文字を取得する
		/// </summary>
		/// <returns></returns>
		string GetTbListColName();

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
		/// <param name="dboInfo">オブジェクト情報</param>
		/// <returns></returns>
		string GetFieldListSelect(DBObjectInfo dboInfo);

		/// <summary>
		/// オブジェクト一覧の表示用SQLの取得
		/// </summary>
		/// <param name="isDspView">View を表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="ownerList">特定のOwnerのテーブルのみ表示する場合は IN句に利用するカンマ区切り文字列を渡す</param>
		/// <returns></returns>
		string GetDspObjList(bool isDspTable, bool isDspView, bool Synonym, bool isDspFunc, bool isDspSP, string ownerList);

		string GetDspFldListDummy();

		string	GetOwnerList(bool isDspSysUser);

		string	GetDDLCreateStr(DBObjectInfo dboInfo, bool usekakko);

		string	GetDDLDropStr(DBObjectInfo dboInfo);

		//string GetDspFldList();
	}
}
