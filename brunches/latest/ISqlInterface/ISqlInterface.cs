using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// SQL�̃o�[�W�����ɂ������̈Ⴂ���炭��SQL���Ȃǂ𐶐�����ׂ̃N���X
	/// �����ł� Interface �Ƃ��Ē�`���A�����͌ʂɍs��
	/// </summary>
	public interface ISqlInterface
	{
		/// <summary>
		/// SQLServer�ɑ΂���R�l�N�V���������Ǘ�����
		/// </summary>
		/// <param name="sqlConnection1"></param>
		void SetConnection(SqlConnection sqlConnection1);

		/// <summary>
		/// DB�̈ꗗ�\�����擾����SQL����Ԃ�
		/// </summary>
		/// <returns></returns>
		string GetDBSelect();

		/// <summary>
		/// �e�[�u���ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		string GetTbListColName();

		/// <summary>
		/// DB�I�[�i�[�̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		string GetOwnerLabel1();
		/// <summary>
		/// ���W�I�{�^���̃��x����Ԃ�
		/// </summary>
		/// <returns></returns>
		string GetOwnerLabel2();
		/// <summary>
		/// �t�B�[���h�ꗗ�̎擾�pSQL����Ԃ�
		/// </summary>
		/// <param name="tbname"></param>
		/// <param name="tbnamelist"></param>
		/// <returns></returns>
		string GetFieldListSelect(string tbname, string []tbnamelist);

		/// <summary>
		/// �e�[�u���ꗗ�̕\���pSQL�̎擾
		/// </summary>
		/// <param name="isDspView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="ownerList">�����Owner�̃e�[�u���̂ݕ\������ꍇ�� IN��ɗ��p����J���}��؂蕶�����n��</param>
		/// <returns></returns>
		string GetDspTableList(bool isDspView, string ownerList);
	}
}