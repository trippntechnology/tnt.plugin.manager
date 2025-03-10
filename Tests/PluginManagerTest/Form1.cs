﻿using Data;
using TNT.Plugin.Manager;

namespace PluginManagerTest;

public partial class Form1 : Form
{
  Manager? _Manager = null;
  public Form1()
  {
    InitializeComponent();

    _Manager = new Manager(Controls, pluginOnClick, onToolTipChanged);
    _Manager.Register(@"D:\src\tnt.plugin.manager\Tests\Plugins\bin\Debug\net9.0-windows");
  }

  private void onToolTipChanged(string hint)
  {
    toolStripStatusLabel1.Text = hint;
  }

  private void onMouseEnter(object sender, EventArgs e)
  {
    ToolStripItem? item = sender as ToolStripItem;
  }

  private void pluginOnClick(object? arg1, EventArgs arg2)
  {
    ToolStripItem? tsi = arg1 as ToolStripItem;
    Plugin? p = tsi?.Tag as Plugin;

    if (tsi == null || p == null) return;
    ApplicationData data = new ApplicationData("This is the name field in the app data");
    p?.Execute(this, tsi, data, checkBox1.Checked);
  }

  private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
  {

  }

  private void toolStripSplitButton1_Click(object sender, EventArgs e)
  {
    MessageBox.Show("Button click");
  }
}
