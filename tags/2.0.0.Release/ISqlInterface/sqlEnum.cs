using System;

namespace quickDBExplorer
{
	/// <summary>
	/// オブジェクト等を検索する場合の検索条件
	/// </summary>
	public enum	SearchType
	{
		/// <summary>
		/// 曖昧検索(含めばよい)
		/// </summary>
		SearchContain = 0,
		/// <summary>
		/// 前方一致
		/// </summary>
		SearchStartWith,
		/// <summary>
		/// 完全一致
		/// </summary>
		SearchExact
	}
}
