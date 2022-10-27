using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;


namespace quickDBExplorer
{
    /// <summary>
    /// �f�[�^�O���b�h�̕\���������w�肷��
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class GridFormatDialog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label label4;
        private quickDBExplorerTextBox txtNumDisp;
        private System.Windows.Forms.ComboBox cmbNumFormat;
        private quickDBExplorerTextBox txtDecimalDisp;
        private quickDBExplorerTextBox txtDateTimeDisp;
        private System.Windows.Forms.ComboBox cmbDecimalFormat;
        private System.Windows.Forms.ComboBox cmbDateFormat;
        private System.Windows.Forms.RichTextBox txtFontDisp;
        private System.Windows.Forms.FontDialog fontDialog1;

        private GridFormatSetting settings;
        private Button btnDefault;

        /// <summary>
        /// �O���b�h�\������
        /// </summary>
        public GridFormatSetting Option
        {
            get { return this.settings; }
            set { this.settings = value; }
        }

        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public GridFormatDialog()
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
            //
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridFormatDialog));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFont = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbNumFormat = new System.Windows.Forms.ComboBox();
            this.cmbDecimalFormat = new System.Windows.Forms.ComboBox();
            this.cmbDateFormat = new System.Windows.Forms.ComboBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.txtFontDisp = new System.Windows.Forms.RichTextBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.txtDecimalDisp = new quickDBExplorer.quickDBExplorerTextBox();
            this.txtNumDisp = new quickDBExplorer.quickDBExplorerTextBox();
            this.txtDateTimeDisp = new quickDBExplorer.quickDBExplorerTextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(560, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "�߂�(&X)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(16, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "����(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "���������_ ����(&B)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "��������(&S)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "���t����(&D)";
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(360, 112);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(104, 24);
            this.btnFont.TabIndex = 8;
            this.btnFont.Text = "�t�H���g�w��(&F)";
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "�O���b�h�\���t�H���g�w��(&G)";
            // 
            // cmbNumFormat
            // 
            this.cmbNumFormat.Items.AddRange(new object[] {
            "D\t\t�J���}�Ȃ�",
            "###,###,###,###\t�J���}����"});
            this.cmbNumFormat.Location = new System.Drawing.Point(360, 16);
            this.cmbNumFormat.Name = "cmbNumFormat";
            this.cmbNumFormat.Size = new System.Drawing.Size(272, 20);
            this.cmbNumFormat.TabIndex = 1;
            this.cmbNumFormat.SelectedIndexChanged += new System.EventHandler(this.cmbNumFormat_SelectedIndexChanged);
            // 
            // cmbDecimalFormat
            // 
            this.cmbDecimalFormat.Items.AddRange(new object[] {
            "g\t�W��",
            "n\t�J���}�t��",
            "F\t�����_",
            "e\t�w��"});
            this.cmbDecimalFormat.Location = new System.Drawing.Point(360, 48);
            this.cmbDecimalFormat.Name = "cmbDecimalFormat";
            this.cmbDecimalFormat.Size = new System.Drawing.Size(272, 20);
            this.cmbDecimalFormat.TabIndex = 3;
            this.cmbDecimalFormat.SelectedIndexChanged += new System.EventHandler(this.cmbDecimalFormat_SelectedIndexChanged);
            // 
            // cmbDateFormat
            // 
            this.cmbDateFormat.Items.AddRange(new object[] {
            "yyyy/MM/dd\t\t2006/01/01",
            "yyyy/M/d\t\t\t2006/1/1",
            "yyyy�NMM��dd��\t\t2006�N01��01��",
            "yyyy�NM��d��\t\t2006�N1��1��",
            "yyyy/MM/dd HH:mm:ss\t2006/01/01 01:23:05",
            "yyyy/M/d HH:mm:ss\t\t2006/1/1 01:23:05",
            "yyyy�NMM��dd�� HH:mm:ss\t2006�N01��01�� 01:23:05",
            "yyyy�NM��d�� HH:mm:ss\t2006�N1��1�� 01:23:05"});
            this.cmbDateFormat.Location = new System.Drawing.Point(360, 80);
            this.cmbDateFormat.Name = "cmbDateFormat";
            this.cmbDateFormat.Size = new System.Drawing.Size(272, 20);
            this.cmbDateFormat.TabIndex = 5;
            this.cmbDateFormat.SelectedIndexChanged += new System.EventHandler(this.cmbDateFormat_SelectedIndexChanged);
            // 
            // txtFontDisp
            // 
            this.txtFontDisp.Location = new System.Drawing.Point(176, 112);
            this.txtFontDisp.Name = "txtFontDisp";
            this.txtFontDisp.ReadOnly = true;
            this.txtFontDisp.Size = new System.Drawing.Size(168, 40);
            this.txtFontDisp.TabIndex = 12;
            this.txtFontDisp.TabStop = false;
            this.txtFontDisp.Text = "����ABC";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(176, 168);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(115, 24);
            this.btnDefault.TabIndex = 10;
            this.btnDefault.Text = "����l�ɖ߂�(&D)";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // txtDecimalDisp
            // 
            this.txtDecimalDisp.CanCtrlDelete = true;
            this.txtDecimalDisp.Histories = null;
            this.txtDecimalDisp.HistoryKey = "txtDecimalDisp";
            this.txtDecimalDisp.IsDigitOnly = false;
            this.txtDecimalDisp.IsShowZoom = false;
            this.txtDecimalDisp.Location = new System.Drawing.Point(176, 48);
            this.txtDecimalDisp.Name = "txtDecimalDisp";
            this.txtDecimalDisp.ReadOnly = true;
            this.txtDecimalDisp.Size = new System.Drawing.Size(168, 19);
            this.txtDecimalDisp.TabIndex = 12;
            this.txtDecimalDisp.TabStop = false;
            // 
            // txtNumDisp
            // 
            this.txtNumDisp.CanCtrlDelete = true;
            this.txtNumDisp.Histories = null;
            this.txtNumDisp.HistoryKey = "txtNumDisp";
            this.txtNumDisp.IsDigitOnly = false;
            this.txtNumDisp.IsShowZoom = false;
            this.txtNumDisp.Location = new System.Drawing.Point(176, 16);
            this.txtNumDisp.Name = "txtNumDisp";
            this.txtNumDisp.ReadOnly = true;
            this.txtNumDisp.Size = new System.Drawing.Size(168, 19);
            this.txtNumDisp.TabIndex = 12;
            this.txtNumDisp.TabStop = false;
            // 
            // txtDateTimeDisp
            // 
            this.txtDateTimeDisp.CanCtrlDelete = true;
            this.txtDateTimeDisp.Histories = null;
            this.txtDateTimeDisp.HistoryKey = "txtDateTimeDisp";
            this.txtDateTimeDisp.IsDigitOnly = false;
            this.txtDateTimeDisp.IsShowZoom = false;
            this.txtDateTimeDisp.Location = new System.Drawing.Point(176, 80);
            this.txtDateTimeDisp.Name = "txtDateTimeDisp";
            this.txtDateTimeDisp.ReadOnly = true;
            this.txtDateTimeDisp.Size = new System.Drawing.Size(168, 19);
            this.txtDateTimeDisp.TabIndex = 12;
            this.txtDateTimeDisp.TabStop = false;
            // 
            // GridFormatDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(648, 205);
            this.Controls.Add(this.txtFontDisp);
            this.Controls.Add(this.txtDecimalDisp);
            this.Controls.Add(this.txtNumDisp);
            this.Controls.Add(this.txtDateTimeDisp);
            this.Controls.Add(this.cmbNumFormat);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDecimalFormat);
            this.Controls.Add(this.cmbDateFormat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GridFormatDialog";
            this.ShowInTaskbar = false;
            this.Text = "�f�[�^�\�������w��";
            this.Load += new System.EventHandler(this.GridFormatDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void label2_Click(object sender, System.EventArgs e)
        {

        }

        private void btnFont_Click(object sender, System.EventArgs e)
        {
            this.fontDialog1.Font = this.Option.GridFont;
            this.fontDialog1.Color = this.Option.GridForeColor;
            this.fontDialog1.ShowColor = false;

            if (this.fontDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.Option.GridFont = this.fontDialog1.Font;
                this.Option.GridForeColor = Color.FromArgb(this.fontDialog1.Color.ToArgb());
                this.txtFontDisp.Text = this.Option.GridFont.Name;
                this.txtFontDisp.ForeColor = this.Option.GridForeColor;
                this.txtFontDisp.Font = this.Option.GridFont;
            }
        }

        private void GridFormatDialog_Load(object sender, System.EventArgs e)
        {
            SetValues();
        }

        private void SetValues()
        {
            this.txtFontDisp.Text = this.Option.GridFont.Name;
            this.txtFontDisp.ForeColor = this.Option.GridForeColor;
            this.txtFontDisp.Font = this.Option.GridFont;
            if (this.Option.GridNumberFormat != null)
            {
                this.cmbNumFormat.SelectedItem = this.Option.GridNumberFormat;
            }
            else
            {
                this.cmbNumFormat.SelectedIndex = 0;
            }
            if (this.Option.GridFloatFormat != null)
            {
                this.cmbDecimalFormat.SelectedItem = this.Option.GridFloatFormat;
            }
            else
            {
                this.cmbDecimalFormat.SelectedIndex = 0;
            }
            if (this.Option.GridDateFormat != null)
            {
                this.cmbDateFormat.SelectedItem = this.Option.GridDateFormat;
            }
            else
            {
                this.cmbDateFormat.SelectedIndex = 0;
            }
        }

        private void cmbNumFormat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int i = 1234;
            this.txtNumDisp.Text = i.ToString(GetFormat(this.cmbNumFormat.SelectedItem.ToString()), System.Globalization.CultureInfo.CurrentCulture);
        }

        private void cmbDecimalFormat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            double dd = 1234.23;
            this.txtDecimalDisp.Text = dd.ToString(GetFormat(this.cmbDecimalFormat.SelectedItem.ToString()), System.Globalization.CultureInfo.CurrentCulture);
        }

        private void cmbDateFormat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.txtDateTimeDisp.Text = DateTime.Now.ToString(GetFormat(this.cmbDateFormat.SelectedItem.ToString()), System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// �t�H�[�}�b�g��������擾����
        /// </summary>
        /// <param name="fstr">��̕\������</param>
        /// <returns>�\������������</returns>
        protected static string GetFormat(string fstr)
        {
            // \t\t ������΁A�v���O�����̏����I���}����̑I��
            int termp = fstr.IndexOf("	");
            if (termp == -1)
            {
                // \t\t ���Ȃ��ꍇ�A���[�U�[�����͂���������
                return fstr;
            }
            return fstr.Substring(0, termp);
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Option.GridNumberFormat = (string)this.cmbNumFormat.SelectedItem;
            this.Option.GridFloatFormat = (string)this.cmbDecimalFormat.SelectedItem;
            this.Option.GridDateFormat = (string)this.cmbDateFormat.SelectedItem;
            this.DialogResult = DialogResult.OK;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            this.Option = GridFormatSetting.Defalt();
            this.SetValues();
        }
    }


}

