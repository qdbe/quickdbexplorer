using System;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
    /// <summary>
    /// プレースホルダーを表示するテキストボックス
    /// </summary>
    public partial class PlaceholderTextBox : TextBox
    {
        /// <summary>
        ///  コンストラクタ
        /// </summary>
        public PlaceholderTextBox()
        {
            this._placeholderText = "";
            this._placeholderColor = Color.Empty;

            this.Leave += PlaceholderTextBox_Leave;
        }

        private void PlaceholderTextBox_Leave(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        #region プロパティ


        /// <summary>
        /// プレースホルダーのテキスト
        /// </summary>
        protected string _placeholderText;
        /// <summary>
        /// プレースホルダー文字列
        /// </summary>
        public string PlaceholderText
        {
            get
            {
                return this._placeholderText;
            }
            set
            {
                this._placeholderText = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// プレースホルダーの色
        /// </summary>
        private Color _placeholderColor;
        /// <summary>
        /// プレースホルダーの色
        /// </summary>
        public Color PlaceholderColor
        {
            get
            {
                return this._placeholderColor;
            }
            set
            {
                this._placeholderColor = value;
                this.Refresh();
            }
        }

        #endregion

        // ペイントを表す定数
        private const int WM_PAINT = 0x000f; // 15

        /// <summary>
        /// WndProcMethodのオーバーライド
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_PAINT)
            {
                if (this.Enabled &&
                    !this.ReadOnly &&
                    !this.Focused &&
                    string.IsNullOrEmpty(this.Text) &&
                    !string.IsNullOrEmpty(this._placeholderText))
                {
                    // テキストボックスが入力可能でフォーカスされていない状態

                    using (Graphics graphics = this.CreateGraphics())
                    {
                        // 描画をいったん消去（背景色で塗りつぶす）
                        Brush brush = new SolidBrush(this.BackColor);
                        graphics.FillRectangle(brush, this.ClientRectangle);

                        // プレースホルダーの色を取得
                        Color placeholderColor = CreateNeutralColor();
                        if (!this._placeholderColor.IsEmpty)
                        {
                            placeholderColor = this._placeholderColor;
                        }

                        // プレースホルダーのテキストを描画する
                        graphics.DrawString(_placeholderText, this.Font, new SolidBrush(placeholderColor), 1, 1);
                    }
                }
            }
        }

        // 前景色と背景色の中間色を作成
        private Color CreateNeutralColor()
        {
            Color color = Color.FromArgb(
                (this.ForeColor.A >> 1 + this.BackColor.A >> 1),
                (this.ForeColor.R >> 1 + this.BackColor.R >> 1),
                (this.ForeColor.G >> 1 + this.BackColor.G >> 1),
                (this.ForeColor.B >> 1 + this.BackColor.B >> 1));
            return color;
        }
    }
}