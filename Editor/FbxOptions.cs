// TODO: TOOLBARが冗長、List<Option<T>>でCommon, Additional x3にまとめる。foreachで回す
// 上無理
// TODO: すべてDISABLEだったらメッセージを出す
// ALL DISABLE ENABLE BUTTON
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Cms;
using UnityEditor.Overlays;

namespace kesera2.FBXOptionsManager
{
    internal class FbxOptions
    {
        internal static OptionSettings optionSettings = new OptionSettings();
        internal Option<bool> _importCameras = optionSettings.importCameras;
        internal Option<bool> _importLights = optionSettings.importLights;
        internal Option<bool> _isReadable = optionSettings.isReadable;
        internal Option<ModelImporterNormals> _importNormals = optionSettings.importNormals;
        internal Option<ModelImporterNormals> _importBlendShapeNormals = optionSettings.importBlendShapeNormals;
        internal Option<bool> _legacyBlendShapeNomals = optionSettings.legacyBlendShapeNomals;
        internal Option<float> _scaleFactor = optionSettings.scaleFactor;
        internal Option<bool> _convertUnits = optionSettings.convertUnits;
        internal Option<bool> _bakeAxisConversion = optionSettings.bakeAxisConversion;
        internal Option<bool> _importBlendShapes = optionSettings.importBlendShapes;
        internal Option<bool> _importDeformPercent = optionSettings.importDeformPercent;
        internal Option<bool> _importVisibility = optionSettings.importVisibility;
        internal Option<bool> _preserveHierarchy = optionSettings.preserveHierarchy;
        internal Option<bool> _sortHierarchyByName = optionSettings.sortHierarchyByName;
        internal Option<ModelImporterMeshCompression> _meshCompression = optionSettings.meshCompression;
        internal Option<MeshOptimizationFlags> _optimizeMesh = optionSettings.optimizeMesh;
        internal Option<bool> _generateColliders = optionSettings.generateColliders;
        internal Option<bool> _keepQuads = optionSettings.keepQuads;
        internal Option<bool> _weldVertices = optionSettings.weldVertices;
        internal Option<ModelImporterIndexFormat> _indexFormat = optionSettings.indexFormat;
        internal Option<ModelImporterNormalCalculationMode> _normalsMode = optionSettings.normalsMode;
        internal Option<ModelImporterNormalSmoothingSource> _smoothnessSource = optionSettings.smoothnessSource;
        internal Option<float> _smoothingAngle = optionSettings.smoothingAngle;
        internal Option<ModelImporterTangents> _tangents = optionSettings.tangents;
        internal Option<bool> _swapUvs = optionSettings.swapUvs;
        internal Option<bool> _generateLightmapUvs = optionSettings.generateLightmapUvs;
        internal Option<bool> _strictVertexDataChecks = optionSettings.strictVertexDataChecks;
        internal List<Option<bool>> boolOptions;
        internal List<Option<float>> floatOptions;
        public FbxOptions()
        {
            boolOptions = new List<Option<bool>>
            {
                _importCameras,
                _importLights,
                _isReadable,
                _legacyBlendShapeNomals,
                _convertUnits,
                _bakeAxisConversion,
                _importBlendShapes,
                _importDeformPercent,
                _importVisibility,
                _preserveHierarchy,
                _sortHierarchyByName,
                _generateColliders,
                _keepQuads,
                _weldVertices,
                _swapUvs,
                _generateLightmapUvs,
                _strictVertexDataChecks,
            };
            floatOptions = new List<Option<float>>
            {
                _scaleFactor,
                _smoothingAngle,
            };
        }

        public void showOptions()
        {
            showOptionToolbar();
            using (new EditorGUI.IndentLevelScope())
            {
                showToggleToolbarButton();
                EditorGUILayout.LabelField(Localization.lang.labelSceneGroup, EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    showSceneOptions();
                }
                EditorGUILayout.LabelField(Localization.lang.labelMeshGroup, EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    showMeshOptions();
                }
                EditorGUILayout.LabelField(Localization.lang.labelGeometryGroup, EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    showGeometryOptions();
                }
            }
        }

