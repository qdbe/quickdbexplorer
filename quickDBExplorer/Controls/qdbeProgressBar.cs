using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer.Controls
{

    /// <summary>
    /// プログレスバー
    /// </summary>
    public partial class qdbeProgressBar : UserControl
    {
        /// <summary>
        /// 最大値
        /// </summary>
        public int Maximum { get; set; }

        /// <summary>
        /// 最小値
        /// </summary>
        public int Minimum { get { return 0; } }

        /// <summary>
        /// 現在値
        /// </summary>
        protected int _value;

        /// <summary>
        /// 現在値
        /// </summary>
        public int Value { 
            get { return _value; }
            set { 
                _value = value;
                this.Invalidate();
            } 
        }

        /// <summary>
        /// バーの色
        /// </summary>
        public Color BarColor { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public qdbeProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// リサイズ時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Maximum == 0)
            {
                return;
            }

            Graphics g = e.Graphics;
            double percent = (double)this._value / this.Maximum;
            Rectangle rect = this.ClientRectangle;

            rect.Width = (int)((double)rect.Width * percent);

            SolidBrush brush = new SolidBrush(BarColor);
            g.SetClip(e.ClipRectangle);
            g.FillRectangle(brush, 0, 0, rect.Width, this.Height);

            brush.Dispose();
            g.Dispose();
        }
    }
}
