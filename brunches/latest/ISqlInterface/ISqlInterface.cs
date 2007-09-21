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
		void SetConnection(IDbConnection sqlConnection);

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
		/// <param name="dboInfo">�I�u�W�F�N�g���</param>
		/// <returns></returns>
		string GetFieldListSelect(DBObjectInfo dboInfo);

		/// <summary>
		/// �I�u�W�F�N�g�ꗗ�̕\���pSQL�̎擾
		/// </summary>
		/// <param name="isDspView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="ownerList">�����Owner�̃e�[�u���̂ݕ\������ꍇ�� IN��ɗ��p����J���}��؂蕶�����n��</param>
		/// <returns></returns>
		string GetDspObjList(bool isDspTable, bool isDspView, bool Synonym, bool isDspFunc, bool isDspSP, string ownerList);

		/// <summary>
		/// �t�B�[���h���X�g�擾���̃_�~�[�N�G���𐶐�����
		/// </summary>
		/// <returns></returns>
		string GetDspFldListDummy();

		/// <summary>
		/// Owner �̈ꗗ���擾����SQL�𐶐�����
		/// </summary>
		/// <param name="isDspSysUser"></param>
		/// <returns></returns>
		string	GetOwnerList(bool isDspSysUser);

		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂��� Create ���𐶐�����
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <param name="usekakko"></param>
		/// <returns></returns>
		string	GetDDLCreateStr(DBObjectInfo dboInfo, bool usekakko);

		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂���DROP ���𐶐�����
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <returns></returns>
		string	GetDDLDropStr(DBObjectInfo dboInfo);

		/// <summary>
		/// �I�u�W�F�N�g�����Z�b�g����DataTable������������
		/// </summary>
		/// <param name="dt"></param>
		void	InitObjTable(DataTable dt);

		/// <summary>
		/// �I�u�W�F�N�g�̏����ADataTable �ɒǉ�����
		/// </summary>
		/// <param name="dboInfo">�ΏۂƂȂ�I�u�W�F�N�g</param>
		/// <param name="dt"></param>
		void	AddObjectInfo(DBObjectInfo dboInfo, DataTable dt);
	}
}
