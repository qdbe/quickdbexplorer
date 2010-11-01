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
    /// 外部ツール情報の編集ダイアログ
    /// </summary>
    public partial class OuterToolEditForm : quickDBExplorer.quickDBExplorerBaseForm
    {

        /// <summary>
        /// 編集結果
        /// </summary>
        public List<ToolInfo> ResultToolList { get; set; }

        private manager.ToolMacroManager macroManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OuterToolEditForm(List<ToolInfo> toolList, manager.ToolMacroManager manager)
        {
            ResultToolList = toolList;
            macroManager = manager;
            InitializeComponent();
        }

        private void OuterToolEditForm_Load(object sender, EventArgs e)
        {
            LoadToolList();
            InitMacro();
        }

        private void InitMacro()
        {
            this.macroList.SuspendLayout();
            foreach (manager.MacroInfo each in macroManager.GetEnumrable())
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = each.GetMacroParam();
                item.SubItems.Add(each.SampleStr);
                item.Tag = each;
                this.macroList.Items.Add(item);
            }
            this.macroList.ResumeLayout();
        }

        private void LoadToolList()
        {
            this.toolList.SuspendLayout();
            foreach (ToolInfo each in ResultToolList)
            {
                AddToolListItem(each);
            }
            this.toolList.ResumeLayout();
        }

        private void AddToolListItem(ToolInfo info)
        {
            ListViewItem item = new ListViewItem();
            item.SubItems[0].Text = info.Name;
            item.SubItems.Add(info.Command);
            item.Tag = info;
            this.toolList.Items.Add(item);
        }

        private void toolList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( this.toolList.SelectedItems.Count == 0 )
            {
                ClearDetail();
                return;
            }
            ToolInfo viewInfo = (ToolInfo)this.toolList.SelectedItems[0].Tag;
            LoadToolInfo(viewInfo);
        }

        private void LoadToolInfo(ToolInfo viewInfo)
        {
            this.txtName.Text = viewInfo.Name;
            this.txtCommand.Text = viewInfo.Command;
        }

        private void ClearDetail()
        {
            this.txtName.Text = string.Empty;
            this.txtCommand.Text = string.Empty;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetail();
        }

        private void btnInsertMacro_Click(object sender, EventArgs e)
        {
            InsertMacroString();
        }

        private void InsertMacroString()
        {
            if( this.macroList.SelectedItems.Count != 1 )
            {
                return;
            }
            this.txtCommand.Paste(((manager.MacroInfo)this.macroList.SelectedItems[0].Tag).GetMacroParam());
        }

        private void macroList_DoubleClick(object sender, EventArgs e)
        {
            InsertMacroString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!checkNewMacro()) return;
            this.ClearDetail();
        }

        private bool checkNewMacro()
        {
            if (this.txtName.Text != string.Empty ||
                this.txtCommand.Text != string.Empty)
            {
                if (MessageBox.Show("クリアしてもよろしいですか？","", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkMacroValid()) return;
            UpdateMacro();
        }

        private void UpdateMacro()
        {
            string macroname = this.txtName.Text;
            int samename = this.toolList.FindStringExact(macroname);
            if (samename >= 0)
            {
                ((ToolInfo)this.toolList.Items[samename].Tag).Command = this.txtCommand.Text;
                this.toolList.Items[samename].SubItems[1].Text = this.txtCommand.Text;
            }
            else
            {
                ToolInfo addinfo = new ToolInfo(this.txtName.Text, this.txtCommand.Text);
                this.AddToolListItem(addinfo);
                this.ResultToolList.Add(addinfo);
            }
        }

        private bool checkMacroValid()
        {
            if (this.txtName.Text == string.Empty ||
                this.txtCommand.Text == string.Empty ){
                MessageBox.Show("名称、コマンドは必ず指定してください");
                return false;
            }
            string macroname = this.txtName.Text;
            int samename = this.macroList.FindStringExact(macroname);
            if (samename >= 0)
            {
                if (MessageBox.Show("上書きしてもよろしいですか？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
