using kesera2.FBXOptionsManager;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
public class FbxOptionsTest
{
    private FBXOptionsManager window;
    private static string fbxFile = "Assets/Ç‡ÇøÇ‡ÇøÇ‹Å[Ç∆/FBXOptionsManager/Tests/FBX/cube.fbx";
    private ModelImporter modelImporter;

    [OneTimeSetUp]
    public void SetUpTestClass()
    {
        window = EditorWindow.GetWindow<FBXOptionsManager>("FbxOptionsTest Window");
        modelImporter = AssetImporter.GetAtPath(fbxFile) as ModelImporter;
        Assert.IsNotNull(modelImporter);
    }

    [SetUp, TearDown]
    public void InitializeToDefaultOptions()
    {
        window.options._importCameras.Value = false;
        window.options._importLights.Value = false;
        window.options._importCameras.Value = false;
        window.options._importLights.Value = false;
        window.options._isReadable.Value = false;
        window.options._importNormals.Value = ModelImporterNormals.None;
        window.options._importBlendShapeNormals.Value = ModelImporterNormals.None;
        window.options._legacyBlendShapeNomals.Value = false;
        window.options._scaleFactor.Value = 1.0f;
        window.options._convertUnits.Value = false;
        window.options._bakeAxisConversion.Value = false;
        window.options._importBlendShapes.Value = false;
        window.options._importDeformPercent.Value = false;
        window.options._importVisibility.Value = false;
        window.options._preserveHierarchy.Value = false;
        window.options._sortHierarchyByName.Value = false;
        window.options._meshCompression.Value = ModelImporterMeshCompression.Off;
        window.options._optimizeMesh.Value = MeshOptimizationFlags.Everything;
        window.options._generateColliders.Value = false;
        window.options._keepQuads.Value = false;
        window.options._weldVertices.Value = false;
        window.options._indexFormat.Value = ModelImporterIndexFormat.Auto;
        window.options._normalsMode.Value = ModelImporterNormalCalculationMode.Unweighted_Legacy;
        window.options._smoothnessSource.Value = ModelImporterNormalSmoothingSource.PreferSmoothingGroups;
        window.options._smoothingAngle.Value = 60;
        window.options._tangents.Value = ModelImporterTangents.CalculateMikk;
        window.options._swapUvs.Value = false;
        window.options._generateLightmapUvs.Value = false;
        window.options._strictVertexDataChecks.Value = false;
    }

    [Test]
    public void TestImportCameras()
    {
        //window.options._importCameras.Value = true;
        //modelImporter.SaveAndReimport();
        //AssetDatabase.SaveAssets();

        var propertiesAndValues = new[]
        {
            (nameof(modelImporter.importCameras)),
            (nameof(modelImporter.importLights)),
            (nameof(modelImporter.isReadable))
        };
        Debug.Log(propertiesAndValues[0]);

        //foreach (var (propertyName, property) in propertiesAndValues)
        //{
        //    if (property.ToolbarEnable == (int)TOOLBAR.ENABLE)
        //    {
        //        typeof(ModelImporter)
        //            .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance)
        //            ?.SetValue(modelImporter, property.Value);
        //    }
        //}
    }

    [Test]
    public void showOptionsPasses()
    {
        window.optionFoldOut = true;
        Assert.IsNotNull(window.options);
    }

}
