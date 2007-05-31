using System;


namespace serialFactory
{
	/// <summary>
	/// ユーザー名を暗号化/複合化する
	/// </summary>
	public class UserNameFactory : StrEncoder
	{
		public UserNameFactory()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		public const int	MinUserLen = 3;	// ユーザー名(最低1文字) + チェックサム
	}
}
