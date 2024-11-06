using kesera2.FBXConfigurator;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class FbxOptionsTest
{
    private FBXConfiguratorView window;
#if UNITY_EDITOR_WIN
    private static readonly string fbxFile = "./Tests/FBX/cube.fbx";
#elif UNITY_EDITOR_OSX
    private static string fbxFile = "Assets/FBXConfigurator/Tests/FBX/cube.fbx";
#endif
    private FbxOptions options;
    private readonly ModelImporter modelImporter = AssetImporter.GetAtPath(fbxFile) as ModelImporter;

    [OneTimeSetUp]
    public void SetUpTestClass()
    {
        window = EditorWindow.GetWindow<FBXConfiguratorView>("FbxOptionsTest Window");
        options = new FbxOptions();
        options.ImportCameras.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportLights.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportCameras.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportLights.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.IsReadable.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportNormals.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportBlendShapeNormals.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.LegacyBlendShapeNormals.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ScaleFactor.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ConvertUnits.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.BakeAxisConversion.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportBlendShapes.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportDeformPercent.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.ImportVisibility.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.PreserveHierarchy.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.SortHierarchyByName.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.MeshCompression.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.OptimizeMesh.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.GenerateColliders.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.KeepQuads.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.WeldVertices.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.IndexFormat.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.NormalsMode.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.SmoothnessSource.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.SmoothingAngle.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.Tangents.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.SwapUvs.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.GenerateLightmapUvs.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
        options.StrictVertexDataChecks.ToolbarEnable = (int)Toolbar.ToolbarState.Enable;
    }

    private void displayPassMessage<T>(Option<T> option)
    {
        var propertyInfo = modelImporter.GetType().GetProperty(option.FieldName);
        Debug.Log(
            $"Passed: {option.Label} -> Option: {option.Value} == Actually: {propertyInfo.GetValue(modelImporter)} {fbxFile}");
    }

    private void Apply()
    {
        modelImporter.SaveAndReimport();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void showCommonLog<T>(Option<T> option)
    {
        Debug.Log(
            $"Label: {option.Label}, fieldName: {option.FieldName}, Value: {option.Value}, Tooltip: {option.Tooltip}, ToolbarEnable: {option.ToolbarEnable}");
    }

    [Test]
    public void showOptionsPasses()
    {
        window.OptionFoldOut = true;
        Assert.IsNotNull(window.Options);
    }

    private T GetField<T>(string fieldName)
    {
        var propertyInfo = modelImporter.GetType().GetProperty(fieldName);
        var propertyValue = (T)propertyInfo.GetValue(modelImporter);
        return propertyValue;
    }

    private void TestBoolOption(Option<bool> option)
    {
        showCommonLog(option);
        option.Value = false;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<bool>(option.FieldName) == option.Value);
        displayPassMessage(option);
        option.Value = true;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<bool>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    private void TestFloatOption(Option<float> option)
    {
        showCommonLog(option);
        option.Value = 0.1f;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<float>(option.FieldName) == option.Value);
        displayPassMessage(option);
        option.Value = 1.0f;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<float>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    private void TestModelImporterNomalsOption(Option<ModelImporterNormals> option)
    {
        showCommonLog(option);
        option.Value = ModelImporterNormals.Calculate;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormals>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormals.Import;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormals>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormals.None;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormals>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestImportCameras()
    {
        TestBoolOption(options.ImportCameras);
    }

    [Test]
    public void TestImportImportLights()
    {
        TestBoolOption(options.ImportLights);
    }

    [Test]
    public void TestImportIsReadable()
    {
        TestBoolOption(options.IsReadable);
    }

    // NOTE: Must use the reflection to change
    [Test]
    public void TestImportLegacyBlendShapeNomals()
    {
        //TestBoolOption(options._legacyBlendShapeNomals);
        var option = options.LegacyBlendShapeNormals;
        var propertyValue = options.GetLegacyBlendShapeNormals(modelImporter);
        option.Value = false;
        options.ApplyLegacyBlendShapeNormals(modelImporter);
        Apply();
        Assert.That(propertyValue == option.Value);
        Debug.Log($"Passed: {option.Label} -> Option: {option.Value} == Actually: {propertyValue} {fbxFile}");
        option.Value = true;
        options.ApplyLegacyBlendShapeNormals(modelImporter);
        Apply();
        propertyValue = options.GetLegacyBlendShapeNormals(modelImporter);
        Assert.That(propertyValue == option.Value);
        Debug.Log($"Passed: {option.Label} -> Option: {option.Value} == Actually: {propertyValue} {fbxFile}");
    }

    [Test]
    public void TestImportConvertUnits()
    {
        TestBoolOption(options.ConvertUnits);
    }

    [Test]
    public void TestImportBakeAxisConversion()
    {
        TestBoolOption(options.BakeAxisConversion);
    }

    [Test]
    public void TestImportImportBlendShapes()
    {
        TestBoolOption(options.ImportBlendShapes);
    }

    [Test]
    public void TestImportImportDeformPercent()
    {
        TestBoolOption(options.ImportDeformPercent);
    }

    [Test]
    public void TestImportImportVisibility()
    {
        TestBoolOption(options.ImportVisibility);
    }

    [Test]
    public void TestImportPreserveHierarchy()
    {
        TestBoolOption(options.PreserveHierarchy);
    }

    [Test]
    public void TestImportSortHierarchyByName()
    {
        TestBoolOption(options.SortHierarchyByName);
    }

    [Test]
    public void TestImportGenerateColliders()
    {
        TestBoolOption(options.GenerateColliders);
    }

    [Test]
    public void TestImportKeepQuads()
    {
        TestBoolOption(options.KeepQuads);
    }

    [Test]
    public void TestImportWeldVertices()
    {
        TestBoolOption(options.WeldVertices);
    }

    [Test]
    public void TestImportSwapUvs()
    {
        TestBoolOption(options.SwapUvs);
    }

    [Test]
    public void TestImportGenerateLightmapUvs()
    {
        TestBoolOption(options.GenerateLightmapUvs);
    }

    [Test]
    public void TestImportStrictVertexDataChecks()
    {
        TestBoolOption(options.StrictVertexDataChecks);
    }

    [Test]
    public void TestScaleFactor()
    {
        TestFloatOption(options.ScaleFactor);
    }

    [Test]
    public void TestSmoothingAngle()
    {
        TestFloatOption(options.SmoothingAngle);
    }

    [Test]
    public void TestImportNormals()
    {
        TestModelImporterNomalsOption(options.ImportNormals);
    }

    [Test]
    public void TestImportBlendShapeNormals()
    {
        TestModelImporterNomalsOption(options.ImportBlendShapeNormals);
    }

    [Test]
    public void TestModelImporterMeshCompression()
    {
        var option = options.MeshCompression;
        showCommonLog(option);
        option.Value = ModelImporterMeshCompression.Low;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterMeshCompression.Medium;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterMeshCompression.High;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterMeshCompression.Off;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterMeshCompression>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestMeshOptimizationFlags()
    {
        var option = options.OptimizeMesh;
        showCommonLog(option);
        option.Value = MeshOptimizationFlags.Everything;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<MeshOptimizationFlags>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = MeshOptimizationFlags.PolygonOrder;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<MeshOptimizationFlags>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = MeshOptimizationFlags.VertexOrder;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<MeshOptimizationFlags>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterIndexFormat()
    {
        var option = options.IndexFormat;
        showCommonLog(option);
        option.Value = ModelImporterIndexFormat.UInt16;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterIndexFormat>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterIndexFormat.UInt32;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterIndexFormat>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterIndexFormat.Auto;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterIndexFormat>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterNormalCalculationMode()
    {
        var option = options.NormalsMode;
        showCommonLog(option);
        option.Value = ModelImporterNormalCalculationMode.Unweighted_Legacy;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.Unweighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.AngleWeighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.AreaAndAngleWeighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalCalculationMode.AreaWeighted;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalCalculationMode>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterNormalSmoothingSource()
    {
        var option = options.SmoothnessSource;
        showCommonLog(option);
        option.Value = ModelImporterNormalSmoothingSource.FromAngle;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalSmoothingSource.FromSmoothingGroups;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterNormalSmoothingSource.PreferSmoothingGroups;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option.FieldName) == option.Value);
        displayPassMessage(option);


        option.Value = ModelImporterNormalSmoothingSource.None;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterNormalSmoothingSource>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }

    [Test]
    public void TestModelImporterTangents()
    {
        // NOTE: Must be ModelImporterNormals.Import or Calculate before testing.
        options.ImportNormals.Value = ModelImporterNormals.Import;
        options.ImportNormals.Update(modelImporter);

        var option = options.Tangents;
        showCommonLog(option);
        option.Value = ModelImporterTangents.Import;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.CalculateLegacy;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.None;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.CalculateLegacyWithSplitTangents;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option.FieldName) == option.Value);
        displayPassMessage(option);

        option.Value = ModelImporterTangents.CalculateMikk;
        option.Update(modelImporter);
        Apply();
        Assert.That(GetField<ModelImporterTangents>(option.FieldName) == option.Value);
        displayPassMessage(option);
    }
}