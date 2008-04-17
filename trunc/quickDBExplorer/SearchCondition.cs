using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// ������������͂���_�C�A���O�N���X
	/// </summary>
	public class SearchCondition : quickDBExplorer.QueryDialog
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkField;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rdoPre;
		private System.Windows.Forms.RadioButton rdoInclude;
		private System.Windows.Forms.RadioButton rdoExtract;
		private System.Windows.Forms.CheckBox chkTable;
		private System.Windows.Forms.CheckBox chkView;
		private System.Windows.Forms.CheckBox chkSynonym;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.CheckBox chkShowObjectSelector;

		private SqlVersion pSqlVersion = null;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public SearchCondition(SqlVersion sqlVer)
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			this.pSqlVersion = sqlVer;
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


		/// <summary>
		/// �t�B�[���h�����������邩�ۂ�
		/// </summary>
		public bool IsSearchField
		{
			get { return this.chkField.Checked; }
		}

		/// <summary>
		/// �e�[�u�������������邩�ۂ�
		/// </summary>
		public bool IsSearchTable
		{
			get { return this.chkTable.Checked; }
		}

		/// <summary>
		/// View�����������邩�ۂ�
		/// </summary>
		public bool IsSearchView
		{
			get { return this.chkView.Checked; }
		}

		/// <summary>
		/// Synonym�����������邩�ۂ�
		/// </summary>
		public bool IsSearchSynonym
		{
			get { return this.chkSynonym.Checked; }
		}

		/// <summary>
		/// �������ʂ����ɃI�u�W�F�N�g��I�����邩�ۂ�
		/// </summary>
		public bool IsShowTableSelect
		{
			get { return this.chkShowObjectSelector.Checked; }
		}

		#region �f�U�C�i�Ő������ꂽ�R�[�h
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
					this.label1 = new System.Windows.Forms.Label();
					this.chkField = new System.Windows.Forms.CheckBox();
					this.chkTable = new System.Windows.Forms.CheckBox();
					this.groupBox1 = new System.Windows.Forms.GroupBox();
					this.chkSynonym = new System.Windows.Forms.CheckBox();
					this.chkView = new System.Windows.Forms.CheckBox();
					this.groupBox2 = new System.Windows.Forms.GroupBox();
					this.rdoPre = new System.Windows.Forms.RadioButton();
					this.rdoInclude = new System.Windows.Forms.RadioButton();
					this.rdoExtract = new System.Windows.Forms.RadioButton();
					this.chkShowObjectSelector = new System.Windows.Forms.CheckBox();
					this.groupBox1.SuspendLayout();
					this.groupBox2.SuspendLayout();
					this.SuspendLayout();
					// 
					// txtInput
					// 
					this.txtInput.Location = new System.Drawing.Point(16, 38);
					this.txtInput.Multiline = false;
					this.txtInput.Name = "txtInput";
					this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
					this.txtInput.Size = new System.Drawing.Size(444, 19);
					this.txtInput.TabIndex = 1;
					// 
					// btnGo
					// 
					this.btnGo.Location = new System.Drawing.Point(16, 212);
					this.btnGo.Name = "btnGo";
					this.btnGo.TabIndex = 4;
					this.btnGo.Text = "����(&O)";
					this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
					// 
					// btnCancel
					// 
					this.btnCancel.Location = new System.Drawing.Point(348, 212);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.TabIndex = 6;
					// 
					// chkReturn
					// 
					this.chkReturn.Location = new System.Drawing.Point(236, 12);
					this.chkReturn.Name = "chkReturn";
					this.chkReturn.Size = new System.Drawing.Size(208, 18);
					this.chkReturn.Visible = false;
					// 
					// btnHistory
					// 
					this.btnHistory.Location = new System.Drawing.Point(144, 212);
					this.btnHistory.Name = "btnHistory";
					this.btnHistory.TabIndex = 5;
					// 
					// label1
					// 
					this.label1.Location = new System.Drawing.Point(18, 10);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(430, 23);
					this.label1.TabIndex = 0;
					this.label1.Text = "��������t�B�[���h�����w�肵�Ă��������B�������ʂ̓N���b�v�{�[�h�ɓ]������܂��B";
					// 
					// chkField
					// 
					this.chkField.Checked = true;
					this.chkField.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkField.Location = new System.Drawing.Point(14, 18);
					this.chkField.Name = "chkField";
					this.chkField.Size = new System.Drawing.Size(104, 20);
					this.chkField.TabIndex = 0;
					this.chkField.Text = "�t�B�[���h��";
					// 
					// chkTable
					// 
					this.chkTable.Checked = true;
					this.chkTable.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkTable.Location = new System.Drawing.Point(14, 40);
					this.chkTable.Name = "chkTable";
					this.chkTable.Size = new System.Drawing.Size(180, 20);
					this.chkTable.TabIndex = 1;
					this.chkTable.Text = "�e�[�u����";
					// 
					// groupBox1
					// 
					this.groupBox1.Controls.Add(this.chkSynonym);
					this.groupBox1.Controls.Add(this.chkField);
					this.groupBox1.Controls.Add(this.chkTable);
					this.groupBox1.Controls.Add(this.chkView);
					this.groupBox1.Location = new System.Drawing.Point(16, 66);
					this.groupBox1.Name = "groupBox1";
					this.groupBox1.Size = new System.Drawing.Size(200, 112);
					this.groupBox1.TabIndex = 2;
					this.groupBox1.TabStop = false;
					this.groupBox1.Text = "�����Ώ�";
					// 
					// chkSynonym
					// 
					this.chkSynonym.Checked = true;
					this.chkSynonym.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkSynonym.Location = new System.Drawing.Point(14, 84);
					this.chkSynonym.Name = "chkSynonym";
					this.chkSynonym.Size = new System.Drawing.Size(180, 20);
					this.chkSynonym.TabIndex = 2;
					this.chkSynonym.Text = "Synonym ��";
					// 
					// chkView
					// 
					this.chkView.Checked = true;
					this.chkView.CheckState = System.Windows.Forms.CheckState.Checked;
					this.chkView.Location = new System.Drawing.Point(14, 62);
					this.chkView.Name = "chkView";
					this.chkView.Size = new System.Drawing.Size(180, 20);
					this.chkView.TabIndex = 1;
					this.chkView.Text = "View��";
					// 
					// groupBox2
					// 
					this.groupBox2.Controls.Add(this.rdoPre);
					this.groupBox2.Controls.Add(this.rdoInclude);
					this.groupBox2.Controls.Add(this.rdoExtract);
					this.groupBox2.Location = new System.Drawing.Point(246, 64);
					this.groupBox2.Name = "groupBox2";
					this.groupBox2.Size = new System.Drawing.Size(204, 110);
					this.groupBox2.TabIndex = 3;
					this.groupBox2.TabStop = false;
					this.groupBox2.Text = "�������@";
					// 
					// rdoPre
					// 
					this.rdoPre.Location = new System.Drawing.Point(14, 50);
					this.rdoPre.Name = "rdoPre";
					this.rdoPre.TabIndex = 1;
					this.rdoPre.Text = "�O����v";
					// 
					// rdoInclude
					// 
					this.rdoInclude.Checked = true;
					this.rdoInclude.Location = new System.Drawing.Point(14, 20);
					this.rdoInclude.Name = "rdoInclude";
					this.rdoInclude.TabIndex = 0;
					this.rdoInclude.TabStop = true;
					this.rdoInclude.Text = "�B������";
					// 
					// rdoExtract
					// 
					this.rdoExtract.Location = new System.Drawing.Point(14, 80);
					this.rdoExtract.Name = "rdoExtract";
					this.rdoExtract.TabIndex = 2;
					this.rdoExtract.Text = "���S��v";
					// 
					// chkShowObjectSelector
					// 
					this.chkShowObjectSelector.Location = new System.Drawing.Point(18, 184);
					this.chkShowObjectSelector.Name = "chkShowObjectSelector";
					this.chkShowObjectSelector.Size = new System.Drawing.Size(306, 24);
					this.chkShowObjectSelector.TabIndex = 7;
					this.chkShowObjectSelector.Text = "�������ʂ����ɃI�u�W�F�N�g��I������";
					// 
					// SearchCondition
					// 
					this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
					this.ClientSize = new System.Drawing.Size(480, 242);
					this.Controls.Add(this.chkShowObjectSelector);
					this.Controls.Add(this.groupBox2);
					this.Controls.Add(this.groupBox1);
					this.Controls.Add(this.label1);
					this.MaximizeBox = false;
					this.Name = "SearchCondition";
					this.Text = "�t�B�[���h����";
					this.Load += new System.EventHandler(this.SearchCondition_Load);
					this.Controls.SetChildIndex(this.btnCancel, 0);
					this.Controls.SetChildIndex(this.chkReturn, 0);
					this.Controls.SetChildIndex(this.label1, 0);
					this.Controls.SetChildIndex(this.groupBox1, 0);
					this.Controls.SetChildIndex(this.groupBox2, 0);
					this.Controls.SetChildIndex(this.chkShowObjectSelector, 0);
					this.Controls.SetChildIndex(this.txtInput, 0);
					this.Controls.SetChildIndex(this.btnGo, 0);
					this.Controls.SetChildIndex(this.btnHistory, 0);
					this.groupBox1.ResumeLayout(false);
					this.groupBox2.ResumeLayout(false);
					this.ResumeLayout(false);

				}
		#endregion

		internal override void btnGo_Click(object sender, System.EventArgs e)
		{
			SelectSql = this.txtInput.Text;
			if( SelectSql == string.Empty )
			{
				MessageBox.Show("�����������w�肵�Ă�������");
				return;
			}
			if( this.chkField.Checked == false &&
				this.chkTable.Checked == false &&
				this.chkView.Checked == false &&
				this.chkSynonym.Checked == false )
			{
				MessageBox.Show("�����Ώۂ͕K����ȏ�w�肵�ĉ�����");
			}
			this.HasReturn = false;
			this.DialogResult = DialogResult.OK;
			qdbeUtil.SetNewHistory("",this.txtInput.Text,this.DHistory);
		}

		/// <summary>
		/// ��ʂ̏����ݒ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchCondition_Load(object sender, System.EventArgs e)
		{
			if( this.pSqlVersion.CanUseSynonym == false)
			{
				this.chkSynonym.Visible = false;
				this.chkSynonym.Enabled = false;
				this.chkSynonym.Checked = false;
			}
			else
			{
				this.chkSynonym.Visible = true;
				this.chkSynonym.Enabled = true;
				this.chkSynonym.Checked = true;
			}
		}
	}
}

