using System;
using System.IO;
using System.Text;
using System.Collections;

namespace serialFactory
{
	/// <summary>
	/// SerialFactory �̊T�v�̐����ł��B
	/// </summary>
	public class SerialFactory
	{

		
		public SerialFactory()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		// �V���A���L�[�t�@�C���̑��݊m�F

		public bool	IsExistsSerialFile( string filename )
		{
			if( !File.Exists( filename ) )
			{
				return false;
			}
			return true;
		}

		// �V���A���L�[�t�@�C���̓ǂݍ��ݏ���
		public string ReadSerialFile( string filename )
		{
			if( !File.Exists( filename ) )
			{
				return null;
			}

			string serialfile;

			StreamReader sr = new StreamReader(filename);
			serialfile = sr.ReadLine();
			sr.Close();

			return serialfile;
		}

		// �V���A���L�[���Ȃ��ꍇ�̐V�K�쐬����

		// �V���A���L�[�t�@�C���̃`�F�b�N����
	}

	internal class SerialData
	{
		string			UserName;			// ���[�U�[��
		private ArrayList serialArray;		// �V���A���L�[
		DateTime		SerialSetupDate;	// �V���A���L�[ �Z�b�g�A�b�v����
		DateTime		KeyMakeDate;		// �V���A���L�[�쐬����
		DateTime		LimitDate;			// ���p��������
		DateTime		LimitLength;		// �Z�b�g�A�b�v�サ���Ȃ�

		// �����񂩂�̃f�[�^�捞
		public bool	loaddata(string serialstr)
		{
			return false;

		}
	}
}
