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
	[System.Runtime.InteropServices.ComVisible(false)]
	public class QdbeDataGridTextBoxColumn : DataGridTextBoxColumn
	{
		private CurrencyManager _source;
		private int				editrow;
		private bool	canSetEmptyString;
		private bool	isThisImage;
		private DataGrid parentdg = new DataGrid();
        private int maxlength = 0;
        private DBFieldInfo FiledInfo{get;set;}

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
            this.maxlength = 0;
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
		/// �f�[�^�J�����ɉ����A�����I�ɁA
		/// �󔒕�����̐ݒ肪�\���ۂ�
		/// �Ǘ�����f�[�^���C���[�W���ۂ�
		/// ��ݒ肷��
		/// �܂��A�f�[�^�J�����ɉ����A�����I�ɏ�����ݒ肷��
		/// </summary>
		/// <param name="pa">�e�ƂȂ�O���b�h</param>
		/// <param name="col">����������f�[�^�J����</param>
		/// <param name="numberFormat">�����\���̏ꍇ�̏���</param>
		/// <param name="floatFormat">�����_�\���̏ꍇ�̏���</param>
		/// <param name="dateFormat">���t�\���̏ꍇ�̏���</param>
        /// <param name="finfo">�t�B�[���h���</param>
		public QdbeDataGridTextBoxColumn(
			DataGrid pa, 
			DataColumn col,
			string numberFormat,
			string floatFormat,
			string dateFormat,
            DBFieldInfo finfo
			) : this(pa,col)
		{
			if( col.DataType.FullName == "System.Int32" ||
				col.DataType.FullName == "System.Int16" ||
				col.DataType.FullName == "System.Int64" ||
				col.DataType.FullName == "System.UInt32" ||
				col.DataType.FullName == "System.UInt16" ||
				col.DataType.FullName == "System.UInt64"  )
			{
				this.Format = numberFormat;
			}
			if( col.DataType.FullName == "System.Double" ||
				col.DataType.FullName == "System.Single" || 
				col.DataType.FullName == "System.Decimal")
			{
				this.Format = floatFormat;
			}
			if( col.DataType.FullName == "System.DateTime" )
			{
				this.Format = dateFormat;
			}
            if (finfo != null)
            {
                this.maxlength = finfo.Length;
                if (this.maxlength < 0)
                {
                    this.maxlength = 0;
                }
            }
            FiledInfo = finfo;
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
			if(	( e.KeyCode == Keys.D3 ||
				e.KeyCode == Keys.W ) &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				// �o�C�i���̏ꍇ�A�C���[�W�\�����s���Ă݂�
				Object obj = GetColumnValueAtRow(this._source, this.editrow );
                if (obj is byte[] || ( this.FiledInfo != null && this.FiledInfo.IsBinary))
                {
                    try
                    {
                        //Notice a subtlety in this code that is particular of the Northwind 
                        //database but has no relevance in general. 
                        //The original Access database was converted into SQL Server's Northwind database, 
                        //so the image field called Photo doesn't contain a true GIF file; 
                        //instead it contains the OLE object that Access builds to wrap any image. 
                        //As a result, the stream of bytes you read from the field is prefixed with a header 
                        //you must strip off to get the bits of the image. 
                        //Such a header is variable-length and also depends on the length of the originally imported file's name. 
                        //For Northwind, the length of this offset is 78 bytes.
                        //
                        int[] offsets = new int[] { 0, 78 };
                        int maxLen = (obj as byte[]).Length;
                        for (int i = 0; i < offsets.Length; i++)
                        {
                            if (maxLen < offsets[i])
                            {
                                continue;
                            }

                            MemoryStream ms = new MemoryStream(obj as byte[], offsets[i], maxLen - offsets[i]);
                            try
                            {
                                Image gazo = Image.FromStream(ms);
                                if (gazo != null)
                                {
                                    ImageViewer viewdlg = new ImageViewer(this.parentdg.ReadOnly);
                                    viewdlg.ViewImage = gazo;
                                    if (viewdlg.ShowDialog() == DialogResult.OK)
                                    {
                                        this.SetColumnValueAtRow(this._source, this.editrow, viewdlg.GetBytes());
                                    }
                                    return;
                                }
                            }
                            catch
                            {
                                ;
                            }
                        }
                    }
                    catch
                    {
                        ;
                    }

                    // �摜�ȊO
                    // �o�C�i���\���_�C�A���O���o��
                    // �\����...

                    quickDBExplorer.Forms.Dialog.BinaryEditor bdlg = new quickDBExplorer.Forms.Dialog.BinaryEditor(obj as byte[], this.maxlength, this.parentdg.ReadOnly);
                    if (bdlg.ShowDialog() == DialogResult.OK)
                    {
                        this.SetColumnValueAtRow(this._source, this.editrow, bdlg.CurrentData);
                    }
                    return;
                }


				// �摜�E�o�C�i���ȊO�̏ꍇ�A�g��\���_�C�A���O�Œl��\��������
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
						dlg.EditText.Length != 0 )
					{
						this.TextBox.Text = dlg.EditText;
						SetColumnValueAtRow(this._source, this.editrow, dlg.EditText);
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
				SetColumnValueAtRow(this._source, this.editrow, "");
			}
		}

		/// <summary>
		/// �ҏW�J�n�������̃I�[�o�[���C�h
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="bounds"></param>
		/// <param name="readOnly"></param>
		/// <param name="displayText"></param>
		/// <param name="cellIsVisible"></param>
		protected override void Edit(CurrencyManager source,
			int rowNum, Rectangle bounds, bool readOnly,
			string displayText, bool cellIsVisible)
		{
			this._source = source;
			this.editrow = rowNum;
			if( this.isThisImage == true )
			{
				//this.TextBox.Text = "";
				base.Edit(source, rowNum, bounds, readOnly, displayText, cellIsVisible);
			}
			else
			{
				base.Edit(source, rowNum, bounds, readOnly, displayText, cellIsVisible);
			}
		}

		/// <summary>
		/// NULL�l���͏����̃I�[�o�[���C�h
		/// </summary>
		protected override void EnterNullValue()
		{
			this.TextBox.Text = this.NullText;
			SetColumnValueAtRow(this._source, this.editrow, DBNull.Value);
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
				(cellValue as string).IndexOf("\r\n",StringComparison.CurrentCulture) >= 0 ||
				(cellValue as string).IndexOf("\n", StringComparison.CurrentCulture) >= 0))
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
