using UnityEditor;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{
    internal class OptionSettings
    {
        // -------------- Scenes -------------- 
        internal readonly Option<float> scaleFactor = new Option<float>(
            value: 1.0f,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelScaleFactor,
            fieldName: "globalScale",
            tooltip: ""
        );
        internal readonly Option<bool> convertUnits = new Option<bool>(
            value: true,
            toolbarEnable: 1,
            label: "Convert Units(Unsupported)",
            fieldName: "useFileUnits",
            tooltip: "このオプションは現在対応しておりません。"
        );
        internal readonly Option<bool> bakeAxisConversion = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelBakeAxisConversion,
            fieldName: "bakeAxisConversion",
            tooltip: ""
        );
        internal readonly Option<bool> importBlendShapes = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelImportBlendShapeNormals,
            fieldName: "importBlendShapes",
            tooltip: ""
        );
        internal readonly Option<bool> importDeformPercent = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelImportDeformPercent,
            fieldName: "importBlendShapeDeformPercent",
            tooltip: ""
        );
        internal readonly Option<bool> importVisibility = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelImportVisibility,
            fieldName: "importVisibility",
            tooltip: ""
        );
        internal readonly Option<bool> importCameras = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: Localization.lang.labelImportCameras,
            fieldName: "importCameras",
            tooltip: "これを有効にすると.FBXファイルからカメラをインポートできます。"
        );
        internal readonly Option<bool> importLights = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: Localization.lang.labelImportLights,
            fieldName: "importLights",
            tooltip: ""
        );
        internal readonly Option<bool> preserveHierarchy = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelPreserveHierarchy,
            fieldName: "preserveHierarchy",
            tooltip: ""
        );
        internal readonly Option<bool> sortHierarchyByName = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelSortHierarchyByName,
            fieldName: "sortHierarchyByName",
            tooltip: ""
        );
        // -------------- Meshes -------------- 
        internal readonly Option<ModelImporterMeshCompression> meshCompression = new Option<ModelImporterMeshCompression>(
            value: ModelImporterMeshCompression.Off,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelMeshCompression,
            fieldName: "meshCompression",
            tooltip: ""
        );
        internal readonly Option<bool> isReadable = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: Localization.lang.labelIsReadable,
            fieldName: "isReadable",
            tooltip: ""
        );
        internal readonly Option<MeshOptimizationFlags> optimizeMesh = new Option<MeshOptimizationFlags>(
            value: MeshOptimizationFlags.Everything,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelOptimizeMesh,
            fieldName: "meshOptimizationFlags",
            tooltip: ""
        );
        internal readonly Option<bool> generateColliders = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelGenerateColliders,
            fieldName: "addCollider",
            tooltip: "");
        // --------------  Germetory -------------- 
        internal readonly Option<bool> keepQuads = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelKeepQuads,
            fieldName: "keepQuads",
            tooltip: ""
        );
        internal readonly Option<bool> weldVertices = new Option<bool>(
            value: true,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelWeldVertices,
            fieldName: "weldVertices",
            tooltip: ""
        );
        internal readonly Option<ModelImporterIndexFormat> indexFormat = new Option<ModelImporterIndexFormat>(
            value: ModelImporterIndexFormat.Auto,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelIndexFormat,
            fieldName: "indexFormat",
            tooltip: ""
        );
        internal readonly Option<bool> legacyBlendShapeNomals = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: Localization.lang.labelLegacyBlendShapeNomals,
            fieldName: "legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes",
            tooltip: ""
        );
        internal readonly Option<ModelImporterNormals> importNormals = new Option<ModelImporterNormals>(
            value: ModelImporterNormals.Import,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: Localization.lang.labelImportNormals,
            fieldName: "importNormals",
            tooltip: ""
        );
        internal readonly Option<ModelImporterNormals> importBlendShapeNormals = new Option<ModelImporterNormals>(
            value: ModelImporterNormals.None,
            toolbarEnable: (int)TOOLBAR.ENABLE,
            label: Localization.lang.labelImportBlendShapeNormals,
            fieldName: "importBlendShapeNormals",
            tooltip: ""
        );
        internal readonly Option<ModelImporterNormalCalculationMode> normalsMode = new Option<ModelImporterNormalCalculationMode>(
            value: ModelImporterNormalCalculationMode.Unweighted_Legacy,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelNormalsMode,
            fieldName: "normalCalculationMode",
            tooltip: ""
        );
        internal readonly Option<ModelImporterNormalSmoothingSource> smoothnessSource = new Option<ModelImporterNormalSmoothingSource>(
            value: ModelImporterNormalSmoothingSource.PreferSmoothingGroups,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelSmoothnessSource,
            fieldName: "normalSmoothingSource",
            tooltip: ""
        );
        internal readonly Option<float> smoothingAngle = new Option<float>(
            value: 60,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelSmoothingAngle,
            fieldName: "normalSmoothingAngle",
            tooltip: ""
        );
        internal readonly Option<ModelImporterTangents> tangents = new Option<ModelImporterTangents>(
            value: ModelImporterTangents.CalculateMikk,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelTangents,
            fieldName: "importTangents",
            tooltip: ""
        );
        internal readonly Option<bool> swapUvs = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelSwapUvs,
            fieldName: "swapUVChannels",
            tooltip: ""
        );
        internal readonly Option<bool> generateLightmapUvs = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelGenerateLightmapUvs,
            fieldName: "generateSecondaryUV",
            tooltip: ""
            );
        internal readonly Option<bool> strictVertexDataChecks = new Option<bool>(
            value: false,
            toolbarEnable: (int)TOOLBAR.DISABLE,
            label: Localization.lang.labelStrictVertexDataChecks,
            fieldName: "strictVertexDataChecks",
            tooltip: ""
        );
    }
}