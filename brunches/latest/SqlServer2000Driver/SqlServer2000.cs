using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2000 �̊T�v�̐����ł��B
	/// </summary>
	public class SqlServerDriver : ISqlInterface
	{

		/// <summary>
		/// �R�l�N�V����
		/// </summary>
		protected System.Data.SqlClient.SqlConnection sqlConnect;

		/// <summary>
		/// SelectCommand ���̃^�C���A�E�g�l
		/// </summary>
		protected int	_timeout;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public SqlServerDriver()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		#region ISqlInterface �����o

		/// <summary>
		/// SQLServer�ɑ΂���R�l�N�V���������Ǘ�����
		/// </summary>
		/// <param name="sqlConnection1"></param>
		public void SetConnection(IDbConnection sqlConnection1)
		{
			this.sqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection1;
		}

		/// <summary>
		/// �^�C���A�E�g�l��ݒ肵�Ȃ���
		/// </summary>
		/// <param name="timeout"></param>
		public void SetTimeout(int timeout)
		{
			this._timeout = timeout;
		}

		/// <summary>
		/// DataAdapter ���擾����
		/// </summary>
		public DbDataAdapter NewDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>
		/// IDbCommand ��V�K�ɍ쐬����B
		/// �������A�R�l�N�V�������ƃ^�C���A�E�g�l�͂��łɃZ�b�g����Ă���
		/// </summary>
		/// <returns></returns>
		public IDbCommand	NewSqlCommand()
		{
			SqlCommand	sqlCmd = new SqlCommand();
			sqlCmd.Connection = this.sqlConnect;
			sqlCmd.CommandTimeout = this._timeout;
			return sqlCmd;
		}

		/// <summary>
		/// IDbCommand ��V�K�ɍ쐬����B
		/// �������A�R�}���h������A�R�l�N�V�������ƃ^�C���A�E�g�l�͂��łɃZ�b�g����Ă���
		/// </summary>
		/// <param name="sqlstr">���s����R�}���h������</param>
		/// <returns></returns>
		public IDbCommand		NewSqlCommand(string sqlstr)
		{
			SqlCommand	sqlCmd = new SqlCommand(sqlstr,this.sqlConnect);
			sqlCmd.CommandTimeout = this._timeout;
			return sqlCmd;
		}

		/// <summary>
		/// DataAdapter �� IDbCommand �� SelectCommand�Ƃ��Ċ֘A�Â���
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
		{
			((SqlDataAdapter)da).SelectCommand = (SqlCommand)cmd;
		}

		/// <summary>
		/// �g�����U�N�V���������擾����
		/// </summary>
		public IDbTransaction	SetTransaction(IDbCommand cmd)
		{
			cmd.Transaction = this.sqlConnect.BeginTransaction();
			return cmd.Transaction;
		}

		/// <summary>
		/// select �R�}���h����Aupdate, insert, delete �R�}���h�𐶐����Ȃ���
		/// </summary>
		/// <param name="da"></param>
		public void	SetCommandBuilder(DbDataAdapter da)
		{
			SqlCommandBuilder  cb = new SqlCommandBuilder((SqlDataAdapter)da);
		}

		/// <summary>
		/// DataReader����byte�z���ǂݍ��ށB
		/// �w�肳�ꂽ�t�B�[���h�͂��Ƃ��ƃo�C�i���f�[�^�ł��邱�Ƃ��O��
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public byte[]	GetDataReaderBytes(IDataReader dr, int col)
		{
			return ((SqlDataReader)dr).GetSqlBinary(col).Value;
		}


		/// <summary>
		/// DB�̈ꗗ�\�����擾����SQL����Ԃ�
		/// </summary>
		/// <returns></returns>
		public string GetDBSelect()
		{
			return "SELECT name FROM sysdatabases order by name";
		}

		/// <summary>
		/// �w�肳�ꂽ�f�[�^�x�[�X�ւƐڑ���ύX����
		/// </summary>
		/// <param name="dbName">�ύX��̃f�[�^�x�[�X��</param>
		/// <returns></returns>
		public void SetDataBase(string dbName)
		{
			this.sqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// �e�[�u���ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		public string GetTbListColName()
		{
			return "Table/View";
		}


		/// <summary>
		/// DB�I�[�i�[�̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel1()
		{
			return "owner/Role(&O)";
		}

		/// <summary>
		/// ���W�I�{�^���̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel2()
		{
			return "�I�[�i�[���E�e�[�u����";
		}

		/// <summary>
		/// �I�u�W�F�N�g�ꗗ�̕\���pSQL�̎擾
		/// </summary>
		/// <param name="isDspTable">�e�[�u����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDspView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDspSyn">�V�m�j����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDspFunc">Function��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDspSP">�X�g�A�h�v���V�[�W����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="ownerList">�����Owner�̃e�[�u���̂ݕ\������ꍇ�� IN��ɗ��p����J���}��؂蕶�����n��</param>
		/// <returns></returns>
		public string GetDspObjList(bool isDspTable, bool isDspView, bool isDspSyn, bool isDspFunc, bool isDspSP, string ownerList)
		{
			string retsql = "";

			string destObj = "";

			ArrayList ar = new ArrayList();

			if( isDspTable == true )
			{
				ar.Add("U");
			}

			if( isDspView == true )
			{
				ar.Add("V");
			}

			if( isDspSyn == true )
			{
				ar.Add("SN");
			}

			if( isDspFunc == true )
			{
				ar.Add("FN");
			}

			if( isDspSP == true )
			{
				ar.Add("P");
				ar.Add("PC");
			}

			if( ar.Count == 0 )
			{
				// �����w�肪�Ȃ���΁A�e�[�u���݂̂ɂ��Ă���
				ar.Add("U");
			}

			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					destObj += ",";
				}
				destObj += "'" + (string)ar[i] + "'";
			}
			retsql = string.Format(@"select 
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					xtype as tvs,
					sysobjects.crdate as cretime,
					'' as synbase,
					'' as syntype
					from sysobjects, sysusers 
					where xtype in ( {0} ) and sysobjects.uid = sysusers.uid ",
				destObj );

			if( ownerList != null && 
				ownerList != string.Empty )
			{
				retsql += " and sysusers.name in ( " + ownerList + " ) ";
			}

			return retsql;
		}

		/// <summary>
		/// �t�B�[���h���X�g�擾���̃_�~�[�N�G���𐶐�����
		/// </summary>
		/// <returns></returns>
		public string GetDspFldListDummy()
		{
			return string.Format(
				@"select * from sysindexes where 0=1" );
		}

		/// <summary>
		/// Owner �̈ꗗ���擾����SQL�𐶐�����
		/// </summary>
		/// <param name="isDspSysUser"></param>
		/// <returns></returns>
		public string	GetOwnerList(bool isDspSysUser)
		{
			if( isDspSysUser )
			{
				return "select * from sysusers order by name";
			}
			else
			{
				return "select * from sysusers where name not like 'db[_]%' order by name";
			}
		}

		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂���DROP ���𐶐�����
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <returns></returns>
		public string	GetDDLDropStr(DBObjectInfo dboInfo)
		{
			switch( dboInfo.ObjType )
			{
				case	"U":
					return string.Format("DROP TABLE {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
				case	"V":
					return string.Format("DROP VIEW {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
				case	"SN":
					if( dboInfo.SynonymBaseType == "U" )
					{
						return string.Format("DROP TABLE {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
					}
					else
					{
						return string.Format("DROP VIEW {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
					}
				default:
					return	string.Empty;
			}
		}


		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂��� Create ���𐶐�����
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <param name="usekakko"></param>
		/// <returns></returns>
		public string	GetDDLCreateStr(DBObjectInfo dboInfo, bool usekakko)
		{
			StringBuilder strline =  new StringBuilder();
			TextWriter	wr = new StringWriter(strline);

			if( dboInfo.ObjType == "U" )
			{

				string sqlstr = "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + dboInfo.Owner +"' and sysobjects.name = '" + dboInfo.ObjName + "' order by syscolumns.colorder";

				SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnect);
				DataSet ds = new DataSet();
				ds.CaseSensitive = true;
				da.Fill(ds,dboInfo.ToString());

				int		maxRow = ds.Tables[dboInfo.ToString()].Rows.Count;
				if( usekakko )
				{
					wr.Write("Create table {0} ", dboInfo.FormalName);
				}
				else
				{
					wr.Write("Create table {0} ", dboInfo.ToString());
				}
				wr.Write(" ( {0}",wr.NewLine);
				string	valtype;
				for( int i = 0; i < maxRow ; i++ )
				{
					if( i != 0 )
					{
						wr.Write(",{0}",wr.NewLine);
					}
					//�t�B�[���h��
					if( usekakko )
					{
						wr.Write("\t[{0}]", ds.Tables[dboInfo.ToString()].Rows[i][0]);
					}
					else
					{
						wr.Write("\t{0}", ds.Tables[dboInfo.ToString()].Rows[i][0]);
					}
					wr.Write("\t");
					// �^
					valtype = (string)ds.Tables[dboInfo.ToString()].Rows[i][1];

					wr.Write("\t");

					if( usekakko )
					{
						wr.Write("[{0}]",valtype);
					}
					else
					{
						wr.Write(valtype);
					}
					if( valtype == "varchar" ||
						valtype == "varbinary" ||
						valtype == "nvarchar" ||
						valtype == "char" ||
						valtype == "nchar" ||
						valtype == "binary" )
					{
						if( (Int16)ds.Tables[dboInfo.ToString()].Rows[i][3] == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ds.Tables[dboInfo.ToString()].Rows[i][3]);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},", ds.Tables[dboInfo.ToString()].Rows[i][3]);
						wr.Write("{0})", ds.Tables[dboInfo.ToString()].Rows[i][4]);
					}
					wr.Write("\t");
						
					if( !ds.Tables[dboInfo.ToString()].Rows[i].IsNull("collation"))
					{
						wr.Write("COLLATE {0}",ds.Tables[dboInfo.ToString()].Rows[i]["collation"]);
						wr.Write("\t");
					}
						
					if( (int)ds.Tables[dboInfo.ToString()].Rows[i]["isnullable"] == 0 )
					{
						wr.Write("\tNOT NULL");
					}
					else
					{
						wr.Write("\tNULL");
					}
				}
				wr.Write("{0}){0}Go{0}",wr.NewLine);
			}
			return strline.ToString();
		}

		/// <summary>
		/// �I�u�W�F�N�g�����Z�b�g����DataTable������������
		/// </summary>
		/// <param name="objTable">�ΏۂƂ���DataTable</param>
		public void	InitObjTable(DataTable objTable)
		{
			objTable.Columns.Add("�I�u�W�F�N�gID",typeof(int));
			objTable.Columns.Add("�I�u�W�F�N�g��");
			objTable.Columns.Add("���L��");
			objTable.Columns.Add("�I�u�W�F�N�g�̌^");
			objTable.Columns.Add("�쐬����");
		}

		/// <summary>
		/// �I�u�W�F�N�g�̏����ADataTable �ɒǉ�����
		/// </summary>
		/// <param name="dboInfo">�ΏۂƂȂ�I�u�W�F�N�g</param>
		/// <param name="dt"></param>
		public void	AddObjectInfo(DBObjectInfo dboInfo, DataTable dt)
		{
			string	strsql = string.Format("select * from sysobjects where id = OBJECT_ID('{0}') ",
				dboInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			da.Fill(odt);



			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["id"];
			}
			dr[1] = dboInfo.ObjName;
			dr[2] = dboInfo.Owner;
			dr[4] = dboInfo.CreateTime;

			switch( dboInfo.ObjType )
			{
				case	"C":
					dr[3] = "CHECK ����";
					break;
				case	"D":
					dr[3] = "Default �܂��� DEFAULT ����";
					break;
				case	"F":
					dr[3] = "FOREIGN KEY ����";
					break;
				case	"L":
					dr[3] = "���O";
					break;
				case	"FN":
					dr[3] = "�X�J���֐�";
					break;
				case	"IF":
					dr[3] = "�C�����C�� �e�[�u���֐�";
					break;
				case	"P":
					dr[3] = "�X�g�A�h �v���V�[�W��";
					break;
				case	"PK":
					dr[3] = "PRIMARY KEY ���� (�^�C�v K)";
					break;
				case	"RF":
					dr[3] = "���v���P�[�V���� �t�B���^ �X�g�A�h �v���V�[�W��";
					break;
				case	"S":
					dr[3] = "�V�X�e�� �e�[�u��";
					break;
				case	"TF":
					dr[3] = "�e�[�u���֐�";
					break;
				case	"TR":
					dr[3] = "�g���K";
					break;
				case	"U":
					dr[3] = "���[�U�[ �e�[�u��";
					break;
				case	"UQ":
					dr[3] = "UNIQUE ���� (�^�C�v K)";
					break;
				case	"V":
					dr[3] = "�r���[";
					break;
				case	"X":
					dr[3] = "�g���X�g�A�h �v���V�[�W��";
					break;
			}
			dt.Rows.Add(dr);
		}

		/// <summary>
		/// �I�u�W�F�N�g�̏ڍ׏����Z�b�g����C�x���g�n���h����Ԃ�
		/// </summary>
		/// <returns></returns>
		public DBObjectInfo.DataGetEventHandler ObjectDetailSet()
		{
			return new DBObjectInfo.DataGetEventHandler(this.dbObjSet);
		}

		private	void	dbObjSet(object sender)
		{
			DBObjectInfo	dboInfo = (DBObjectInfo)sender;
			DataSet		ds = new DataSet(dboInfo.ObjName);


			// �܂��͕K�v�ȏ���S�Ď��W����

			// FillSchema �ł̏����W
			string strsql = string.Format("select * from {0} where 0=1",
				dboInfo.FormalName );
			SqlDataAdapter da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable []dt = da.FillSchema(ds,SchemaType.Mapped,"schema");
			dboInfo.SchemaBaseInfo = dt[0];

			// ���ۂׂ̍������𒼐ڎ擾����
			SqlDataAdapter tableda = new SqlDataAdapter(
				string.Format(
				@"select 
syscolumns.name colname, 
systypes.name valtype, 
convert(int,syscolumns.length) as length, 
isnull(convert(int,syscolumns.prec),0) as prec, 
isnull(convert(int,syscolumns.xscale),0) as xscale, 
convert(int,syscolumns.colid) as colid, 
convert(int,syscolumns.colorder) as colorder, 
syscolumns.isnullable, 
syscolumns.collation, 
sysobjects.id  
from 
	sysobjects, 
	syscolumns, 
	sysusers, 
	systypes 
where 
	sysobjects.id = syscolumns.id 
and sysobjects.uid= sysusers.uid 
and syscolumns.xusertype=systypes.xusertype 
and sysobjects.id = OBJECT_ID('{0}')
order by syscolumns.colorder",
				dboInfo.RealObjName
				),
				this.sqlConnect );
			tableda.Fill(ds,"fieldList");

			DBFieldInfo addInfo;
			ArrayList	ar = new ArrayList();
			foreach(DataRow fdr in ds.Tables["fieldList"].Rows )
			{
				// �t�B�[���h�̏��ł��邮��܂���āA�Z�b�g���Ă���
				addInfo = new DBFieldInfo();
				addInfo.Col = ds.Tables["schema"].Columns[fdr["colname"].ToString()];
				addInfo.Colid = (int)fdr["colid"];
				if( fdr["collation"] != DBNull.Value )
				{
					addInfo.Collation = (string)fdr["collation"];
				}
				addInfo.Colorder = (int)fdr["colorder"];
				if( addInfo.Col.MaxLength < 0 &&
					(int)fdr["length"] > 0 )
				{
					addInfo.Length = (int)fdr["length"];
				}
				else
				{
					addInfo.Length = addInfo.Col.MaxLength;
				}
				addInfo.Prec = (int)fdr["prec"];
				addInfo.Xscale = (int)fdr["xscale"];
				addInfo.TypeName = (string)fdr["valtype"];
				// �v���C�}���L�[���ǂ������`�F�b�N
				for(int i = 0; i < ds.Tables["schema"].PrimaryKey.Length; i++ )
				{
					if( addInfo.Col.ColumnName == ds.Tables["schema"].PrimaryKey[i].ColumnName )
					{
						addInfo.PrimaryKeyOrder = i;
						break;
					}
				}
				ar.Add(addInfo);
			}
			dboInfo.FieldInfo = ar;
		}

		#endregion
	}
}
