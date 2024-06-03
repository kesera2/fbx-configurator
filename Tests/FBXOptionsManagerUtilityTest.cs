using kesera2.FBXOptionsManager;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FBXOptionsManagerUtilityTest
{
    //private static FBXOptionsManager window;
    [SetUp]
    public void Setup()
    {
        //window = EditorWindow.GetWindow<FBXOptionsManager>("Test Window");
    }

    [Test]
    public void TestGetFBXFiles()
    {
        List<string> fbxFiles = Utility.GetFBXFiles("Assets/");
        Debug.Log(fbxFiles.Count);
    }

}
