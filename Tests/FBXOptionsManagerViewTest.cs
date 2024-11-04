using kesera2.FBXOptionsManager;
using NUnit.Framework;
using UnityEditor;

public class FBXOptionsManagerViewTest
{
    private static FBXOptionsManagerView window;

    [SetUp]
    public void Setup()
    {
        window = EditorWindow.GetWindow<FBXOptionsManagerView>("Test Window");
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
        // �E�B���h�E��\������
        FBXOptionsManagerView.ShowWindow();

        // �{�^�����N���b�N����
        //EditorWindow.GetWindow<FBXOptionsManager>().OnGUI();
        //Assert.AreEqual("Default Value", EditorWindow.GetWindow<FBXOptionsManager>()._myString);
    }
}
