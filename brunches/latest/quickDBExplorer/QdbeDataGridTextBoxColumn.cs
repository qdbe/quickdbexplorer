using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;


namespace quickDBExplorer
{
	/// <summary>
	/// �f�[�^�O���b�h�̕\���`�����Ǘ�����
	/// </summary>
	public class QdbeDataGridTextBoxColumn : DataGridTextBoxColumn
	{
		private CurrencyManager _sorce;
		private int				editrow;
		private bool	canSetEmptyString;
		private bool	isThisImage;
		private DataGrid parentdg = new DataGrid();

		/// <summary>
		/// �ҏW�����𒆒f����
		/// </summary>
		public void	CancelEdit()
		{
			this.HideEditBox();
		}

		/// <summary>
		/// �Ǘ�����f�[�^���C���[�W���ۂ����擾�E�ݒ肷��
		/// </summary>
		public bool IsThisImage
		{
			get { return this.isThisImage; }
			set 
			{ 
				this.isThisImage  = value; 
				if( this.isThisImage == true )
				{
					this.ReadOnly = true;
					this.TextBox.Text = "";
					this.TextBox.ReadOnly = true;
				}
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="pa">�e�ƂȂ�O���b�h</param>
		/// <param name="canset">�󔒕�����̐ݒ肪�\���ۂ�</param>
		public QdbeDataGridTextBoxColumn(DataGrid pa, bool canset) : this(pa,canset,false)
		{
		}

		/// <summary>
		/// �R���X�g���N�^
		/// �f�[�^�J�����ɉ����A�����I�ɁA
		/// �󔒕�����̐ݒ肪�\���ۂ�
		/// �Ǘ�����f�[�^���C���[�W���ۂ�
		/// ��ݒ肷��
		/// </summary>
		/// <param name="pa">�e�ƂȂ�O���b�h</param>
		/// <param name="col">����������f�[�^�J����</param>
		public QdbeDataGridTextBoxColumn(DataGrid pa, DataColumn col) : this(pa,false,false)
		{
			if( col.DataType.FullName == "System.String" )
			{
				this.canSetEmptyString = true;
				this.IsThisImage = false;
			}
			else if( col.DataType.FullName == "System.Byte[]" )
			{
				this.canSetEmptyString = false;
				this.IsThisImage = true;
			}
			else
			{
				this.canSetEmptyString = false;
				this.IsThisImage = false;
			}
			//�}�b�v�����w�肷��
			this.MappingName = col.ColumnName;
			if( col.AllowDBNull == true )
			{
				this.HeaderText = "��"+col.ColumnName;
			}
			else
			{
				this.HeaderText = col.ColumnName;
			}
		}


		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="pa">�e�ƂȂ�O���b�h</param>
		/// <param name="canset">�󔒕�����̐ݒ肪�\���ۂ�</param>
		/// <param name="isImage">�Ǘ�����f�[�^���C���[�W���ۂ�</param>
		public QdbeDataGridTextBoxColumn(DataGrid pa, bool canset, bool isImage)
		{
			this.NullText = "";
			this.TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridKeyDownControler);
			this.canSetEmptyString = canset;
			this.IsThisImage = isImage;
			this.TextBox.Enter += new EventHandler(this.GridTextControler);
			this.TextBox.GotFocus += new EventHandler(this.GridTextControler);
			this.TextBox.TextChanged += new EventHandler(this.GridTextControler);
			this.parentdg = pa;
		}

		/// <summary>
		/// TextBox�ւ̃C�x���g�n���h��
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void GridTextControler(object sender, EventArgs e)
		{
			if( isThisImage == true )
			{
				((TextBox)sender).Text = "";
			}
		}


		/// <summary>
		/// �O���b�h��ł̃L�[�_�E���C�x���g����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridKeyDownControler(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Ctrl+3 �Œl�̕ҏW�_�C�A���O��\��
			if(	e.KeyCode == Keys.D3 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				// �o�C�i���̏ꍇ�A�C���[�W�\�����s���Ă݂�
				Object obj = GetColumnValueAtRow(this._sorce, this.editrow );
				if( obj is byte[] )
				{
					MemoryStream ms = new MemoryStream((byte[])obj);
					try
					{
						Image gazo = Image.FromStream(ms);
						if( gazo != null )
						{
							ImageViewer viewdlg = new ImageViewer();
							viewdlg.ViewImage = gazo;
							viewdlg.ShowDialog();
							return;
						}
					}
					catch
					{
						return;
					}
				}


				// �摜�ȊO�̏ꍇ�A�g��\���_�C�A���O�Œl��\��������
				ZoomDialog dlg  = new ZoomDialog();
				dlg.EditText = this.TextBox.Text;
				if( this.TextBox.ReadOnly == true )
				{
					dlg.IsDispOnly = true;
					dlg.LableName = "�l�Q��";
					dlg.ShowDialog();
				}
				else
				{
					dlg.LableName = "�l�ҏW";
					if( dlg.ShowDialog() == DialogResult.OK &&
						dlg.EditText != "" )
					{
						this.TextBox.Text = dlg.EditText;
						SetColumnValueAtRow(this._sorce, this.editrow, dlg.EditText);
					}
				}
			}

			// �Q�Ƃ݂̂̏ꍇ�A����ȍ~�̏����͍s���K�v�Ȃ��B
			if( this.parentdg.ReadOnly == true )
			{
				e.Handled = true;
				return;
			}

			// CTRL+1��NULL�l�̓���
			if( e.KeyCode == Keys.D1 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				EnterNullValue();
			}
			if( this.TextBox.ReadOnly == true )
			{
				e.Handled = true;
				return;
			}
			// CTRL+2 �ŋ󔒕�����̓���
			if( canSetEmptyString == true&&
				e.KeyCode == Keys.D2 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				this.TextBox.Text = this.NullText;
				SetColumnValueAtRow(this._sorce, this.editrow, "");
			}
		}

