using kesera2.FBXOptionsManager;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class FbxOptionsTest
{
    private FBXOptionsManager window;
#if UNITY_EDITOR_WIN
    private static string fbxFile = "Assets/もちもちまーと/FBXOptionsManager/Tests/FBX/cube.fbx";
#elif UNITY_EDITOR_OSX
    private static string fbxFile = "Assets/FBXOptionsManager/Tests/FBX/cube.fbx";
#endif
    private FbxOptions options;
    private readonly ModelImporter modelImporter = AssetImporter.GetAtPath(fbxFile) as ModelImporter;

    [OneTimeSetUp]
    public void SetUpTestClass()
    {
        window = EditorWindow.GetWindow<FBXOptionsManager>("FbxOptionsTest Window");
        options = new FbxOptions();
        options._importCameras.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importLights.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importCameras.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importLights.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._isReadable.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importNormals.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importBlendShapeNormals.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._legacyBlendShapeNomals.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._scaleFactor.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._convertUnits.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._bakeAxisConversion.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importBlendShapes.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importDeformPercent.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._importVisibility.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._preserveHierarchy.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._sortHierarchyByName.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._meshCompression.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._optimizeMesh.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._generateColliders.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._keepQuads.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._weldVertices.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._indexFormat.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._normalsMode.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._smoothnessSource.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._smoothingAngle.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._tangents.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._swapUvs.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._generateLightmapUvs.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
        options._strictVertexDataChecks.ToolbarEnable = (int)Toolbar.TOOLBAR.ENABLE;
    }

    private void displayPassMessage<T>(Option<T> option)
    {
        var propertyInfo = modelImporter.GetType().GetProperty(option._fieldName);
        Debug.Log($"Passed: {option.Label} -> Option: {option.Value} == Actually: {propertyInfo.GetValue(modelImporter)} {fbxFile}");
    }

    private void Apply()
    {
        modelImporter.SaveAndReimport();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void showCommonLog<T>(Option<T> option)
    {
        Debug.Log($"Label: {option.Label}, fieldName: {option._fieldName}, Value: {option.Value}, Tooltip: {option.Tooltip}, ToolbarEnable: {option.ToolbarEnable}");
    }

    [Test]
    public void showOptionsPasses()
    {
        window.optionFoldOut = true;
        Assert.IsNotNull(window.options);
    }

    private T GetField<T>(string fieldName)
    {
        var propertyInfo = modelImporter.GetType().GetProperty(fieldName);
        T propertyValue = (T)propertyInfo.GetValue(modelImporter);
        return propertyValue;
    }

    private void TestBoolOption(Option<bool> option)
    {
        showCommonLog(option);
        option.Value = false;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<bool>(option._fieldName) == option.Value);
        displayPassMessage(option);
        option.Value = true;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<bool>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    private void TestFloatOption(Option<float> option)
    {
        showCommonLog(option);
        option.Value = 0.1f;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<float>(option._fieldName) == option.Value);
        displayPassMessage(option);
        option.Value = 1.0f;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<float>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    private void TestModelImporterNomalsOption(Option<ModelImporterNormals> option)
    {
        showCommonLog(option);
        option.Value = ModelImporterNormals.Calculate;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormals>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormals.Import;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormals>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormals.None;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormals>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestImportCameras()
    {
        TestBoolOption(options._importCameras);
    }
    [Test]
    public void TestImportImportLights()
    {
        TestBoolOption(options._importLights);
    }

    [Test]
    public void TestImportIsReadable()
    {
        TestBoolOption(options._isReadable);
    }

    // NOTE: Must use the reflection to change
    [Test]
    public void TestImportLegacyBlendShapeNomals()
    {
        //TestBoolOption(options._legacyBlendShapeNomals);
        Option<bool> option = options._legacyBlendShapeNomals;
        bool propertyValue = options.GetLegacyBlendShapeNomals(modelImporter);
        option.Value = false;
        options.ApplyLegacyBlendShapeNomals(modelImporter);
        Apply();
        Assert.That(propertyValue == option.Value);
        Debug.Log($"Passed: {option.Label} -> Option: {option.Value} == Actually: {propertyValue} {fbxFile}");
        option.Value = true;
        options.ApplyLegacyBlendShapeNomals(modelImporter);
        Apply();
        propertyValue = options.GetLegacyBlendShapeNomals(modelImporter);
        Assert.That(propertyValue == option.Value);
        Debug.Log($"Passed: {option.Label} -> Option: {option.Value} == Actually: {propertyValue} {fbxFile}");
    }

    [Test]
    public void TestImportConvertUnits()
    {
        TestBoolOption(options._convertUnits);
    }

    [Test]
    public void TestImportBakeAxisConversion()
    {
        TestBoolOption(options._bakeAxisConversion);
    }

    [Test]
    public void TestImportImportBlendShapes()
    {
        TestBoolOption(options._importBlendShapes);
    }

    [Test]
    public void TestImportImportDeformPercent()
    {
        TestBoolOption(options._importDeformPercent);
    }

    [Test]
    public void TestImportImportVisibility()
    {
        TestBoolOption(options._importVisibility);
    }

    [Test]
    public void TestImportPreserveHierarchy()
    {
        TestBoolOption(options._preserveHierarchy);
    }

    [Test]
    public void TestImportSortHierarchyByName()
    {
        TestBoolOption(options._sortHierarchyByName);
    }

    [Test]
    public void TestImportGenerateColliders()
    {
        TestBoolOption(options._generateColliders);
    }

    [Test]
    public void TestImportKeepQuads()
    {
        TestBoolOption(options._keepQuads);
    }

    [Test]
    public void TestImportWeldVertices()
    {
        TestBoolOption(options._weldVertices);
    }

    [Test]
    public void TestImportSwapUvs()
    {
        TestBoolOption(options._swapUvs);
    }

    [Test]
    public void TestImportGenerateLightmapUvs()
    {
        TestBoolOption(options._generateLightmapUvs);
    }

    [Test]
    public void TestImportStrictVertexDataChecks()
    {
        TestBoolOption(options._strictVertexDataChecks);
    }

    [Test]
    public void TestScaleFactor()
    {
        TestFloatOption(options._scaleFactor);
    }

    [Test]
    public void TestSmoothingAngle()
    {
        TestFloatOption(options._smoothingAngle);
    }

    [Test]
    public void TestImportNormals()
    {
        TestModelImporterNomalsOption(options._importNormals);
    }

    [Test]
    public void TestImportBlendShapeNormals()
    {
        TestModelImporterNomalsOption(options._importBlendShapeNormals);
    }

    [Test]
    public void TestModelImporterMeshCompression()
    {
        Option<ModelImporterMeshCompression> option = options._meshCompression;
        showCommonLog(option);
        option.Value = ModelImporterMeshCompression.Low;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterMeshCompression.Medium;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterMeshCompression.High;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterMeshCompression.Off;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestMeshOptimizationFlags()
    {
        Option<MeshOptimizationFlags> option = options._optimizeMesh;
        showCommonLog(option);
        option.Value = MeshOptimizationFlags.Everything;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<MeshOptimizationFlags>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = MeshOptimizationFlags.PolygonOrder;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<MeshOptimizationFlags>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = MeshOptimizationFlags.VertexOrder;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<MeshOptimizationFlags>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterIndexFormat()
    {
        Option<ModelImporterIndexFormat> option = options._indexFormat;
        showCommonLog(option);
        option.Value = ModelImporterIndexFormat.UInt16;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterIndexFormat>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterIndexFormat.UInt32;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterIndexFormat>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterIndexFormat.Auto;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterIndexFormat>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterNormalCalculationMode()
    {
        Option<ModelImporterNormalCalculationMode> option = options._normalsMode;
        showCommonLog(option);
        option.Value = ModelImporterNormalCalculationMode.Unweighted_Legacy;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.Unweighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.AngleWeighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.AreaAndAngleWeighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.AreaWeighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterNormalSmoothingSource()
    {
        Option<ModelImporterNormalSmoothingSource> option = options._smoothnessSource;
        showCommonLog(option);
        option.Value = ModelImporterNormalSmoothingSource.FromAngle;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalSmoothingSource.FromSmoothingGroups;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalSmoothingSource.PreferSmoothingGroups;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option._fieldName) == option.Value);
        displayPassMessage(option);


        option.Value = ModelImporterNormalSmoothingSource.None;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterTangents()
    {
        // NOTE: Must be ModelImporterNormals.Import or Calculate before testing.
        options._importNormals.Value = ModelImporterNormals.Import;
        options._importNormals.Update(modelImporter);

        Option<ModelImporterTangents> option = options._tangents;
        showCommonLog(option);
        option.Value = ModelImporterTangents.Import;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.CalculateLegacy;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.None;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.CalculateLegacyWithSplitTangents;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option._fieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.CalculateMikk;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option._fieldName) == option.Value);
        displayPassMessage(option);
    }

}
