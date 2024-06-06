using UnityEditor;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{
    internal static class OptionSettings
    {
        // -------------- Scenes -------------- 
        internal static Option<float> _scaleFactor { get; } = new Option<float>(
            1.0f,
            (int)TOOLBAR.DISABLE,
            "Scale Factor"
        );
        internal static Option<bool> _convertUnits { get; } = new Option<bool>(
            true,
            1,
            "Convert Units(Unsupported)",
            "このオプションは現在対応しておりません。"
        );
        internal static Option<bool> _bakeAxisConversion { get; } = new Option<bool>(
            false,
            (int)TOOLBAR.DISABLE,
            "Bake Axis Conversion"
        );
        internal static Option<bool> _importBlendShapes { get; } = new Option<bool>(
            true,
            (int)TOOLBAR.DISABLE,
            "Import Blend Shapes"
        );
        internal static Option<bool> _importDeformPercent { get; } = new Option<bool>(
            false,
            (int)TOOLBAR.DISABLE,
            "Import Deform Percent"
        );
        internal static Option<bool> _importVisibility { get; } = new Option<bool>(
            true,
            (int)TOOLBAR.DISABLE,
            "Import Visibility"
        );
        internal static Option<bool> _importCameras { get; } = new Option<bool>(
            false,
            (int)TOOLBAR.ENABLE,
            "Import Cameras",
            "これを有効にすると.FBXファイルからカメラをインポートできます。"
        );
        internal static Option<bool> _importLights { get; } = new Option<bool>(
            false,
            (int)TOOLBAR.ENABLE,
            "Import Lights"
        );
        internal static Option<bool> _preserveHierarchy { get; } = new Option<bool>(
            true,
            (int)TOOLBAR.DISABLE,
            "Preserve Hierarchy"
        );
        internal static Option<bool> _sortHierarchyByName { get; } = new Option<bool>(
            true,
            (int)TOOLBAR.DISABLE,
            "Sort Hierarchy ByName"
        );
        // -------------- Meshes -------------- 
        internal static Option<ModelImporterMeshCompression> _meshCompression { get; } = new Option<ModelImporterMeshCompression>(
            ModelImporterMeshCompression.Off,
            (int)TOOLBAR.DISABLE,
            "Mesh Compression"
        );
        internal static Option<bool> _isReadable { get; } = new Option<bool>(
            true,
            (int)TOOLBAR.ENABLE,
            "Read/Write"
        );
        internal static Option<MeshOptimizationFlags> _optimizeMesh { get; } = new Option<MeshOptimizationFlags>(
            MeshOptimizationFlags.Everything,
            (int)TOOLBAR.DISABLE,
            "Optimize Mesh"
        );
        internal static Option<bool> _generateColliders { get; } = new Option<bool>(false,
            (int)TOOLBAR.DISABLE,
            "Generate Colliders"
        );
        // --------------  Germetory -------------- 
        internal static Option<bool> _keepQuads { get; } = new Option<bool>(false,
            (int)TOOLBAR.DISABLE,
            "Keep Quads"
        );
        internal static Option<bool> _weldVertices { get; } = new Option<bool>(true,
            (int)TOOLBAR.DISABLE,
            "Weld Vertices"
        );
        internal static Option<ModelImporterIndexFormat> _indexFormat { get; } = new Option<ModelImporterIndexFormat>(
            ModelImporterIndexFormat.Auto,
            (int)TOOLBAR.DISABLE,
            "Index Format"
        );
        internal static Option<bool> _legacyBlendShapeNomals { get; } = new Option<bool>(
            false,
            (int)TOOLBAR.ENABLE,
            "Legacy BlendShape Nomals"
        );
        internal static Option<ModelImporterNormals> _importNormals { get; } = new Option<ModelImporterNormals>(
            ModelImporterNormals.Import,
            (int)TOOLBAR.ENABLE,
            "Nomals"
        );
        internal static Option<ModelImporterNormals> _importBlendShapeNormals { get; } = new Option<ModelImporterNormals>(
            ModelImporterNormals.None,
            (int)TOOLBAR.ENABLE,
            "Blend Shape Nomals"
        );
        internal static Option<ModelImporterNormalCalculationMode> _normalsMode { get; } = new Option<ModelImporterNormalCalculationMode>(
            ModelImporterNormalCalculationMode.Unweighted_Legacy,
            (int)TOOLBAR.DISABLE,
            "Normals Mode"
        );
        internal static Option<ModelImporterNormalSmoothingSource> _smoothnessSource { get; } = new Option<ModelImporterNormalSmoothingSource>(
            ModelImporterNormalSmoothingSource.PreferSmoothingGroups,
            (int)TOOLBAR.DISABLE,
            ""
        );
        internal static Option<float> _smoothingAngle { get; } = new Option<float>(60,
            (int)TOOLBAR.DISABLE,
            "Smoothing Angle"
        );
        internal static Option<ModelImporterTangents> _tangents { get; } = new Option<ModelImporterTangents>(
            ModelImporterTangents.CalculateMikk,
            (int)TOOLBAR.DISABLE,
            "Tangents"
        );
        internal static Option<bool> _swapUvs { get; } = new Option<bool>(false,
            (int)TOOLBAR.DISABLE,
            "Swap Uvs"
        );
        internal static Option<bool> _generateLightmapUvs { get; } = new Option<bool>(false,
            (int)TOOLBAR.DISABLE,
            "Generate Lightmap UVs"
        );
        internal static Option<bool> _strictVertexDataChecks { get; } = new Option<bool>(false,
            (int)TOOLBAR.DISABLE,
            "Strict Vertext Data Checks"
        );
    }
}