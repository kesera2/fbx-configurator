using kesera2.FBXConfigurator;
using NUnit.Framework;
using UnityEditor;

public class FBXConfiguratorViewTest
{
    private static FBXConfiguratorView window;

    [SetUp]
    public void Setup()
    {
        window = EditorWindow.GetWindow<FBXConfiguratorView>("Test Window");
    }
    [Test]
    public void TestShowWindow()
    {
        Assert.IsNotNull(window);
    }

    [Test]
    public void TestRelativePath()
    {
        Assert.That(window.RelativePath.StartsWith("Assets"));
    }

    [Test]
    public void TestButtonClick()
    {
        // ?E?B???h?E??\??????
        FBXConfiguratorView.ShowWindow();

        // ?{?^?????N???b?N????
        //EditorWindow.GetWindow<FBXConfigurator>().OnGUI();
        //Assert.AreEqual("Default Value", EditorWindow.GetWindow<FBXConfigurator>()._myString);
    }
}
