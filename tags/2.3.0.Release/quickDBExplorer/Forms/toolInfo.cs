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
        /// コマンド引数
        /// </summary>
        public string Args { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        public ToolInfo(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="command"></param>
        /// <param name="args"></param>
        public ToolInfo(string name, string command, string args)
        {
            this.Name = name;
            this.Command = command;
            this.Args = args;
        }

        /// <summary>
        /// 文字列化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
