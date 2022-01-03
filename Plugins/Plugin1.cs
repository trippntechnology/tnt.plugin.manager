using Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace Plugins
{
	public class Plugin1 : TNT.Plugin.Manager.Plugin
	{
		public override string MenuStripName => "menustrip2";

		public override string ToolStripName => "toolstrip1";

		public override bool LicenseRequired => true;

		public override string Text => "License Required";

		public override string ToolTipText => "This feature requires a license";

		public override Image Image
		{
			get
			{
				return this.GetImage("Plugins.Images.application_put.png");
			}
		}


		public override MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem menu = new ToolStripMenuItem("&File");

			// Causes the Menu item in this menu strip to match the merging menu strip
			menu.MergeAction = MergeAction.MatchOnly;
			menu.DropDownItems.Add((ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>());
			menuStrip.Items.Add(menu);

			return menuStrip;
		}
		public override ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();
			toolStrip.Items.Add(CreateToolStripItem<ToolStripButton>());
			return toolStrip;
		}

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;
			var plugin = _Manager.GetPlugins(p => p.Text == "Checkable");
			(plugin.FirstOrDefault() as CheckablePlugin)?.SetChecked(owner, content);
			MessageBox.Show($"Plugin1: {appData.Name}");
		}
	}
}
