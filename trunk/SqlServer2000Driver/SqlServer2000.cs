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
	/// SqlServer2000 �̊T�v�̐����ł��B
	/// </summary>
	public class SqlServerDriver2000 : ISqlInterface
	{

		/// <summary>
		/// �R�l�N�V����
		/// </summary>
		private System.Data.SqlClient.SqlConnection pSqlConnect;

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
		private int pQueryTimeout;

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
		public SqlServerDriver2000()
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
			this.pSqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection;
			this.QueryTimeout = timeout;
		}

		/// <summary>
		/// SQLSERVER�ɑ΂���R�l�N�V�����������
		/// </summary>
		public void CloseConnection()
		{
			this.pSqlConnect.Close();
		}

		/// <summary>
		/// �^�C���A�E�g�l��ݒ肵�Ȃ���
		/// </summary>
		/// <param name="timeout"></param>
		public void SetTimeout(int timeout)
		{
			this.QueryTimeout = timeout;
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
			sqlCmd.Connection = this.pSqlConnect;
			sqlCmd.CommandTimeout = this.QueryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// IDbCommand ��V�K�ɍ쐬����B
		/// �������A�R�}���h������A�R�l�N�V�������ƃ^�C���A�E�g�l�͂��łɃZ�b�g����Ă���
		/// </summary>
		/// <param name="stSql">���s����R�}���h������</param>
		/// <returns></returns>
		public IDbCommand		NewSqlCommand(string stSql)
		{
			SqlCommand	sqlCmd = new SqlCommand(stSql,this.pSqlConnect);
			sqlCmd.CommandTimeout = this.QueryTimeout;
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
		/// �g�����U�N�V���������擾����
		/// </summary>
		public IDbTransaction	SetTransaction(IDbCommand cmd)
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
		public string GetDBSelect()
		{
			return "SELECT name FROM sysdatabases order by name";
		}

		/// <summary>
		/// �w�肳�ꂽ�f�[�^�x�[�X�ւƐڑ���ύX����
		/// </summary>
		/// <param name="dbName">�ύX��̃f�[�^�x�[�X��</param>
		/// <returns></returns>
		public void SetDatabase(string dbName)
		{
			this.pSqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// �I�u�W�F�N�g�ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		public string GetTableListColumnName()
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
			return "�I�[�i�[���E�I�u�W�F�N�g��";
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
		public string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
		{
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

			StringBuilder sb = new StringBuilder();
			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					sb.Append(",");
				}
				sb.Append("'").Append(ar[i]).Append("'");
			}
			destObj = sb.ToString();
			sb.Length  = 0;
			sb.AppendFormat(@"select 
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					xtype as tvs,
					sysobjects.crdate as cretime,
					'' as synbase,
					'' as synType,
                    id as objectid
					from sysobjects, sysusers 
					where xtype in ( {0} ) and sysobjects.uid = sysusers.uid ",
				destObj );

			if( ownerList != null && 
				ownerList.Length != 0 )
			{
				sb.AppendFormat(" and sysusers.name in ( {0} ) ", ownerList);
			}

			return sb.ToString();
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
				return "select * from sysusers order by name";
			}
			else
			{
				return "select * from sysusers where name not like 'db[_]%' order by name";
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
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "isqlw";
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
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -E ",
						serverstr,
						dbName
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -E ",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -U {2} -P {3} ",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -U {1} -P {2} ",
						serverstr,
						logOnUserId,
						logOnPassword );
				}
			}
			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
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
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "SQL Server Enterprise Manager.MSC";
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

			Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\80\Tools\ClientSetup\", false);
			string profilerPath = string.Empty;
			if (rkey != null)
			{
				bool isPathExists = true;
				object robj = rkey.GetValue("Path");
				if (robj != null)
				{
					profilerPath = robj.ToString();
				}
				if( profilerPath == string.Empty )
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
						profilerPath += @"binn\";
					}
				}
			}

			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = profilerPath + "profiler.exe";
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
				da.SelectCommand.CommandTimeout = this.QueryTimeout;
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
		public void	InitObjTable(DataTable objTable)
		{
			if( objTable == null )
			{
				throw new ArgumentNullException("objTable");
			}
			objTable.Columns.Add("�I�u�W�F�N�gID",typeof(int));
			objTable.Columns.Add("�I�u�W�F�N�g��");
			objTable.Columns.Add("���L��");
			objTable.Columns.Add("�I�u�W�F�N�g�̌^");
			objTable.Columns.Add("�쐬����");
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
			string	strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from sysobjects where id = OBJECT_ID('{0}') ",
				databaseObjectInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.pSqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.pSqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			odt.Locale = System.Globalization.CultureInfo.CurrentCulture;
			da.Fill(odt);

			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["id"];
			}
			dr[1] = databaseObjectInfo.ObjName;
			dr[2] = databaseObjectInfo.Owner;
			dr[4] = databaseObjectInfo.CreateTime;

			switch( databaseObjectInfo.ObjType )
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
		public DataGetEventHandler ObjectDetailSet()
		{
			return new DataGetEventHandler(this.DatabaseObjSet);
		}

		/// <summary>
		/// �w�肳�ꂽ�I�u�W�F�N�g�̏ڍ׏����擾����
		/// sender �� �ΏۃI�u�W�F�N�g�� DBObjectInfo �^�Ƃ��ăZ�b�g����Ă��邱�Ƃ��O��
		/// </summary>
		/// <param name="sender">�Ώۂ̃I�u�W�F�N�g</param>
		/// <param name="e">�_�~�[</param>
        private void DatabaseObjSet(DBObjectInfo sender, System.EventArgs e)
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
syscolumns.name colname, 
t1.name valtype, 
t2.name baseValType, 
convert(int,syscolumns.length) as length, 
isnull(convert(int,syscolumns.prec),0) as prec, 
isnull(convert(int,syscolumns.xscale),0) as xscale, 
convert(int,syscolumns.colid) as colid, 
convert(int,syscolumns.colorder) as colorder, 
syscolumns.isnullable, 
syscolumns.collation, 
sysobjects.id  ,
case
when syscolumns.colstat & 1 = 1 then 1
else								 0
end		as is_identity,
case
when syscolumns.colstat & 1 = 1 
	then ident_seed('{0}')
