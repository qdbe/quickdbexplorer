using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2000 �̊T�v�̐����ł��B
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

		public void SetConnection(System.Data.SqlClient.SqlConnection sqlConnection1)
		{
			this.sqlConnect = sqlConnection1;
			// TODO:  SqlServer2000.SetConnection ������ǉ����܂��B
		}

		public string GetDBSelect()
		{
			return "SELECT name FROM sysdatabases order by name";
		}

		/// <summary>
		/// �e�[�u���ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		public string GetTbListColName()
		{
			return "Table/View";
		}


		public string GetOwnerLabel1()
		{
			return "owner/Role(&O)";
		}

		public string GetOwnerLabel2()
		{
			return "�I�[�i�[���E�e�[�u����";
		}

		public string GetFieldListSelect(DBObjectInfo dboInfo)
		{
			return "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation, sysobjects.id  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + dboInfo.Owner +"' and sysobjects.name = '" + dboInfo.ObjName + "' order by syscolumns.colorder";
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


		#endregion
	}
}
