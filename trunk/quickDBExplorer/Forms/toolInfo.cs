using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.Forms
{
    /// <summary>
    /// ツール情報
    /// </summary>
    public class ToolInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// コマンド文字列
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        public ToolInfo(string name)
        {
            this.Name = name;
        }
        public ToolInfo(string name, string command)
        {
            this.Name = name;
            this.Command = command;
        }
    }
}
