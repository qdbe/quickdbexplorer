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
	/// SqlServer2000 �̊T�v�̐����ł��B
	/// </summary>
	public class SqlServerDriver2000 : ISqlInterface
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
			return "SELECT name FROM sysdatabases order by name";
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
		/// <param name="isDisplayTable">�e�[�u����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isSynonym">�V�m�j����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayFunction">Function��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplaySP">�X�g�A�h�v���V�[�W����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="ownerList">�����Owner�̃e�[�u���̂ݕ\������ꍇ�� IN��ɗ��p����J���}��؂蕶�����n��</param>
		/// <returns></returns>
		public string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
		{
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

			StringBuilder sb = new StringBuilder();
			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					sb.Append(",");
				}
				sb.Append("'").Append((string)ar[i]).Append("'");
			}
			destObj = sb.ToString();
			sb.Length  = 0;
			sb.AppendFormat(@"select 
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					xtype as tvs,
					sysobjects.crdate as cretime,
					'' as synbase,
					'' as synType
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

			
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "profiler.exe";
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

				string stSql = "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + databaseObjectInfo.Owner +"' and sysobjects.name = '" + databaseObjectInfo.ObjName + "' order by syscolumns.colorder";

				SqlDataAdapter da = new SqlDataAdapter(stSql, this.sqlConnect);
				DataSet ds = new DataSet();
				ds.CaseSensitive = true;
				ds.Locale = System.Globalization.CultureInfo.CurrentCulture;
				da.Fill(ds,databaseObjectInfo.ToString());

				int		maxRow = ds.Tables[databaseObjectInfo.ToString()].Rows.Count;
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
						wr.Write("\t[{0}]", ds.Tables[databaseObjectInfo.ToString()].Rows[i][0]);
					}
					else
					{
						wr.Write("\t{0}", ds.Tables[databaseObjectInfo.ToString()].Rows[i][0]);
					}
					wr.Write("\t");
					// �^
					valtype = (string)ds.Tables[databaseObjectInfo.ToString()].Rows[i][1];

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
						if( (Int16)ds.Tables[databaseObjectInfo.ToString()].Rows[i][3] == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ds.Tables[databaseObjectInfo.ToString()].Rows[i][3]);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},", ds.Tables[databaseObjectInfo.ToString()].Rows[i][3]);
						wr.Write("{0})", ds.Tables[databaseObjectInfo.ToString()].Rows[i][4]);
					}
					wr.Write("\t");
						
					if( !ds.Tables[databaseObjectInfo.ToString()].Rows[i].IsNull("collation"))
					{
						wr.Write("COLLATE {0}",ds.Tables[databaseObjectInfo.ToString()].Rows[i]["collation"]);
						wr.Write("\t");
					}
						
					if( (int)ds.Tables[databaseObjectInfo.ToString()].Rows[i]["isnullable"] == 0 )
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
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
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
			databaseObjectInfo.FieldInfo = ar;
		}

		#endregion
	}
}
