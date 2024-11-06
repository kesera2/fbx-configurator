using kesera2.FBXConfigurator;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FBXConfiguratorUtilityTest
{
    //private static FBXConfigurator window;
    [SetUp]
    public void Setup()
    {
        //window = EditorWindow.GetWindow<FBXConfigurator>("Test Window");
    }

    [Test]
    public void TestGetFBXFiles()
    {
        List<string> fbxFiles = Utility.GetFBXFiles("Assets/");
        Debug.Log(fbxFiles.Count);
    }

}
