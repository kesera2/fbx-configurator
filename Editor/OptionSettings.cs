using UnityEditor;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{
    internal static class OptionSettings
    {
        // -------------- Scenes -------------- 
        internal static Option<float> _scaleFactor { get; } = new Option<float>(
            value: 1.0f,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Scale Factor",
            fieldName: "globalScale",
            tooltip: ""
        );
        internal static Option<bool> _convertUnits { get; } = new Option<bool>(
            value: true,
            toolbarEnable: 1,
            label: "Convert Units(Unsupported)",
            fieldName: "useFileUnits",
            tooltip: "このオプションは現在対応しておりません。"
        );
        internal static Option<bool> _bakeAxisConversion { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Bake Axis Conversion",
            fieldName: "bakeAxisConversion",
            tooltip: ""
        );
        internal static Option<bool> _importBlendShapes { get; } = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Import Blend Shapes",
            fieldName: "importBlendShapes",
            tooltip: ""
        );
        internal static Option<bool> _importDeformPercent { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Import Deform Percent",
            fieldName: "importBlendShapeDeformPercent",
            tooltip: ""
        );
        internal static Option<bool> _importVisibility { get; } = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Import Visibility",
            fieldName: "importVisibility",
            tooltip: ""
        );
        internal static Option<bool> _importCameras { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Import Cameras",
            fieldName: "importCameras",
            tooltip: "これを有効にすると.FBXファイルからカメラをインポートできます。"
        );
        internal static Option<bool> _importLights { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Import Lights",
            fieldName: "importLights",
            tooltip: ""
        );
        internal static Option<bool> _preserveHierarchy { get; } = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Preserve Hierarchy",
            fieldName: "preserveHierarchy",
            tooltip: ""
        );
        internal static Option<bool> _sortHierarchyByName { get; } = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Sort Hierarchy ByName",
            fieldName: "sortHierarchyByName",
            tooltip: ""
        );
        // -------------- Meshes -------------- 
        internal static Option<ModelImporterMeshCompression> _meshCompression { get; } = new Option<ModelImporterMeshCompression>(
            value: ModelImporterMeshCompression.Off,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Mesh Compression",
            fieldName: "meshCompression",
            tooltip: ""
        );
        internal static Option<bool> _isReadable { get; } = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Read/Write",
            fieldName: "isReadable",
            tooltip: ""
        );
        internal static Option<MeshOptimizationFlags> _optimizeMesh { get; } = new Option<MeshOptimizationFlags>(
            value: MeshOptimizationFlags.Everything,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Optimize Mesh",
            fieldName: "meshOptimizationFlags",
            tooltip: ""
        );
        internal static Option<bool> _generateColliders { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Generate Colliders",
            fieldName: "addCollider",
            tooltip: "");
        // --------------  Germetory -------------- 
        internal static Option<bool> _keepQuads { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Keep Quads",
            fieldName: "keepQuads",
            tooltip: ""
        );
        internal static Option<bool> _weldVertices { get; } = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Weld Vertices",
            fieldName: "weldVertices",
            tooltip: ""
        );
        internal static Option<ModelImporterIndexFormat> _indexFormat { get; } = new Option<ModelImporterIndexFormat>(
            value: ModelImporterIndexFormat.Auto,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Index Format",
            fieldName: "indexFormat",
            tooltip: ""
        );
        internal static Option<bool> _legacyBlendShapeNomals { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Legacy BlendShape Nomals",
            fieldName: "legacyBlendShapeNomals",
            tooltip: ""
        );
        internal static Option<ModelImporterNormals> _importNormals { get; } = new Option<ModelImporterNormals>(
            value: ModelImporterNormals.Import,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Nomals",
            fieldName: "importNormals",
            tooltip: ""
        );
        internal static Option<ModelImporterNormals> _importBlendShapeNormals { get; } = new Option<ModelImporterNormals>(
            value: ModelImporterNormals.None,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Blend Shape Nomals",
            fieldName: "importBlendShapeNormals",
            tooltip: ""
        );
        internal static Option<ModelImporterNormalCalculationMode> _normalsMode { get; } = new Option<ModelImporterNormalCalculationMode>(
            value: ModelImporterNormalCalculationMode.Unweighted_Legacy,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Normals Mode",
            fieldName: "normalCalculationMode",
            tooltip: ""
        );
        internal static Option<ModelImporterNormalSmoothingSource> _smoothnessSource { get; } = new Option<ModelImporterNormalSmoothingSource>(
            value: ModelImporterNormalSmoothingSource.PreferSmoothingGroups,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Smoothness Source",
            fieldName: "normalSmoothingSource",
            tooltip: ""
        );
        internal static Option<float> _smoothingAngle { get; } = new Option<float>(
            value: 60,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Smoothing Angle",
            fieldName: "normalSmoothingAngle",
            tooltip: ""
        );
        internal static Option<ModelImporterTangents> _tangents { get; } = new Option<ModelImporterTangents>(
            value: ModelImporterTangents.CalculateMikk,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Tangents",
            fieldName: "importTangents",
            tooltip: ""
        );
        internal static Option<bool> _swapUvs { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Swap Uvs",
            fieldName: "swapUVChannels",
            tooltip: ""
        );
        internal static Option<bool> _generateLightmapUvs { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Generate Lightmap UVs",
            fieldName: "generateSecondaryUV",
            tooltip: ""
            );
        internal static Option<bool> _strictVertexDataChecks { get; } = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Strict Vertext Data Checks",
            fieldName: "strictVertexDataChecks",
            tooltip: ""
        );
    }
}