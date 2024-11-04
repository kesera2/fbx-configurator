// TODO: TOOLBARが冗長、List<Option<T>>でCommon, Additional x3にまとめる。foreachで回す
// 上無理
// TODO: すべてDISABLEだったらメッセージを出す
// ALL DISABLE ENABLE BUTTON

using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    internal class FbxOptions
    {
        private static readonly OptionSettings OptionSettings = new();
        private readonly List<Option<bool>> _boolOptions;
        private readonly List<Option<float>> _floatOptions;
        internal readonly Option<bool> BakeAxisConversion = OptionSettings.BakeAxisConversion;
        internal readonly Option<bool> ConvertUnits = OptionSettings.ConvertUnits;
        internal readonly Option<bool> GenerateColliders = OptionSettings.GenerateColliders;
        internal readonly Option<bool> GenerateLightmapUvs = OptionSettings.GenerateLightmapUvs;
        internal readonly Option<ModelImporterNormals> ImportBlendShapeNormals = OptionSettings.ImportBlendShapeNormals;
        internal readonly Option<bool> ImportBlendShapes = OptionSettings.ImportBlendShapes;
        internal readonly Option<bool> ImportCameras = OptionSettings.ImportCameras;
        internal readonly Option<bool> ImportDeformPercent = OptionSettings.ImportDeformPercent;
        internal readonly Option<bool> ImportLights = OptionSettings.ImportLights;
        internal readonly Option<ModelImporterNormals> ImportNormals = OptionSettings.ImportNormals;
        internal readonly Option<bool> ImportVisibility = OptionSettings.ImportVisibility;
        internal readonly Option<ModelImporterIndexFormat> IndexFormat = OptionSettings.IndexFormat;
        internal readonly Option<bool> IsReadable = OptionSettings.IsReadable;
        internal readonly Option<bool> KeepQuads = OptionSettings.KeepQuads;
        internal readonly Option<bool> LegacyBlendShapeNormals = OptionSettings.LegacyBlendShapeNomals;
        internal readonly Option<ModelImporterMeshCompression> MeshCompression = OptionSettings.MeshCompression;
        internal readonly Option<ModelImporterNormalCalculationMode> NormalsMode = OptionSettings.NormalsMode;
        internal readonly Option<MeshOptimizationFlags> OptimizeMesh = OptionSettings.OptimizeMesh;
        internal readonly Option<bool> PreserveHierarchy = OptionSettings.PreserveHierarchy;
        internal readonly Option<float> ScaleFactor = OptionSettings.ScaleFactor;
        internal readonly Option<float> SmoothingAngle = OptionSettings.SmoothingAngle;
        internal readonly Option<ModelImporterNormalSmoothingSource> SmoothnessSource = OptionSettings.SmoothnessSource;
        internal readonly Option<bool> SortHierarchyByName = OptionSettings.SortHierarchyByName;
        internal readonly Option<bool> StrictVertexDataChecks = OptionSettings.StrictVertexDataChecks;
        internal readonly Option<bool> SwapUvs = OptionSettings.SwapUvs;
        internal readonly Option<ModelImporterTangents> Tangents = OptionSettings.Tangents;
        internal readonly Option<bool> WeldVertices = OptionSettings.WeldVertices;

        public FbxOptions()
        {
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
            Option<float>.showOption(ScaleFactor);
            //Option<bool>.showOption(_convertUnits); // TODO: uncomment out
            using (new DisabledColorScope(Color.gray, true))
            {
                EditorGUILayout.LabelField(new GUIContent(ConvertUnits.Label, ConvertUnits.Tooltip));
            }

            Option<bool>.showOption(BakeAxisConversion);
            Option<bool>.showOption(ImportBlendShapes);
            Option<bool>.showOption(ImportDeformPercent);
            Option<bool>.showOption(ImportVisibility);
            Option<bool>.showOption(ImportCameras);
            Option<bool>.showOption(ImportLights);
            Option<bool>.showOption(PreserveHierarchy);
            Option<bool>.showOption(SortHierarchyByName);
        }

        private void ShowMeshOptions()
        {
            Option<ModelImporterMeshCompression>.showOption(MeshCompression);
            Option<bool>.showOption(IsReadable);
            Option<MeshOptimizationFlags>.showOption(OptimizeMesh);
            Option<bool>.showOption(GenerateColliders);
        }

        private void ShowGeometryOptions()
        {
            Option<bool>.showOption(KeepQuads);
            Option<bool>.showOption(WeldVertices);
            Option<ModelImporterIndexFormat>.showOption(IndexFormat);
            Option<bool>.showOption(LegacyBlendShapeNormals);
            Option<ModelImporterNormals>.showOption(ImportNormals);
            Option<ModelImporterNormals>.showOption(ImportBlendShapeNormals);
            Option<ModelImporterNormalCalculationMode>.showOption(NormalsMode);
            Option<bool>.showOption(SmoothingAngle);
            Option<ModelImporterTangents>.showOption(Tangents);
            Option<bool>.showOption(SwapUvs);
            Option<bool>.showOption(GenerateLightmapUvs);
            Option<bool>.showOption(StrictVertexDataChecks);
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