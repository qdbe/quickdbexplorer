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

        private manager.ToolMacroManager toolManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OuterToolEditForm(List<ToolInfo> toolList, manager.ToolMacroManager manager)
        {
            ResultToolList = toolList;
            toolManager = manager;
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
            foreach (manager.MacroInfo each in toolManager.GetEnumrator())
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(each.GetMacroParam());
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
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(each.Name);
                item.SubItems.Add(each.Command);
                item.Tag = each;
                this.toolList.Items.Add(item);
            }
            this.toolList.ResumeLayout();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.txtName.Text = string.Empty;
            this.txtCommand.Text = string.Empty;
        }
    }
}
