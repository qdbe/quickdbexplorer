using System;
using System.Data;

namespace quickDBExplorer
{
	/// <summary>
	/// �e�탆�[�e�B���e�B�֐�
	/// Static ���\�b�h�̂ݕێ����Ă���
	/// </summary>
	public class qdbeUtil
	{
		/// <summary>
		/// �e�[�u��������͂��A[owner]�A[tablename]�ɕ�������
		/// ��̖��O��[]���t���Ă��Ȃ����Ƃ��O��
		/// </summary>
		/// <param name="tbname">�e�[�u����(owner.tablename�`��)</param>
		/// <returns>[owner]��[tablename]�ɕ������ꂽ�����z��</returns>
		public static string[] SplitTbname(string tbname)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			string []retstr = new string[2];
			retstr[0] = string.Format("[{0}]",str[0]);
			retstr[1] = string.Format("[{0}]",str[1]);
			return retstr;
		}

		/// <summary>
		/// �e�[�u��������͂��A[owner].[tablename]�`���ɕύX����
		/// ��̖��O��[]���t���Ă��Ȃ����Ƃ��O��
		/// </summary>
		/// <param name="tbname">�e�[�u����(owner.tablename�`��)</param>
		/// <returns></returns>
		public static string GetTbname(string tbname)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			return string.Format("[{0}].[{1}]",str[0],str[1]);
		}

		/// <summary>
		/// �e�[�u��������͂��A[owner].[tablename]�`���ɕύX����B
		/// ���̍ہA�w�肳�ꂽ��������e�[�u�����̂ɕt���������ʂ�Ԃ�
		/// ��̖��O��[]���t���Ă��Ȃ����Ƃ��O��
		/// </summary>
		/// <param name="tbname">�e�[�u����(owner.tablename�`��)</param>
		/// <param name="addstr">�e�[�u�����ɕt�����镶����</param>
		/// <returns>��͌�̃e�[�u����([owner].[tabblname]�`��)</returns>
		public static string GetTbnameAdd(string tbname,string addstr)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			return string.Format("[{0}].[{1}_{2}]",str[0], str[1], addstr);
		}

		/// <summary>
		/// �e�헚�����ւ̍��ڂ̒ǉ����s��
		/// </summary>
		/// <param name="key">�e�[�u������</param>
		/// <param name="hvalue">�ǉ����闚������</param>
		/// <param name="tdata">�ǉ������̗������</param>
		static public void SetNewHistory(string key, string hvalue, ref textHistory tdata)
		{
			if( hvalue == null || hvalue == "" )
			{
				return;
			}

			DataRow []drl = tdata.textHistoryData.Select( string.Format("KeyValue = '{0}'", key ) );

			for( int i = 0; i < drl.Length; i++ )
			{
				if( (string)drl[i]["DataValue"] == hvalue )
				{
					// �����e�[�u���ɑ΂��A���ɓ����������o�^����Ă��邽�߁A�������Ȃ�
					return;
				}
			}
			
			textHistory.textHistoryDataRow ndr = tdata.textHistoryData.NewtextHistoryDataRow();
			ndr.KeyValue = key;
			ndr.DataValue = hvalue;
			tdata.textHistoryData.Rows.Add(ndr);
		}

	}
}