else 0
end     as seed,
case
when syscolumns.colstat & 1 = 1 
	then ident_incr('{0}')
else 0
end     as incr
from 
	sysobjects, 
	syscolumns, 
	sysusers, 
	systypes t1,
	systypes t2
where 
	sysobjects.id = syscolumns.id 
and sysobjects.uid= sysusers.uid 
and syscolumns.xusertype=t1.xusertype 
and syscolumns.xtype=t2.xusertype 
and sysobjects.id = OBJECT_ID('{0}')
order by syscolumns.colorder",
				databaseObjectInfo.RealObjName
				),
				this.pSqlConnect );
			tableda.Fill(ds,"fieldList");

			DBFieldInfo addInfo;
            databaseObjectInfo.ClearField();
            foreach (DataRow fdr in ds.Tables["fieldList"].Rows)
			{
                addInfo = GetDBFieldInfo(ds, fdr);
                databaseObjectInfo.AddField(addInfo);
			}
		}

        private DBFieldInfo GetDBFieldInfo(DataSet ds, DataRow fdr)
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
            if (addInfo.Col.MaxLength < 0 &&
                (int)fdr["length"] > 0)
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
            addInfo.RealTypeName = (string)fdr["baseValType"];

            if (fdr["is_identity"] != DBNull.Value &&
                (int)fdr["is_identity"] == 1)
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
			bool isCaseSensitive,
			List<string> limitSchema)
		{
			searchCondition = searchCondition.Replace("'","''");
			if( isCaseSensitive == false )
			{
				searchCondition = searchCondition.ToLower(System.Globalization.CultureInfo.CurrentCulture);
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
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '%{0}%' ", searchCondition );
					break;
				case SearchType.SearchStartWith:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '{0}%' ", searchCondition);
					break;
				case SearchType.SearchExact:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " = '{0}' ", searchCondition);
					break;
				default:
					break;
			}

			string addCondition = string.Empty;
			if( isCaseSensitive == true )
			{
				addCondition = " t3.name ";
			}
			else
			{
				addCondition = " LOWER(t3.name) ";
			}

			return string.Format(System.Globalization.CultureInfo.CurrentCulture,
				@"select t2.name as UserName, t1.name as ObjName, t3.name as FieldName
from
	sysobjects t1
	inner join sysusers  t2 on
		t1.uid = t2.uid
	inner join syscolumns t3 on
		t1.id = t3.id		
where
	{0} {1} {2} ", addCondition, condSql , schemaFilter
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
			bool isCaseSensitive,
			List<string> limitSchema,
			bool isTable, 
			bool isView, 
			bool isSynonym, 
			bool isFunction, 
			bool isProcedure)
		{
			searchCondition = searchCondition.Replace("'","''");
			if( isCaseSensitive == false )
			{
				searchCondition = searchCondition.ToLower(System.Globalization.CultureInfo.CurrentCulture);
			}

			string condSql = string.Empty;

			switch(searchType)
			{
				case SearchType.SearchContain:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '%{0}%' ", searchCondition);
					break;
				case SearchType.SearchStartWith:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '{0}%' ", searchCondition);
					break;
				case SearchType.SearchExact:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " = '{0}' ", searchCondition);
					break;
				default:
					break;
			}

			List<string> ar = new List<string>();

			if( isTable == true )
			{
				ar.Add("U ");
				ar.Add("S ");
			}
			if( isView == true )
			{
				ar.Add("V ");
			}
			if( isSynonym == true )
			{
				ar.Add("SN");
			}

			if( isFunction == true )
			{
				ar.Add("FN");
				ar.Add("TF");
				ar.Add("IF");
			}

			if( isProcedure == true )
			{
				ar.Add("P ");
				ar.Add("X ");
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
			if( isCaseSensitive == true )
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
	sysobjects t1
	inner join sysusers  t2 on
		t1.uid = t2.uid
where
	{0} {1} and t1.xtype in ( {2} ) {3} ", addCondition, condSql, typeCondition, schemaFilter
				);		
		}
		#endregion
	}
}
