using System;
using System.Data;

namespace quickDBExplorer
{
	/// <summary>
	/// �e�탆�[�e�B���e�B�֐�
	/// Static ���\�b�h�̂ݕێ����Ă���
	/// </summary>
	public sealed class qdbeUtil
	{

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		private qdbeUtil()
		{
		}

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
			retstr[0] = string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}]",str[0]);
			retstr[1] = string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}]",str[1]);
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
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}]",str[0],str[1]);
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
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}_{2}]",str[0], str[1], addstr);
		}

		/// <summary>
		/// �e�헚�����ւ̍��ڂ̒ǉ����s��
		/// </summary>
		/// <param name="key">�e�[�u������</param>
		/// <param name="hvalue">�ǉ����闚������</param>
		/// <param name="tdata">�ǉ������̗������</param>
		static public void SetNewHistory(string key, string hvalue, TextHistoryDataSet tdata)
		{
			if( hvalue == null || hvalue == "" )
			{
				return;
			}

			DataRow []drl = tdata.TextHistoryDataSets.Select( string.Format(System.Globalization.CultureInfo.CurrentCulture,"KeyValue = '{0}'", key ) );

			TextHistoryDataSet.TextHistoryDataSetsRow dr = null;
			int maxKeyNo = 0;
			for( int i = 0; i < tdata.TextHistoryDataSets.Rows.Count; i++ )
			{
				dr = tdata.TextHistoryDataSets[i];
				if( dr.KeyValue == key &&
					dr.DataValue == hvalue )
				{
					// �����e�[�u���ɑ΂��A���ɓ����������o�^����Ă��邽�߁A�L�[NO�����V�������邽�߁A
					// �ߋ��̃f�[�^�͈�U�폜���V�����ǉ����Ȃ���
					dr.Delete();
					continue;
				}
				if( dr.IsKeyNoNull() == true )
				{
					dr.KeyNo = i;
				}
				if( maxKeyNo < dr.KeyNo )
				{
					maxKeyNo = dr.KeyNo;
				}
			}
			maxKeyNo++;
			TextHistoryDataSet.TextHistoryDataSetsRow ndr = tdata.TextHistoryDataSets.NewTextHistoryDataSetsRow();
			ndr.KeyValue = key;
			ndr.DataValue = hvalue;
			ndr.KeyNo = maxKeyNo;
			tdata.TextHistoryDataSets.Rows.Add(ndr);
			tdata.TextHistoryDataSets.AcceptChanges();
		}

	}
}
