using System;
using System.Collections;
using System.Collections.Generic;
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
	public class SqlServerDriver2005 : ISqlInterface
	{

		/// <summary>
		/// �R�l�N�V����
		/// </summary>
		protected System.Data.SqlClient.SqlConnection pSqlConnect;

		/// <summary>
		/// �R�l�N�V����
		/// </summary>
		protected System.Data.SqlClient.SqlConnection SqlConnect
		{
			get { return pSqlConnect; }
			set { pSqlConnect = value; }
		}

		/// <summary>
		/// SelectCommand ���̃^�C���A�E�g�l
		/// </summary>
		protected int pQueryTimeout;

		/// <summary>
		/// SelectCommand ���̃^�C���A�E�g�l
		/// </summary>
		protected int QueryTimeout
		{
			get { return pQueryTimeout; }
			set { pQueryTimeout = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public SqlServerDriver2005()
		{
		}
		#region ISqlInterface �����o

		/// <summary>
		/// SQLServer�ɑ΂���R�l�N�V���������Ǘ�����
		/// </summary>
		/// <param name="sqlConnection">�R�l�N�V�������</param>
		/// <param name="timeout">�R�}���h���s�^�C���A�E�g�l</param>
		public virtual void SetConnection(IDbConnection sqlConnection, int timeout)
		{
			this.pSqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection;
			this.pQueryTimeout = timeout;
		}

		/// <summary>
		/// SQLSERVER�ɑ΂���R�l�N�V�����������
		/// </summary>
		public virtual void CloseConnection()
		{
			this.pSqlConnect.Close();
		}

		/// <summary>
		/// �^�C���A�E�g�l��ݒ肵�Ȃ���
		/// </summary>
		/// <param name="timeout"></param>
		public virtual void SetTimeout(int timeout)
		{
			this.pQueryTimeout = timeout;
		}

		/// <summary>
		/// DataAdapter ���擾����
		/// </summary>
		public virtual DbDataAdapter NewDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>
		/// IDbCommand ��V�K�ɍ쐬����B
		/// �������A�R�l�N�V�������ƃ^�C���A�E�g�l�͂��łɃZ�b�g����Ă���
		/// </summary>
		/// <returns></returns>
		public virtual IDbCommand	NewSqlCommand()
		{
			SqlCommand	sqlCmd = new SqlCommand();
			sqlCmd.Connection = this.pSqlConnect;
			sqlCmd.CommandTimeout = this.pQueryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// DataAdapter �� IDbCommand �� SelectCommand�Ƃ��Ċ֘A�Â���
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public virtual void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
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
		public virtual IDbCommand		NewSqlCommand(string stSql)
		{
			SqlCommand	sqlCmd = new SqlCommand(stSql,this.pSqlConnect);
			sqlCmd.CommandTimeout = this.pQueryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// �g�����U�N�V���������擾����
		/// </summary>
		public virtual IDbTransaction	SetTransaction(IDbCommand cmd)
		{
			if( cmd == null )
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.Transaction = this.pSqlConnect.BeginTransaction();
			return cmd.Transaction;
		}

		/// <summary>
		/// select �R�}���h����Aupdate, insert, delete �R�}���h�𐶐����Ȃ���
		/// </summary>
		/// <param name="da"></param>
		public virtual void	SetCommandBuilder(DbDataAdapter da)
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
		public virtual byte[]	GetDataReaderBytes(IDataReader dr, int col)
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
		/// DataReader����DateTimeOffset�l��ǂݍ��ށB
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public virtual DateTimeOffset GetDataReaderDateTimeOffSet(IDataReader dr, int col)
		{
			throw new InvalidOperationException("GetDataReaderDateTimeOffSet �� SQL Server 2005 �ł͗��p�ł��܂���");
		}

		/// <summary>
		/// DB�̈ꗗ�\�����擾����SQL����Ԃ�
		/// </summary>
		/// <returns></returns>
		public virtual string GetDBSelect()
		{
			return "SELECT name FROM sys.databases  order by name";;
		}

		/// <summary>
		/// �w�肳�ꂽ�f�[�^�x�[�X�ւƐڑ���ύX����
		/// </summary>
		/// <param name="dbName">�ύX��̃f�[�^�x�[�X��</param>
		/// <returns></returns>
		public virtual void SetDatabase(string dbName)
		{
			this.pSqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// �I�u�W�F�N�g�ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		public virtual string GetTableListColumnName()
		{
			return "Table/View/Synonym";
		}


		/// <summary>
		/// DB�I�[�i�[�̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		public virtual string GetOwnerLabel1()
		{
			return "Schema(&O)";
		}

		/// <summary>
		/// ���W�I�{�^���̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		public virtual string GetOwnerLabel2()
		{
			return "�X�L�[�}���E�I�u�W�F�N�g��";
		}

		/// <summary>
		/// �I�u�W�F�N�g�ꗗ�̕\���pSQL�̎擾
		/// </summary>
		/// <param name="isDisplayTable">�e�[�u����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isSynonym">�V�m�j����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayFunction">Function��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplaySP">�X�g�A�h�v���V�[�W����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="ownerList">�����Owner�̃I�u�W�F�N�g�̂ݕ\������ꍇ�� IN��ɗ��p����J���}��؂蕶�����n��</param>
		/// <returns></returns>
		public virtual string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
		{
			string retsql = "";

			string destObj = "";

			List<string> ar = new List<string>();

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
				destObj += "'" + ar[i] + "'";
			}

			retsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,@"select 
	t1.name as tbname, 
	t2.name as uname ,
	t1.type as tvs,
	t1.create_date as cretime,
	isnull(t3.base_object_name,'') as synbase,
	isnull(t4.type, ' ') as synType,
    t1.object_id  as objectid
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
		public virtual string	GetOwnerList(bool isDisplaySysUser)
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
		public virtual void	CallISQL(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
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
		public virtual void	CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
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
            isqlProcess.StartInfo.FileName = this.sqlVersion.ManagementExe;
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
		public virtual void	CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}

            Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(this.sqlVersion.regkey, false);
			string profilerPath = string.Empty;
			if (rkey != null)
			{
				bool isPathExists = true;
				object robj = rkey.GetValue("Path");
				if (robj != null)
				{
					profilerPath = robj.ToString();
				}
				if (profilerPath == string.Empty)
				{
					isPathExists = false;
					robj = rkey.GetValue("SQLPath");
					if (robj != null)
					{
						profilerPath = robj.ToString();
					}
				}
				if (profilerPath != null)
				{
					if (profilerPath.EndsWith(@"\") == false)
					{
						profilerPath += @"\";
					}
					if (isPathExists == false)
					{
						profilerPath += this.sqlVersion.BinDir;
					}
				}
			}

			Process isqlProcess = new Process();
            isqlProcess.StartInfo.FileName = profilerPath + this.sqlVersion.ProfilerExe;
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
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," /S{0} /D{1} /U{2} /P{3} ",
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
		public virtual string	GetDDLDropStr(DBObjectInfo databaseObjectInfo)
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
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP TABLE {0}{1}GO{1}{1}", databaseObjectInfo.RealObjName,Environment.NewLine);
					}
					else
					{
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP VIEW {0}{1}GO{1}{1}", databaseObjectInfo.RealObjName,Environment.NewLine);
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
		public virtual string	GetDdlCreateString(DBObjectInfo databaseObjectInfo, bool useParentheses)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			StringBuilder strline =  new StringBuilder();
			TextWriter	wr = new StringWriter(strline,System.Globalization.CultureInfo.CurrentCulture);

			if( databaseObjectInfo.RealObjType == "U" )
			{
				//int		maxRow = databaseObjectInfo.FieldInfo.Count;
				if( useParentheses )
				{
					wr.Write("Create table {0} ", databaseObjectInfo.RealObjName);
				}
				else
				{
					wr.Write("Create table {0} ", databaseObjectInfo.RealObjNameNoPare);
				}
				wr.Write(" ( {0}",wr.NewLine);

                int i = 0;
                foreach(DBFieldInfo each in databaseObjectInfo.FieldInfo)
                {
					if( i != 0 )
					{
						wr.Write(",{0}",wr.NewLine);
					}
                    wr.Write("\t");
                    //�t�B�[���h��
					if( useParentheses )
					{
						wr.Write(each.FormalName);
					}
					else
					{
						wr.Write(each.Name);
					}
					wr.Write("\t");
					// �^
                    wr.Write(each.GetDDLTypeString(useParentheses));

                    i++;
				}
				wr.Write("{0}){0}Go{0}",wr.NewLine);
			}
			else
			{
				DataTable dt = new DataTable();
				dt.CaseSensitive = true;
				dt.Locale = System.Globalization.CultureInfo.CurrentCulture;


				string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"sp_helptext '{0}'", databaseObjectInfo.RealObjName );
				SqlDataAdapter	da = new SqlDataAdapter(strsql,this.pSqlConnect);
				da.SelectCommand.CommandTimeout = this.pQueryTimeout;
				da.Fill(dt);
				// �A�������󔒍s�͗}������悤�ɂ���
				string pretext = "";
				foreach(DataRow dr in dt.Rows)
				{
					if( dr["Text"] != DBNull.Value &&
						dr["Text"] is string &&
						pretext == string.Empty &&
						dr["Text"].ToString().Trim() == string.Empty )
					{
						continue;
					}
					pretext = dr["Text"].ToString().Trim();
					wr.WriteLine(dr["Text"].ToString().Trim());
				}
				wr.WriteLine("Go");
			}
			return strline.ToString();
		}

		/// <summary>
		/// �I�u�W�F�N�g�����Z�b�g����DataTable������������
		/// </summary>
		/// <param name="objTable">�ΏۂƂ���DataTable</param>
		public virtual void	InitObjTable(DataTable objTable)
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
		public virtual void	AddObjectInfo(DBObjectInfo databaseObjectInfo, DataTable dt)
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
			SqlCommand	cm = new SqlCommand(strsql,this.pSqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.pSqlConnect);
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
		public virtual DataGetEventHandler ObjectDetailSet()
		{
			return new DataGetEventHandler(this.DatabaseObjSet);
		}

		/// <summary>
		/// �w�肳�ꂽ�I�u�W�F�N�g�̏ڍ׏����擾����
		/// sender �� �ΏۃI�u�W�F�N�g�� DBObjectInfo �^�Ƃ��ăZ�b�g����Ă��邱�Ƃ��O��
		/// </summary>
		/// <param name="sender">�Ώۂ̃I�u�W�F�N�g</param>
		/// <param name="e">�_�~�[</param>
        protected virtual void DatabaseObjSet(DBObjectInfo sender, System.EventArgs e)
		{
			DBObjectInfo	databaseObjectInfo = (DBObjectInfo)sender;
			DataSet		ds = new DataSet(databaseObjectInfo.ObjName);
			ds.CaseSensitive = true;
			ds.Locale = System.Globalization.CultureInfo.CurrentCulture;


			// �܂��͕K�v�ȏ���S�Ď��W����

			// FillSchema �ł̏����W
			string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from {0} where 0=1",
				databaseObjectInfo.FormalName );
			SqlDataAdapter da = new SqlDataAdapter(strsql,this.pSqlConnect);
			DataTable []dt = da.FillSchema(ds,SchemaType.Mapped,"schema");
            databaseObjectInfo.SetSchemaInfo(dt[0]);

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
	WHEN t1.user_type_id = t1.system_type_id and  t2.name IN (N'image') THEN -1
	WHEN t1.user_type_id != t1.system_type_id and  t4.name IN (N'image') THEN -1
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
	t1.is_identity,
	case 
	when t5.object_id is not null then ident_seed('{0}') 
	else 0
	end as seed,
	case 
	when t5.object_id is not null then ident_incr('{0}') 
	else 0
	end as incr,
	t6.assembly_id,
	t6.assembly_class,
	t6.assembly_qualified_name
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
	left outer join sys.assembly_types t6 on
		t6.user_type_id = t1.user_type_id
where 
	t1.object_id = @objid
order by colorder",
				databaseObjectInfo.RealObjName
				),
				this.pSqlConnect );
            tableda.SelectCommand.Parameters.Add(new SqlParameter("@objid", databaseObjectInfo.ObjId));
            tableda.Fill(ds, "fieldList");

			DBFieldInfo addInfo;
            databaseObjectInfo.ClearField();
            int i = 1; // column_id �� ��єԂɂȂ��Ă���\������
            foreach (DataRow fdr in ds.Tables["fieldList"].Rows )
			{
                // �t�B�[���h�̏��ł��邮��܂���āA�Z�b�g���Ă���
                fdr["colorder"] = i;
                i++;
                addInfo = GetDBFieldInfo(ds, fdr);
                databaseObjectInfo.AddField(addInfo);
			}
		}

        /// <summary>
        /// DB�̃t�B�[���h����DataSet����擾����
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="fdr"></param>
        /// <returns></returns>
        protected virtual DBFieldInfo GetDBFieldInfo(DataSet ds, DataRow fdr)
        {
            DBFieldInfo addInfo;
            addInfo = new DBFieldInfo();
            addInfo.Col = ds.Tables["schema"].Columns[fdr["colname"].ToString()];
            addInfo.Colid = (int)fdr["colid"];
            if (fdr["collation"] != DBNull.Value)
            {
                addInfo.Collation = (string)fdr["collation"];
            }
            addInfo.ColOrder = (int)fdr["colorder"];
            addInfo.Length = (int)fdr["length"];
            addInfo.Prec = (int)fdr["prec"];
            addInfo.Xscale = (int)fdr["xscale"];
            if ((int)fdr["isUserType"] == 0)
            {
                // �V�X�e���̒�`
                addInfo.TypeName = (string)fdr["valtype"];
            }
            else
            {
                // ���[�U�[��`�^
                addInfo.TypeName = string.Format(System.Globalization.CultureInfo.CurrentCulture, "[{0}].[{1}]",
                        (string)fdr["typeSchema"],
                        (string)fdr["valtype"]);
            }
            // �{���̌^��`
            if (DBNull.Value.Equals(fdr["baseValType"]) == true)
            {
                addInfo.RealTypeName = addInfo.TypeName;
            }
            else
            {
                addInfo.RealTypeName = (string)fdr["baseValType"];
            }
            if (fdr["is_identity"] != DBNull.Value &&
                (bool)fdr["is_identity"] == true)
            {
                addInfo.IsIdentity = true;
            }
            else
            {
                addInfo.IsIdentity = false;
            }
            if (fdr["seed"] != DBNull.Value)
            {
                addInfo.IncSeed = (decimal)fdr["seed"];
            }
            if (fdr["incr"] != DBNull.Value)
            {
                addInfo.IncStep = (decimal)fdr["incr"];
            }
            // �v���C�}���L�[���ǂ������`�F�b�N
            for (int i = 0; i < ds.Tables["schema"].PrimaryKey.Length; i++)
            {
                if (addInfo.Col.ColumnName == ds.Tables["schema"].PrimaryKey[i].ColumnName)
                {
                    addInfo.PrimaryKeyOrder = i;
                    break;
                }
            }
            if (fdr["assembly_id"] != DBNull.Value)
            {
                addInfo.AssemblyId = (int)fdr["assembly_id"];
            }
            if (fdr["assembly_class"] != DBNull.Value)
            {
                addInfo.AssemblyClassName = (string)fdr["assembly_class"];
            }
            if (fdr["assembly_qualified_name"] != DBNull.Value)
            {
                addInfo.AssemblyQFN = (string)fdr["assembly_qualified_name"];
            }
            return addInfo;
        }

		/// <summary>
		/// �t�B�[���h������������ SQL���𐶐�����
		/// </summary>
		/// <param name="searchCondition">�����Ώۂ̕���</param>
		/// <param name="searchType">�������@</param>
		/// <param name="isCaseSensitive">�啶������������ʂ��邩�ۂ�</param>
		/// <param name="limitSchema">�X�L�[�}�̍i���ݑΏ�</param>
		/// <returns></returns>
		public string	GetSearchFieldSql(
			string searchCondition, 
			quickDBExplorer.SearchType searchType, 
			List<string> limitSchema,
            ObjectSearchCondition condition)
		{
			searchCondition = searchCondition.Replace("'","''");
			if(condition.IsCaseSensitive == false )
			{
				searchCondition = searchCondition.ToLower();
			}

			string schemaFilter = string.Empty;
			if( limitSchema != null && 
				limitSchema.Count != 0 )
			{
				schemaFilter = "and t2.name in ( ";
				for( int j = 0; j < limitSchema.Count; j++ )
				{
					if( j != 0 )
					{
						schemaFilter += ",";
					}
					schemaFilter += "'" + limitSchema[j] + "'";
				}
				schemaFilter += ")";
			}

			string condSql = string.Empty;

			switch(searchType)
			{
				case SearchType.SearchContain:
					condSql = string.Format(" like '%{0}%' ", searchCondition );
					break;
				case SearchType.SearchStartWith:
					condSql = string.Format(" like '{0}%' ", searchCondition );
					break;
				case SearchType.SearchExact:
					condSql = string.Format(" = '{0}' ", searchCondition );
					break;
				default:
					break;
			}

			string addCondition = string.Empty;
			if( condition.IsCaseSensitive == true )
			{
				addCondition = " t3.name ";
			}
			else
			{
				addCondition = " LOWER(t3.name) ";
			}

            List<string> ar = new List<string>();

            if (condition.IsFieldTable == true)
            {
                ar.Add("U ");
                ar.Add("S ");
            }
            if (condition.IsFieldView == true)
            {
                ar.Add("V ");
            }
            if (condition.IsFieldSynonym == true)
            {
                ar.Add("SN");
            }

            string typeCondition = string.Empty;
            if( ar.Count != 0 )
            {
                typeCondition = " and t1.type in ( ";
                for (int ix = 0; ix < ar.Count; ix++)
                {
                    string eachtype = ar[ix];
                    if (ix != 0)
                    {
                        typeCondition += ",";
                    }
                    typeCondition += string.Format("'{0}'", eachtype);
                }
                typeCondition += ") ";
            }

			return string.Format(System.Globalization.CultureInfo.CurrentCulture,
				@"select t2.name as UserName, t1.name as ObjName, t3.name as FieldName
from
	sys.all_objects t1
	inner join sys.schemas t2 on
		t1.schema_id = t2.schema_id
	inner join sys.all_columns t3 on
		t1.object_id = t3.object_id
where
	{0} {1} {2} {3}", addCondition, condSql , schemaFilter, typeCondition
                );
		}

		/// <summary>
		/// �I�u�W�F�N�g������������ SQL���𐶐�����
		/// </summary>
		/// <param name="searchCondition">�����Ώۂ̕���</param>
		/// <param name="searchType">�������@</param>
		/// <param name="isCaseSensitive">�啶������������ʂ��邩�ۂ�</param>
		/// <param name="limitSchema">�X�L�[�}�̍i���ݑΏ�</param>
		/// <param name="isTable">�e�[�u�����������邩�ۂ�</param>
		/// <param name="isView">View���������邩�ۂ�</param>
		/// <param name="isSynonym">�V�m�j�����������邩�ۂ�</param>
		/// <param name="isFunction">�t�@���N�V�������������邩�ۂ�</param>
		/// <param name="isProcedure">�X�g�A�h�v���V�[�W���[���������邩�ۂ�</param>
		/// <returns></returns>
		public string	GetSearchObjectSql(
			string searchCondition, 
			quickDBExplorer.SearchType searchType, 
			List<string> limitSchema,
            ObjectSearchCondition condition)
		{
			searchCondition = searchCondition.Replace("'","''");
			if(condition.IsCaseSensitive== false )
			{
				searchCondition = searchCondition.ToLower();
			}

			string condSql = string.Empty;

			switch(searchType)
			{
				case SearchType.SearchContain:
					condSql = string.Format(" like '%{0}%' ", searchCondition );
					break;
				case SearchType.SearchStartWith:
					condSql = string.Format(" like '{0}%' ", searchCondition );
					break;
				case SearchType.SearchExact:
					condSql = string.Format(" = '{0}' ", searchCondition );
					break;
				default:
					break;
			}

			List<string> ar = new List<string>();

			if( condition.IsSearchTable == true )
			{
				ar.Add("U ");
				ar.Add("S ");
			}
			if(condition.IsSearchView == true )
			{
				ar.Add("V ");
			}
			if(condition.IsSearchSynonym == true )
			{
				ar.Add("SN");
			}

			if(condition.IsSearchFunction == true )
			{
				ar.Add("AF");
				ar.Add("FN");
				ar.Add("FS");
				ar.Add("FT");
				ar.Add("TF");
				ar.Add("IF");

			}

			if(condition.IsSearchProcedure == true )
			{
				ar.Add("P ");
				ar.Add("X ");
				ar.Add("PC");
			}

			string typeCondition = string.Empty;
			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					typeCondition += ",";
				}
				typeCondition += "'" + ar[i] + "'";
			}

			string addCondition = string.Empty;
			if(condition.IsCaseSensitive == true )
			{
				addCondition = " t1.name ";
			}
			else
			{
				addCondition = " LOWER(t1.name) ";
			}

			string schemaFilter = string.Empty;
			if( limitSchema != null && 
				limitSchema.Count != 0 )
			{
				schemaFilter = " and t2.name in ( ";
				for( int j = 0; j < limitSchema.Count; j++ )
				{
					if( j != 0 )
					{
						schemaFilter += ",";
					}
					schemaFilter += "'" + limitSchema[j] + "'";
				}
				schemaFilter += ")";
			}

			return string.Format(System.Globalization.CultureInfo.CurrentCulture,
				@"select t2.name as UserName, t1.name as ObjName, null as FieldName
from
	sys.all_objects t1
	inner join sys.schemas t2 on
		t1.schema_id = t2.schema_id
where
	{0} {1} and t1.type in ( {2} ) {3} ", addCondition, condSql, typeCondition, schemaFilter
				);		
		}

		#endregion

        /// <summary>
        /// SQL Version���
        /// </summary>
        protected SqlVersion sqlVersion { get; set; }

        /// <summary>
        /// SQL Version�����Z�b�g����
        /// </summary>
        /// <param name="version"></param>
        public void SetupVersion(SqlVersion version)
        {
            this.sqlVersion = version;
        }

        /// <summary>
        /// DB�ւ̍Đڑ�����������
        /// </summary>
        public void ReConnect()
        {
            try
            {
                if (this.pSqlConnect.State != ConnectionState.Broken &&
                    this.pSqlConnect.State != ConnectionState.Closed )
                {
                    this.SqlConnect.Close();
                }
                this.SqlConnect.Open();
            }
            catch (Exception exp)
            {
                throw;
            }
        }
    }
}
