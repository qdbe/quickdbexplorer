using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer.Forms
{
    /// <summary>
    /// 処理中の状態を表示する
    /// </summary>
    public partial class ProcessingDialog : quickDBExplorer.quickDBExplorerBaseForm, IDisposable
    {

        /// <summary>
        /// 当ダイアログの呼び出し元
        /// </summary>
        public Form Caller { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="maxVal"></param>
        public ProcessingDialog(int maxVal) : base()
        {
            InitializeComponent();
            this.FormClosing += ProcessingDialog_FormClosing;
            this.MaxVal = maxVal;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProcessingDialog()
        {
            InitializeComponent();
            this.FormClosing += ProcessingDialog_FormClosing;
        }

        private void ProcessingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Caller != null)
            {
                ((MainForm)this.Caller).SetEnable(true);
                if (this.Caller is MainForm)
                {
                    ((MainForm)this.Caller).Activate();
                }
            }

        }

        /// <summary>
        /// 現在値
        /// </summary>
        protected int _curVal;
        /// <summary>
        /// 現在値
        /// </summary>
        public int CurVal
        {
            get
            {
                return _curVal;
            }
            set
            {
                _curVal = value;
                if (this.txtCurVal != null)
                {
                    this.txtCurVal.Text = _curVal.ToString();
                    if( this.progressBar1.Minimum <= value &&
                        this.progressBar1.Maximum >= value 
                        )
                    {
                        this.progressBar1.Value = value;
                        Application.DoEvents();
                    }
                }

            }
        }

        /// <summary>
        /// 進行の最大値
        /// </summary>
        protected string _curTarget;
        /// <summary>
        /// 進行の最大値
        /// </summary>
        public string CurTarget { 
            get {  return _curTarget; }
            set
            {
                this._curTarget = value;
                if (this.lblCurTarget != null) {
                    this.lblCurTarget.Text = value;
                    Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// 最大値
        /// </summary>
        public int MaxVal { get; set; }

        /// <summary>
        /// キャンセルされたか否か
        /// </summary>
        public bool IsCancel { get; protected set; }

        /// <summary>
        /// ダイアログを表示
        /// </summary>
        /// <param name="owner"></param>
        public virtual void Show(Form owner)
        {
            this.IsCancel = false;
            //this.Owner = owner;
            this.Caller = owner;

            if (this.Caller is MainForm)
            {
                //((MainForm)this.Caller).IsProcessing = true;
                //this.MdiParent = ((MainForm)this.Caller).MdiParent;

                ((MainForm)this.Caller).SetEnable(false);
            }
            else
            {
                this.Caller.Enabled = false;
            }
            this.progressBar1.Maximum = this.MaxVal;
            this.txtTotal.Text = this.MaxVal.ToString();
            this.Show();
            this.Location = new Point(
                this.Caller.Location.X + (this.Caller.Width / 2) - (this.Width / 2),
                this.Caller.Location.Y + (this.Caller.Height / 2) - (this.Height / 2)
                );
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsCancel = true;
            this.Close();
        }
    }
}
