using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// �t�B�[���h���̎擾�����w��_�C�A���O
	/// </summary>
	public class FieldGetDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtAlias;
		private System.Windows.Forms.CheckBox chkComma;
		private System.Windows.Forms.CheckBox chkCRLF;
		private System.Windows.Forms.ComboBox cmbPattern;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// �e�[�u����
		/// </summary>
		protected string	baseTableName = "";
		/// <summary>
		/// �e�[�u����
		/// </summary>
		public string	BaseTableName
		{
			get { return this.baseTableName; }
			set { this.baseTableName = value; }
		}

		/// <summary>
		/// �e�[�u���̏C���q(alias) �߂�l
		/// </summary>
		protected string retTableAccessor = "";

		/// <summary>
		/// �e�[�u���̏C���q(alias) �߂�l
		/// </summary>
		public string RetTableAccessor
		{
			get { return this.retTableAccessor; }
			set { this.retTableAccessor = value; }
		}

		/// <summary>
		/// ���s�����邩���Ȃ����̎w��(�߂�l)
		/// </summary>
		protected bool retCRLF = false;
		/// <summary>
		/// ���s�����邩���Ȃ����̎w��(�߂�l)
		/// </summary>
		public bool RetCRLF 
		{
			get { return this.retCRLF; }
			set { this.retCRLF = value; }
		}
		private System.Windows.Forms.ToolTip toolTip1;
		/// <summary>
		/// �J���}�����邩���Ȃ���(�߂�l)
		/// </summary>
		protected bool retComma = false;
		/// <summary>
		/// �J���}�����邩���Ȃ���(�߂�l)
		/// </summary>
		public bool RetComma 
		{
			get { return this.retComma; }
			set { this.retComma = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public FieldGetDialog()
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			// TODO: InitializeComponent �Ăяo���̌�ɏ�����������ǉ����܂��B
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

		#region �f�U�C�i�Ő������ꂽ�R�[�h
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtAlias = new System.Windows.Forms.TextBox();
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
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(144, 240);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(320, 24);
			// 
			// txtAlias
			// 
			this.txtAlias.Location = new System.Drawing.Point(128, 56);
			this.txtAlias.Name = "txtAlias";
			this.txtAlias.Size = new System.Drawing.Size(256, 19);
			this.txtAlias.TabIndex = 3;
			this.txtAlias.Text = "";
			this.toolTip1.SetToolTip(this.txtAlias, "�t�B�[���h���̑O�ɂ���C�������w�肵�܂�");
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "�t�B�[���h�C���q(&A)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new System.Drawing.Point(8, 160);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "����(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// chkComma
			// 
			this.chkComma.Location = new System.Drawing.Point(8, 128);
			this.chkComma.Name = "chkComma";
			this.chkComma.Size = new System.Drawing.Size(216, 16);
			this.chkComma.TabIndex = 5;
			this.chkComma.Text = "�J���}�t��(&M)";
			this.toolTip1.SetToolTip(this.chkComma, "�t�B�[���h���ɃJ���}�i,) �����邩�ۂ����w�肵�܂�");
			// 
			// chkCRLF
			// 
			this.chkCRLF.Location = new System.Drawing.Point(8, 96);
			this.chkCRLF.Name = "chkCRLF";
			this.chkCRLF.Size = new System.Drawing.Size(216, 16);
			this.chkCRLF.TabIndex = 4;
			this.chkCRLF.Text = "���s(&L)";
			this.toolTip1.SetToolTip(this.chkCRLF, "�t�B�[���h���ɉ��s���w�肷�邩�ǂ������w�肵�܂�");
			// 
			// cmbPattern
			// 
			this.cmbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPattern.Location = new System.Drawing.Point(136, 16);
			this.cmbPattern.Name = "cmbPattern";
			this.cmbPattern.Size = new System.Drawing.Size(248, 20);
			this.cmbPattern.TabIndex = 1;
			this.toolTip1.SetToolTip(this.cmbPattern, "�悭���p����p�^�[�����ꗗ����I���ł��܂�");
			this.cmbPattern.SelectedIndexChanged += new System.EventHandler(this.cmbPattern_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "�p�^�[���I��(&P)";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(320, 160);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "�߂�(&X)";
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
			this.Text = "�t�B�[���h���X�g�擾";
			this.Load += new System.EventHandler(this.FieldGetDialog_Load);
			this.Controls.SetChildIndex(this.txtAlias, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.chkCRLF, 0);
			this.Controls.SetChildIndex(this.chkComma, 0);
			this.Controls.SetChildIndex(this.cmbPattern, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// ��ʏ����\��������
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void FieldGetDialog_Load(object sender, System.EventArgs e)
		{
			// �\���G���A���z��������ꏏ�ɃZ�b�g���A�I�����̏����ɗ��p���Ă���
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
			// �����\���͈�ԍŏ�
			this.cmbPattern.SelectedIndex = 0;
		}

		/// <summary>
		/// �R���{�{�b�N�X�I�����n���h��
		/// �I�����ꂽ���ڂɉ����āA�e���ڂ̓��e���Z�b�g����
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void cmbPattern_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string []selstr = this.cmbPattern.SelectedItem.ToString().Split(new char[]{':'},3)[2].Split(new char[]{','},3);
			// �e�[�u���C���q�̎w��
			switch( int.Parse(selstr[0]) )
			{
				case 1:
					this.txtAlias.Text = "t1";
					break;
				case 2:
					this.txtAlias.Text = qdbeUtil.SplitTbname(this.baseTableName)[1];
					break;
				case 3:
					this.txtAlias.Text = qdbeUtil.GetTbname(this.baseTableName);
					break;
				default:
					break;
			}
			// �J���}���p�̎w��
			if(int.Parse(selstr[1]) == 1 )
			{
				this.chkComma.Checked = true;
			}
			else
			{
				this.chkComma.Checked = false;
			}
			// ���s�t���̎w��
			if(int.Parse(selstr[2]) == 1 )
			{
				this.chkCRLF.Checked = true;
			}
			else
			{
				this.chkCRLF.Checked = false;
			}
		}


		/// <summary>
		/// OK�{�^���������n���h��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.retTableAccessor = this.txtAlias.Text;
			this.retCRLF = this.chkCRLF.Checked;
			this.retComma = this.chkComma.Checked;
			this.DialogResult = DialogResult.OK;
		}
	}
}
