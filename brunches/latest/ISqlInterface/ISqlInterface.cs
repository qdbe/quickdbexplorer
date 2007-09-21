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
		void SetConnection(IDbConnection sqlConnection);

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

		/// <summary>
		/// フィールドリスト取得時のダミークエリを生成する
		/// </summary>
		/// <returns></returns>
		string GetDspFldListDummy();

		/// <summary>
		/// Owner の一覧を取得するSQLを生成する
		/// </summary>
		/// <param name="isDspSysUser"></param>
		/// <returns></returns>
		string	GetOwnerList(bool isDspSysUser);

		/// <summary>
		/// オブジェクトに対する Create 文を生成する
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <param name="usekakko"></param>
		/// <returns></returns>
		string	GetDDLCreateStr(DBObjectInfo dboInfo, bool usekakko);

		/// <summary>
		/// オブジェクトに対するDROP 文を生成する
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <returns></returns>
		string	GetDDLDropStr(DBObjectInfo dboInfo);

		/// <summary>
		/// オブジェクト情報をセットするDataTableを初期化する
		/// </summary>
		/// <param name="dt"></param>
		void	InitObjTable(DataTable dt);

		/// <summary>
		/// オブジェクトの情報を、DataTable に追加する
		/// </summary>
		/// <param name="dboInfo">対象となるオブジェクト</param>
		/// <param name="dt"></param>
		void	AddObjectInfo(DBObjectInfo dboInfo, DataTable dt);
	}
}