		/// <summary>
		/// �ҏW�J�n�������̃I�[�o�[���C�h
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="bounds"></param>
		/// <param name="readOnly"></param>
		/// <param name="instantText"></param>
		/// <param name="cellIsVisible"></param>
		protected override void Edit(CurrencyManager source,
			int rowNum, Rectangle bounds, bool readOnly,
			string instantText, bool cellIsVisible)
		{
			this._sorce = source;
			this.editrow = rowNum;
			if( this.isThisImage == true )
			{
				//this.TextBox.Text = "";
				base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			}
			else
			{
				base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			}
		}

		/// <summary>
		/// NULL�l���͏����̃I�[�o�[���C�h
		/// </summary>
		protected override void EnterNullValue()
		{
			this.TextBox.Text = this.NullText;
			SetColumnValueAtRow(this._sorce, this.editrow, DBNull.Value);
		}

		/// <summary>
		/// Paint���\�b�h���I�[�o�[���C�h����
		/// </summary>
		/// <param name="g"></param>
		/// <param name="bounds"></param>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="backBrush"></param>
		/// <param name="foreBrush"></param>
		/// <param name="alignToRight"></param>
		protected override void Paint(Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum, 
			Brush backBrush,
			Brush foreBrush,
			bool alignToRight)
		{
			//�Z���̒l���擾����
			object cellValue =
				this.GetColumnValueAtRow(source, rowNum);
			if (cellValue == DBNull.Value)
			{
				// NULL�̏ꍇ�͐��F�ɒ��F
				backBrush = new SolidBrush(Color.FromArgb(0xbf,0xef,0xff));
			}
			if (cellValue is byte[])
			{
				// �o�C�i���f�[�^�̏ꍇ�A�R�o���g�O���[���ɒ��F
				backBrush = new SolidBrush(Color.FromArgb(0x10,0xC9,0x8D));
				g.FillRectangle(backBrush,bounds);
				return;
			}
			else if( cellValue is string && 
				( 
				((string)cellValue).IndexOf("\r\n") >= 0 ||
				((string)cellValue).IndexOf("\n") >= 0 ) )
			{
				// ������ŕ����s�ɂ킽��ꍇ�A�Ƃ��F�ɒ��F
				backBrush = new SolidBrush(Color.FromArgb(0xf4,0xb3,0xc2));
			}

			//��{�N���X��Paint���\�b�h���Ăяo��
			base.Paint(g, bounds, source, rowNum,
				backBrush, foreBrush, alignToRight);
		}
	}
}
