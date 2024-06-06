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
            Option<float>.showOption(_scaleFactor);
            Option<bool>.showOption(_convertUnits);
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
            Option<ModelImporterMeshCompression>.showOption(_isReadable);
            Option<bool>.showOption(_isReadable);
            Option<MeshOptimizationFlags>.showOption(_optimizeMesh);
            Option<bool>.showOption(_generateColliders);


        }

        private void showGeometoryOptions()
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

