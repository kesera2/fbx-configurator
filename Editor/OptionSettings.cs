using UnityEditor;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{
    internal class OptionSettings
    {
        internal readonly Option<bool> BakeAxisConversion = new(
            false,
            (int)ToolbarState.Disable,
            Localization.Lang.labelBakeAxisConversion,
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
            Localization.Lang.labelGenerateColliders,
            "addCollider",
            "");

        internal readonly Option<bool> GenerateLightmapUvs = new(
            false,
            (int)ToolbarState.Disable,
            Localization.Lang.labelGenerateLightmapUvs,
            "generateSecondaryUV",
            ""
        );

        internal readonly Option<ModelImporterNormals> ImportBlendShapeNormals = new(
            ModelImporterNormals.None,
            (int)ToolbarState.Enable,
            Localization.Lang.labelImportBlendShapeNormals,
            "importBlendShapeNormals",
            ""
        );

        internal readonly Option<bool> ImportBlendShapes = new(
            true,
            (int)ToolbarState.Disable,
            Localization.Lang.labelImportBlendShapeNormals,
            "importBlendShapes",
            ""
        );

        internal readonly Option<bool> ImportCameras = new(
            false,
            (int)ToolbarState.Enable,
            Localization.Lang.labelImportCameras,
            "importCameras",
            "これを有効にすると.FBXファイルからカメラをインポートできます。"
        );

        internal readonly Option<bool> ImportDeformPercent = new(
            false,
            (int)ToolbarState.Disable,
            Localization.Lang.labelImportDeformPercent,
            "importBlendShapeDeformPercent",
            ""
        );

        internal readonly Option<bool> ImportLights = new(
            false,
            (int)ToolbarState.Enable,
            Localization.Lang.labelImportLights,
            "importLights",
            ""
        );

        internal readonly Option<ModelImporterNormals> ImportNormals = new(
            ModelImporterNormals.Import,
            (int)ToolbarState.Enable,
            Localization.Lang.labelImportNormals,
            "importNormals",
            ""
        );

        internal readonly Option<bool> ImportVisibility = new(
            true,
            (int)ToolbarState.Disable,
            Localization.Lang.labelImportVisibility,
            "importVisibility",
            ""
        );

        internal readonly Option<ModelImporterIndexFormat> IndexFormat = new(
            ModelImporterIndexFormat.Auto,
            (int)ToolbarState.Disable,
            Localization.Lang.labelIndexFormat,
            "indexFormat",
            ""
        );

        internal readonly Option<bool> IsReadable = new(
            true,
            (int)ToolbarState.Enable,
            Localization.Lang.labelIsReadable,
            "isReadable",
            ""
        );

        // --------------  Germetory -------------- 
        internal readonly Option<bool> KeepQuads = new(
            false,
            (int)ToolbarState.Disable,
            Localization.Lang.labelKeepQuads,
            "keepQuads",
            ""
        );

        internal readonly Option<bool> LegacyBlendShapeNomals = new(
            false,
            (int)ToolbarState.Enable,
            Localization.Lang.labelLegacyBlendShapeNomals,
            "legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes",
            ""
        );

        // -------------- Meshes -------------- 
        internal readonly Option<ModelImporterMeshCompression> MeshCompression = new(
            ModelImporterMeshCompression.Off,
            (int)ToolbarState.Disable,
            Localization.Lang.labelMeshCompression,
            "meshCompression",
            ""
        );

        internal readonly Option<ModelImporterNormalCalculationMode> NormalsMode = new(
            ModelImporterNormalCalculationMode.Unweighted_Legacy,
            (int)ToolbarState.Disable,
            Localization.Lang.labelNormalsMode,
            "normalCalculationMode",
            ""
        );

        internal readonly Option<MeshOptimizationFlags> OptimizeMesh = new(
            MeshOptimizationFlags.Everything,
            (int)ToolbarState.Disable,
            Localization.Lang.labelOptimizeMesh,
            "meshOptimizationFlags",
            ""
        );

        internal readonly Option<bool> PreserveHierarchy = new(
            true,
            (int)ToolbarState.Disable,
            Localization.Lang.labelPreserveHierarchy,
            "preserveHierarchy",
            ""
        );

        // -------------- Scenes -------------- 
        internal readonly Option<float> ScaleFactor = new(
            1.0f,
            (int)ToolbarState.Disable,
            Localization.Lang.labelScaleFactor,
            "globalScale",
            ""
        );

        internal readonly Option<float> SmoothingAngle = new(
            60,
            (int)ToolbarState.Disable,
            Localization.Lang.labelSmoothingAngle,
            "normalSmoothingAngle",
            ""
        );

        internal readonly Option<ModelImporterNormalSmoothingSource> SmoothnessSource = new(
            ModelImporterNormalSmoothingSource.PreferSmoothingGroups,
            (int)ToolbarState.Disable,
            Localization.Lang.labelSmoothnessSource,
            "normalSmoothingSource",
            ""
        );

        internal readonly Option<bool> SortHierarchyByName = new(
            true,
            (int)ToolbarState.Disable,
            Localization.Lang.labelSortHierarchyByName,
            "sortHierarchyByName",
            ""
        );

        internal readonly Option<bool> StrictVertexDataChecks = new(
            false,
            (int)ToolbarState.Disable,
            Localization.Lang.labelStrictVertexDataChecks,
            "strictVertexDataChecks",
            ""
        );

        internal readonly Option<bool> SwapUvs = new(
            false,
            (int)ToolbarState.Disable,
            Localization.Lang.labelSwapUvs,
            "swapUVChannels",
            ""
        );

        internal readonly Option<ModelImporterTangents> Tangents = new(
            ModelImporterTangents.CalculateMikk,
            (int)ToolbarState.Disable,
            Localization.Lang.labelTangents,
            "importTangents",
            ""
        );

        internal readonly Option<bool> WeldVertices = new(
            true,
            (int)ToolbarState.Disable,
            Localization.Lang.labelWeldVertices,
            "weldVertices",
            ""
        );
    }
}