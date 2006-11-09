using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;


namespace dbAdpt
{
	/// <summary>
	/// dbadptSelectList ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class dbadptSelectList : System.Windows.Forms.ListView
	{
		public dbadptSelectList()
		{
			this.View = System.Windows.Forms.View.Details;
			this.FullRowSelect = true;
		}

		public void	AllSelect()
		{
			if( this.CheckBoxes )
			{
				foreach( ListViewItem it in this.Items )
				{
					if( it.Checked == false )
					{
						it.Checked = true;
					}				
				}
			}
			else
			{
				foreach( ListViewItem it in this.Items )
				{
					if( it.Selected == false )
					{
						it.Selected = true;
					}
				}
			}
		}

		public void	AllUnSelect()
		{
			if( this.CheckBoxes )
			{
				foreach( ListViewItem it in this.Items )
				{
					if( it.Checked == true )
					{
						it.Checked = false;
					}				
				}
			}
			else
			{
				foreach( ListViewItem it in this.Items )
				{
					if( it.Selected == true )
					{
						it.Selected = false;
					}
				}
			}
		}

		public ListViewItemCollection	GetSelectItem()
		{
			if( this.CheckBoxes )
			{
				return (ListViewItemCollection)this.CheckedItems;
			}
			else
			{
				return (ListViewItemCollection)this.SelectedItems;
			}
		}
	}
}
