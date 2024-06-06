// TODO: TOOLBARが冗長、List<Option<T>>でCommon, Additional x3にまとめる。foreachで回す
// 上無理
// TODO: すべてDISABLEだったらメッセージを出す
// ALL DISABLE ENABLE BUTTON
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static kesera2.FBXOptionsManager.Toolbar;

namespace kesera2.FBXOptionsManager
{

    internal class FbxOptions
    {
        private Option<bool> _importCameras = OptionSettings._importCameras;
        private Option<bool> _importLights = OptionSettings._importLights;
        private Option<bool> _isReadable = OptionSettings._isReadable;
        private Option<ModelImporterNormals> _importNormals = OptionSettings._importNormals;
        private Option<ModelImporterNormals> _importBlendShapeNormals = OptionSettings._importBlendShapeNormals;
        private Option<bool> _legacyBlendShapeNomals = OptionSettings._legacyBlendShapeNomals;
        private Option<float> _scaleFactor = OptionSettings._scaleFactor;
        private Option<bool> _convertUnits = OptionSettings._convertUnits;
        private Option<bool> _bakeAxisConversion = OptionSettings._bakeAxisConversion;
        private Option<bool> _importBlendShapes = OptionSettings._importBlendShapes;
        private Option<bool> _importDeformPercent = OptionSettings._importDeformPercent;
        private Option<bool> _importVisibility = OptionSettings._importVisibility;
        private Option<bool> _preserveHierarchy = OptionSettings._preserveHierarchy;
        private Option<bool> _sortHierarchyByName = OptionSettings._sortHierarchyByName;
        private Option<ModelImporterMeshCompression> _meshCompression = OptionSettings._meshCompression;
        private Option<MeshOptimizationFlags> _optimizeMesh = OptionSettings._optimizeMesh;
        private Option<bool> _generateColliders = OptionSettings._generateColliders;
        private Option<bool> _keepQuads = OptionSettings._keepQuads;
        private Option<bool> _weldVertices = OptionSettings._weldVertices;
        private Option<ModelImporterIndexFormat> _indexFormat = OptionSettings._indexFormat;
        private Option<ModelImporterNormalCalculationMode> _normalsMode = OptionSettings._normalsMode;
        private Option<ModelImporterNormalSmoothingSource> _smoothnessSource = OptionSettings._smoothnessSource;
        private Option<float> _smoothingAngle = OptionSettings._smoothingAngle;
        private Option<ModelImporterTangents> _tangents = OptionSettings._tangents;
        private Option<bool> _swapUvs = OptionSettings._swapUvs;
        private Option<bool> _generateLightmapUvs = OptionSettings._generateLightmapUvs;
        private Option<bool> _strictVertexDataChecks = OptionSettings._strictVertexDataChecks;

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

            // FIXME: 調査中
            //if (_convertUnits.ToolbarEnable == (int)TOOLBAR.ENABLE)
            //{
            //    modelImporter.useFileUnits = _convertUnits.Value;
            //}

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

