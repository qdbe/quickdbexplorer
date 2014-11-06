using System;

namespace quickDBExplorer
{
	/// <summary>
	/// FieldList�ɕ\�����鍀�ڂ̏ڍ�
	/// </summary>
	public class FieldListItem
	{
		// �\��������
		private string dispText = "";
		// �֘A�I�u�W�F�N�g
		private object pBackObj = null;

		/// <summary>
		/// �֘A�I�u�W�F�N�g��Ԃ�
		/// </summary>
		public object BackObj
		{
			get { return this.pBackObj; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="text"></param>
		/// <param name="refObj"></param>
		public FieldListItem(string text, object refObj)
		{
			this.dispText = text;
			this.pBackObj = refObj;
		}

		/// <summary>
		/// �ʏ�͕\�������݂̂�Ԃ�
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.dispText;
		}
	}
}
