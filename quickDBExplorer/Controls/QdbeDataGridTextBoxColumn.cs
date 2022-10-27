using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace quickDBExplorer
{
	/// <summary>
	/// �f�[�^�O���b�h�̕\���`�����Ǘ�����
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class QdbeDataGridTextBoxColumn : DataGridViewTextBoxColumn
	{
		//private CurrencyManager _source;
		//private int				editrow;
		private bool canSetEmptyString;
		private bool isThisImage;
		private DataGridView parentdg = new DataGridView();

		/// <summary>
		/// �t�B�[���h���
		/// </summary>
		public DBFieldInfo FiledInfo { get; private set; }

		/// <summary>
		/// ����l�̕�
		/// </summary>
		public int DefalutWidth { get; private set; }

		private int maxlength = 0;
		/// <summary>
		/// ���͉\�ȍő啶����
		/// </summary>
		public int MaxLength
		{
			get { return maxlength; }
			private set { this.maxlength = value; }
		}

		/// <summary>
		/// �󕶎����ݒ�ł��邩�ۂ�
		/// </summary>
		public bool CanSetEmptyString
		{
			get { return this.canSetEmptyString; }
			private set { this.canSetEmptyString = value; }
		}

		/// <summary>
		/// NULL��ݒ�ł��邩�ۂ�
		/// </summary>
		public bool CanSetNull { get; protected set; }

		/// <summary>
		/// �Ǘ�����f�[�^���C���[�W���ۂ����擾�E�ݒ肷��
		/// </summary>
		public bool IsThisImage
		{
			get { return this.isThisImage; }
			set 
			{ 
				this.isThisImage  = value; 
				if(this.DataGridView != null && this.isThisImage == true )
				{
                    this.ReadOnly = true;
				}
			}
		}

		/// <summary>
		/// ����̃w�b�_�̔w�i�F
		/// </summary>
		protected Color DefaultHeaderBackColor;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="pa">�e�ƂȂ�O���b�h</param>
		/// <param name="canset">�󔒕�����̐ݒ肪�\���ۂ�</param>
		public QdbeDataGridTextBoxColumn(DataGridView pa, bool canset) : this(pa,canset,false)
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
		public QdbeDataGridTextBoxColumn(DataGridView pa, DataColumn col) : this(pa,false,false)
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
			this.DataPropertyName = col.ColumnName;
			if( col.AllowDBNull == true )
			{
				this.HeaderText = "��"+col.ColumnName;
				this.CanSetNull = true;
			}
			else
			{
				bool isPKey = false;
				if (col.Table != null &&
					col.Table.PrimaryKey != null && 
					col.Table.PrimaryKey.Length != 0)
				{
					for (int j = 0; j < col.Table.PrimaryKey.Length; j++)
					{
						if (col.Table.PrimaryKey[j].ColumnName == col.ColumnName)
						{
							isPKey = true;
							break;
						}
					}

				}
				if (isPKey == true)
				{
					this.HeaderText = "��"+col.ColumnName;
				}
				else
				{
					this.HeaderText = col.ColumnName;
				}
				this.CanSetNull = false;
			}
			this.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
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
		/// <param name="setting">�O���b�h�\������</param>
        /// <param name="finfo">�t�B�[���h���</param>
		public QdbeDataGridTextBoxColumn(
			DataGridView pa, 
			DataColumn col,
			GridFormatSetting setting,
            DBFieldInfo finfo
			) : this(pa,col)
		{

			if ( col.DataType.FullName == "System.Int32" ||
				col.DataType.FullName == "System.Int16" ||
				col.DataType.FullName == "System.Int64" ||
				col.DataType.FullName == "System.UInt32" ||
				col.DataType.FullName == "System.UInt16" ||
				col.DataType.FullName == "System.UInt64"  )
			{
				this.DefaultCellStyle.Format = setting.GridNumberFormatPlain;
			}
			if( col.DataType.FullName == "System.Double" ||
				col.DataType.FullName == "System.Single" || 
				col.DataType.FullName == "System.Decimal")
			{
				this.DefaultCellStyle.Format = setting.GridFloatFormatPlain;
			}
			if( col.DataType.FullName == "System.DateTime" )
			{
				this.DefaultCellStyle.Format = setting.GridDateFormatPlain;
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
			this.Tag = finfo;
		}


		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="pa">�e�ƂȂ�O���b�h</param>
		/// <param name="canset">�󔒕�����̐ݒ肪�\���ۂ�</param>
		/// <param name="isImage">�Ǘ�����f�[�^���C���[�W���ۂ�</param>
		public QdbeDataGridTextBoxColumn(DataGridView pa, bool canset, bool isImage)
		{
			// DataGridView.DefaultCellStyle.NullValue �Őݒ肷�ׂ�
			this.DefaultCellStyle.NullValue = string.Empty;
			// Mainform ��EditingControlShowing�ɂđΉ�
			this.canSetEmptyString = canset;
			this.IsThisImage = isImage;
			this.parentdg = pa;
			this.SortMode = DataGridViewColumnSortMode.Automatic;
			this.Width = 75;
			this.DefalutWidth = this.Width;
			this.DefaultHeaderBackColor = this.HeaderCell.Style.BackColor;
		}


		/// <summary>
		/// ��������l�ɖ߂�
		/// </summary>
		public void ResetWidth2Defalt()
		{
			this.Width = this.DefalutWidth;
		}

		/// <summary>
		/// �Œ��̐F�ɕύX����
		/// </summary>
		public void SetFreezeColor()
		{
			// # c1e4e9 = �������炠��
			this.HeaderCell.Style.BackColor = Color.FromArgb(0xc1, 0xe4, 0xe9);
		}

		/// <summary>
		/// �ʏ��̐F�ɕύX����
		/// </summary>
		public void UnsetFreezeColor()
		{
			this.HeaderCell.Style.BackColor = this.DefaultHeaderBackColor;
		}

		/// <summary>
		/// �l���`�F�b�N����
		/// </summary>
		/// <param name="val"></param>
		/// <param name="errmsg"></param>
		/// <returns></returns>
		public bool CheckValue(object val, out string errmsg)
		{
			errmsg = string.Empty;
			if (!this.CanSetNull)
			{
				if (val == null || val == DBNull.Value)
				{
					return false;
				}
			}
			if (this.FiledInfo.CheckValue(val, out errmsg))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
