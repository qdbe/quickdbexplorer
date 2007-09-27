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
	/// SqlServer2005 �̊T�v�̐����ł��B
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
		/// DataAdapter �� IDbCommand �� SelectCommand�Ƃ��Ċ֘A�Â���
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
		{
			((SqlDataAdapter)da).SelectCommand = (SqlCommand)cmd;
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
			return "SELECT name FROM sys.databases  order by name";;
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
			return "Table/View/Synonym";
		}


		/// <summary>
		/// DB�I�[�i�[�̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel1()
		{
			return "Schema(&O)";
		}

		/// <summary>
		/// ���W�I�{�^���̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel2()
		{
			return "�X�L�[�}���E�e�[�u����";
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
	t1.name as tbname, 
	t2.name as uname ,
	t1.type as tvs,
	t1.create_date as cretime,
	isnull(t3.base_object_name,'') as synbase,
	isnull(t4.type, ' ') as syntype
from 
	sys.all_objects t1
	inner join 	sys.schemas t2 on 
		t1.schema_id = t2.schema_id
	left outer join sys.synonyms t3 on
		t1.object_id = t3.object_id 
	left outer join sys.all_objects t4 on
		OBJECT_ID(t3.base_object_name) = t4.object_id
where 
	( t1.type in ( {0} ) or
	( t1.type = 'SN' and 
	  t4.type in ( {0} ) ) )
",
				destObj );

			if( ownerList != null && 
				ownerList != string.Empty )
			{
				retsql += " and t2.name in ( " + ownerList + " ) ";
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
				@"select * from sys.indexes where 0=1" );
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
				return  "select * from sys.schemas order by name";
			}
			else
			{
				return @"select * from sys.schemas where name not in ( 'sys', 'INFORMATION_SCHEMA', 'guest', 'db_owner', 
							'db_accessadmin', 'db_securityadmin', 'db_ddladmin', 'db_backupoperator', 'db_datareader',
							'db_datawriter', 'db_denydatareader', 'db_denydatawriter'  ) order by name";
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
				int		maxRow = dboInfo.FieldInfo.Count;
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
						wr.Write("\t[{0}]", ((DBFieldInfo)dboInfo.FieldInfo[i]).Name);
					}
					else
					{
						wr.Write("\t{0}", ((DBFieldInfo)dboInfo.FieldInfo[i]).Name);
					}
					wr.Write("\t");
					// �^
					valtype = ((DBFieldInfo)dboInfo.FieldInfo[i]).TypeName;

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
						if( ((DBFieldInfo)dboInfo.FieldInfo[i]).Length == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ((DBFieldInfo)dboInfo.FieldInfo[i]).Length);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},{1})", ((DBFieldInfo)dboInfo.FieldInfo[i]).Prec,
							((DBFieldInfo)dboInfo.FieldInfo[i]).Xscale);
					}
					wr.Write("\t");
						
					if( ((DBFieldInfo)dboInfo.FieldInfo[i]).Collation != string.Empty)
					{
						wr.Write("COLLATE {0}",((DBFieldInfo)dboInfo.FieldInfo[i]).Collation);
						wr.Write("\t");
					}

					if( ((DBFieldInfo)dboInfo.FieldInfo[i]).IncSeed != 0)
					{
						wr.Write("\tIDENTITY({0},{1})",
							((DBFieldInfo)dboInfo.FieldInfo[i]).IncSeed,
							((DBFieldInfo)dboInfo.FieldInfo[i]).IncStep );
					}
						
					if( ((DBFieldInfo)dboInfo.FieldInfo[i]).IsNullable == false )
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
			objTable.Columns.Add("�I�u�W�F�N�gID");
			objTable.Columns.Add("�I�u�W�F�N�g��");
			objTable.Columns.Add("���L��");
			objTable.Columns.Add("�I�u�W�F�N�g�̌^");
			objTable.Columns.Add("�쐬����");
			objTable.Columns.Add("�X�V����");
			objTable.Columns.Add("�V�m�j���Q�Ɛ�");
			objTable.Columns.Add("�V�m�j���Q�Ɛ�I�u�W�F�N�g�̌^");
		}

		/// <summary>
		/// �I�u�W�F�N�g�̏����ADataTable �ɒǉ�����
		/// </summary>
		/// <param name="dboInfo">�ΏۂƂȂ�I�u�W�F�N�g</param>
		/// <param name="dt"></param>
		public void	AddObjectInfo(DBObjectInfo dboInfo, DataTable dt)
		{
			string	strsql = string.Format("select * from sys.all_objects where object_id = OBJECT_ID('{0}') ",
				dboInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			da.Fill(odt);



			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["object_id"];
				dr[5] = odt.Rows[0]["modify_date"].ToString();
			}
			dr[1] = dboInfo.ObjName;
			dr[2] = dboInfo.Owner;
			dr[3] = this.GetObjTypeName(dboInfo.ObjType);
			dr[4] = dboInfo.CreateTime;
			if( dboInfo.IsSynonym )
			{
				dr[6] = dboInfo.SynonymBase;
				dr[7] = this.GetObjTypeName(dboInfo.SynonymBaseType);
			}
			else
			{
				dr[6] = string.Empty;
				dr[7] = string.Empty;
			}

			dt.Rows.Add(dr);
		}

		internal string GetObjTypeName(string objType)
		{
			switch( objType )
			{

				case	"AF":	return "�W�v�֐� (CLR)";
				case	"C":	return "CHECK ����";
				case	"D":	return "DEFAULT (����܂��̓X�^���h�A����)";
				case	"F":	return "FOREIGN KEY ����";
				case	"PK":	return "PRIMARY KEY ����";
				case	"P":	return "SQL �X�g�A�h �v���V�[�W��";
				case	"PC":	return "�A�Z���u�� (CLR) �X�g�A�h �v���V�[�W��";
				case	"FN":	return "SQL �X�J���֐�";
				case	"FS":	return "�A�Z���u�� (CLR) �X�J���֐�";
				case	"FT":	return "�A�Z���u�� (CLR) �e�[�u���l�֐�";
				case	"R":	return "���[�� (���`���A�X�^���h�A����)";
				case	"RF":	return "���v���P�[�V���� �t�B���^ �v���V�[�W��";
				case	"SN":	return "�V�m�j��";
				case	"SQ":	return "�T�[�r�X �L���[";
				case	"TA":	return "�A�Z���u�� (CLR) �g���K";
				case	"TR":	return "SQL �g���K";
				case	"IF":	return "SQL �C�����C�� �e�[�u���l�֐�";
				case	"TF":	return "SQL �e�[�u���l�֐�";
				case	"U":	return "�e�[�u�� (���[�U�[��`)";
				case	"UQ":	return "UNIQUE ����";
				case	"V":	return "�r���[";
				case	"X":	return "�g���X�g�A�h �v���V�[�W��";
				case	"IT":	return "�����e�[�u��";
				default:
					return	string.Empty;
			}
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
	t1.name colname, 
	case 
	when t1.user_type_id != t1.system_type_id then 1
	else	0
	end	as isUserType,
	t2.name valtype, 
	t3.name	typeSchema, 
	t1.max_length	,
	t4.name as baseValType,
 convert(int,
	CASE 
	WHEN t1.user_type_id = t1.system_type_id and  t2.name IN (N'nchar', N'nvarchar') and t1.max_length != -1 THEN t1.max_length/2 
	WHEN t1.user_type_id != t1.system_type_id and  t4.name IN (N'nchar', N'nvarchar') and t1.max_length != -1 THEN t1.max_length/2 
	ELSE t1.max_length	
	end) as length, 
	convert(int,
	CASE 
	WHEN t1.user_type_id = t1.system_type_id and t2.name IN (N'nchar', N'nvarchar') THEN t1.max_length/2 
	WHEN t1.user_type_id != t1.system_type_id and t2.name IN (N'nchar', N'nvarchar') THEN t1.max_length/2 
	ELSE t1.precision	
	end ) as [prec], 
	convert(int,t1.scale) as xscale, 
	t1.column_id as colid, 
	t1.column_id as colorder, 
	convert(int,t1.is_nullable) as isnullable, 
	t1.collation_name as collation, 
	t1.object_id as id,
	case 
	when t5.object_id is not null then ident_seed('{0}') 
	else null
	end as seed,
	case 
	when t5.object_id is not null then ident_incr('{0}') 
	else null
	end as incr
from 
	sys.all_columns t1 
	inner join sys.types t2 on 
		t1.user_type_id  = t2.user_type_id  and 
		t1.system_type_id = t2.system_type_id 
	inner JOIN sys.schemas as t3 on 
		t2.schema_id = t3.schema_id
	left outer join sys.types t4 on
		t4.user_type_id = t1.system_type_id and
		t4.system_type_id = t4.user_type_id 
	left outer join sys.identity_columns t5 on
		t1.object_id = t5.object_id and 
		t1.column_id = t5.column_id
where 
	t1.object_id = OBJECT_ID('{0}')
order by colorder",
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
				addInfo.Length = (int)fdr["length"];
				addInfo.Prec = (int)fdr["prec"];
				addInfo.Xscale = (int)fdr["xscale"];
				if( (int)fdr["isUserType"] == 0 )
				{
					// �V�X�e���̒�`
					addInfo.TypeName = (string)fdr["valtype"];
				}
				else
				{
					// ���[�U�[��`�^
					addInfo.TypeName = string.Format("[{0}].[{1}]",
							(string)fdr["typeSchema"] ,
							(string)fdr["valtype"] );
				}
				if( fdr["seed"] != DBNull.Value )
				{
					addInfo.IncSeed = (decimal)fdr["seed"];
				}
				if( fdr["incr"] != DBNull.Value )
				{
					addInfo.IncStep = (decimal)fdr["incr"];
				}
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
