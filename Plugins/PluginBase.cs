using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace Plugins
{
	public abstract class Plugin : TNT.Plugin.Manager.Plugin
	{
		//protected Image GetImage(string name)
		//{
		//	System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
		//	Stream myStream = myAssembly.GetManifestResourceStream(name);
		//	return new Bitmap(myStream);
		//}
	}
}
