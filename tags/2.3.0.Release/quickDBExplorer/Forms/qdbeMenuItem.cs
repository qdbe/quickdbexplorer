using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer.Forms
{
    /// <summary>
    /// 右クリック用＆ボタンクリック時のメニューの情報を管理する
    /// </summary>
    class qdbeMenuItem
    {
        private bool isSeparater = false;
        /// <summary>
        /// セパレーターか否か
        /// </summary>
        public bool IsSeparater
        {
            get { return this.isSeparater; }
        }
        private bool isObjTarget = true;
        /// <summary>
        /// オブジェクトを対象に処理を行う動作か否か
        /// </summary>
        public bool IsObjTarget
        {
            get { return this.isObjTarget; }
        }
        private string callBtnName;
        /// <summary>
        /// ボタンからの呼び出しがある場合、そのボタンのコントロール名称
        /// </summary>
        public string CallBtnName
        {
            get { return this.callBtnName; }
        }
        /// <summary>
        /// メニューとして表示する文字列
        /// </summary>
        private string menuName = "";
        /// <summary>
        /// クリック時に呼び出されるイベントハンドラ
        /// </summary>
        private System.EventHandler clickHandler;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="iss">セパレーターか否か true: セパレーター false: 通常メニュー</param>
        /// <param name="isot">オブジェクトを対象に処理を行う動作か否か</param>
        /// <param name="btn">ボタンからの呼び出しがある場合、そのボタンのコントロール名称</param>
        /// <param name="text">メニューとして表示する文字列</param>
        /// <param name="clickEv">クリック時に呼び出されるイベントハンドラ</param>
        public qdbeMenuItem(bool iss, bool isot, string btn, string text, System.EventHandler clickEv)
        {
            this.isSeparater = iss;
            this.isObjTarget = isot;
            this.callBtnName = btn;
            this.menuName = text;
            this.clickHandler = clickEv;
        }

        /// <summary>
        /// メニューITEMを作成する
        /// </summary>
        /// <param name="index">メニューの中でのINDEX</param>
        /// <param name="shortCutNo">0: ショートカットキーはつけない 1以上: 指定された数値でショートカットキーを設定する</param>
        /// <returns></returns>
        public MenuItem CreateMenuItem(int index, int shortCutNo)
        {
            MenuItem it = new MenuItem();
            it.Index = index;

            if (this.isSeparater == true)
            {
                it.Text = "-";
            }
            else
            {
                if (shortCutNo > 0)
                {
                    it.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "(&{0}) {1}",
                        shortCutNo.ToString("x", System.Globalization.CultureInfo.CurrentCulture), this.menuName);
                }
                else
                {
                    it.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}",
                        this.menuName);
                }
                it.Click += this.clickHandler;
            }
            return it;
        }
    }
}
