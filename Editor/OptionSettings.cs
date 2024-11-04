using UnityEditor;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{
    internal class OptionSettings
    {
        internal readonly Option<bool> BakeAxisConversion = new(
            false,
            (int)ToolbarState.Disable,
            Localization.lang.labelBakeAxisConversion,
            "bakeAxisConversion",
            ""
        );

        internal readonly Option<bool> ConvertUnits = new(
            true,
            1,
            "Convert Units(Unsupported)",
            "useFileUnits",
            "このオプションは現在対応しておりません。"
        );

        internal readonly Option<bool> GenerateColliders = new(
            false,
            (int)ToolbarState.Disable,
            Localization.lang.labelGenerateColliders,
            "addCollider",
            "");

        internal readonly Option<bool> GenerateLightmapUvs = new(
            false,
            (int)ToolbarState.Disable,
            Localization.lang.labelGenerateLightmapUvs,
            "generateSecondaryUV",
            ""
        );

        internal readonly Option<ModelImporterNormals> ImportBlendShapeNormals = new(
            ModelImporterNormals.None,
            (int)ToolbarState.Enable,
            Localization.lang.labelImportBlendShapeNormals,
            "importBlendShapeNormals",
            ""
        );

        internal readonly Option<bool> ImportBlendShapes = new(
            true,
            (int)ToolbarState.Disable,
            Localization.lang.labelImportBlendShapeNormals,
            "importBlendShapes",
            ""
        );

        internal readonly Option<bool> ImportCameras = new(
            false,
            (int)ToolbarState.Enable,
            Localization.lang.labelImportCameras,
            "importCameras",
            "これを有効にすると.FBXファイルからカメラをインポートできます。"
        );

        internal readonly Option<bool> ImportDeformPercent = new(
            false,
            (int)ToolbarState.Disable,
            Localization.lang.labelImportDeformPercent,
            "importBlendShapeDeformPercent",
            ""
        );

        internal readonly Option<bool> ImportLights = new(
            false,
            (int)ToolbarState.Enable,
            Localization.lang.labelImportLights,
            "importLights",
            ""
        );

        internal readonly Option<ModelImporterNormals> ImportNormals = new(
            ModelImporterNormals.Import,
            (int)ToolbarState.Enable,
            Localization.lang.labelImportNormals,
            "importNormals",
            ""
        );

        internal readonly Option<bool> ImportVisibility = new(
            true,
            (int)ToolbarState.Disable,
            Localization.lang.labelImportVisibility,
            "importVisibility",
            ""
        );

        internal readonly Option<ModelImporterIndexFormat> IndexFormat = new(
            ModelImporterIndexFormat.Auto,
            (int)ToolbarState.Disable,
            Localization.lang.labelIndexFormat,
            "indexFormat",
            ""
        );

        internal readonly Option<bool> IsReadable = new(
            true,
            (int)ToolbarState.Enable,
            Localization.lang.labelIsReadable,
            "isReadable",
            ""
        );

        // --------------  Germetory -------------- 
        internal readonly Option<bool> KeepQuads = new(
            false,
            (int)ToolbarState.Disable,
            Localization.lang.labelKeepQuads,
            "keepQuads",
            ""
        );

        internal readonly Option<bool> LegacyBlendShapeNomals = new(
            false,
            (int)ToolbarState.Enable,
            Localization.lang.labelLegacyBlendShapeNomals,
            "legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes",
            ""
        );

        // -------------- Meshes -------------- 
        internal readonly Option<ModelImporterMeshCompression> MeshCompression = new(
            ModelImporterMeshCompression.Off,
            (int)ToolbarState.Disable,
            Localization.lang.labelMeshCompression,
            "meshCompression",
            ""
        );

        internal readonly Option<ModelImporterNormalCalculationMode> NormalsMode = new(
            ModelImporterNormalCalculationMode.Unweighted_Legacy,
            (int)ToolbarState.Disable,
            Localization.lang.labelNormalsMode,
            "normalCalculationMode",
            ""
        );

        internal readonly Option<MeshOptimizationFlags> OptimizeMesh = new(
            MeshOptimizationFlags.Everything,
            (int)ToolbarState.Disable,
            Localization.lang.labelOptimizeMesh,
            "meshOptimizationFlags",
            ""
        );

        internal readonly Option<bool> PreserveHierarchy = new(
            true,
            (int)ToolbarState.Disable,
            Localization.lang.labelPreserveHierarchy,
            "preserveHierarchy",
            ""
        );

        // -------------- Scenes -------------- 
        internal readonly Option<float> ScaleFactor = new(
            1.0f,
            (int)ToolbarState.Disable,
            Localization.lang.labelScaleFactor,
            "globalScale",
            ""
        );

        internal readonly Option<float> SmoothingAngle = new(
            60,
            (int)ToolbarState.Disable,
            Localization.lang.labelSmoothingAngle,
            "normalSmoothingAngle",
            ""
        );

        internal readonly Option<ModelImporterNormalSmoothingSource> SmoothnessSource = new(
            ModelImporterNormalSmoothingSource.PreferSmoothingGroups,
            (int)ToolbarState.Disable,
            Localization.lang.labelSmoothnessSource,
            "normalSmoothingSource",
            ""
        );

        internal readonly Option<bool> SortHierarchyByName = new(
            true,
            (int)ToolbarState.Disable,
            Localization.lang.labelSortHierarchyByName,
            "sortHierarchyByName",
            ""
        );

        internal readonly Option<bool> StrictVertexDataChecks = new(
            false,
            (int)ToolbarState.Disable,
            Localization.lang.labelStrictVertexDataChecks,
            "strictVertexDataChecks",
            ""
        );

        internal readonly Option<bool> SwapUvs = new(
            false,
            (int)ToolbarState.Disable,
            Localization.lang.labelSwapUvs,
            "swapUVChannels",
            ""
        );

        internal readonly Option<ModelImporterTangents> Tangents = new(
            ModelImporterTangents.CalculateMikk,
            (int)ToolbarState.Disable,
            Localization.lang.labelTangents,
            "importTangents",
            ""
        );

        internal readonly Option<bool> WeldVertices = new(
            true,
            (int)ToolbarState.Disable,
            Localization.lang.labelWeldVertices,
            "weldVertices",
            ""
        );
    }
}