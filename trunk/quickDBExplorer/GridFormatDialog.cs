using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;


namespace quickDBExplorer
{
	/// <summary>
	/// データグリッドの表示書式を指定する
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

		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// 表示フォントの指定
		/// </summary>
		private	Font	gridFont;
		/// <summary>
		/// 表示フォントの指定
		/// </summary>
		public	Font	Gfont
		{
			get { return this.gridFont; }
			set { this.gridFont = value; }
		}
		/// <summary>
		/// フォント表示色の指定
		/// </summary>
		private	Color	gridColor;
		/// <summary>
		/// フォント表示色の指定
		/// </summary>
		public	Color	Gcolor
		{
			get { return this.gridColor; }
			set { this.gridColor = value; }
		}
		/// <summary>
		/// 数値変換書式の指定
		/// </summary>
		private	string	numberFormat;
		/// <summary>
		/// 数値変換書式の指定
		/// </summary>
		public	string	NumFormat
		{
			get { return this.numberFormat; }
			set { this.numberFormat = value; }

		}
		/// <summary>
		/// 小数点書式の指定
		/// </summary>
		private	string	gridFloatFormat;
		/// <summary>
		/// 小数点書式の指定
		/// </summary>
		public	string	FloatFormat
		{
			get { return this.gridFloatFormat; }
			set { this.gridFloatFormat = value; }
		}
		/// <summary>
		/// 日付変換書式の指定
		/// </summary>
		private	string	gridDateFormat;
		/// <summary>
		/// 日付変換書式の指定
		/// </summary>
		public	string	DateFormat
		{
			get { return this.gridDateFormat; }
			set { this.gridDateFormat = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GridFormatDialog()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
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
			this.txtNumDisp = new quickDBExplorer.quickDBExplorerTextBox();
			this.cmbNumFormat = new System.Windows.Forms.ComboBox();
			this.txtDecimalDisp = new quickDBExplorer.quickDBExplorerTextBox();
			this.txtDateTimeDisp = new quickDBExplorer.quickDBExplorerTextBox();
			this.cmbDecimalFormat = new System.Windows.Forms.ComboBox();
			this.cmbDateFormat = new System.Windows.Forms.ComboBox();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.txtFontDisp = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(560, 168);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "戻る(&X)";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(16, 168);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "決定(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "浮動小数点 書式(&B)";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "整数書式(&S)";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "日付書式(&D)";
			// 
			// btnFont
			// 
			this.btnFont.Location = new System.Drawing.Point(360, 112);
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(104, 24);
			this.btnFont.TabIndex = 8;
			this.btnFont.Text = "フォント指定(&F)";
			this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "グリッド表示フォント指定(&G)";
			// 
			// txtNumDisp
			// 
			this.txtNumDisp.IsCTRLDelete = true;
			this.txtNumDisp.IsDigitOnly = false;
			this.txtNumDisp.Location = new System.Drawing.Point(176, 16);
			this.txtNumDisp.Name = "txtNumDisp";
			this.txtNumDisp.ReadOnly = true;
			this.txtNumDisp.Size = new System.Drawing.Size(168, 19);
			this.txtNumDisp.TabIndex = 12;
			this.txtNumDisp.TabStop = false;
			// 
			// cmbNumFormat
			// 
			this.cmbNumFormat.Items.AddRange(new object[] {
            "D\t\tカンマなし",
            "###,###,###,###\tカンマあり"});
			this.cmbNumFormat.Location = new System.Drawing.Point(360, 16);
			this.cmbNumFormat.Name = "cmbNumFormat";
			this.cmbNumFormat.Size = new System.Drawing.Size(272, 20);
			this.cmbNumFormat.TabIndex = 1;
			this.cmbNumFormat.SelectedIndexChanged += new System.EventHandler(this.cmbNumFormat_SelectedIndexChanged);
			// 
			// txtDecimalDisp
			// 
			this.txtDecimalDisp.IsCTRLDelete = true;
			this.txtDecimalDisp.IsDigitOnly = false;
			this.txtDecimalDisp.Location = new System.Drawing.Point(176, 48);
			this.txtDecimalDisp.Name = "txtDecimalDisp";
			this.txtDecimalDisp.ReadOnly = true;
			this.txtDecimalDisp.Size = new System.Drawing.Size(168, 19);
			this.txtDecimalDisp.TabIndex = 12;
			this.txtDecimalDisp.TabStop = false;
			// 
			// txtDateTimeDisp
			// 
			this.txtDateTimeDisp.IsCTRLDelete = true;
			this.txtDateTimeDisp.IsDigitOnly = false;
			this.txtDateTimeDisp.Location = new System.Drawing.Point(176, 80);
			this.txtDateTimeDisp.Name = "txtDateTimeDisp";
			this.txtDateTimeDisp.ReadOnly = true;
			this.txtDateTimeDisp.Size = new System.Drawing.Size(168, 19);
			this.txtDateTimeDisp.TabIndex = 12;
			this.txtDateTimeDisp.TabStop = false;
			// 
			// cmbDecimalFormat
			// 
			this.cmbDecimalFormat.Items.AddRange(new object[] {
            "g\t標準",
            "n\tカンマ付き",
            "F\t小数点",
            "e\t指数"});
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
            "yyyy年MM月dd日\t\t2006年01月01日",
            "yyyy年M月d日\t\t2006年1月1日",
            "yyyy/MM/dd HH:mm:ss\t2006/01/01 01:23:05",
            "yyyy/M/d HH:mm:ss\t\t2006/1/1 01:23:05",
            "yyyy年MM月dd日 HH:mm:ss\t2006年01月01日 01:23:05",
            "yyyy年M月d日 HH:mm:ss\t2006年1月1日 01:23:05"});
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
			this.txtFontDisp.Text = "文字ABC";
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
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cmbDecimalFormat);
			this.Controls.Add(this.cmbDateFormat);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GridFormatDialog";
			this.ShowInTaskbar = false;
			this.Text = "データ表示書式指定";
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
			this.fontDialog1.Font = gridFont;
			this.fontDialog1.Color = gridColor;
			this.fontDialog1.ShowColor = false;
			
			if( this.fontDialog1.ShowDialog() == DialogResult.OK )
			{
				this.gridFont = this.fontDialog1.Font;
				this.gridColor = Color.FromArgb(this.fontDialog1.Color.ToArgb());
				this.txtFontDisp.Text = this.gridFont.Name;
				this.txtFontDisp.ForeColor = this.gridColor;
				this.txtFontDisp.Font = this.gridFont;
			}
		}

		private void GridFormatDialog_Load(object sender, System.EventArgs e)
		{
			this.txtFontDisp.Text = this.gridFont.Name;
			this.txtFontDisp.ForeColor = this.gridColor;
			this.txtFontDisp.Font = this.gridFont;
			if( this.NumFormat != null )
			{
				this.cmbNumFormat.SelectedItem = this.NumFormat;
			}
			else
			{
				this.cmbNumFormat.SelectedIndex = 0;
			}
			if( this.FloatFormat != null )
			{
				this.cmbDecimalFormat.SelectedItem = this.FloatFormat;
			}
			else
			{
				this.cmbDecimalFormat.SelectedIndex = 0;
			}
			if( this.DateFormat != null )
			{
				this.cmbDateFormat.SelectedItem = this.DateFormat;
			}
			else
			{
				this.cmbDateFormat.SelectedIndex = 0;
			}
		}

		private void cmbNumFormat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int i = 1234;
			this.txtNumDisp.Text = i.ToString(GetFormat(this.cmbNumFormat.SelectedItem.ToString()),System.Globalization.CultureInfo.CurrentCulture);
		}

		private void cmbDecimalFormat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			double dd = 1234.23;
			this.txtDecimalDisp.Text = dd.ToString(GetFormat(this.cmbDecimalFormat.SelectedItem.ToString()),System.Globalization.CultureInfo.CurrentCulture);
		}

		private void cmbDateFormat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.txtDateTimeDisp.Text = DateTime.Now.ToString(GetFormat(this.cmbDateFormat.SelectedItem.ToString()),System.Globalization.CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// フォーマット文字列を取得する
		/// </summary>
		/// <param name="fstr">基の表示書式</param>
		/// <returns>表示書式文字列</returns>
		protected static string GetFormat(string fstr)
		{
			// \t\t があれば、プログラムの初期選択枝からの選択
			int termp = fstr.IndexOf("	");
			if( termp == -1 )
			{
				// \t\t がない場合、ユーザーが入力した文字列
				return fstr;
			}
			return fstr.Substring(0,termp);
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.NumFormat = (string)this.cmbNumFormat.SelectedItem;
			this.FloatFormat = (string)this.cmbDecimalFormat.SelectedItem;
			this.DateFormat = (string)this.cmbDateFormat.SelectedItem;
			this.DialogResult = DialogResult.OK;
		}

	}
}
