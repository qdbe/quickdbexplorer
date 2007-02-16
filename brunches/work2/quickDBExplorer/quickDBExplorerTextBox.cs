using System;
using System.Windows.Forms;
using System.Windows;

namespace quickDBExplorer
{
	/// <summary>
	/// quickDBExplorerTextBox ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class quickDBExplorerTextBox : TextBox
	{
		public quickDBExplorerTextBox() : base()
		{
			this.KeyDown += new KeyEventHandler(quickDBExplorerTextBox_KeyDown);
		}

		protected virtual void quickDBExplorerTextBox_KeyDown(object sender, KeyEventArgs ev)
		{
			if( ((TextBox)sender).Multiline != true &&
				ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.A )
			{
				this.SelectAll();
				ev.Handled = true;
			}
		}
	}
}
