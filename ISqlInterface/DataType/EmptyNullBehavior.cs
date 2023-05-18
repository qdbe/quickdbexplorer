using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace quickDBExplorer.DataType
{
    /// <summary>
    /// 空文字とNULLの扱い
    /// </summary>
    public enum EmptyNullBehavior
    {
        /// <summary>
        /// 変換なし
        /// </summary>
        NoConv = 0,
        /// <summary>
        /// NULLとして設定
        /// </summary>
        AsNULL = 1,
        /// <summary>
        /// 空文字として設定
        /// </summary>
        AsEmpty = 2
    }
}
