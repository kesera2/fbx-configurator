// TODO: TOOLBARが冗長、List<Option<T>>でCommon, Additional x3にまとめる。foreachで回す
// 上無理
// TODO: すべてDISABLEだったらメッセージを出す
// ALL DISABLE ENABLE BUTTON
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    internal class FbxOptions
    {
        internal Option<bool> _importCameras = OptionSettings._importCameras;
        internal Option<bool> _importLights = OptionSettings._importLights;
        internal Option<bool> _isReadable = OptionSettings._isReadable;
        internal Option<ModelImporterNormals> _importNormals = OptionSettings._importNormals;
        internal Option<ModelImporterNormals> _importBlendShapeNormals = OptionSettings._importBlendShapeNormals;
        internal Option<bool> _legacyBlendShapeNomals = OptionSettings._legacyBlendShapeNomals;
        internal Option<float> _scaleFactor = OptionSettings._scaleFactor;
        internal Option<bool> _convertUnits = OptionSettings._convertUnits;
        internal Option<bool> _bakeAxisConversion = OptionSettings._bakeAxisConversion;
        internal Option<bool> _importBlendShapes = OptionSettings._importBlendShapes;
        internal Option<bool> _importDeformPercent = OptionSettings._importDeformPercent;
        internal Option<bool> _importVisibility = OptionSettings._importVisibility;
        internal Option<bool> _preserveHierarchy = OptionSettings._preserveHierarchy;
        internal Option<bool> _sortHierarchyByName = OptionSettings._sortHierarchyByName;
        internal Option<ModelImporterMeshCompression> _meshCompression = OptionSettings._meshCompression;
        internal Option<MeshOptimizationFlags> _optimizeMesh = OptionSettings._optimizeMesh;
        internal Option<bool> _generateColliders = OptionSettings._generateColliders;
        internal Option<bool> _keepQuads = OptionSettings._keepQuads;
        internal Option<bool> _weldVertices = OptionSettings._weldVertices;
        internal Option<ModelImporterIndexFormat> _indexFormat = OptionSettings._indexFormat;
        internal Option<ModelImporterNormalCalculationMode> _normalsMode = OptionSettings._normalsMode;
        internal Option<ModelImporterNormalSmoothingSource> _smoothnessSource = OptionSettings._smoothnessSource;
        internal Option<float> _smoothingAngle = OptionSettings._smoothingAngle;
        internal Option<ModelImporterTangents> _tangents = OptionSettings._tangents;
        internal Option<bool> _swapUvs = OptionSettings._swapUvs;
        internal Option<bool> _generateLightmapUvs = OptionSettings._generateLightmapUvs;
        internal Option<bool> _strictVertexDataChecks = OptionSettings._strictVertexDataChecks;

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
        internal void execute(ModelImporter modelImporter)
        {
            ApplyScaleFactor(modelImporter);
            //ApplyConvertUntis(modelImporter);
            ApplyBakeAxisConversion(modelImporter);
            ApplyImportBlendShapes(modelImporter);
            ApplyImportDeformPercent(modelImporter);
            ApplyImportVisibility(modelImporter);
            ApplyImportCamera(modelImporter);
            ApplyImportLights(modelImporter);
            ApplyPreserveHierarchy(modelImporter);
            ApplySortHierarchyByName(modelImporter);
            ApplyMeshCompression(modelImporter);
            ApplyIsReadable(modelImporter);
            ApplyOptimizeMesh(modelImporter);
            ApplyGenerateColliders(modelImporter);
            ApplyKeepQuads(modelImporter);
            ApplyWeldVertices(modelImporter);
            ApplyIndexFormat(modelImporter);
            ApplyLegacyBlendShapeNomals(modelImporter);
            ApplyImportNormals(modelImporter);
            ApplyImportBlendShapeNormals(modelImporter);
            ApplyNormalsMode(modelImporter);
            ApplySmoothnessSource(modelImporter);
            ApplySmoothingAngle(modelImporter);
            ApplyTangents(modelImporter);
            ApplySwapUvs(modelImporter);
            ApplyGenerateLightmapUvs(modelImporter);
            ApplyStrictVertexDataChecks(modelImporter);
            //modelImporter.SaveAndReimport();
            //AssetDatabase.SaveAssets();
        }

        internal void ApplyScaleFactor(ModelImporter modelImporter)
        {
            _scaleFactor.Update(modelImporter);
        }

        /// <summary>
        /// アップデートできないので調査中。
        /// </summary>
        [Obsolete("このメソッドの使用はサポートしていません。")]
        internal void ApplyConvertUntis(ModelImporter modelImporter)
        {
            _convertUnits.Update(modelImporter);
        }

        internal void ApplyBakeAxisConversion(ModelImporter modelImporter)
        {
            _bakeAxisConversion.Update(modelImporter);

        }

        internal void ApplyImportBlendShapes(ModelImporter modelImporter)
        {
            _importBlendShapes.Update(modelImporter);
        }

        internal void ApplyImportDeformPercent(ModelImporter modelImporter)
        {
            _importDeformPercent.Update(modelImporter);
        }

        internal void ApplyImportVisibility(ModelImporter modelImporter)
        {
            _importVisibility.Update(modelImporter);
        }

        internal void ApplyImportCamera(ModelImporter modelImporter)
        {
            _importCameras.Update(modelImporter);
        }

        internal void ApplyImportLights(ModelImporter modelImporter)
        {
            _importLights.Update(modelImporter);
        }

        internal void ApplyPreserveHierarchy(ModelImporter modelImporter)
        {
            _preserveHierarchy.Update(modelImporter);
        }

        internal void ApplySortHierarchyByName(ModelImporter modelImporter)
        {
            _sortHierarchyByName.Update(modelImporter);
        }

        internal void ApplyMeshCompression(ModelImporter modelImporter)
        {
            _meshCompression.Update(modelImporter);
        }

        internal void ApplyIsReadable(ModelImporter modelImporter)
        {
            _isReadable.Update(modelImporter);
        }

        internal void ApplyOptimizeMesh(ModelImporter modelImporter)
        {
            _optimizeMesh.Update(modelImporter);
        }
        internal void ApplyGenerateColliders(ModelImporter modelImporter)
        {
            _generateColliders.Update(modelImporter);
        }

        internal void ApplyKeepQuads(ModelImporter modelImporter)
        {
            _keepQuads.Update(modelImporter);
        }
        internal void ApplyWeldVertices(ModelImporter modelImporter)
        {
            _weldVertices.Update(modelImporter);
        }

        internal void ApplyIndexFormat(ModelImporter modelImporter)
        {
            _indexFormat.Update(modelImporter);
        }


        internal void ApplyImportNormals(ModelImporter modelImporter)
        {
            _importNormals.Update(modelImporter);
        }

        internal void ApplyImportBlendShapeNormals(ModelImporter modelImporter)
        {
            _importBlendShapeNormals.Update(modelImporter);
        }

        internal void ApplyNormalsMode(ModelImporter modelImporter)
        {
            _normalsMode.Update(modelImporter);
        }

        internal void ApplySmoothnessSource(ModelImporter modelImporter)
        {
            _smoothnessSource.Update(modelImporter);
        }

        internal void ApplySmoothingAngle(ModelImporter modelImporter)
        {
            _smoothingAngle.Update(modelImporter);
        }
        internal void ApplyTangents(ModelImporter modelImporter)
        {
            _tangents.Update(modelImporter);
        }

        internal void ApplySwapUvs(ModelImporter modelImporter)
        {
            _swapUvs.Update(modelImporter);
        }
        internal void ApplyGenerateLightmapUvs(ModelImporter modelImporter)
        {
            _generateLightmapUvs.Update(modelImporter);
        }
        internal void ApplyStrictVertexDataChecks(ModelImporter modelImporter)
        {
            _strictVertexDataChecks.Update(modelImporter);
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

