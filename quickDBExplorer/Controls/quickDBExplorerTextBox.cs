using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows;
using System.Text.RegularExpressions;

namespace quickDBExplorer
{
	/// <summary>
	/// �e�L�X�g�{�b�N�X�� Ctrl+S���������ꂽ�ꍇ�̃C�x���g�n���h��
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ShowHistoryEventHandler(
		object sender,
		System.EventArgs e
	);

	/// <summary>
	/// �e�L�X�g�{�b�N�X�� Ctrl+W���������ꂽ�ꍇ�̃C�x���g�n���h��
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ShowZoomEventHandler(
		object sender,
		System.EventArgs e
	);

	/// <summary>
	/// CTRL+A�ł̑S�I���@�\�ACTRL+D�ł̕����폜��ǉ������e�L�X�g�{�b�N�X
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class quickDBExplorerTextBox : TextBox
	{
		/// <summary>
		/// �e�L�X�g���̓G���A�ł̉E�N���b�N���j���[
		/// </summary>
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuCopy;
		private System.Windows.Forms.MenuItem menuCut;
		private System.Windows.Forms.MenuItem menuPaste;
		private System.Windows.Forms.MenuItem menuAllSelect;
		private System.Windows.Forms.MenuItem menuAllDelete;
		private System.Windows.Forms.MenuItem menuShowHistory;
		private System.Windows.Forms.MenuItem menuShowZoom;

		/// <summary>
		/// Ctrl+S�������ꂽ�ꍇ�̃C�x���g
		/// </summary>
		public event ShowHistoryEventHandler ShowHistory = null;

		/// <summary>
		/// Ctrl+S�������ꂽ�ꍇ�̃C�x���g
		/// </summary>
		public event ShowZoomEventHandler ShowZoom = null;


        private Dictionary<string, TextHistoryDataSet> pHistories;
        /// <summary>
        /// ���͗�������
        /// </summary>
        public Dictionary<string, TextHistoryDataSet> Histories {
            get { return this.pHistories; }
            set
            {
                this.pHistories = value;
                ResetHistoryMemuItem();
            }
        }

        /// <summary>
        /// �����L�[
        /// </summary>
        private string pHistoryKey { get; set; }

        /// <summary>
        /// �����L�[
        /// </summary>
        public string HistoryKey
        {
            get {
                if (pHistoryKey == null)
                {
                    pHistoryKey = this.Name;
                }
                return pHistoryKey; 
            }
            set
            {
                pHistoryKey = value;
                ResetHistoryMemuItem();
            }
        }

        private void ResetHistoryMemuItem()
        {
            if (Histories == null)
            {
                if (this.contextMenu1.MenuItems.Contains(this.menuShowHistory))
                {
                    this.contextMenu1.MenuItems.Remove(this.menuShowHistory);
                }
            }
            else
            {
                if (!this.contextMenu1.MenuItems.Contains(this.menuShowHistory))
                {
                    this.menuShowHistory.Index = this.contextMenu1.MenuItems.Count + 1;
                    this.contextMenu1.MenuItems.Add(this.menuShowHistory);
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
		public TextHistoryDataSet History
		{
			get {
                if (this.Histories == null)
                {
                    return null;
                }
                if (!Histories.ContainsKey(HistoryKey))
                {
                    Histories.Add(pHistoryKey, new TextHistoryDataSet());
                }
                return Histories[HistoryKey];
            }
		}

		/// <summary>
		/// �l�̊g��\�����\�Ƃ��邩�ۂ�
		/// </summary>
		private bool pShowZoom = false;

		/// <summary>
		/// �l�̊g��\�����\�Ƃ��邩�ۂ�
		/// </summary>
		public bool IsShowZoom
		{
			get { return pShowZoom; }
			set { 
				pShowZoom = value;
				if (this.pShowZoom == true)
				{
					if (!this.contextMenu1.MenuItems.Contains(this.menuShowZoom))
					{
						this.menuShowHistory.Index = this.contextMenu1.MenuItems.Count + 1;
						this.contextMenu1.MenuItems.Add(this.menuShowZoom);
					}
				}
				else
				{
					if (this.contextMenu1.MenuItems.Contains(this.menuShowZoom))
					{
						this.contextMenu1.MenuItems.Remove(this.menuShowZoom);
					}
				}
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public quickDBExplorerTextBox() : base()
		{
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuCopy = new System.Windows.Forms.MenuItem();
			this.menuCut = new System.Windows.Forms.MenuItem();
			this.menuPaste = new System.Windows.Forms.MenuItem();
			this.menuAllSelect = new System.Windows.Forms.MenuItem();
			this.menuAllDelete = new System.Windows.Forms.MenuItem();
			this.menuShowHistory = new System.Windows.Forms.MenuItem();
			this.menuShowZoom = new System.Windows.Forms.MenuItem();

			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuCopy,
            this.menuCut,
            this.menuPaste,
            this.menuAllSelect,
            this.menuAllDelete
			});
			// 
			// menuCopy
			// 
			this.menuCopy.Index = 0;
			this.menuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuCopy.Text = "�R�s�[";
			this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
			// 
			// menuCut
			// 
			this.menuCut.Index = 1;
			this.menuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuCut.Text = "�؂���";
			this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
			// 
			// menuPaste
			// 
			this.menuPaste.Index = 2;
			this.menuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuPaste.Text = "�\��t��";
			this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
			// 
			// menuAllSelect
			// 
			this.menuAllSelect.Index = 3;
			this.menuAllSelect.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.menuAllSelect.Text = "�S�đI��";
			this.menuAllSelect.Click += new System.EventHandler(this.menuAllSelect_Click);
			// 
			// menuAllSelect
			// 
			this.menuAllDelete.Index = 4;
			this.menuAllDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
			this.menuAllDelete.Text = "�S�č폜";
			this.menuAllDelete.Click += new EventHandler(menuAllDelete_Click);

			// 
			// menuShowHistory
			// 
			this.menuShowHistory.Index = 5;
			this.menuShowHistory.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuShowHistory.Text = "����\��";
			this.menuShowHistory.Click += new EventHandler(menuShowHistory_Click);

			// 
			// menuShowHistory
			// 
			this.menuShowZoom.Index = 6;
			this.menuShowZoom.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
			this.menuShowZoom.Text = "�l�g��\��";
			this.menuShowZoom.Click += new EventHandler(menuShowZoom_Click);

			this.ContextMenu = this.contextMenu1;

			this.KeyDown += new KeyEventHandler(quickDBExplorerTextBox_KeyDown);
			this.Enter +=new EventHandler(quickDBExplorerTextBox_Enter);
			this.TextChanged +=new EventHandler(quickDBExplorerTextBox_TextChanged);
		}

		private void menuAllDelete_Click(object sender, EventArgs e)
		{
			this.Text = string.Empty;
		}

		/// <summary>
		/// Ctrl+S ���������ꂽ�ꍇ�ɒl�̗���\���C�x���g���Ăяo��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuShowHistory_Click(object sender, EventArgs e)
		{
			// �������o�^����Ă���Η���I����ʂ�\������
			if (this.History != null &&
				this.ShowHistory != null)
			{
				this.ShowHistory(this, new EventArgs());
			}
		}

		/// <summary>
		/// Ctrl+W ���������ꂽ�ꍇ�ɒl�̊g��\���C�x���g���Ăяo��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuShowZoom_Click(object sender, EventArgs e)
		{
			// �������o�^����Ă���Η���I����ʂ�\������
			if (this.IsShowZoom == true &&
				this.ShowZoom != null)
			{
				this.ShowZoom(this, new EventArgs());
			}
		}

		/// <summary>
		/// ���l�̂ݓ��͉\�ɂ��邩�ۂ�
		/// </summary>
		private bool pIsDigitOnly = false;

		/// <summary>
		/// ���l�̂ݓ��͉\�ɂ��邩�ۂ�
		/// false: ���l�ȊO�����͉\
		/// true: ���l�̂݉\
		/// </summary>
		public bool IsDigitOnly
		{
			get { return this.pIsDigitOnly; }
			set { this.pIsDigitOnly = value; }
		}

		///// <summary>
		///// MultiLine �Ɍ��炸 Ctrl + A �őS�I�������邩�ۂ�
		///// </summary>
		//private bool forceSelectAll = false;

		///// <summary>
		///// MultiLine �Ɍ��炸 Ctrl + A �őS�I�������邩�ۂ�
		///// </summary>
		//public bool ForceSelectAll
		//{
		//    get { return forceSelectAll; }
		//    set { forceSelectAll = value; }
		//}


		private bool pCanCtrlDelete = true;
		
		/// <summary>
		/// CTRL+D�ŕ������폜�\�ɂ��邩�ۂ�
		/// false: �폜�ł��Ȃ�
		/// true: �폜�ł���(����l)
		/// </summary>
		public bool CanCtrlDelete
		{
			get { return this.pCanCtrlDelete; }
			set { this.pCanCtrlDelete = value; }
		}

		/// <summary>
		/// �ύX�O�̃e�L�X�g
		/// </summary>
		private string orgString;

		/// <summary>
		/// Enter���̃e�L�X�g
		/// </summary>
		private string enterString = string.Empty;

		/// <summary>
		/// Enter ��A�l�ɕύX�����������ۂ���Ԃ�
		/// Focus ���Ȃ��ꍇ�A���false��Ԃ�
		/// </summary>
		public bool IsTextChanged
		{
			get
			{
				if (this.Focused == true)
				{
					if (this.enterString == this.Text)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				else
				{
					return false;
				}
			}
		}

		private void menuCopy_Click(object sender, System.EventArgs e)
		{
			this.Copy();
		}

		private void menuCut_Click(object sender, System.EventArgs e)
		{
			this.Cut();
		}

		private void menuPaste_Click(object sender, System.EventArgs e)
		{
			this.Paste();
		}

		private void menuAllSelect_Click(object sender, System.EventArgs e)
		{
			this.SelectAll();
		}

		/// <summary>
		/// �L�[�_�E���C�x���g�n���h��
		/// CTRL+A�ł̑S�I��������ǉ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ev"></param>
		internal virtual void quickDBExplorerTextBox_KeyDown(object sender, KeyEventArgs ev)
		{
//			(((TextBox)sender).Multiline != true ||
//				forceSelectAll == true ) &&
			if( ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.A )
			{
				this.SelectAll();
				ev.Handled = true;
			}

			// Ctrl�{S�ŉߋ����͗����_�C�A���O��\������
			if (ev.Alt == false &&
				ev.Control == true &&
				ev.KeyCode == Keys.S)
			{
				if (this.History != null &&
					this.ShowHistory != null)
				{
					this.ShowHistory(this,new EventArgs());
				}
			}

			// Ctrl�{W�ŉߋ����͗����_�C�A���O��\������
			if (ev.Alt == false &&
				ev.Control == true &&
				ev.KeyCode == Keys.W)
			{
				// �������o�^����Ă���Η���I����ʂ�\������
				if (this.IsShowZoom == true &&
					this.ShowZoom != null)
				{
					this.ShowZoom(this, new EventArgs());
				}
			}


			if( this.pCanCtrlDelete == true &&
				ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.D )
			{
				this.Text = string.Empty;
			}
		}

		/// <summary>
		/// �l�g��_�C�A���O��\������
		/// </summary>
		/// <param name="labelName">Window�^�C�g��</param>
		/// <param name="dlgEnter">�_�C�A���O��OK�{�^�����������ꂽ���̃C�x���g�n���h��</param>
		public void DoShowZoom(string labelName, System.EventHandler dlgEnter)
		{
			ZoomFloatingDialog dlg = new ZoomFloatingDialog();
			dlg.EditText = this.Text;
			dlg.LableName = labelName;
			dlg.Enter += dlgEnter;
			dlg.Show();
			dlg.BringToFront();
			dlg.Focus();
		}

		/// <summary>
		/// ����I���_�C�A���O��\������
		/// </summary>
		/// <param name="key">������I������ۂ̃L�[���</param>
		public bool DoShowHistory(string key)
		{
			if (this.History == null)
			{
				return false;
			}
			// ���͗����̑I���_�C�A���O��\������
			HistoryViewer hv = new HistoryViewer(this.History, key);
			if (key != null)
			{
				hv.IsShowTable = true;
			}
			else
			{
				hv.IsShowTable = false;
			}
			if (DialogResult.OK == hv.ShowDialog())
			{
				if (this.Text != hv.RetString)
				{
					//�Ⴄ���ł���΁A�����\�����A�����Ƃ��Ēǉ�����
					this.Text = hv.RetString;
					qdbeUtil.SetNewHistory(key, hv.RetString, this.History);
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		private void quickDBExplorerTextBox_Enter(object sender, EventArgs e)
		{
			// �ҏW�J�n���_�̕�������L��
			this.orgString = this.Text;
			this.enterString = this.Text;
		}

		private void quickDBExplorerTextBox_TextChanged(object sender, EventArgs e)
		{
			string nowText = this.Text;
			if( this.pIsDigitOnly == true && nowText != string.Empty)
			{
				// ���l�݂̂̏ꍇ�A�����ύX�̌��� �����̂ݎc���Ă��邩�ǂ������`�F�b�N����
				if( Regex.IsMatch(nowText,"^[0-9]+$") == false )
				{
					// ��v���Ȃ��ꍇ�͌��ɖ߂��Ă��
					this.Text = this.orgString;
				}
				else
				{
					// ���݂̒l���ŐV�̕�����Ƃ��ċL�^���Ă����A��r�ޗ��ƃX��
					this.orgString = this.Text;
				}
			}
			else
			{
				this.orgString = this.Text;
			}
		}

        /// <summary>
        /// ������ۑ�����
        /// </summary>
        /// <param name="key"></param>
        public void SaveHistory(string key)
        {
            qdbeUtil.SetNewHistory(key,this.Text,this.History);
        }
	}
}
