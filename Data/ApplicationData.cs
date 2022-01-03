using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNT.Plugin.Manager;

namespace Data
{
	public class ApplicationData : IApplicationData
	{
		public string Name { get; set; }

		public ApplicationData(string name)
		{
			this.Name = name;
		}
	}
}
