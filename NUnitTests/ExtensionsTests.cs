using System.Diagnostics.CodeAnalysis;
using TNT.Plugin.Manager;

namespace NUnitTests;

[ExcludeFromCodeCoverage]
public class ExtensionsTests
{
  [Test]
  public void FindItem_ShouldReturnItem_WhenNameMatches()
  {
    // Arrange
    var menuStrip = new MenuStrip();
    var item1 = new ToolStripMenuItem { Name = "item1", Text = "Item 1" };
    var item2 = new ToolStripMenuItem { Name = "item2", Text = "Item 2" };
    menuStrip.Items.Add(item1);
    menuStrip.Items.Add(item2);

    // Act
    var result = menuStrip.Items.FindItem("item1");

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Name, Is.EqualTo("item1"));
  }

  [Test]
  public void FindItem_ShouldReturnNull_WhenNameDoesNotMatch()
  {
    // Arrange
    var menuStrip = new MenuStrip();
    var item1 = new ToolStripMenuItem { Name = "item1", Text = "Item 1" };
    var item2 = new ToolStripMenuItem { Name = "item2", Text = "Item 2" };
    menuStrip.Items.Add(item1);
    menuStrip.Items.Add(item2);

    // Act
    var result = menuStrip.Items.FindItem("item3");

    // Assert
    Assert.That(result, Is.Null);
  }

  [Test]
  public void FindItem_ShouldReturnItem_WhenTextMatches()
  {
    // Arrange
    var menuStrip = new MenuStrip();
    var item1 = new ToolStripMenuItem { Name = "item1", Text = "Item 1" };
    var item2 = new ToolStripMenuItem { Name = "item2", Text = "Item 2" };
    menuStrip.Items.Add(item1);
    menuStrip.Items.Add(item2);

    // Act
    var result = menuStrip.Items.FindItem("Item 2");

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Text, Is.EqualTo("Item 2"));
  }

  [Test]
  public void FindItem_ShouldReturnItem_WhenNestedItemMatches()
  {
    // Arrange
    var menuStrip = new MenuStrip();
    var parentItem = new ToolStripMenuItem { Name = "parent", Text = "Parent" };
    var childItem = new ToolStripMenuItem { Name = "child", Text = "Child" };
    parentItem.DropDownItems.Add(childItem);
    menuStrip.Items.Add(parentItem);

    // Act
    var result = menuStrip.Items.FindItem("child");

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Name, Is.EqualTo("child"));
  }

  [Test]
  public void GetMenuStrip_ShouldReturnMenuStrip_WhenNameMatches()
  {
    // Arrange
    var form = new Form();
    var menuStrip = new MenuStrip { Name = "mainMenu" };
    form.Controls.Add(menuStrip);

    // Act
    var result = form.Controls.GetMenuStrip("mainMenu");

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Name, Is.EqualTo("mainMenu"));
  }

  [Test]
  public void GetMenuStrip_ShouldReturnNull_WhenNameDoesNotMatch()
  {
    // Arrange
    var form = new Form();
    var menuStrip = new MenuStrip { Name = "mainMenu" };
    form.Controls.Add(menuStrip);

    // Act
    var result = form.Controls.GetMenuStrip("nonExistentMenu");

    // Assert
    Assert.That(result, Is.Null);
  }

  [Test]
  public void GetMenuStrip_ShouldReturnNull_WhenNameIsEmpty()
  {
    // Arrange
    var form = new Form();
    var menuStrip = new MenuStrip { Name = "mainMenu" };
    form.Controls.Add(menuStrip);

    // Act
    var result = form.Controls.GetMenuStrip(string.Empty);

    // Assert
    Assert.That(result, Is.Null);
  }

  [Test]
  public void GetMenuStrip_ShouldReturnMenuStrip_WhenNestedMenuStripMatches()
  {
    // Arrange
    var form = new Form();
    var panel = new Panel();
    var menuStrip = new MenuStrip { Name = "nestedMenu" };
    panel.Controls.Add(menuStrip);
    form.Controls.Add(panel);

    // Act
    var result = form.Controls.GetMenuStrip("nestedMenu");

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Name, Is.EqualTo("nestedMenu"));
  }

  [Test]
  public void GetToolStrip_ShouldReturnToolStrip_WhenNameMatches()
  {
    // Arrange
    var form = new Form();
    var toolStrip = new ToolStrip { Name = "mainToolStrip" };
    form.Controls.Add(toolStrip);

    // Act
    var result = form.Controls.GetToolStrip("mainToolStrip");

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Name, Is.EqualTo("mainToolStrip"));
  }

  [Test]
  public void GetToolStrip_ShouldReturnNull_WhenNameDoesNotMatch()
  {
    // Arrange
    var form = new Form();
    var toolStrip = new ToolStrip { Name = "mainToolStrip" };
    form.Controls.Add(toolStrip);

    // Act
    var result = form.Controls.GetToolStrip("nonExistentToolStrip");

    // Assert
    Assert.That(result, Is.Null);
  }

  [Test]
  public void GetToolStrip_ShouldReturnNull_WhenNameIsEmpty()
  {
    // Arrange
    var form = new Form();
    var toolStrip = new ToolStrip { Name = "mainToolStrip" };
    form.Controls.Add(toolStrip);

    // Act
    var result = form.Controls.GetToolStrip(string.Empty);

    // Assert
    Assert.That(result, Is.Null);
  }

  [Test]
  public void GetToolStrip_ShouldReturnToolStrip_WhenNestedToolStripMatches()
  {
    // Arrange
    var form = new Form();
    var panel = new Panel();
    var toolStrip = new ToolStrip { Name = "nestedToolStrip" };
    panel.Controls.Add(toolStrip);
    form.Controls.Add(panel);

    // Act
    var result = form.Controls.GetToolStrip("nestedToolStrip");

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Name, Is.EqualTo("nestedToolStrip"));
  }
}
