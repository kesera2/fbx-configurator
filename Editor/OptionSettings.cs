using UnityEditor;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{
    internal static class OptionSettings
    {
        // -------------- Scenes -------------- 
        internal static readonly Option<float> scaleFactor = new Option<float>(
            value: 1.0f,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Scale Factor",
            fieldName: "globalScale",
            tooltip: ""
        );
        internal static readonly Option<bool> convertUnits = new Option<bool>(
            value: true,
            toolbarEnable: 1,
            label: "Convert Units(Unsupported)",
            fieldName: "useFileUnits",
            tooltip: "このオプションは現在対応しておりません。"
        );
        internal static readonly Option<bool> bakeAxisConversion = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Bake Axis Conversion",
            fieldName: "bakeAxisConversion",
            tooltip: ""
        );
        internal static readonly Option<bool> importBlendShapes = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Import Blend Shapes",
            fieldName: "importBlendShapes",
            tooltip: ""
        );
        internal static readonly Option<bool> importDeformPercent = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Import Deform Percent",
            fieldName: "importBlendShapeDeformPercent",
            tooltip: ""
        );
        internal static readonly Option<bool> importVisibility = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Import Visibility",
            fieldName: "importVisibility",
            tooltip: ""
        );
        internal static readonly Option<bool> importCameras = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Import Cameras",
            fieldName: "importCameras",
            tooltip: "これを有効にすると.FBXファイルからカメラをインポートできます。"
        );
        internal static readonly Option<bool> importLights = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Import Lights",
            fieldName: "importLights",
            tooltip: ""
        );
        internal static readonly Option<bool> preserveHierarchy = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Preserve Hierarchy",
            fieldName: "preserveHierarchy",
            tooltip: ""
        );
        internal static readonly Option<bool> sortHierarchyByName = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Sort Hierarchy ByName",
            fieldName: "sortHierarchyByName",
            tooltip: ""
        );
        // -------------- Meshes -------------- 
        internal static readonly Option<ModelImporterMeshCompression> meshCompression = new Option<ModelImporterMeshCompression>(
            value: ModelImporterMeshCompression.Off,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Mesh Compression",
            fieldName: "meshCompression",
            tooltip: ""
        );
        internal static readonly Option<bool> isReadable = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Read/Write",
            fieldName: "isReadable",
            tooltip: ""
        );
        internal static readonly Option<MeshOptimizationFlags> optimizeMesh = new Option<MeshOptimizationFlags>(
            value: MeshOptimizationFlags.Everything,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Optimize Mesh",
            fieldName: "meshOptimizationFlags",
            tooltip: ""
        );
        internal static readonly Option<bool> generateColliders = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Generate Colliders",
            fieldName: "addCollider",
            tooltip: "");
        // --------------  Germetory -------------- 
        internal static readonly Option<bool> keepQuads = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Keep Quads",
            fieldName: "keepQuads",
            tooltip: ""
        );
        internal static readonly Option<bool> weldVertices = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Weld Vertices",
            fieldName: "weldVertices",
            tooltip: ""
        );
        internal static readonly Option<ModelImporterIndexFormat> indexFormat = new Option<ModelImporterIndexFormat>(
            value: ModelImporterIndexFormat.Auto,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Index Format",
            fieldName: "indexFormat",
            tooltip: ""
        );
        internal static readonly Option<bool> legacyBlendShapeNomals = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Legacy BlendShape Nomals",
            fieldName: "legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes",
            tooltip: ""
        );
        internal static readonly Option<ModelImporterNormals> importNormals = new Option<ModelImporterNormals>(
            value: ModelImporterNormals.Import,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Nomals",
            fieldName: "importNormals",
            tooltip: ""
        );
        internal static readonly Option<ModelImporterNormals> importBlendShapeNormals = new Option<ModelImporterNormals>(
            value: ModelImporterNormals.None,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: "Blend Shape Nomals",
            fieldName: "importBlendShapeNormals",
            tooltip: ""
        );
        internal static readonly Option<ModelImporterNormalCalculationMode> normalsMode = new Option<ModelImporterNormalCalculationMode>(
            value: ModelImporterNormalCalculationMode.Unweighted_Legacy,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Normals Mode",
            fieldName: "normalCalculationMode",
            tooltip: ""
        );
        internal static readonly Option<ModelImporterNormalSmoothingSource> smoothnessSource = new Option<ModelImporterNormalSmoothingSource>(
            value: ModelImporterNormalSmoothingSource.PreferSmoothingGroups,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Smoothness Source",
            fieldName: "normalSmoothingSource",
            tooltip: ""
        );
        internal static readonly Option<float> smoothingAngle = new Option<float>(
            value: 60,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Smoothing Angle",
            fieldName: "normalSmoothingAngle",
            tooltip: ""
        );
        internal static readonly Option<ModelImporterTangents> tangents = new Option<ModelImporterTangents>(
            value: ModelImporterTangents.CalculateMikk,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Tangents",
            fieldName: "importTangents",
            tooltip: ""
        );
        internal static readonly Option<bool> swapUvs = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Swap Uvs",
            fieldName: "swapUVChannels",
            tooltip: ""
        );
        internal static readonly Option<bool> generateLightmapUvs = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Generate Lightmap UVs",
            fieldName: "generateSecondaryUV",
            tooltip: ""
            );
        internal static readonly Option<bool> strictVertexDataChecks = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: "Strict Vertext Data Checks",
            fieldName: "strictVertexDataChecks",
            tooltip: ""
        );
    }
}