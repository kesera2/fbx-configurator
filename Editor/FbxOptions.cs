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
        private const float INTERVAL_WIDTH = 10;
        private GUILayoutOption[] optionsWidth = { GUILayout.Width(OPTION_WIDTH) };
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
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _scaleFactor.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_scaleFactor.Label, optionsWidth);
                    }
                    else
                    {
                        _scaleFactor.Value = EditorGUILayout.FloatField(_scaleFactor.Label, _scaleFactor.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _scaleFactor.ToolbarEnable = drawToggleEnableToolbar(_scaleFactor.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {

                bool isDisabled = _convertUnits.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_convertUnits.Label, optionsWidth);
                    }
                    else
                    {
                        _convertUnits.Value = EditorGUILayout.Toggle(_convertUnits.Label, _convertUnits.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _convertUnits.ToolbarEnable = drawToggleEnableToolbar(_convertUnits.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _bakeAxisConversion.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_bakeAxisConversion.Label, optionsWidth);
                    }
                    else
                    {
                        _bakeAxisConversion.Value = EditorGUILayout.Toggle(_bakeAxisConversion.Label, _bakeAxisConversion.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _bakeAxisConversion.ToolbarEnable = drawToggleEnableToolbar(_bakeAxisConversion.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _importBlendShapes.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_importBlendShapes.Label, optionsWidth);
                    }
                    else
                    {
                        _importBlendShapes.Value = EditorGUILayout.Toggle(_importBlendShapes.Label, _importBlendShapes.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _importBlendShapes.ToolbarEnable = drawToggleEnableToolbar(_importBlendShapes.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _importDeformPercent.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_importDeformPercent.Label, optionsWidth);
                    }
                    else
                    {
                        _importDeformPercent.Value = EditorGUILayout.Toggle(_importDeformPercent.Label, _importDeformPercent.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _importDeformPercent.ToolbarEnable = drawToggleEnableToolbar(_importDeformPercent.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _importVisibility.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_importVisibility.Label, optionsWidth);
                    }
                    else
                    {
                        _importVisibility.Value = EditorGUILayout.Toggle(_importVisibility.Label, _importVisibility.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _importVisibility.ToolbarEnable = drawToggleEnableToolbar(_importVisibility.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _importCameras.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_importCameras.Label, optionsWidth);
                    }
                    else
                    {
                        _importCameras.Value = EditorGUILayout.Toggle(new GUIContent(_importCameras.Label, _importCameras.Tooltip), _importCameras.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _importCameras.ToolbarEnable = drawToggleEnableToolbar(_importCameras.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _importLights.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_importLights.Label, optionsWidth);
                    }
                    else
                    {
                        _importLights.Value = EditorGUILayout.Toggle(_importLights.Label, _importLights.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _importLights.ToolbarEnable = drawToggleEnableToolbar(_importLights.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _preserveHierarchy.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_preserveHierarchy.Label, optionsWidth);
                    }
                    else
                    {
                        _preserveHierarchy.Value = EditorGUILayout.Toggle(_preserveHierarchy.Label, _preserveHierarchy.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _preserveHierarchy.ToolbarEnable = drawToggleEnableToolbar(_preserveHierarchy.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _sortHierarchyByName.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_sortHierarchyByName.Label, optionsWidth);
                    }
                    else
                    {
                        _sortHierarchyByName.Value = EditorGUILayout.Toggle(_sortHierarchyByName.Label, _sortHierarchyByName.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _sortHierarchyByName.ToolbarEnable = drawToggleEnableToolbar(_sortHierarchyByName.ToolbarEnable);
            }
        }

        private void showMeshOptions()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _meshCompression.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_meshCompression.Label, optionsWidth);
                    }
                    else
                    {
                        _meshCompression.Value = (ModelImporterMeshCompression)EditorGUILayout.EnumPopup(_meshCompression.Label, _meshCompression.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _meshCompression.ToolbarEnable = drawToggleEnableToolbar(_meshCompression.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _isReadable.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_isReadable.Label, optionsWidth);
                    }
                    else
                    {
                        _isReadable.Value = EditorGUILayout.Toggle(_isReadable.Label, _isReadable.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _isReadable.ToolbarEnable = drawToggleEnableToolbar(_isReadable.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _optimizeMesh.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_optimizeMesh.Label, optionsWidth);
                    }
                    else
                    {
                        _optimizeMesh.Value = (MeshOptimizationFlags)EditorGUILayout.EnumPopup(_optimizeMesh.Label, _optimizeMesh.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _optimizeMesh.ToolbarEnable = drawToggleEnableToolbar(_optimizeMesh.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _generateColliders.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_generateColliders.Label, optionsWidth);
                    }
                    else
                    {
                        _generateColliders.Value = EditorGUILayout.Toggle(_generateColliders.Label, _generateColliders.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _generateColliders.ToolbarEnable = drawToggleEnableToolbar(_generateColliders.ToolbarEnable);
            }
        }

        private void showGeometoryOptions()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _keepQuads.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_keepQuads.Label, optionsWidth);
                    }
                    else
                    {
                        _keepQuads.Value = EditorGUILayout.Toggle(_keepQuads.Label, _keepQuads.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _keepQuads.ToolbarEnable = drawToggleEnableToolbar(_keepQuads.ToolbarEnable);

            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _weldVertices.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_weldVertices.Label, optionsWidth);
                    }
                    else
                    {
                        _weldVertices.Value = EditorGUILayout.Toggle(_weldVertices.Label, _weldVertices.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _weldVertices.ToolbarEnable = drawToggleEnableToolbar(_weldVertices.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _indexFormat.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_indexFormat.Label, optionsWidth);
                    }
                    else
                    {
                        _indexFormat.Value = (ModelImporterIndexFormat)EditorGUILayout.EnumPopup(_indexFormat.Label, _indexFormat.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _indexFormat.ToolbarEnable = drawToggleEnableToolbar(_indexFormat.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _legacyBlendShapeNomals.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_legacyBlendShapeNomals.Label, optionsWidth);
                    }
                    else
                    {
                        _legacyBlendShapeNomals.Value = EditorGUILayout.Toggle(_legacyBlendShapeNomals.Label, _legacyBlendShapeNomals.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _legacyBlendShapeNomals.ToolbarEnable = drawToggleEnableToolbar(_legacyBlendShapeNomals.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _importNormals.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_importNormals.Label, optionsWidth);
                    }
                    else
                    {
                        _importNormals.Value = (ModelImporterNormals)EditorGUILayout.EnumPopup(_importNormals.Label, _importNormals.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _importNormals.ToolbarEnable = drawToggleEnableToolbar(_importNormals.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _importBlendShapeNormals.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_importBlendShapeNormals.Label, optionsWidth);
                    }
                    else
                    {
                        _importBlendShapeNormals.Value = (ModelImporterNormals)EditorGUILayout.EnumPopup(_importBlendShapeNormals.Label, _importBlendShapeNormals.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _importBlendShapeNormals.ToolbarEnable = drawToggleEnableToolbar(_importBlendShapeNormals.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _normalsMode.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_normalsMode.Label, optionsWidth);
                    }
                    else
                    {
                        _normalsMode.Value = (ModelImporterNormalCalculationMode)EditorGUILayout.EnumPopup(_normalsMode.Label, _normalsMode.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _normalsMode.ToolbarEnable = drawToggleEnableToolbar(_normalsMode.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _smoothingAngle.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_smoothingAngle.Label, optionsWidth);
                    }
                    else
                    {
                        _smoothingAngle.Value = EditorGUILayout.Slider(_smoothingAngle.Label, _smoothingAngle.Value, 0, 180, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _smoothingAngle.ToolbarEnable = drawToggleEnableToolbar(_smoothingAngle.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _tangents.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_tangents.Label, optionsWidth);
                    }
                    else
                    {
                        _tangents.Value = (ModelImporterTangents)EditorGUILayout.EnumPopup(_tangents.Label, _tangents.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _tangents.ToolbarEnable = drawToggleEnableToolbar(_tangents.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _swapUvs.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_swapUvs.Label, optionsWidth);
                    }
                    else
                    {
                        _swapUvs.Value = EditorGUILayout.Toggle(_swapUvs.Label, _swapUvs.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _swapUvs.ToolbarEnable = drawToggleEnableToolbar(_swapUvs.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _generateLightmapUvs.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_generateLightmapUvs.Label, optionsWidth);
                    }
                    else
                    {
                        _generateLightmapUvs.Value = EditorGUILayout.Toggle(_generateLightmapUvs.Label, _generateLightmapUvs.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _generateLightmapUvs.ToolbarEnable = drawToggleEnableToolbar(_generateLightmapUvs.ToolbarEnable);
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                bool isDisabled = _strictVertexDataChecks.ToolbarEnable == (int)TOOLBAR.DISABLE;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                    {
                        EditorGUILayout.LabelField(_strictVertexDataChecks.Label, optionsWidth);
                    }
                    else
                    {
                        _strictVertexDataChecks.Value = EditorGUILayout.Toggle(_strictVertexDataChecks.Label, _strictVertexDataChecks.Value, optionsWidth);
                    }
                }
                GUILayout.Space(INTERVAL_WIDTH);
                _strictVertexDataChecks.ToolbarEnable = drawToggleEnableToolbar(_strictVertexDataChecks.ToolbarEnable);
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

