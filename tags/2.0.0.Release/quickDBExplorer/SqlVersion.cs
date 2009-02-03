using System;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlVersion の概要の説明です。
	/// </summary>
	/// 
	public class SqlVersion
	{

		/// <summary>
		/// バージョン番号をあらわす文字列
		/// </summary>
		private string pFullVersionString;
		/// <summary>
		/// バージョン番号をあらわす文字列
		/// </summary>
		public string FullVersionString
		{
			get { return this.pFullVersionString; }
		}

		/// <summary>
		/// 2000,2005,2008 等
		/// </summary>
		private int pPublicVersion;

		/// <summary>
		/// 製品名につくバージョン（2000,2005,2008等)
		/// </summary>
		public int PublicVersionNo
		{
			get { return this.pPublicVersion; }
		}

		/// <summary>
		/// 接続用DLLにつく名称
		/// </summary>
		public string AdapterNameString
		{
			get { return this.pPublicVersion.ToString(); }
		}

		/// <summary>
		/// 該当バーージョンで Synonym を利用できるか
		/// </summary>
		private bool pIsSynonym = true;

		/// <summary>
		/// 該当バーージョンで Synonym を利用できるか
		/// </summary>
		public bool CanUseSynonym
		{
			get { return this.pIsSynonym; }
		}

		/// <summary>
		/// クエリアナライザーを利用可能か否か
		/// </summary>
		private bool pCanUseQueryAnalyzer = true;
		/// <summary>
		/// クエリアナライザーを利用可能か否か
		/// </summary>
		public bool CanUseQueryAnalyzer
		{
			get { return this.pCanUseQueryAnalyzer; }
		}

		/// <summary>
		/// Management Studio と呼ぶか否か
		/// </summary>
		private bool pIsManagementStudio = false;

		/// <summary>
		/// Management Studio と呼ぶか否か
		/// </summary>
		public bool IsManagementStudio
		{
			get { return pIsManagementStudio; }
			set { pIsManagementStudio = value; }
		}


		/// <summary>
		/// SQL Server 2000 を表すインスタンスを生成する
		/// </summary>
		/// <returns></returns>
		public static SqlVersion SQLSERVER2000()
		{
			return new SqlVersion("08.00.2039");
		}

		/// <summary>
		/// SQL Server 2005 を表すインスタンスを生成する
		/// </summary>
		public static SqlVersion SQLSERVER2005()
		{
			return new SqlVersion("09.00.3054");
		}

		/// <summary>
		/// SQL Server 2008 を表すインスタンスを生成する
		/// </summary>
		public static SqlVersion SQLSERVER2008()
		{
			return new SqlVersion("10.");
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="versionStr">Connection.ServerVersion の結果を渡す</param>
		public SqlVersion(string versionStr)
		{
			// Connection.ServerVersion の結果が渡されるので、ここで判断する
			if(versionStr.StartsWith("08") )
			{
				// SQL Server 2000
				this.pPublicVersion = 2000;
				this.pFullVersionString = versionStr;
				this.pIsSynonym = false;
				this.pCanUseQueryAnalyzer = true;
				this.pIsManagementStudio = false;
			}
			else if(versionStr.StartsWith("09") )
			{
				this.pPublicVersion = 2005;
				this.pFullVersionString = versionStr;
				this.pIsSynonym = true;
				this.pCanUseQueryAnalyzer = false;
				this.pIsManagementStudio = true;
			}
			else
			{
				// それ以外は2008と同等としてみなそう
				this.pPublicVersion = 2008;
				this.pFullVersionString = versionStr;
				this.pIsSynonym = true;
				this.pCanUseQueryAnalyzer = false;
				this.pIsManagementStudio = true;
			}
		}
	}
}
