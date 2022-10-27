using System;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
    /// <summary>
    /// �v���[�X�z���_�[��\������e�L�X�g�{�b�N�X
    /// </summary>
    public partial class PlaceholderTextBox : TextBox
    {
        /// <summary>
        ///  �R���X�g���N�^
        /// </summary>
        public PlaceholderTextBox()
        {
            this._placeholderText = "";
            this._placeholderColor = Color.Empty;

            this.Leave += PlaceholderTextBox_Leave;
        }

        private void PlaceholderTextBox_Leave(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        #region �v���p�e�B


        /// <summary>
        /// �v���[�X�z���_�[�̃e�L�X�g
        /// </summary>
        protected string _placeholderText;
        /// <summary>
        /// �v���[�X�z���_�[������
        /// </summary>
        public string PlaceholderText
        {
            get
            {
                return this._placeholderText;
            }
            set
            {
                this._placeholderText = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// �v���[�X�z���_�[�̐F
        /// </summary>
        private Color _placeholderColor;
        /// <summary>
        /// �v���[�X�z���_�[�̐F
        /// </summary>
        public Color PlaceholderColor
        {
            get
            {
                return this._placeholderColor;
            }
            set
            {
                this._placeholderColor = value;
                this.Refresh();
            }
        }

        #endregion

        // �y�C���g��\���萔
        private const int WM_PAINT = 0x000f; // 15

        /// <summary>
        /// WndProcMethod�̃I�[�o�[���C�h
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_PAINT)
            {
                if (this.Enabled &&
                    !this.ReadOnly &&
                    !this.Focused &&
                    string.IsNullOrEmpty(this.Text) &&
                    !string.IsNullOrEmpty(this._placeholderText))
                {
                    // �e�L�X�g�{�b�N�X�����͉\�Ńt�H�[�J�X����Ă��Ȃ����

                    using (Graphics graphics = this.CreateGraphics())
                    {
                        // �`���������������i�w�i�F�œh��Ԃ��j
                        Brush brush = new SolidBrush(this.BackColor);
                        graphics.FillRectangle(brush, this.ClientRectangle);

                        // �v���[�X�z���_�[�̐F���擾
                        Color placeholderColor = CreateNeutralColor();
                        if (!this._placeholderColor.IsEmpty)
                        {
                            placeholderColor = this._placeholderColor;
                        }

                        // �v���[�X�z���_�[�̃e�L�X�g��`�悷��
                        graphics.DrawString(_placeholderText, this.Font, new SolidBrush(placeholderColor), 1, 1);
                    }
                }
            }
        }

        // �O�i�F�Ɣw�i�F�̒��ԐF���쐬
        private Color CreateNeutralColor()
        {
            Color color = Color.FromArgb(
                (this.ForeColor.A >> 1 + this.BackColor.A >> 1),
                (this.ForeColor.R >> 1 + this.BackColor.R >> 1),
                (this.ForeColor.G >> 1 + this.BackColor.G >> 1),
                (this.ForeColor.B >> 1 + this.BackColor.B >> 1));
            return color;
        }
    }
}