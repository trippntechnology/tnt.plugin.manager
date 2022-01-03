using System.Drawing;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace Plugins
{
	class CheckablePlugin : TNT.Plugin.Manager.Plugin
	{
		private ToolStripButton toolStripButton = null;

		public override string MenuStripName => "menustrip1";

		public override string ToolStripName => "toolstrip2";

		public override string Text => "Checkable";

		public override string ToolTipText => "Checkable plug-in";

		public override Image Image => base.GetImage("Plugins.Images.accept.png");

		public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
		{
			MessageBox.Show($"{Text}: {toolStripButton.Checked.ToString()}");
		}

		public void SetChecked(IWin32Window owner, IApplicationData content)
		{
			toolStripButton.Checked = true;
			Execute(owner, toolStripButton, content);
		}

		public override MenuStrip GetMenuStrip() => null;

		public override ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();
			toolStripButton = CreateToolStripItem<ToolStripButton>() as ToolStripButton;
			toolStripButton.CheckOnClick = true;
			toolStrip.Items.Add(toolStripButton);
			return toolStrip;
		}
	}
}
