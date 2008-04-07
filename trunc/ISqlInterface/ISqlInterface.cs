using System;
using System.Data;
using System.Data.Common;
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
		/// <param name="sqlConnection">�R�l�N�V�������</param>
		/// <param name="timeout">�R�}���h���s�^�C���A�E�g�l</param>
		void SetConnection(IDbConnection sqlConnection, int timeout);

		/// <summary>
		/// SQLSERVER�ɑ΂���R�l�N�V�����������
		/// </summary>
		void CloseConnection();


		/// <summary>
		/// �^�C���A�E�g�l��ݒ肵�Ȃ���
		/// </summary>
		/// <param name="timeout"></param>
		void SetTimeout(int timeout);

		/// <summary>
		/// DataAdapter ���擾����
		/// </summary>
		DbDataAdapter	NewDataAdapter();

		/// <summary>
		/// IDbCommand ��V�K�ɍ쐬����B
		/// �������A�R�l�N�V�������ƃ^�C���A�E�g�l�͂��łɃZ�b�g����Ă���
		/// </summary>
		/// <returns></returns>
		IDbCommand		NewSqlCommand();

		/// <summary>
		/// IDbCommand ��V�K�ɍ쐬����B
		/// �������A�R�}���h������A�R�l�N�V�������ƃ^�C���A�E�g�l�͂��łɃZ�b�g����Ă���
		/// </summary>
		/// <param name="stSql">���s����R�}���h������</param>
		/// <returns></returns>
		IDbCommand		NewSqlCommand(string stSql);

		/// <summary>
		/// DataAdapter �� IDbCommand �� SelectCommand�Ƃ��Ċ֘A�Â���
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd);

		/// <summary>
		/// �g�����U�N�V��������ݒ肷��
		/// </summary>
		IDbTransaction	SetTransaction(IDbCommand cmd);

		/// <summary>
		/// select �R�}���h����Aupdate, insert, delete �R�}���h�𐶐����Ȃ���
		/// </summary>
		/// <param name="da"></param>
		void	SetCommandBuilder(DbDataAdapter da);

		/// <summary>
		/// DataReader����byte�z���ǂݍ��ށB
		/// �w�肳�ꂽ�t�B�[���h�͂��Ƃ��ƃo�C�i���f�[�^�ł��邱�Ƃ��O��
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		byte[]	GetDataReaderBytes(IDataReader dr, int col);

		/// <summary>
		/// DB�̈ꗗ�\�����擾����SQL����Ԃ�
		/// </summary>
		/// <returns></returns>
		string GetDBSelect();

		/// <summary>
		/// �w�肳�ꂽ�f�[�^�x�[�X�ւƐڑ���ύX����
		/// </summary>
		/// <param name="dbName">�ύX��̃f�[�^�x�[�X��</param>
		/// <returns></returns>
		void SetDatabase(string dbName);

		/// <summary>
		/// �e�[�u���ꗗ�̃J�����w�b�_�̕\���������擾����
		/// </summary>
		/// <returns></returns>
		string GetTableListColumnName();

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
		/// �I�u�W�F�N�g�ꗗ�̕\���pSQL�̎擾
		/// </summary>
		/// <param name="isDisplayTable">�e�[�u����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayView">View ��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isSynonym">�V�m�j����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplayFunction">Function��\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="isDisplaySP">�X�g�A�h�v���V�[�W����\�������邩�ۂ� true: �\������ false: �\�������Ȃ�</param>
		/// <param name="ownerList">�����Owner�̃e�[�u���̂ݕ\������ꍇ�� IN��ɗ��p����J���}��؂蕶�����n��</param>
		/// <returns></returns>
		string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList);


		/// <summary>
		/// Owner �̈ꗗ���擾����SQL�𐶐�����
		/// </summary>
		/// <param name="isDisplaySysUser"></param>
		/// <returns></returns>
		string	GetOwnerList(bool isDisplaySysUser);

		/// <summary>
		/// ISQL ���N������B
		/// </summary>
		/// <param name="serverRealName">�T�[�o�[��</param>
		/// <param name="instanceName">�C���X�^���X��</param>
		/// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
		/// <param name="dbName">�f�[�^�x�[�X��</param>
		/// <param name="logOnUserId">���O�C��ID</param>
		/// <param name="logOnPassword">���O�C���p�X���[�h</param>
		void	CallISQL(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword);


		/// <summary>
		/// EnterPriseManager���N������
		/// </summary>
		/// <param name="serverRealName">�T�[�o�[��</param>
		/// <param name="instanceName">�C���X�^���X��</param>
		/// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
		/// <param name="dbName">�f�[�^�x�[�X��</param>
		/// <param name="logOnUserId">���O�C��ID</param>
		/// <param name="logOnPassword">���O�C���p�X���[�h</param>
		void	CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword);

		/// <summary>
		/// Profiler���N������
		/// </summary>
		/// <param name="serverRealName">�T�[�o�[��</param>
		/// <param name="instanceName">�C���X�^���X��</param>
		/// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
		/// <param name="dbName">�f�[�^�x�[�X��</param>
		/// <param name="logOnUserId">���O�C��ID</param>
		/// <param name="logOnPassword">���O�C���p�X���[�h</param>
		void	CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword);


		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂��� Create ���𐶐�����
		/// </summary>
		/// <param name="databaseObjectInfo"></param>
		/// <param name="useParentheses"></param>
		/// <returns></returns>
		string	GetDdlCreateString(DBObjectInfo databaseObjectInfo, bool useParentheses);

		/// <summary>
		/// �I�u�W�F�N�g�ɑ΂���DROP ���𐶐�����
		/// </summary>
		/// <param name="databaseObjectInfo"></param>
		/// <returns></returns>
		string	GetDDLDropStr(DBObjectInfo databaseObjectInfo);

		/// <summary>
		/// �I�u�W�F�N�g�����Z�b�g����DataTable������������
		/// </summary>
		/// <param name="objTable"></param>
		void	InitObjTable(DataTable objTable);

		/// <summary>
		/// �I�u�W�F�N�g�̏����ADataTable �ɒǉ�����
		/// </summary>
		/// <param name="databaseObjectInfo">�ΏۂƂȂ�I�u�W�F�N�g</param>
		/// <param name="dt"></param>
		void	AddObjectInfo(DBObjectInfo databaseObjectInfo, DataTable dt);

		/// <summary>
		/// �I�u�W�F�N�g�̏ڍ׏����Z�b�g����C�x���g�n���h����Ԃ�
		/// </summary>
		/// <returns></returns>
		DataGetEventHandler ObjectDetailSet();

	}
}
