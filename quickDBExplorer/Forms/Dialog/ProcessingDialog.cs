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
        /// コンストラクタ
        /// </summary>
        /// <param name="maxVal"></param>
        public ProcessingDialog(int maxVal) : base()
        {
            InitializeComponent();
            this.FormClosing += ProcessingDialog_FormClosing;
            this.MaxVal = maxVal;
        }

        public ProcessingDialog()
        {
            InitializeComponent();
            this.FormClosing += ProcessingDialog_FormClosing;
        }

        private void ProcessingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Enabled = true;
            }
        }

        protected int _curVal;
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

        protected string _curTarget;
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

        public int MaxVal { get; set; }

        public bool IsCancel { get; protected set; }


        public virtual void Show(Form owner)
        {
            this.IsCancel = false;
            this.Owner = owner;
            this.Owner.Enabled = false;
            this.progressBar1.Maximum = this.MaxVal;
            this.txtTotal.Text = this.MaxVal.ToString();
            this.Show();
            this.Location = new Point(
                this.Owner.Location.X + (this.Owner.Width / 2) - (this.Width / 2),
                this.Owner.Location.Y + (this.Owner.Height / 2) - (this.Height / 2)
                );

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsCancel = true;
            this.Close();
        }
    }
}
