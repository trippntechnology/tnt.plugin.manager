using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace TNT.Plugin.Manager
{
	/// <summary>
	/// TNT.Plugin.Manager extension functions
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Finds a <see cref="ToolStripMenuItem"/> in a <see cref="ToolStripMenuItem"/> collection. This searches all children as well
		/// </summary>
		/// <param name="collection">Collection of <see cref="ToolStripItem"/> to search</param>
		/// <param name="name"><see cref="ToolStripItem.Name"/> or <see cref="ToolStripItem.Text"/></param>
		/// <returns><see cref="ToolStripMenuItem"/> if found, null otherwise</returns>
		public static ToolStripMenuItem FindItem(this ToolStripItemCollection collection, string name)
		{
			var found = (from ToolStripItem i in collection where i.Name == name || i.Text == name select i as ToolStripMenuItem).SingleOrDefault();

			if (found == null)
			{
				foreach (ToolStripItem item in collection)
				{
					var toolStripMenuItem = item as ToolStripMenuItem;
					found = toolStripMenuItem == null ? null : toolStripMenuItem.DropDownItems.FindItem(name);
					if (found != null) break;
				}
			}

			return found;
		}

		/// <summary>
		/// Gets the <see cref="MenuStrip"/> with the given <paramref name="name"/> in the <paramref name="controls"/>
		/// </summary>
		/// <param name="controls">Collection of controls to search</param>
		/// <param name="name">Name of the <see cref="MenuStrip"/></param>
		/// <returns><see cref="MenuStrip"/> if found, null otherwise</returns>
		public static MenuStrip GetMenuStrip(this ControlCollection controls, string name)
		{
			if (string.IsNullOrEmpty(name)) return null;
			return (MenuStrip)controls.Find(name, true).FirstOrDefault();
		}

		/// <summary>
		/// Gets the <see cref="ToolStrip"/> with the given <paramref name="name"/> in the <paramref name="controls"/>
		/// </summary>
		/// <param name="controls">Collection of controls to search</param>
		/// <param name="name">Name of the <see cref="ToolStrip"/></param>
		/// <returns><see cref="ToolStrip"/> if found, null otherwise</returns>
		public static ToolStrip GetToolStrip(this ControlCollection controls, string name)
		{
			if (string.IsNullOrEmpty(name)) return null;
			return (ToolStrip)controls.Find(name, true).FirstOrDefault();
		}
	}
}
