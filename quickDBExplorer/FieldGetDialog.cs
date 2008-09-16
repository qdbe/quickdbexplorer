using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// フィールド情報の取得条件指定ダイアログ
	/// フィールド情報をコピーする条件を指定する
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class FieldGetDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private quickDBExplorerTextBox txtAlias;
		private System.Windows.Forms.CheckBox chkComma;
		private System.Windows.Forms.CheckBox chkCRLF;
		private System.Windows.Forms.ComboBox cmbPattern;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// オブジェクト名
		/// </summary>
		private string	pBaseTableName = "";
		/// <summary>
		/// オブジェクト名
		/// </summary>
		public string	BaseTableName
		{
			get { return this.pBaseTableName; }
			set { this.pBaseTableName = value; }
		}

		/// <summary>
		/// オブジェクトの修飾子(alias) 戻り値
		/// </summary>
		private string pRetTableAccessor = "";

		/// <summary>
		/// オブジェクトの修飾子(alias) 戻り値
		/// </summary>
		public string RetTableAccessor
		{
			get { return this.pRetTableAccessor; }
			set { this.pRetTableAccessor = value; }
		}

		/// <summary>
		/// 改行をつけるかつけないかの指定(戻り値)
		/// </summary>
		private bool pRetCrlf = false;
		/// <summary>
		/// 改行をつけるかつけないかの指定(戻り値)
		/// </summary>
		public bool RetCrlf 
		{
			get { return this.pRetCrlf; }
			set { this.pRetCrlf = value; }
		}
		private System.Windows.Forms.ToolTip toolTip1;
		/// <summary>
		/// カンマをつけるかつけないか(戻り値)
		/// </summary>
		private bool pRetComma = false;
		/// <summary>
		/// カンマをつけるかつけないか(戻り値)
		/// </summary>
		public bool RetComma 
		{
			get { return this.pRetComma; }
			set { this.pRetComma = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FieldGetDialog()
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();

			// TODO: InitializeComponent 呼び出しの後に初期化処理を追加します。
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region デザイナで生成されたコード
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtAlias = new quickDBExplorer.quickDBExplorerTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.chkComma = new System.Windows.Forms.CheckBox();
			this.chkCRLF = new System.Windows.Forms.CheckBox();
			this.cmbPattern = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(144, 240);
			this.MsgArea.Name = "MsgArea";
			this.MsgArea.Size = new System.Drawing.Size(320, 24);
			// 
			// txtAlias
			// 
			this.txtAlias.IsCTRLDelete = true;
			this.txtAlias.IsDigitOnly = false;
			this.txtAlias.Location = new System.Drawing.Point(128, 56);
			this.txtAlias.Name = "txtAlias";
			this.txtAlias.Size = new System.Drawing.Size(256, 19);
			this.txtAlias.TabIndex = 3;
			this.txtAlias.Text = "";
			this.toolTip1.SetToolTip(this.txtAlias, "フィールド名の前につける修飾名を指定します");
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "フィールド修飾子(&A)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new System.Drawing.Point(8, 160);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "決定(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// chkComma
			// 
			this.chkComma.Location = new System.Drawing.Point(8, 128);
			this.chkComma.Name = "chkComma";
			this.chkComma.Size = new System.Drawing.Size(216, 16);
			this.chkComma.TabIndex = 5;
			this.chkComma.Text = "カンマ付き(&M)";
			this.toolTip1.SetToolTip(this.chkComma, "フィールド毎にカンマ（,) をつけるか否かを指定します");
			// 
			// chkCRLF
			// 
			this.chkCRLF.Location = new System.Drawing.Point(8, 96);
			this.chkCRLF.Name = "chkCRLF";
			this.chkCRLF.Size = new System.Drawing.Size(216, 16);
			this.chkCRLF.TabIndex = 4;
			this.chkCRLF.Text = "改行(&L)";
			this.toolTip1.SetToolTip(this.chkCRLF, "フィールド毎に改行を指定するかどうかを指定します");
			// 
			// cmbPattern
			// 
			this.cmbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPattern.Location = new System.Drawing.Point(136, 16);
			this.cmbPattern.Name = "cmbPattern";
			this.cmbPattern.Size = new System.Drawing.Size(248, 20);
			this.cmbPattern.TabIndex = 1;
			this.toolTip1.SetToolTip(this.cmbPattern, "よく利用するパターンを一覧から選択できます");
			this.cmbPattern.SelectedIndexChanged += new System.EventHandler(this.cmbPattern_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "パターン選択(&P)";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(320, 160);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "戻る(&X)";
			// 
			// FieldGetDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(408, 189);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbPattern);
			this.Controls.Add(this.chkComma);
			this.Controls.Add(this.chkCRLF);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtAlias);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FieldGetDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "フィールドリスト取得";
			this.Load += new System.EventHandler(this.FieldGetDialog_Load);
			this.Controls.SetChildIndex(this.txtAlias, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.chkCRLF, 0);
			this.Controls.SetChildIndex(this.chkComma, 0);
			this.Controls.SetChildIndex(this.cmbPattern, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 画面初期表示時処理
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void FieldGetDialog_Load(object sender, System.EventArgs e)
		{
			// 表示エリアを越える情報を一緒にセットし、選択時の処理に利用している
			this.cmbPattern.Items.Add("0:alias.FieldName,CRLF                                      :1,1,1");
			this.cmbPattern.Items.Add("1:alias.FieldName CRLF                                      :1,0,1");
			this.cmbPattern.Items.Add("2:alias.FieldName                                           :1,0,0");
			this.cmbPattern.Items.Add("3:alias.FieldName,                                          :1,1,0");
			this.cmbPattern.Items.Add("4:TableName.FieldName,CRLF                                  :2,1,1");
			this.cmbPattern.Items.Add("5:TableName.FieldName CRLF                                  :2,0,1");
			this.cmbPattern.Items.Add("6:TableName.FieldName                                       :2,0,0");
			this.cmbPattern.Items.Add("7:TableName.FieldName,                                      :2,1,0");
			this.cmbPattern.Items.Add("8:Schema.TableName.FieldName,CRLF                           :3,1,1");
			this.cmbPattern.Items.Add("9:Schema.TableName.FieldName CRLF                           :3,0,1");
			this.cmbPattern.Items.Add("A:Schema.TableName.FieldName                                :3,0,0");
			this.cmbPattern.Items.Add("B:Schema.TableName.FieldName,                               :3,1,0");
			// 初期表示は一番最初
			this.cmbPattern.SelectedIndex = 0;
		}

		/// <summary>
		/// コンボボックス選択時ハンドラ
		/// 選択された項目に応じて、各項目の内容をセットする
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void cmbPattern_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string []selstr = this.cmbPattern.SelectedItem.ToString().Split(new char[]{':'},3)[2].Split(new char[]{','},3);
			// オブジェクト修飾子の指定
			switch( int.Parse(selstr[0],System.Globalization.CultureInfo.CurrentCulture) )
			{
				case 1:
					this.txtAlias.Text = "t1";
					break;
				case 2:
					this.txtAlias.Text = qdbeUtil.SplitTbname(this.pBaseTableName)[1];
					break;
				case 3:
					this.txtAlias.Text = qdbeUtil.GetTbname(this.pBaseTableName);
					break;
				default:
					break;
			}
			// カンマ利用の指定
			if(int.Parse(selstr[1],System.Globalization.CultureInfo.CurrentCulture) == 1 )
			{
				this.chkComma.Checked = true;
			}
			else
			{
				this.chkComma.Checked = false;
			}
			// 改行付加の指定
			if(int.Parse(selstr[2],System.Globalization.CultureInfo.CurrentCulture) == 1 )
			{
				this.chkCRLF.Checked = true;
			}
			else
			{
				this.chkCRLF.Checked = false;
			}
		}


		/// <summary>
		/// OKボタン押下時ハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.pRetTableAccessor = this.txtAlias.Text;
			this.pRetCrlf = this.chkCRLF.Checked;
			this.pRetComma = this.chkComma.Checked;
			this.DialogResult = DialogResult.OK;
		}
	}
}

