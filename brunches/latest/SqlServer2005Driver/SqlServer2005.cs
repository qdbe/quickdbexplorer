using System;
using System.Collections;
using System.Data;
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

		System.Data.SqlClient.SqlConnection sqlConnect;

		public SqlServerDriver()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}
		#region ISqlInterface �����o

		public void SetConnection(IDbConnection sqlConnection1)
		{
			this.sqlConnect = sqlConnection1;
		}

		public string GetDBSelect()
		{
			return "SELECT name FROM sys.databases  order by name";;
		}

		/// <summary>
		/// �e�[�u���ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		public string GetTbListColName()
		{
			return "Table/View/Synonym";
		}


		public string GetOwnerLabel1()
		{
			return "Schema(&O)";
		}

		public string GetOwnerLabel2()
		{
			return "�X�L�[�}���E�e�[�u����";
		}

		public string GetFieldListSelect(DBObjectInfo dboInfo)
		{
			return string.Format(
				@"select 
	sys.all_columns.name colname, 
	st.name valtype, 
	convert(smallint,sys.all_columns.max_length) as length, 
	convert(smallint,
	CASE 
	WHEN st.user_type_id = st.system_type_id and st.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	WHEN st.user_type_id != st.system_type_id and baset.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	ELSE sys.all_columns.precision	
	end ) as [prec], 
	convert(smallint,sys.all_columns.scale) as xscale, 
	sys.all_columns.column_id as colid, 
	sys.all_columns.column_id as colorder, 
	convert(int,sys.all_columns.is_nullable) as isnullable, 
	sys.all_columns.collation_name as collation, 
	sys.all_objects.object_id as id
from 
	sys.all_objects 
	inner join sys.all_columns on sys.all_objects.object_id = sys.all_columns.object_id 
	inner join sys.types st on sys.all_columns.user_type_id  = st.user_type_id  
	LEFT OUTER JOIN sys.types AS baset ON 
		baset.user_type_id = st.system_type_id and baset.user_type_id = baset.system_type_id
where 
	sys.all_objects.object_id = OBJECT_ID('{0}')
order by colorder",
				dboInfo.RealObjName
				);
		}

		/// <summary>
		/// �I�u�W�F�N�g�ꗗ�̕\���pSQL�̎擾
		/// </summary>
		/// <param name="isDspView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
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
		/// �e�[�u�����擾�ׂ̈�SQL���𐶐�����
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <returns></returns>
		string	GetDDLTableSelectStr(DBObjectInfo dboInfo)
		{
			return string.Format(
				@"select 
	sys.all_columns.name colname, 
	st.name valtype, 
	convert(smallint,sys.all_columns.max_length) as length, 
	convert(smallint,
	CASE 
	WHEN st.user_type_id = st.system_type_id and st.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	WHEN st.user_type_id != st.system_type_id and baset.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	ELSE sys.all_columns.precision	
	end ) as [prec], 
	convert(smallint,sys.all_columns.scale) as xscale, 
	sys.all_columns.column_id as colid, 
	sys.all_columns.column_id as colorder, 
	convert(int,sys.all_columns.is_nullable) as isnullable, 
	sys.all_columns.collation_name as collation, 
	sys.all_objects.object_id as id
from 
	sys.all_objects 
	inner join sys.all_columns on sys.all_objects.object_id = sys.all_columns.object_id 
	inner join sys.schemas on sys.all_objects.schema_id= sys.schemas.schema_id 
	inner join sys.types st on sys.all_columns.user_type_id  = st.user_type_id  
	LEFT OUTER JOIN sys.types AS baset ON 
		baset.user_type_id = st.system_type_id and baset.user_type_id = baset.system_type_id
where 
	sys.all_objects.object_id = OBJECT_ID('{0}')
order by colorder",
				dboInfo.RealObjName
				);
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

				string sqlstr = this.GetDDLTableSelectStr(dboInfo);

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
		/// <param name="dt"></param>
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
		#endregion
	}
}