        private void showOptionToolbar()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                // TODO: move to filed label
                string[] label = { Localization.lang.toolbarMenuGroupModel, Localization.lang.toolbarMenuGroupOther };
                GUILayout.Toolbar(0, label);
            }
        }

        private void showToggleToolbarButton()
        {
            EditorGUILayout.LabelField(Localization.lang.labelToggleToolbar, EditorStyles.boldLabel);
            using (new EditorGUILayout.VerticalScope())
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(Localization.lang.labelToggleToolbarToEnable, GUILayout.Width(360));
                        if (GUILayout.Button(Localization.lang.buttonAllEnable))
                        {
                            toggleToolbar(Toolbar.TOOLBAR.ENABLE);
                        }
                    }
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(Localization.lang.labelToggleToolbarToDefault, GUILayout.Width(360));
                        if (GUILayout.Button(Localization.lang.buttonUseDefault))
                        {
                            setToolbarDefault();
                        }
                    }
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(Localization.lang.labelToggleToolbarToDisable, GUILayout.Width(360));
                        if (GUILayout.Button(Localization.lang.buttonAllDisable))
                        {
                            toggleToolbar(Toolbar.TOOLBAR.DISABLE);
                        }
                    }
                }
            }
        }

        private void setToolbarDefault()
        {
            _importCameras.ToolbarEnable = (int)_importCameras.defaultSelected;
            _importLights.ToolbarEnable = (int)_importLights.defaultSelected;
            _isReadable.ToolbarEnable = (int)_isReadable.defaultSelected;
            _importNormals.ToolbarEnable = (int)_importNormals.defaultSelected;
            _importBlendShapeNormals.ToolbarEnable = (int)_importBlendShapeNormals.defaultSelected;
            _legacyBlendShapeNomals.ToolbarEnable = (int)_legacyBlendShapeNomals.defaultSelected;
            _scaleFactor.ToolbarEnable = (int)_scaleFactor.defaultSelected;
            _convertUnits.ToolbarEnable = (int)_convertUnits.defaultSelected;
            _bakeAxisConversion.ToolbarEnable = (int)_bakeAxisConversion.defaultSelected;
            _importBlendShapes.ToolbarEnable = (int)_importBlendShapes.defaultSelected;
            _importDeformPercent.ToolbarEnable = (int)_importDeformPercent.defaultSelected;
            _importVisibility.ToolbarEnable = (int)_importVisibility.defaultSelected;
            _preserveHierarchy.ToolbarEnable = (int)_preserveHierarchy.defaultSelected;
            _sortHierarchyByName.ToolbarEnable = (int)_sortHierarchyByName.defaultSelected;
            _meshCompression.ToolbarEnable = (int)_meshCompression.defaultSelected;
            _optimizeMesh.ToolbarEnable = (int)_optimizeMesh.defaultSelected;
            _generateColliders.ToolbarEnable = (int)_generateColliders.defaultSelected;
            _keepQuads.ToolbarEnable = (int)_keepQuads.defaultSelected;
            _weldVertices.ToolbarEnable = (int)_weldVertices.defaultSelected;
            _indexFormat.ToolbarEnable = (int)_indexFormat.defaultSelected;
            _normalsMode.ToolbarEnable = (int)_normalsMode.defaultSelected;
            _smoothnessSource.ToolbarEnable = (int)_smoothnessSource.defaultSelected;
            _smoothingAngle.ToolbarEnable = (int)_smoothingAngle.defaultSelected;
            _tangents.ToolbarEnable = (int)_tangents.defaultSelected;
            _swapUvs.ToolbarEnable = (int)_swapUvs.defaultSelected;
            _generateLightmapUvs.ToolbarEnable = (int)_generateLightmapUvs.defaultSelected;
            _strictVertexDataChecks.ToolbarEnable = (int)_strictVertexDataChecks.defaultSelected;
        }

        private void toggleToolbar(Toolbar.TOOLBAR toolbar)
        {
            foreach (Option<bool> option in boolOptions)
            {
                option.ToolbarEnable = (int)toolbar;
            }
            foreach (Option<float> option in floatOptions)
            {
                option.ToolbarEnable = (int)toolbar;
            }
            _importNormals.ToolbarEnable = (int)toolbar;
            _importBlendShapeNormals.ToolbarEnable = (int)toolbar;
            _meshCompression.ToolbarEnable = (int)toolbar;
            _optimizeMesh.ToolbarEnable = (int)toolbar;
            _indexFormat.ToolbarEnable = (int)toolbar;
            _normalsMode.ToolbarEnable = (int)toolbar;
            _smoothnessSource.ToolbarEnable = (int)toolbar;
            _tangents.ToolbarEnable = (int)toolbar;
        }

        private void showSceneOptions()
        {
            Option<float>.showOption(_scaleFactor);
            //Option<bool>.showOption(_convertUnits); // TODO: uncomment out
            using (new DisabledColorScope(Color.gray, true)) { EditorGUILayout.LabelField(new GUIContent(_convertUnits.Label, _convertUnits.Tooltip)); };
            Option<bool>.showOption(_bakeAxisConversion);
            Option<bool>.showOption(_importBlendShapes);
            Option<bool>.showOption(_importDeformPercent);
            Option<bool>.showOption(_importVisibility);
            Option<bool>.showOption(_importCameras);
            Option<bool>.showOption(_importLights);
            Option<bool>.showOption(_preserveHierarchy);
            Option<bool>.showOption(_sortHierarchyByName);
        }

        private void showMeshOptions()
        {
            Option<ModelImporterMeshCompression>.showOption(_meshCompression);
            Option<bool>.showOption(_isReadable);
            Option<MeshOptimizationFlags>.showOption(_optimizeMesh);
            Option<bool>.showOption(_generateColliders);
        }

        private void showGeometryOptions()
        {
            Option<bool>.showOption(_keepQuads);
            Option<bool>.showOption(_weldVertices);
            Option<ModelImporterIndexFormat>.showOption(_indexFormat);
            Option<bool>.showOption(_legacyBlendShapeNomals);
            Option<ModelImporterNormals>.showOption(_importNormals);
            Option<ModelImporterNormals>.showOption(_importBlendShapeNormals);
            Option<ModelImporterNormalCalculationMode>.showOption(_normalsMode);
            Option<bool>.showOption(_smoothingAngle);
            Option<ModelImporterTangents>.showOption(_tangents);
            Option<bool>.showOption(_swapUvs);
            Option<bool>.showOption(_generateLightmapUvs);
            Option<bool>.showOption(_strictVertexDataChecks);
        }
        internal void execute(ModelImporter modelImporter)
        {
            _scaleFactor.Update(modelImporter);
            _convertUnits.Update(modelImporter);
            _bakeAxisConversion.Update(modelImporter);
            _importBlendShapes.Update(modelImporter);
            _importDeformPercent.Update(modelImporter);
            _importVisibility.Update(modelImporter);
            _importCameras.Update(modelImporter);
            _importLights.Update(modelImporter);
            _preserveHierarchy.Update(modelImporter);
            _sortHierarchyByName.Update(modelImporter);
            _meshCompression.Update(modelImporter);
            _isReadable.Update(modelImporter);
            _optimizeMesh.Update(modelImporter);
            _generateColliders.Update(modelImporter);
            _keepQuads.Update(modelImporter);
            _weldVertices.Update(modelImporter);
            _indexFormat.Update(modelImporter);

            _importNormals.Update(modelImporter);

            _importBlendShapeNormals.Update(modelImporter);
            // FIXME: Nomals Mode, Smoothing Angle and Tangents has dependency of ImportBlendShapeNormals.Import or Caluclute!
            _normalsMode.Update(modelImporter);
            _smoothingAngle.Update(modelImporter);
            _tangents.Update(modelImporter);

            _smoothnessSource.Update(modelImporter);
            _swapUvs.Update(modelImporter);
            _generateLightmapUvs.Update(modelImporter);
            _strictVertexDataChecks.Update(modelImporter);
            ApplyLegacyBlendShapeNomals(modelImporter);
        }

        public PropertyInfo GetLegacyBlendShapeNomalsProp(ModelImporter modelImporter)
        {
            return modelImporter.GetType().GetProperty("legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        public void ApplyLegacyBlendShapeNomals(ModelImporter modelImporter)
        {
            PropertyInfo prop = GetLegacyBlendShapeNomalsProp(modelImporter);
            if (prop != null)
            {
                prop.SetValue(modelImporter, _legacyBlendShapeNomals.Value);
            }
        }

        public bool GetLegacyBlendShapeNomals(ModelImporter modelImporter)
        {
            PropertyInfo prop = GetLegacyBlendShapeNomalsProp(modelImporter);
            bool value = (bool)prop.GetValue(modelImporter);
            return value;
        }
    }
}

