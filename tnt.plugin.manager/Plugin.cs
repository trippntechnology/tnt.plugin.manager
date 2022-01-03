﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TNT.Plugin.Manager
{
	/// <summary>
	/// Abstract <see cref="Plugin"/> class. All plug-ins must derive from this class.
	/// </summary>
	public abstract class Plugin
	{
		/// <summary>
		///  <see cref="List{T}"/> of <see cref="ToolStripItem"/> that are generated by this <see cref="Plugin"/>
		/// </summary>
		protected List<ToolStripItem> _ToolStripItems = new List<ToolStripItem>();

		/// <summary>
		/// The <see cref="Manager"/> that created the plug-in
		/// </summary>
		public Manager _Manager = null;

		/// <summary>
		/// Call to set the <see cref="ToolStripItem"/> Click event
		/// </summary>
		/// <param name="onClick">External <see cref="EventHandler"/> that is triggered when the <see cref="Plugin"/> is clicked</param>
		public virtual void SetOnClickEvent(EventHandler onClick)
		{
			_ToolStripItems.ForEach(t =>
			{
				if (t is ToolStripSplitButton)
				{
					(t as ToolStripSplitButton).ButtonClick += onClick;
				}
				else
				{
					t.Click += onClick;
				}
			});
		}

		/// <summary>
		/// Creates a <see cref="ToolStripItem"/>
		/// </summary>
		/// <typeparam name="T">Type of <see cref="ToolStripItem"/> to create</typeparam>
		/// <returns></returns>
		protected ToolStripItem CreateToolStripItem<T>(string text = "", Image image = null, string toolTipText = "") where T : ToolStripItem, new()
		{
			ToolStripItem item = new T();

			item.Text = string.IsNullOrEmpty(text) ? this.Text : text;
			item.Image = image == null ? this.Image : image;
			item.ToolTipText = string.IsNullOrEmpty(toolTipText) ? this.ToolTipText : toolTipText;
			item.Tag = this;

			item.MouseEnter += Item_MouseEnter;
			item.MouseLeave += Item_MouseLeave;

			_ToolStripItems.Add(item);

			return item;
		}

		/// <summary>
		/// Calls <see cref="OnToolTipChanged"/> if defined with an empty string
		/// </summary>
		/// <param name="sender"><see cref="ToolStripItem"/></param>
		/// <param name="e">Unused</param>
		protected void Item_MouseLeave(object sender, EventArgs e)
		{
			if (this.OnToolTipChanged != null)
			{
				this.OnToolTipChanged(string.Empty);
			}
		}

		/// <summary>
		/// Calls <see cref="OnToolTipChanged"/> if defined with the <paramref name="sender"/> tool tip text
		/// </summary>
		/// <param name="sender"><see cref="ToolStripItem"/></param>
		/// <param name="e">Unused</param>
		protected void Item_MouseEnter(object sender, EventArgs e)
		{
			if (this.OnToolTipChanged != null)
			{
				ToolStripItem item = sender as ToolStripItem;
				this.OnToolTipChanged(item.ToolTipText);
			}
		}

		/// <summary>
		/// Application's <see cref="MenuStrip"/> name where the plug-in's <see cref="MenuStrip"/> should be merged
		/// </summary>
		public abstract string MenuStripName { get; }

		/// <summary>
		/// Application's <see cref="ToolStrip"/> name where the plug-in's <see cref="MenuStrip"/> should be merged
		/// </summary>
		public abstract string ToolStripName { get; }

		/// <summary>
		/// Text to display on the plug-in's <see cref="ToolStripItem"/>s
		/// </summary>
		public abstract string Text { get; }

		/// <summary>
		/// Tool tip to display on the plug-in's <see cref="ToolStripItem"/>s
		/// </summary>
		public abstract string ToolTipText { get; }

		/// <summary>
		/// Image that should be displayed by the plug-in
		/// </summary>
		public abstract Image Image { get; }

		/// <summary>
		/// Override when this plug-in requires a license to execute
		/// </summary>
		public virtual bool LicenseRequired { get; } = false;

		/// <summary>
		/// Implement to associate an action with the <see cref="Plugin"/>
		/// </summary>
		/// <param name="owner">Calling application's window</param>
		/// <param name="sender"><see cref="ToolStripItem"/> that initiated the call</param>
		/// <param name="content">Content from the application that can be accessed</param>
		public abstract void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content);

		/// <summary>
		/// Set to capture event for when the tool tip changes
		/// </summary>
		public ToolTipChangedEventHandler OnToolTipChanged { get; set; }

		/// <summary>
		/// Call to only execute if <paramref name="hasLicense"/> is true. If a license is not applicable, the caller only need
		/// to call <see cref="Execute(IWin32Window, ToolStripItem, IApplicationData)"/>.
		/// </summary>
		/// <param name="owner">Calling application's window</param>
		/// <param name="sender"><see cref="ToolStripItem"/> that initiated the call</param>
		/// <param name="content">Content from the application that can be accessed</param>
		/// <param name="hasLicense">Indicates if the calling application has a license</param>
		public virtual void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content, bool hasLicense)
		{
			if (this.LicenseRequired && !hasLicense)
			{
				MessageBox.Show(owner, "You have discovered a feature only available in the licensed versions of the application.", "Feature Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				this.Execute(owner, sender, content);
			}
		}

		/// <summary>
		/// Implement to generate a <see cref="MenuStrip"/> that can be merged with the calling application
		/// </summary>
		/// <returns><see cref="MenuStrip"/></returns>
		public abstract MenuStrip GetMenuStrip();

		/// <summary>
		/// Implement to generate a <see cref="ToolStrip"/> that can be merged with the calling application
		/// </summary>
		/// <returns><see cref="ToolStrip"/></returns>
		public abstract ToolStrip GetToolStrip();

		/// <summary>
		/// Returns an image reference by <paramref name="resource"/> 
		/// </summary>
		/// <param name="resource">Embedded resource file containing the image</param>
		/// <returns><see cref="Image"/> represented by <paramref name="resource"/></returns>
		protected Image GetImage(string resource)
		{
			Bitmap bitmap = null;

			if (!string.IsNullOrEmpty(resource))
			{
				System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());
				Stream myStream = myAssembly.GetManifestResourceStream(resource);
				bitmap = new Bitmap(myStream);
			}

			return bitmap;
		}
	}
}