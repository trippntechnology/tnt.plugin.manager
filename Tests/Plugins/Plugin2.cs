using System;
using System.Drawing;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace Plugins
{
	public class Plugin2 : TNT.Plugin.Manager.Plugin
	{
		public override string MenuStripName => "menustrip1";

		public override string ToolStripName => "toolstrip2";

		public override string Text => "Plugin2";

		public override string ToolTipText => "Tool tip for plugin2";

		public override Image Image
		{
			get
			{
				return base.GetImage("Plugins.Images.application_put.png");
			}
		}

		public override MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem menu = new ToolStripMenuItem("&File");

			// Causes the Menu item in this menu strip to match the merging menu strip
			menu.MergeAction = MergeAction.MatchOnly;


			// Add menu items to te menu's drop down in reverse order
			//menu.DropDownItems.Add(new ToolStripSeparator());

			menu.DropDownItems.Add((ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>());

			//if (LeadingMenuSeparator)
			//{
			//	menu.DropDownItems.Add(new ToolStripSeparator());
			//}

			//if (MenuMergeIndex >= 0)
			//{
			//	// Set to Insert with indexes
			//	foreach (ToolStripItem item in menu.DropDownItems)
			//	{
			//		item.MergeAction = MergeAction.Insert;
			//		item.MergeIndex = MenuMergeIndex;
			//	}
			//}

			menu.DropDownItems[0].MergeAction = MergeAction.Insert;
			menu.DropDownItems[0].MergeIndex = 0;

			menuStrip.Items.Add(menu);

			return menuStrip;
		}

		public override ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			//if (TrailingButtonSeparator)
			//{
			//	toolStrip.Items.Add(new ToolStripSeparator());
			//}

			ToolStripSplitButton button = (ToolStripSplitButton)CreateToolStripItem<ToolStripSplitButton>();
			toolStrip.Items.Add(button);

			ToolStripMenuItem item = new ToolStripMenuItem("One", null, OnOneClick);
			item.ToolTipText = "Tool tip text";
			item.MouseEnter += base.Item_MouseEnter;
			item.MouseLeave += base.Item_MouseLeave;
			button.DropDownItems.Add(item);

			//if (LeadingButtonSeparator)
			//{
			//	toolStrip.Items.Add(new ToolStripSeparator());
			//}

			//if (ToolStripMergeIndex >= 0)
			//{
			//	foreach (ToolStripItem item in toolStrip.Items)
			//	{
			//		item.MergeAction = MergeAction.Insert;
			//		item.MergeIndex = ToolStripMergeIndex;
			//	}
			//}

			return toolStrip;
		}

		private void OnOneClick(object sender, EventArgs e)
		{
			MessageBox.Show("OneClicked");
		}

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			MessageBox.Show("Plugin2");
		}
	}
}
