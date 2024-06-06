// TODO: TOOLBARが冗長、List<Option<T>>でCommon, Additional x3にまとめる。foreachで回す
// 上無理
// TODO: すべてDISABLEだったらメッセージを出す
// ALL DISABLE ENABLE BUTTON
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{

    public class FbxOptions
    {
        // common
        private Option<bool> _importCameras = new Option<bool>(
            false,
            (int)TOOLBAR.ENABLE,
            "Import Cameras",
            "これを有効にすると.FBXファイルからカメラをインポートできます。"
            );
        private Option<bool> _importLights = new Option<bool>(
            false,
            (int)TOOLBAR.ENABLE,
            "Import Lights"
            );
        private Option<bool> _isReadable = new Option<bool>(
            true,
            (int)TOOLBAR.ENABLE,
            "Read/Write"
            );
        private Option<ModelImporterNormals> _importNormals = new Option<ModelImporterNormals>(
            ModelImporterNormals.Import,
            (int)TOOLBAR.ENABLE,
            "Nomals"
            );
        private Option<ModelImporterNormals> _importBlendShapeNormals = new Option<ModelImporterNormals>(
            ModelImporterNormals.None,
            (int)TOOLBAR.ENABLE,
            "Blend Shape Nomals"
            );
        private Option<bool> _legacyBlendShapeNomals = new Option<bool>(
            false,
            (int)TOOLBAR.ENABLE,
            "Legacy BlendShape Nomals"
            );
        // Scenes
        private Option<float> _scaleFactor = new Option<float>(1.0f, (int)TOOLBAR.DISABLE, "Scale Factor");
        private Option<bool> _convertUnits = new Option<bool>(true, (int)TOOLBAR.DISABLE, "Convert Units");
        private Option<bool> _bakeAxisConversion = new Option<bool>(false, (int)TOOLBAR.DISABLE, "Bake Axis Conversion");
        private Option<bool> _importBlendShapes = new Option<bool>(true, (int)TOOLBAR.DISABLE, "Import Blend Shapes");
        private Option<bool> _importDeformPercent = new Option<bool>(false, (int)TOOLBAR.DISABLE, "Import Deform Percent");
        private Option<bool> _importVisibility = new Option<bool>(true, (int)TOOLBAR.DISABLE, "Import Visibility");
        private Option<bool> _preserveHierarchy = new Option<bool>(true, (int)TOOLBAR.DISABLE, "Preserve Hierarchy");
        private Option<bool> _sortHierarchyByName = new Option<bool>(true, (int)TOOLBAR.DISABLE, "Sort Hierarchy ByName");
        // Meshes
        private Option<ModelImporterMeshCompression> _meshCompression = new Option<ModelImporterMeshCompression>(
            ModelImporterMeshCompression.Off, (int)TOOLBAR.DISABLE, "Mesh Compression");
        private Option<MeshOptimizationFlags> _optimizeMesh = new Option<MeshOptimizationFlags>(
            MeshOptimizationFlags.Everything, (int)TOOLBAR.DISABLE, "Optimize Mesh");
        private Option<bool> _generateColliders = new Option<bool>(false, (int)TOOLBAR.DISABLE, "Generate Colliders");
        // Germetory
        private Option<bool> _keepQuads = new Option<bool>(false, (int)TOOLBAR.DISABLE, "Keep Quads");
        private Option<bool> _weldVertices = new Option<bool>(true, (int)TOOLBAR.DISABLE, "Weld Vertices");
        private Option<ModelImporterIndexFormat> _indexFormat = new Option<ModelImporterIndexFormat>(
            ModelImporterIndexFormat.Auto, (int)TOOLBAR.DISABLE, "Index Format");
        private Option<ModelImporterNormalCalculationMode> _normalsMode = new Option<ModelImporterNormalCalculationMode>(
            ModelImporterNormalCalculationMode.Unweighted_Legacy, (int)TOOLBAR.DISABLE, "Normals Mode");

        private Option<ModelImporterNormalSmoothingSource> _smoothnessSource = new Option<ModelImporterNormalSmoothingSource>(
            ModelImporterNormalSmoothingSource.PreferSmoothingGroups, (int)TOOLBAR.DISABLE, "");
        [Range(0, 180)]
        private Option<float> _smoothingAngle = new Option<float>(60, (int)TOOLBAR.DISABLE, "Smoothing Angle");
        private Option<ModelImporterTangents> _tangents = new Option<ModelImporterTangents>(
            ModelImporterTangents.CalculateMikk, (int)TOOLBAR.DISABLE, "Tangents");
        private Option<bool> _swapUvs = new Option<bool>(false, (int)TOOLBAR.DISABLE, "Swap Uvs");
        private Option<bool> _generateLightmapUvs = new Option<bool>(false, (int)TOOLBAR.DISABLE, "Generate Lightmap UVs");
        private Option<bool> _strictVertexDataChecks = new Option<bool>(false, (int)TOOLBAR.DISABLE, "Strict Vertext Data Checks");

        private const int OPTION_WIDTH = 350;

        // Enable/Disableを切り替える共通部品
        private int drawToggleEnableToolbar(int currentSelection)
        {
            return GUILayout.Toolbar(currentSelection, TOOLBAR_LABLE);
        }

        public void showOptions()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                string[] label = { "Model", "Other options are comming Soon" };
                GUILayout.Toolbar(0, label);
            }
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.LabelField("Scene", EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    showSceneOptions();
                }
                EditorGUILayout.LabelField("Meshes", EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    showMeshOptions();
                }
                EditorGUILayout.LabelField("Geometory", EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    showGeometoryOptions();
                }
            }
        }

        private void showSceneOptions()
        {
            GUILayoutOption[] verticalOptions = { GUILayout.Width(OPTION_WIDTH) };
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope(verticalOptions))
                {
                    using (new EditorGUI.DisabledScope(_scaleFactor.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _scaleFactor.Value = EditorGUILayout.FloatField(_scaleFactor.Label, _scaleFactor.Value);
                    }
                    using (new EditorGUI.DisabledScope(_convertUnits.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _convertUnits.Value = EditorGUILayout.Toggle(_convertUnits.Label, _convertUnits.Value);
                    }
                    using (new EditorGUI.DisabledScope(_bakeAxisConversion.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _bakeAxisConversion.Value = EditorGUILayout.Toggle(_bakeAxisConversion.Label, _bakeAxisConversion.Value);
                    }
                    using (new EditorGUI.DisabledScope(_importBlendShapes.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _importBlendShapes.Value = EditorGUILayout.Toggle(_importBlendShapes.Label, _importBlendShapes.Value);
                    }
                    using (new EditorGUI.DisabledScope(_importDeformPercent.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _importDeformPercent.Value = EditorGUILayout.Toggle(_importDeformPercent.Label, _importDeformPercent.Value);
                    }
                    using (new EditorGUI.DisabledScope(_importVisibility.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _importVisibility.Value = EditorGUILayout.Toggle(_importVisibility.Label, _importVisibility.Value);
                    }
                    using (new EditorGUI.DisabledScope(_importCameras.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _importCameras.Value = EditorGUILayout.Toggle(new GUIContent(_importCameras.Label, _importCameras.Tooltip), _importCameras.Value); ;
                    }
                    using (new EditorGUI.DisabledScope(_importLights.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _importLights.Value = EditorGUILayout.Toggle(_importLights.Label, _importLights.Value);
                    }
                    using (new EditorGUI.DisabledScope(_preserveHierarchy.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _preserveHierarchy.Value = EditorGUILayout.Toggle(_preserveHierarchy.Label, _preserveHierarchy.Value);
                    }
                    using (new EditorGUI.DisabledScope(_sortHierarchyByName.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _sortHierarchyByName.Value = EditorGUILayout.Toggle(_sortHierarchyByName.Label, _sortHierarchyByName.Value);
                    }
                }
                GUILayout.Space(10);
                using (new EditorGUILayout.VerticalScope())
                {
                    _scaleFactor.ToolbarEnable = drawToggleEnableToolbar(_scaleFactor.ToolbarEnable);
                    _convertUnits.ToolbarEnable = drawToggleEnableToolbar(_convertUnits.ToolbarEnable);
                    _bakeAxisConversion.ToolbarEnable = drawToggleEnableToolbar(_bakeAxisConversion.ToolbarEnable);
                    _importBlendShapes.ToolbarEnable = drawToggleEnableToolbar(_importBlendShapes.ToolbarEnable);
                    _importDeformPercent.ToolbarEnable = drawToggleEnableToolbar(_importDeformPercent.ToolbarEnable);
                    _importVisibility.ToolbarEnable = drawToggleEnableToolbar(_importVisibility.ToolbarEnable);
                    _importCameras.ToolbarEnable = drawToggleEnableToolbar(_importCameras.ToolbarEnable);
                    _importLights.ToolbarEnable = drawToggleEnableToolbar(_importLights.ToolbarEnable);
                    _preserveHierarchy.ToolbarEnable = drawToggleEnableToolbar(_preserveHierarchy.ToolbarEnable);
                    _sortHierarchyByName.ToolbarEnable = drawToggleEnableToolbar(_sortHierarchyByName.ToolbarEnable);
                }
            }
        }

        private void showMeshOptions()
        {
            GUILayoutOption[] verticalOptions = { GUILayout.Width(OPTION_WIDTH) };
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope(verticalOptions))
                {
                    using (new EditorGUI.DisabledScope(_meshCompression.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _meshCompression.Value = (ModelImporterMeshCompression)EditorGUILayout.EnumPopup(_meshCompression.Label, _meshCompression.Value);
                    }
                    using (new EditorGUI.DisabledScope(_isReadable.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _isReadable.Value = EditorGUILayout.Toggle(_isReadable.Label, _isReadable.Value);
                    }
                    using (new EditorGUI.DisabledScope(_optimizeMesh.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _optimizeMesh.Value = (MeshOptimizationFlags)EditorGUILayout.EnumPopup(_optimizeMesh.Label, _optimizeMesh.Value);
                    }
                    using (new EditorGUI.DisabledScope(_generateColliders.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _generateColliders.Value = EditorGUILayout.Toggle(_generateColliders.Label, _generateColliders.Value);
                    }
                }
                GUILayout.Space(10);
                using (new EditorGUILayout.VerticalScope())
                {
                    _meshCompression.ToolbarEnable = drawToggleEnableToolbar(_meshCompression.ToolbarEnable);
                    _isReadable.ToolbarEnable = drawToggleEnableToolbar(_isReadable.ToolbarEnable);
                    _optimizeMesh.ToolbarEnable = drawToggleEnableToolbar(_optimizeMesh.ToolbarEnable);
                    _generateColliders.ToolbarEnable = drawToggleEnableToolbar(_generateColliders.ToolbarEnable);

                }
            }
        }

        private void showGeometoryOptions()
        {
            GUILayoutOption[] verticalOptions = { GUILayout.Width(350) };
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope(verticalOptions))
                {
                    using (new EditorGUI.DisabledScope(_keepQuads.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _keepQuads.Value = EditorGUILayout.Toggle(_keepQuads.Label, _keepQuads.Value);
                    }
                    using (new EditorGUI.DisabledScope(_weldVertices.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _weldVertices.Value = EditorGUILayout.Toggle(_weldVertices.Label, _weldVertices.Value);
                    }
                    using (new EditorGUI.DisabledScope(_indexFormat.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _indexFormat.Value = (ModelImporterIndexFormat)EditorGUILayout.EnumPopup(_indexFormat.Label, _indexFormat.Value);
                    }
                    using (new EditorGUI.DisabledScope(_legacyBlendShapeNomals.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _legacyBlendShapeNomals.Value = EditorGUILayout.Toggle(_legacyBlendShapeNomals.Label, _legacyBlendShapeNomals.Value);
                    }
                    using (new EditorGUI.DisabledScope(_importNormals.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _importNormals.Value = (ModelImporterNormals)EditorGUILayout.EnumPopup(_importNormals.Label, _importNormals.Value);
                    }
                    using (new EditorGUI.DisabledScope(_importBlendShapeNormals.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _importBlendShapeNormals.Value = (ModelImporterNormals)EditorGUILayout.EnumPopup(_importBlendShapeNormals.Label, _importBlendShapeNormals.Value);
                    }
                    using (new EditorGUI.DisabledScope(_normalsMode.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _normalsMode.Value = (ModelImporterNormalCalculationMode)EditorGUILayout.EnumPopup(_normalsMode.Label, _normalsMode.Value);
                    }
                    using (new EditorGUI.DisabledScope(_smoothingAngle.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _smoothingAngle.Value = EditorGUILayout.Slider(_smoothingAngle.Label, _smoothingAngle.Value, 0, 180);
                    }
                    using (new EditorGUI.DisabledScope(_tangents.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _tangents.Value = (ModelImporterTangents)EditorGUILayout.EnumPopup(_tangents.Label, _tangents.Value);
                    }
                    using (new EditorGUI.DisabledScope(_swapUvs.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _swapUvs.Value = EditorGUILayout.Toggle(_swapUvs.Label, _swapUvs.Value);
                    }
                    using (new EditorGUI.DisabledScope(_generateLightmapUvs.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _generateLightmapUvs.Value = EditorGUILayout.Toggle(_generateLightmapUvs.Label, _generateLightmapUvs.Value);
                    }
                    using (new EditorGUI.DisabledScope(_strictVertexDataChecks.ToolbarEnable == (int)TOOLBAR.DISABLE))
                    {
                        _strictVertexDataChecks.Value = EditorGUILayout.Toggle(_strictVertexDataChecks.Label, _strictVertexDataChecks.Value);
                    }
                }
                GUILayout.Space(10);
                using (new EditorGUILayout.VerticalScope())
                {
                    _keepQuads.ToolbarEnable = drawToggleEnableToolbar(_keepQuads.ToolbarEnable);
                    _weldVertices.ToolbarEnable = drawToggleEnableToolbar(_weldVertices.ToolbarEnable);
                    _indexFormat.ToolbarEnable = drawToggleEnableToolbar(_indexFormat.ToolbarEnable);
                    _legacyBlendShapeNomals.ToolbarEnable = drawToggleEnableToolbar(_legacyBlendShapeNomals.ToolbarEnable);
                    _importNormals.ToolbarEnable = drawToggleEnableToolbar(_importNormals.ToolbarEnable);
                    _importBlendShapeNormals.ToolbarEnable = drawToggleEnableToolbar(_importBlendShapeNormals.ToolbarEnable);
                    _normalsMode.ToolbarEnable = drawToggleEnableToolbar(_normalsMode.ToolbarEnable);
                    _smoothingAngle.ToolbarEnable = drawToggleEnableToolbar(_smoothingAngle.ToolbarEnable);
                    _tangents.ToolbarEnable = drawToggleEnableToolbar(_tangents.ToolbarEnable);
                    _swapUvs.ToolbarEnable = drawToggleEnableToolbar(_swapUvs.ToolbarEnable);
                    _generateLightmapUvs.ToolbarEnable = drawToggleEnableToolbar(_generateLightmapUvs.ToolbarEnable);
                    _strictVertexDataChecks.ToolbarEnable = drawToggleEnableToolbar(_strictVertexDataChecks.ToolbarEnable);
                }
            }
        }

        public PropertyInfo GetLegacyBlendShapeNomalsProp(ModelImporter modelImporter)
        {
            return modelImporter.GetType().GetProperty("legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        public void ChangeLegacyBlendShapeNomals(ModelImporter modelImporter)
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

        internal void execute(ModelImporter modelImporter)
        {
            if (_importCameras.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importCameras = _importCameras.Value;
            }

            if (_importLights.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importLights = _importLights.Value;
            }

            if (_isReadable.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.isReadable = _isReadable.Value;
            }

            if (_importNormals.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importNormals = _importNormals.Value;
            }

            if (_importBlendShapeNormals.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importBlendShapeNormals = _importBlendShapeNormals.Value;
            }

            if (_legacyBlendShapeNomals.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                ChangeLegacyBlendShapeNomals(modelImporter);
            }

            if (_scaleFactor.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.globalScale = _scaleFactor.Value;
            }

            if (_convertUnits.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.useFileUnits = _convertUnits.Value;
            }

            if (_bakeAxisConversion.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.bakeAxisConversion = _bakeAxisConversion.Value;
            }

            if (_importBlendShapes.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importBlendShapes = _importBlendShapes.Value;
            }

            if (_importDeformPercent.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importBlendShapeDeformPercent = _importDeformPercent.Value;
            }

            if (_importVisibility.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importVisibility = _importVisibility.Value;
            }

            if (_preserveHierarchy.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.preserveHierarchy = _preserveHierarchy.Value;
            }

            if (_sortHierarchyByName.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.sortHierarchyByName = _sortHierarchyByName.Value;
            }

            if (_meshCompression.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.meshCompression = _meshCompression.Value;
            }

            if (_optimizeMesh.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.meshOptimizationFlags = _optimizeMesh.Value;
            }

            if (_generateColliders.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.addCollider = _generateColliders.Value;
            }

            if (_keepQuads.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.keepQuads = _keepQuads.Value;
            }

            if (_weldVertices.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.weldVertices = _weldVertices.Value;
            }

            if (_indexFormat.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.indexFormat = _indexFormat.Value;
            }

            if (_normalsMode.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.normalCalculationMode = _normalsMode.Value;
            }

            if (_smoothnessSource.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.normalSmoothingSource = _smoothnessSource.Value;
            }

            if (_smoothingAngle.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.normalSmoothingAngle = _smoothingAngle.Value;
            }

            if (_tangents.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.importTangents = _tangents.Value;
            }

            if (_swapUvs.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.swapUVChannels = _swapUvs.Value;
            }

            if (_generateLightmapUvs.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.generateSecondaryUV = _generateLightmapUvs.Value;
            }

            if (_strictVertexDataChecks.ToolbarEnable == (int)TOOLBAR.ENABLE)
            {
                modelImporter.strictVertexDataChecks = _strictVertexDataChecks.Value;
            }

        }
    }
}

