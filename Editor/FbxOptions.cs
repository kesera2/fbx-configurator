using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    internal class FbxOptions
    {
        private readonly List<Option<bool>> _boolOptions;
        private readonly List<Option<float>> _floatOptions;
        internal readonly Option<bool> BakeAxisConversion;
        internal readonly Option<bool> ConvertUnits;
        internal readonly Option<bool> GenerateColliders;
        internal readonly Option<bool> GenerateLightmapUvs;
        internal readonly Option<ModelImporterNormals> ImportBlendShapeNormals;
        internal readonly Option<bool> ImportBlendShapes;
        internal readonly Option<bool> ImportCameras;
        internal readonly Option<bool> ImportDeformPercent;
        internal readonly Option<bool> ImportLights;
        internal readonly Option<ModelImporterNormals> ImportNormals;
        internal readonly Option<bool> ImportVisibility;
        internal readonly Option<ModelImporterIndexFormat> IndexFormat;
        internal readonly Option<bool> IsReadable;
        internal readonly Option<bool> KeepQuads;
        internal readonly Option<bool> LegacyBlendShapeNormals;
        internal readonly Option<ModelImporterMeshCompression> MeshCompression;
        internal readonly Option<ModelImporterNormalCalculationMode> NormalsMode;
        internal readonly Option<MeshOptimizationFlags> OptimizeMesh;
        internal readonly Option<bool> PreserveHierarchy;
        internal readonly Option<float> ScaleFactor;
        internal readonly Option<float> SmoothingAngle;
        internal readonly Option<ModelImporterNormalSmoothingSource> SmoothnessSource;
        internal readonly Option<bool> SortHierarchyByName;
        internal readonly Option<bool> StrictVertexDataChecks;
        internal readonly Option<bool> SwapUvs;
        internal readonly Option<ModelImporterTangents> Tangents;
        internal readonly Option<bool> WeldVertices;

        public FbxOptions()
        {
            var optionSettings = new OptionSettings();
            BakeAxisConversion = optionSettings.BakeAxisConversion;
            ConvertUnits = optionSettings.ConvertUnits;
            GenerateColliders = optionSettings.GenerateColliders;
            GenerateLightmapUvs = optionSettings.GenerateLightmapUvs;
            ImportBlendShapeNormals = optionSettings.ImportBlendShapeNormals;
            ImportBlendShapes = optionSettings.ImportBlendShapes;
            ImportCameras = optionSettings.ImportCameras;
            ImportDeformPercent = optionSettings.ImportDeformPercent;
            ImportLights = optionSettings.ImportLights;
            ImportNormals = optionSettings.ImportNormals;
            ImportVisibility = optionSettings.ImportVisibility;
            IndexFormat = optionSettings.IndexFormat;
            IsReadable = optionSettings.IsReadable;
            KeepQuads = optionSettings.KeepQuads;
            LegacyBlendShapeNormals = optionSettings.LegacyBlendShapeNomals;
            MeshCompression = optionSettings.MeshCompression;
            NormalsMode = optionSettings.NormalsMode;
            OptimizeMesh = optionSettings.OptimizeMesh;
            PreserveHierarchy = optionSettings.PreserveHierarchy;
            ScaleFactor = optionSettings.ScaleFactor;
            SmoothingAngle = optionSettings.SmoothingAngle;
            SmoothnessSource = optionSettings.SmoothnessSource;
            SortHierarchyByName = optionSettings.SortHierarchyByName;
            StrictVertexDataChecks = optionSettings.StrictVertexDataChecks;
            SwapUvs = optionSettings.SwapUvs;
            Tangents = optionSettings.Tangents;
            WeldVertices = optionSettings.WeldVertices;

            _boolOptions = new List<Option<bool>>
            {
                ImportCameras,
                ImportLights,
                IsReadable,
                LegacyBlendShapeNormals,
                ConvertUnits,
                BakeAxisConversion,
                ImportBlendShapes,
                ImportDeformPercent,
                ImportVisibility,
                PreserveHierarchy,
                SortHierarchyByName,
                GenerateColliders,
                KeepQuads,
                WeldVertices,
                SwapUvs,
                GenerateLightmapUvs,
                StrictVertexDataChecks
            };
            _floatOptions = new List<Option<float>>
            {
                ScaleFactor,
                SmoothingAngle
            };
        }

        public void ShowOptions()
        {
            ShowOptionToolbar();
            using (new EditorGUI.IndentLevelScope())
            {
                ShowToggleToolbarButton();
                EditorGUILayout.LabelField(Localization.Lang.labelSceneGroup, EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    ShowSceneOptions();
                }

                EditorGUILayout.LabelField(Localization.Lang.labelMeshGroup, EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    ShowMeshOptions();
                }

                EditorGUILayout.LabelField(Localization.Lang.labelGeometryGroup, EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    ShowGeometryOptions();
                }
            }
        }

        private void ShowOptionToolbar()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                // TODO: move to filed label
                string[] label = { Localization.Lang.toolbarMenuGroupModel, Localization.Lang.toolbarMenuGroupOther };
                GUILayout.Toolbar(0, label);
            }
        }

        private void ShowToggleToolbarButton()
        {
            EditorGUILayout.LabelField(Localization.Lang.labelToggleToolbar, EditorStyles.boldLabel);
            using (new EditorGUILayout.VerticalScope())
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(Localization.Lang.labelToggleToolbarToEnable, GUILayout.Width(360));
                        if (GUILayout.Button(Localization.Lang.buttonAllEnable))
                            ToggleToolbar(Toolbar.ToolbarState.Enable);
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(Localization.Lang.labelToggleToolbarToDefault, GUILayout.Width(360));
                        if (GUILayout.Button(Localization.Lang.buttonUseDefault)) SetToolbarDefault();
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(Localization.Lang.labelToggleToolbarToDisable, GUILayout.Width(360));
                        if (GUILayout.Button(Localization.Lang.buttonAllDisable))
                            ToggleToolbar(Toolbar.ToolbarState.Disable);
                    }
                }
            }
        }

        private void SetToolbarDefault()
        {
            ImportCameras.ToolbarEnable = ImportCameras.defaultSelected;
            ImportLights.ToolbarEnable = ImportLights.defaultSelected;
            IsReadable.ToolbarEnable = IsReadable.defaultSelected;
            ImportNormals.ToolbarEnable = ImportNormals.defaultSelected;
            ImportBlendShapeNormals.ToolbarEnable = ImportBlendShapeNormals.defaultSelected;
            LegacyBlendShapeNormals.ToolbarEnable = LegacyBlendShapeNormals.defaultSelected;
            ScaleFactor.ToolbarEnable = ScaleFactor.defaultSelected;
            ConvertUnits.ToolbarEnable = ConvertUnits.defaultSelected;
            BakeAxisConversion.ToolbarEnable = BakeAxisConversion.defaultSelected;
            ImportBlendShapes.ToolbarEnable = ImportBlendShapes.defaultSelected;
            ImportDeformPercent.ToolbarEnable = ImportDeformPercent.defaultSelected;
            ImportVisibility.ToolbarEnable = ImportVisibility.defaultSelected;
            PreserveHierarchy.ToolbarEnable = PreserveHierarchy.defaultSelected;
            SortHierarchyByName.ToolbarEnable = SortHierarchyByName.defaultSelected;
            MeshCompression.ToolbarEnable = MeshCompression.defaultSelected;
            OptimizeMesh.ToolbarEnable = OptimizeMesh.defaultSelected;
            GenerateColliders.ToolbarEnable = GenerateColliders.defaultSelected;
            KeepQuads.ToolbarEnable = KeepQuads.defaultSelected;
            WeldVertices.ToolbarEnable = WeldVertices.defaultSelected;
            IndexFormat.ToolbarEnable = IndexFormat.defaultSelected;
            NormalsMode.ToolbarEnable = NormalsMode.defaultSelected;
            SmoothnessSource.ToolbarEnable = SmoothnessSource.defaultSelected;
            SmoothingAngle.ToolbarEnable = SmoothingAngle.defaultSelected;
            Tangents.ToolbarEnable = Tangents.defaultSelected;
            SwapUvs.ToolbarEnable = SwapUvs.defaultSelected;
            GenerateLightmapUvs.ToolbarEnable = GenerateLightmapUvs.defaultSelected;
            StrictVertexDataChecks.ToolbarEnable = StrictVertexDataChecks.defaultSelected;
        }

        private void ToggleToolbar(Toolbar.ToolbarState toolbarState)
        {
            foreach (var option in _boolOptions) option.ToolbarEnable = (int)toolbarState;
            foreach (var option in _floatOptions) option.ToolbarEnable = (int)toolbarState;
            ImportNormals.ToolbarEnable = (int)toolbarState;
            ImportBlendShapeNormals.ToolbarEnable = (int)toolbarState;
            MeshCompression.ToolbarEnable = (int)toolbarState;
            OptimizeMesh.ToolbarEnable = (int)toolbarState;
            IndexFormat.ToolbarEnable = (int)toolbarState;
            NormalsMode.ToolbarEnable = (int)toolbarState;
            SmoothnessSource.ToolbarEnable = (int)toolbarState;
            Tangents.ToolbarEnable = (int)toolbarState;
        }

        private void ShowSceneOptions()
        {
            var scaleFactor = new Option<float>(ScaleFactor.Value, ScaleFactor.ToolbarEnable,
                ScaleFactor.Label, ScaleFactor._fieldName, ScaleFactor.Tooltip);
            scaleFactor.showOption();
            // FIXME: unsupported
            // var convertUnits = new Option<bool>(ConvertUnits.Value, ConvertUnits.ToolbarEnable,
            //     ConvertUnits.Label, ConvertUnits._fieldName, ConvertUnits.Tooltip);
            // convertUnits.showOption();
            var bakeAxisConversion = new Option<bool>(BakeAxisConversion.Value, BakeAxisConversion.ToolbarEnable,
                BakeAxisConversion.Label, BakeAxisConversion._fieldName, BakeAxisConversion.Tooltip);
            bakeAxisConversion.showOption();
            var importBlendShapes = new Option<bool>(ImportBlendShapes.Value, ImportBlendShapes.ToolbarEnable,
                ImportBlendShapes.Label, ImportBlendShapes._fieldName, ImportBlendShapes.Tooltip);
            importBlendShapes.showOption();
            var importDeformPercent = new Option<bool>(ImportDeformPercent.Value, ImportDeformPercent.ToolbarEnable,
                ImportDeformPercent.Label, ImportDeformPercent._fieldName, ImportDeformPercent.Tooltip);
            importDeformPercent.showOption();
            var importVisibility = new Option<bool>(ImportVisibility.Value, ImportVisibility.ToolbarEnable,
                ImportVisibility.Label, ImportVisibility._fieldName, ImportVisibility.Tooltip);
            importVisibility.showOption();
            var importCameras = new Option<bool>(ImportCameras.Value, ImportCameras.ToolbarEnable, ImportCameras.Label,
                ImportCameras._fieldName, ImportCameras.Tooltip);
            importCameras.showOption();
            var importLights = new Option<bool>(ImportLights.Value, ImportLights.ToolbarEnable, ImportLights.Label,
                ImportLights._fieldName, ImportLights.Tooltip);
            importLights.showOption();
            var preserveHierarchy = new Option<bool>(PreserveHierarchy.Value, PreserveHierarchy.ToolbarEnable,
                PreserveHierarchy.Label, PreserveHierarchy._fieldName, PreserveHierarchy.Tooltip);
            preserveHierarchy.showOption();
            var sortHierarchyByName = new Option<bool>(SortHierarchyByName.Value, SortHierarchyByName.ToolbarEnable,
                SortHierarchyByName.Label, SortHierarchyByName._fieldName, SortHierarchyByName.Tooltip);
            sortHierarchyByName.showOption();
        }

        private void ShowMeshOptions()
        {
            var meshCompression = new Option<ModelImporterMeshCompression>(MeshCompression.Value,
                MeshCompression.ToolbarEnable,
                MeshCompression.Label, MeshCompression._fieldName, MeshCompression.Tooltip);
            meshCompression.showOption();
            var isReadable = new Option<bool>(IsReadable.Value, IsReadable.ToolbarEnable,
                IsReadable.Label, IsReadable._fieldName, IsReadable.Tooltip);
            isReadable.showOption();
            var optimizeMesh = new Option<MeshOptimizationFlags>(OptimizeMesh.Value, OptimizeMesh.ToolbarEnable,
                OptimizeMesh.Label, OptimizeMesh._fieldName, OptimizeMesh.Tooltip);
            optimizeMesh.showOption();
            var generateColliders = new Option<bool>(GenerateColliders.Value, GenerateColliders.ToolbarEnable,
                GenerateColliders.Label, GenerateColliders._fieldName, GenerateColliders.Tooltip);
            generateColliders.showOption();
        }

        private void ShowGeometryOptions()
        {
            var keepQuads = new Option<bool>(KeepQuads.Value, KeepQuads.ToolbarEnable,
                KeepQuads.Label, KeepQuads._fieldName, KeepQuads.Tooltip);
            keepQuads.showOption();
            var weldVertices = new Option<bool>(WeldVertices.Value, WeldVertices.ToolbarEnable,
                WeldVertices.Label, WeldVertices._fieldName, WeldVertices.Tooltip);
            weldVertices.showOption();
            var indexFormat = new Option<ModelImporterIndexFormat>(IndexFormat.Value, IndexFormat.ToolbarEnable,
                IndexFormat.Label, IndexFormat._fieldName, IndexFormat.Tooltip);
            indexFormat.showOption();
            var legacyBlendShapeNormals = new Option<bool>(LegacyBlendShapeNormals.Value,
                LegacyBlendShapeNormals.ToolbarEnable,
                LegacyBlendShapeNormals.Label, LegacyBlendShapeNormals._fieldName, LegacyBlendShapeNormals.Tooltip);
            legacyBlendShapeNormals.showOption();
            var importNormals = new Option<ModelImporterNormals>(ImportNormals.Value, ImportNormals.ToolbarEnable,
                ImportNormals.Label, ImportNormals._fieldName, ImportNormals.Tooltip);
            importNormals.showOption();
            var importBlendShapeNormals = new Option<ModelImporterNormals>(ImportBlendShapeNormals.Value,
                ImportBlendShapeNormals.ToolbarEnable,
                ImportBlendShapeNormals.Label, ImportBlendShapeNormals._fieldName, ImportBlendShapeNormals.Tooltip);
            importBlendShapeNormals.showOption();
            var normalsMode = new Option<ModelImporterNormalCalculationMode>(NormalsMode.Value,
                NormalsMode.ToolbarEnable,
                NormalsMode.Label, NormalsMode._fieldName, NormalsMode.Tooltip);
            normalsMode.showOption();
            var smoothingAngle = new Option<float>(SmoothingAngle.Value, SmoothingAngle.ToolbarEnable,
                SmoothingAngle.Label, SmoothingAngle._fieldName, SmoothingAngle.Tooltip);
            smoothingAngle.showOption();
            var tangents = new Option<ModelImporterTangents>(Tangents.Value, Tangents.ToolbarEnable,
                Tangents.Label, Tangents._fieldName, Tangents.Tooltip);
            tangents.showOption();
            var swapUvs = new Option<bool>(SwapUvs.Value, SwapUvs.ToolbarEnable,
                SwapUvs.Label, SwapUvs._fieldName, SwapUvs.Tooltip);
            swapUvs.showOption();
            var generateLightmapUvs = new Option<bool>(GenerateLightmapUvs.Value, GenerateLightmapUvs.ToolbarEnable,
                GenerateLightmapUvs.Label, GenerateLightmapUvs._fieldName, GenerateLightmapUvs.Tooltip);
            generateLightmapUvs.showOption();
            var strictVertexDataChecks = new Option<bool>(StrictVertexDataChecks.Value,
                StrictVertexDataChecks.ToolbarEnable,
                StrictVertexDataChecks.Label, StrictVertexDataChecks._fieldName, StrictVertexDataChecks.Tooltip);
            strictVertexDataChecks.showOption();
        }

        internal void Execute(ModelImporter modelImporter)
        {
            ScaleFactor.Update(modelImporter);
            ConvertUnits.Update(modelImporter);
            BakeAxisConversion.Update(modelImporter);
            ImportBlendShapes.Update(modelImporter);
            ImportDeformPercent.Update(modelImporter);
            ImportVisibility.Update(modelImporter);
            ImportCameras.Update(modelImporter);
            ImportLights.Update(modelImporter);
            PreserveHierarchy.Update(modelImporter);
            SortHierarchyByName.Update(modelImporter);
            MeshCompression.Update(modelImporter);
            IsReadable.Update(modelImporter);
            OptimizeMesh.Update(modelImporter);
            GenerateColliders.Update(modelImporter);
            KeepQuads.Update(modelImporter);
            WeldVertices.Update(modelImporter);
            IndexFormat.Update(modelImporter);

            ImportNormals.Update(modelImporter);

            ImportBlendShapeNormals.Update(modelImporter);
            // FIXME: Normals Mode, Smoothing Angle and Tangents has dependency of ImportBlendShapeNormals.Import or Calculate!
            NormalsMode.Update(modelImporter);
            SmoothingAngle.Update(modelImporter);
            Tangents.Update(modelImporter);

            SmoothnessSource.Update(modelImporter);
            SwapUvs.Update(modelImporter);
            GenerateLightmapUvs.Update(modelImporter);
            StrictVertexDataChecks.Update(modelImporter);
            ApplyLegacyBlendShapeNormals(modelImporter);
        }

        private PropertyInfo GetLegacyBlendShapeNormalsProp(ModelImporter modelImporter)
        {
            return modelImporter.GetType()
                .GetProperty("legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        public void ApplyLegacyBlendShapeNormals(ModelImporter modelImporter)
        {
            var prop = GetLegacyBlendShapeNormalsProp(modelImporter);
            if (prop != null) prop.SetValue(modelImporter, LegacyBlendShapeNormals.Value);
        }

        public bool GetLegacyBlendShapeNormals(ModelImporter modelImporter)
        {
            var prop = GetLegacyBlendShapeNormalsProp(modelImporter);
            var value = (bool)prop.GetValue(modelImporter);
            return value;
        }
    }
}