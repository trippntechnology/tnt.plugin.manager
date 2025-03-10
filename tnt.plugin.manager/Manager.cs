using System.Reflection;
using static System.Windows.Forms.Control;

namespace TNT.Plugin.Manager;

/// <summary>
/// Method signature for when a tool tip changes
/// </summary>
/// <param name="hint"></param>
public delegate void ToolTipChangedEventHandler(string hint);

/// <summary>
/// Class that manages <see cref="Plugin"/>
/// </summary>
public class Manager
{
  /// <summary>
  /// <see cref="EventHandler"/> that gets associated with each <see cref="ToolStripItem"/> click handler
  /// </summary>
  private EventHandler _OnClick;

  /// <summary>
  /// Application's controls
  /// </summary>
  private ControlCollection? _Controls = null;

  /// <summary>
  /// Event that is fired when a hint is changed
  /// </summary>
  private ToolTipChangedEventHandler _OnToolTipChanged;

  /// <summary>
  /// <see cref="List{Plugin}"/> managed by this <see cref="Manager"/>
  /// </summary>
  private List<Plugin> _Plugins = new List<Plugin>();

  /// <summary>
  /// Initialization constructor
  /// </summary>
  /// <param name="controls">Application's controls</param>
  /// <param name="onClickHandler">Application's <see cref="EventHandler"/></param>
  /// <param name="toolTipChangedEventHandler">Application's event handler for handling a tool tip change event</param>
  public Manager(ControlCollection controls, EventHandler onClickHandler, ToolTipChangedEventHandler toolTipChangedEventHandler)
  {
    this._Controls = controls;
    this._OnClick = onClickHandler;
    this._OnToolTipChanged = toolTipChangedEventHandler;
  }

  /// <summary>
  /// Checks to see if <paramref name="type"/> derives from <paramref name="baseType"/>
  /// </summary>
  /// <param name="type"><see cref="Type"/> to check</param>
  /// <param name="baseType"><see cref="Type"/> that represents the base class</param>
  /// <returns>True if <paramref name="type"/> derives from <paramref name="baseType"/></returns>
  protected bool HasBaseType(Type type, Type baseType)
  {
    if (type.BaseType == baseType)
    {
      return true;
    }
    else if (type.BaseType == null)
    {
      return false;
    }
    else
    {
      return HasBaseType(type.BaseType, baseType);
    }
  }

  /// <summary>
  /// Registers a <see cref="Plugin"/>
  /// </summary>
  /// <param name="pluginDirectory">Directory where plug-ins are located</param>
  public void Register(string pluginDirectory)
  {

    if (!Directory.Exists(pluginDirectory))
    {
      throw new DirectoryNotFoundException($"Unable to locate {pluginDirectory}.");
    }

    var files = Directory.GetFiles(pluginDirectory, "*.dll");

    foreach (string file in files)
    {
      var assembly = Assembly.LoadFrom(file);
      var types = Utilities.Utilities.GetTypes(assembly, t =>
      {
        return HasBaseType(t, typeof(Plugin)) && !t.IsAbstract;
      });

      foreach (Type type in types)
      {
        var plugin = (Plugin?)Activator.CreateInstance(type);
        if (plugin == null) continue;
        plugin._Manager = this;
        _Plugins.Add(plugin);
        MergePlugin(plugin);
      }
    }
  }

  /// <summary>
  /// Returns a <see cref="List{Plugin}"/>
  /// </summary>
  /// <param name="filter">Filter that can be applied to the results</param>
  /// <returns>A <see cref="List{Plugin}"/> managed by this <see cref="Manager"/></returns>
  public List<Plugin> GetPlugins(Func<Plugin, bool>? filter = null)
  {
    if (filter == null) filter = (_) => { return true; };
    return (from p in _Plugins where filter(p) select p).ToList();
  }

  /// <summary>
  /// Finds the <see cref="ToolStripMenuItem"/> with the <paramref name="name"/> in the specified <paramref name="menuStripName"/>
  /// </summary>
  /// <param name="menuStripName">Name of <see cref="MenuStrip"/> to search</param>
  /// <param name="name">Name of <see cref="ToolStripMenuItem"/> to search for</param>
  /// <returns><see cref="ToolStripMenuItem"/> if found, null otherwise</returns>
  public ToolStripMenuItem? FindToolStripMenuItem(string menuStripName, string name)
  {
    if (string.IsNullOrEmpty(name) || _Controls == null) return null;
    var menuStrip = _Controls.GetMenuStrip(menuStripName);
    return menuStrip?.Items.FindItem(name);
  }

  /// <summary>
  /// Merges the plug-in into the MenuStrip and ToolStrip
  /// </summary>
  /// <param name="plugin"><see cref="Plugin"/> to register with the <see cref="Manager"/></param>
  private void MergePlugin(Plugin plugin)
  {
    ToolStrip? appMenuStrip = _Controls?.GetMenuStrip(plugin.MenuStripName);
    ToolStrip? appToolStrip = _Controls?.GetToolStrip(plugin.ToolStripName);
    MenuStrip? ms = plugin.GetMenuStrip();
    ToolStrip ts = plugin.GetToolStrip();

    plugin.SetOnClickEvent(_OnClick);
    plugin.OnToolTipChanged = _OnToolTipChanged;

    if (appMenuStrip != null && ms != null)
    {
      bool result = ToolStripManager.Merge(ms, appMenuStrip);
    }

    if (appToolStrip != null && ts != null)
    {
      bool result = ToolStripManager.Merge(ts, appToolStrip);
    }
  }
}
