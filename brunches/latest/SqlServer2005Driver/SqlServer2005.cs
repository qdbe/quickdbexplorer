using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Threading;
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
		protected int	queryTimeout;

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
		/// <param name="sqlConnection">�R�l�N�V�������</param>
		/// <param name="timeout">�R�}���h���s�^�C���A�E�g�l</param>
		public void SetConnection(IDbConnection sqlConnection, int timeout)
		{
			this.sqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection;
			this.queryTimeout = timeout;
		}

		/// <summary>
		/// SQLSERVER�ɑ΂���R�l�N�V�����������
		/// </summary>
		public void CloseConnection()
		{
			this.sqlConnect.Close();
		}

		/// <summary>
		/// �^�C���A�E�g�l��ݒ肵�Ȃ���
		/// </summary>
		/// <param name="timeout"></param>
		public void SetTimeout(int timeout)
		{
			this.queryTimeout = timeout;
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
			sqlCmd.CommandTimeout = this.queryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// DataAdapter �� IDbCommand �� SelectCommand�Ƃ��Ċ֘A�Â���
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
		{
			if( da == null )
			{
				throw new ArgumentNullException("da");
			}
			if( cmd == null )
			{
				throw new ArgumentNullException("cmd");
			}
			((SqlDataAdapter)da).SelectCommand = (SqlCommand)cmd;
		}

		/// <summary>
		/// IDbCommand ��V�K�ɍ쐬����B
		/// �������A�R�}���h������A�R�l�N�V�������ƃ^�C���A�E�g�l�͂��łɃZ�b�g����Ă���
		/// </summary>
		/// <param name="stSql">���s����R�}���h������</param>
		/// <returns></returns>
		public IDbCommand		NewSqlCommand(string stSql)
		{
			SqlCommand	sqlCmd = new SqlCommand(stSql,this.sqlConnect);
			sqlCmd.CommandTimeout = this.queryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// �g�����U�N�V���������擾����
		/// </summary>
		public IDbTransaction	SetTransaction(IDbCommand cmd)
		{
			if( cmd == null )
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.Transaction = this.sqlConnect.BeginTransaction();
			return cmd.Transaction;
		}

		/// <summary>
		/// select �R�}���h����Aupdate, insert, delete �R�}���h�𐶐����Ȃ���
		/// </summary>
		/// <param name="da"></param>
		public void	SetCommandBuilder(DbDataAdapter da)
		{
			if( da == null )
			{
				throw new ArgumentNullException("da");
			}
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
			if( dr == null )
			{
				throw new ArgumentNullException("dr");
			}
			if( col < 0 )
			{
				throw new ArgumentException("col must greater equal 0");
			}
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
		public void SetDatabase(string dbName)
		{
			this.sqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// �e�[�u���ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		public string GetTableListColumnName()
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
		/// <param name="isDisplayTable">�e�[�u����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isSynonym">�V�m�j����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayFunction">Function��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplaySP">�X�g�A�h�v���V�[�W����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="ownerList">�����Owner�̃e�[�u���̂ݕ\������ꍇ�� IN��ɗ��p����J���}��؂蕶�����n��</param>
		/// <returns></returns>
		public string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
		{
			string retsql = "";

			string destObj = "";

			ArrayList ar = new ArrayList();

			if( isDisplayTable == true )
			{
				ar.Add("U");
			}

			if( isDisplayView == true )
			{
				ar.Add("V");
			}

			if( isSynonym == true )
			{
				ar.Add("SN");
			}

			if( isDisplayFunction == true )
			{
				ar.Add("FN");
			}

			if( isDisplaySP == true )
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

			retsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,@"select 
	t1.name as tbname, 
	t2.name as uname ,
	t1.type as tvs,
	t1.create_date as cretime,
	isnull(t3.base_object_name,'') as synbase,
	isnull(t4.type, ' ') as synType
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
				ownerList.Length != 0 )
			{
				retsql += " and t2.name in ( " + ownerList + " ) ";
			}

			return retsql;
		}


		/// <summary>
		/// Owner �̈ꗗ���擾����SQL�𐶐�����
		/// </summary>
		/// <param name="isDisplaySysUser"></param>
		/// <returns></returns>
		public string	GetOwnerList(bool isDisplaySysUser)
		{
			if( isDisplaySysUser )
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
		/// ISQL ���N������B
		/// </summary>
		/// <param name="serverRealName">�T�[�o�[��</param>
		/// <param name="instanceName">�C���X�^���X��</param>
		/// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
		/// <param name="dbName">�f�[�^�x�[�X��</param>
		/// <param name="logOnUserId">���O�C��ID</param>
		/// <param name="logOnPassword">���O�C���p�X���[�h</param>
		public void	CallISQL(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			this.CallEPM(serverRealName, instanceName, isUseTrust, dbName, logOnUserId, logOnPassword);
		}

		/// <summary>
		/// EnterPriseManager���N������
		/// </summary>
		/// <param name="serverRealName">�T�[�o�[��</param>
		/// <param name="instanceName">�C���X�^���X��</param>
		/// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
		/// <param name="dbName">�f�[�^�x�[�X��</param>
		/// <param name="logOnUserId">���O�C��ID</param>
		/// <param name="logOnPassword">���O�C���p�X���[�h</param>
		public void	CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "SqlWb";
			string serverstr = "";
			if( instanceName.Length != 0 )
			{
				serverstr = serverRealName + "\\" + instanceName;
			}
			else
			{
				serverstr = serverRealName;
			}
			if( isUseTrust == true )
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -E -nosplash",
						serverstr,
						dbName
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -E -nosplash",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -U {2} -P {3} -nosplash",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -U {1} -P {2} -nosplash",
						serverstr,
						logOnUserId,
						logOnPassword );
				}
			}
			
			isqlProcess.StartInfo.ErrorDialog = true;

			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}

		/// <summary>
		/// Profiler���N������
		/// </summary>
		/// <param name="serverRealName">�T�[�o�[��</param>
		/// <param name="instanceName">�C���X�^���X��</param>
		/// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
		/// <param name="dbName">�f�[�^�x�[�X��</param>
		/// <param name="logOnUserId">���O�C��ID</param>
		/// <param name="logOnPassword">���O�C���p�X���[�h</param>
		public void	CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "profiler90.exe";
			isqlProcess.StartInfo.ErrorDialog = true;
			string serverstr = "";
			if( instanceName.Length != 0 )
			{
				serverstr = serverRealName + "\\" + instanceName;
			}
			else
			{
				serverstr = serverRealName;
			}
			if( isUseTrust == true )
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture,"/S{0} /D{1} /E ",
						serverstr,
						dbName
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture,"/S{0} /E ",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," /S{0} D{1} /U{2} /P{3} ",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," /S{0} /U{1} /P{2} ",
						serverstr,
						logOnUserId,
						logOnPassword );
				}
			}
			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}


		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂���DROP ���𐶐�����
		/// </summary>
		/// <param name="databaseObjectInfo"></param>
		/// <returns></returns>
		public string	GetDDLDropStr(DBObjectInfo databaseObjectInfo)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			switch( databaseObjectInfo.ObjType )
			{
				case	"U":
					return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP TABLE {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
				case	"V":
					return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP VIEW {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
				case	"SN":
					if( databaseObjectInfo.SynonymBaseType == "U" )
					{
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP TABLE {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
					}
					else
					{
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP VIEW {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
					}
				default:
					return	string.Empty;
			}
		}

		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂��� Create ���𐶐�����
		/// </summary>
		/// <param name="databaseObjectInfo"></param>
		/// <param name="useParentheses"></param>
		/// <returns></returns>
		public string	GetDdlCreateString(DBObjectInfo databaseObjectInfo, bool useParentheses)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			StringBuilder strline =  new StringBuilder();
			TextWriter	wr = new StringWriter(strline,System.Globalization.CultureInfo.CurrentCulture);

			if( databaseObjectInfo.ObjType == "U" )
			{
				int		maxRow = databaseObjectInfo.FieldInfo.Count;
				if( useParentheses )
				{
					wr.Write("Create table {0} ", databaseObjectInfo.FormalName);
				}
				else
				{
					wr.Write("Create table {0} ", databaseObjectInfo.ToString());
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
					if( useParentheses )
					{
						wr.Write("\t[{0}]", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Name);
					}
					else
					{
						wr.Write("\t{0}", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Name);
					}
					wr.Write("\t");
					// �^
					valtype = ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).TypeName;

					wr.Write("\t");

					if( useParentheses )
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
						if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Length == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Length);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},{1})", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Prec,
							((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Xscale);
					}
					wr.Write("\t");
						
					if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Collation.Length != 0)
					{
						wr.Write("COLLATE {0}",((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Collation);
						wr.Write("\t");
					}

					if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IncSeed != 0)
					{
						wr.Write("\tIDENTITY({0},{1})",
							((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IncSeed,
							((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IncStep );
					}
						
					if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IsNullable == false )
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
			else
			{
				DataTable dt = new DataTable();
				dt.CaseSensitive = true;
				dt.Locale = System.Globalization.CultureInfo.CurrentCulture;


				string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"sp_helptext '{0}'", databaseObjectInfo.FormalName );
				SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
				da.SelectCommand.CommandTimeout = this.queryTimeout;
				da.Fill(dt);
				foreach(DataRow dr in dt.Rows)
				{
					wr.Write(dr["Text"].ToString());
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
			if( objTable == null )
			{
				throw new ArgumentNullException("objTable");
			}
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
		/// <param name="databaseObjectInfo">�ΏۂƂȂ�I�u�W�F�N�g</param>
		/// <param name="dt"></param>
		public void	AddObjectInfo(DBObjectInfo databaseObjectInfo, DataTable dt)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			if( dt == null )
			{
				throw new ArgumentNullException("dt");
			}
			string	strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from sys.all_objects where object_id = OBJECT_ID('{0}') ",
				databaseObjectInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			odt.Locale = System.Globalization.CultureInfo.CurrentCulture;
			da.Fill(odt);



			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["object_id"];
				dr[5] = odt.Rows[0]["modify_date"].ToString();
			}
			dr[1] = databaseObjectInfo.ObjName;
			dr[2] = databaseObjectInfo.Owner;
			dr[3] = GetObjTypeName(databaseObjectInfo.ObjType);
			dr[4] = databaseObjectInfo.CreateTime;
			if( databaseObjectInfo.IsSynonym )
			{
				dr[6] = databaseObjectInfo.SynonymBase;
				dr[7] = GetObjTypeName(databaseObjectInfo.SynonymBaseType);
			}
			else
			{
				dr[6] = string.Empty;
				dr[7] = string.Empty;
			}

			dt.Rows.Add(dr);
		}

		internal static string GetObjTypeName(string objType)
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
		public DataGetEventHandler ObjectDetailSet()
		{
			return new DataGetEventHandler(this.dbObjSet);
		}

		private	void	dbObjSet(object sender, System.EventArgs e)
		{
			DBObjectInfo	databaseObjectInfo = (DBObjectInfo)sender;
			DataSet		ds = new DataSet(databaseObjectInfo.ObjName);
			ds.CaseSensitive = true;
			ds.Locale = System.Globalization.CultureInfo.CurrentCulture;


			// �܂��͕K�v�ȏ���S�Ď��W����

			// FillSchema �ł̏����W
			string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from {0} where 0=1",
				databaseObjectInfo.FormalName );
			SqlDataAdapter da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable []dt = da.FillSchema(ds,SchemaType.Mapped,"schema");
			databaseObjectInfo.SchemaBaseInfo = dt[0];

			// ���ۂׂ̍������𒼐ڎ擾����
			SqlDataAdapter tableda = new SqlDataAdapter(
				string.Format(System.Globalization.CultureInfo.CurrentCulture,
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
				databaseObjectInfo.RealObjName
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
				addInfo.ColOrder = (int)fdr["colorder"];
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
					addInfo.TypeName = string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}]",
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
			databaseObjectInfo.FieldInfo = ar;
		}


		#endregion
	}
}
