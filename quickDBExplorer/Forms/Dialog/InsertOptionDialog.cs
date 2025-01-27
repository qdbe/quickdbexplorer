using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer.Forms.Dialog
{
    /// <summary>
    /// INSERT文設定ダイアログ
    /// </summary>
    public partial class InsertOptionDialog : quickDBExplorer.quickDBExplorerBaseForm
    {
        /// <summary>
        /// INSERT文設定
        /// </summary>
        protected virtual InsertOptionSetting _insertOption { get; set; }

        /// <summary>
        /// サーバ情報
        /// </summary>
        protected virtual ServerJsonData _svData { get; set; }

        /// <summary>
        /// SQLServerのバージョン 
        /// </summary>
        protected virtual SqlVersion _version { get; set; }


        /// <summary>
        /// 起動中か否かの判定
        /// </summary>
        protected bool isLoading = true;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="svData"></param>  
        /// <param name="_version"></param>
        public InsertOptionDialog(ServerJsonData svData, SqlVersion _version)
        {
            this._svData = svData; 
            this._insertOption = svData.InsertOption;
            this._version = _version;

            InitializeComponent();
        }


        /// <summary>
        /// コンストラクタ（デザイン用）
        /// </summary>
        public InsertOptionDialog()
        {
        }

        /// <summary>
        /// 表示時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            base.OnLoad(e);


            if( this._insertOption != null)
            {
                DispData(this._insertOption);
            }

            this.isLoading = false;
        }


        /// <summary>
        /// データを出力する
        /// </summary>
        /// <param name="insertOption"></param>
        private void DispData( InsertOptionSetting insertOption)
        {
            if (insertOption.InsertType == 0)
            {
                this.rdoAllInsert.Checked = true;
                this.rdoMultiValues.Checked = false;
            }
            else
            {
                this.rdoAllInsert.Checked = false;
                this.rdoMultiValues.Checked = true;
            }

            rdoInsert_CheckedChanged(null, null);

            this.txtGoInsertLine.Text = insertOption.GoInsertLine.ToString();
            this.txtValueLine.Text = insertOption.ValuesLine.ToString();

            DispSample();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void rdoInsert_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            if (this.rdoAllInsert.Checked)
            {
                this.txtGoInsertLine.Enabled = true;
                this.txtValueLine.Enabled = false;
                if(this.txtGoInsertLine.Text == "0")
                {
                    this.txtGoInsertLine.Text = "1000";
                }
            }
            else
            {
                if (!isLoading && 
                    (this._version.PublicVersionNo == "2000" ||
                    this._version.PublicVersionNo == "2005" 
                    )
                    )
                {
                    if (MessageBox.Show("古いSQLSERVERのバージョンではこの形式は利用できませんがよろしいですか", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        this.rdoAllInsert.Checked = true;
                        return;
                    }
                }
                this.txtGoInsertLine.Enabled = false;
                this.txtValueLine.Enabled = true;
                if (this.txtValueLine.Text == "0")
                {
                    this.txtValueLine.Text = "1000";
                }
            }
            DispSample();
        }

        /// <summary>
        /// サンプルの表示
        /// </summary>
        private void DispSample()
        {
            string sampleText = "";
            if (this.rdoAllInsert.Checked)
            {
                sampleText = $@"
INSERT INTO xxx.TABLE1 (KEY_CD, COLNAME, CVALUE) values ( '001', '名称1', 'SDATA1' );
INSERT INTO xxx.TABLE1 (KEY_CD, COLNAME, CVALUE) values ( '002', '名称2', 'SDATA2' );
INSERT INTO xxx.TABLE1 (KEY_CD, COLNAME, CVALUE) values ( '003', '名称3', 'SDATA3' );
INSERT INTO xxx.TABLE1 (KEY_CD, COLNAME, CVALUE) values ( '004', '名称4', 'SDATA4' );
 :
 :
go ";
            }
            else
            {
                sampleText = $@"
INSERT INTO xxx.TABLE1 (KEY_CD, COLNAME, CVALUE) 
values 
 ( '001', '名称1', 'SDATA1' )
,( '002', '名称2', 'SDATA2' )
,( '003', '名称3', 'SDATA3' )
,( '004', '名称4', 'SDATA4' )
 :
 :
go ";
            }
            this.lblSample.Text = sampleText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (checkData() == false)
            {
                return;
            }
            if (MessageBox.Show("更新してよろしいですか", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int tmpValue = 0;
                if (this.rdoAllInsert.Checked)
                {
                    this._insertOption.InsertType = 0;

                    int.TryParse(this.txtGoInsertLine.Text, out tmpValue);
                    this._insertOption.GoInsertLine = tmpValue;
                }
                else
                {
                    this._insertOption.InsertType = 1;
                    int.TryParse(this.txtValueLine.Text, out tmpValue);
                    this._insertOption.ValuesLine = tmpValue;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool checkData()
        {
            int tmpValue;
            bool ret;
            if (this.rdoAllInsert.Checked)
            {
                if (!string.IsNullOrEmpty(this.txtGoInsertLine.Text))
                {
                    ret = int.TryParse(this.txtValueLine.Text, out tmpValue);
                    if (ret == false)
                    {
                        MessageBox.Show("GO を出力する行数　は1以上の正の整数で指定してください。");
                        return false;
                    }
                    if (tmpValue <= 0)
                    {
                        MessageBox.Show("GO を出力する行数　は1以上の正の整数で指定してください。");
                        return false;
                    }
                }

            }
            else if (this.rdoMultiValues.Checked == true)
            {
                ret = int.TryParse(this.txtValueLine.Text, out tmpValue);
                if (ret == false)
                {
                    MessageBox.Show("1つのVALUESに出力する行数　は1000以下の正の整数で指定してください。");
                    return false;
                }
                if (tmpValue <= 0 || tmpValue > 1000)
                {
                    MessageBox.Show("1つのVALUESに出力する行数　は1000以下の正の整数で指定してください。");
                    return false;
                }
            }

            return true;
        }
    }
}
