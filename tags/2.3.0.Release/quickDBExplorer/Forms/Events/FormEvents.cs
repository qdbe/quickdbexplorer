using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.Forms.Events
{
    /// <summary>
    /// ログイン実行時のイベント
    /// </summary>
    /// <param name="conInfo"></param>
    internal delegate void LoginConnectedHandler(ConnectionInfo conInfo);

    /// <summary>
    /// 変換
    /// </summary>
    internal delegate void SaveBookMarkHandler(BookmarkInfo bookmarkInfo);

    internal delegate void LoadBookMarkHandler(BookmarkInfo bookmarkInfo);
}
