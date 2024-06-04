using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{

    public class FbxOptions
    {
        internal class Option<T>
        {
            private T _value { get; set; }
            private int _toolbarEnable = 0;
            private string _label;
            private String _tooltip;
            internal Option(T value, int toolbarEnable, string label, String tooltip)
            {
                _value = value;
                _toolbarEnable = toolbarEnable;
                _label = label;
                _tooltip = tooltip;
            }
            public T Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public int ToolbarEnable
            {
                get { return _toolbarEnable; }
                set { _toolbarEnable = value; }
            }

            public string Label
            {
                get { return _label; }
            }

            public string Tooltip
            {
                get { return _tooltip; }
            }
        }

        private int toolbarSelected = 0;
        private static string[] TOOLBAR_LABLE = { "Enable", "Disable" };
        // Scenes
        private Option<float> _scaleFactor = new Option<float>(1.0f, 0, "Scale Factor", "");
        //private float _scaleFactor = 1.0f;
        //private int _scaleFactorEnabled = 0;
        //private string _scaleFactorTooltip = "sample";
        private bool _convertUnits = true;
        //private int 
        private bool bakeAxisConversion = false;
        private bool importBlendShapes = true;
        private bool importDeformPercent = false;
        private bool importVisibility = true;
        private bool importCameras = false;
        private bool importLights = false;
        private bool preserveHierarchy = false;
        private bool sortHierarchyByName = true;
        // Meshes
        private ModelImporterMeshCompression meshCompression = ModelImporterMeshCompression.Off;
        private bool isReadable = true;
        private MeshOptimizationFlags optimizeMesh = MeshOptimizationFlags.Everything;
        private bool generateColliders = false;
        // Germetory
        private bool keepQuads = false;
        private bool weldVertices = true;
        private ModelImporterIndexFormat _indexFormat = ModelImporterIndexFormat.Auto;
        private bool legacyBlendShapeNomals = false;
        private ModelImporterNormals importNormals = ModelImporterNormals.Import;
        private ModelImporterNormals importBlendShapeNormals = ModelImporterNormals.None;
        private ModelImporterNormalCalculationMode normalsMode = ModelImporterNormalCalculationMode.Unweighted_Legacy;
        private ModelImporterNormalSmoothingSource smoothnessSource = ModelImporterNormalSmoothingSource.PreferSmoothingGroups;
        [Range(0, 180)]
        private float smoothingAngle = 60;
        private ModelImporterTangents tangents = ModelImporterTangents.CalculateMikk;
        private bool swapUvs = false;
        private bool generateLightmapUvs = false;
        private bool strictVertexDataChecks = false;

        public void showCommonOptions()
        {
            //TODO: Add tooltips
            GUILayoutOption[] options = { GUILayout.Width(20) };
            GUILayoutOption[] verticalOptions = { GUILayout.Width(300) };
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope(verticalOptions))
                {
                    importCameras = EditorGUILayout.Toggle(new GUIContent("Import Cameras", "これを有効にすると.FBXファイルからカメラをインポートできます。"), importCameras);
                    importLights = EditorGUILayout.Toggle("Import Lights", importLights);
                    isReadable = EditorGUILayout.Toggle("Read/Write", isReadable);
                    importNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup("Nomals", importNormals);
                    importBlendShapeNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup("Blend Shape Nomals", importBlendShapeNormals);
                    legacyBlendShapeNomals = EditorGUILayout.Toggle("Legacy BlendShape Nomals", legacyBlendShapeNomals);
                }
                GUILayout.Space(10);
                using (new EditorGUILayout.VerticalScope())
                {
                    _scaleFactor.ToolbarEnable = drawToggleEnableToolbar(_scaleFactor.ToolbarEnable);
                    toolbarSelected = drawToggleEnableToolbar(toolbarSelected);
                    toolbarSelected = drawToggleEnableToolbar(toolbarSelected);
                    toolbarSelected = drawToggleEnableToolbar(toolbarSelected);
                    toolbarSelected = drawToggleEnableToolbar(toolbarSelected);
                    toolbarSelected = drawToggleEnableToolbar(toolbarSelected);
                }
            }
        }

        private int drawToggleEnableToolbar(int currentSelection)
        {
            return GUILayout.Toolbar(currentSelection, TOOLBAR_LABLE);
        }

        public void showAdditoinalSceneOptions()
        {
            _scaleFactor.Value = EditorGUILayout.FloatField(_scaleFactor.Label, _scaleFactor.Value);
            _convertUnits = EditorGUILayout.Toggle("Convert Units", _convertUnits);
            bakeAxisConversion = EditorGUILayout.Toggle("Bake Axis Conversion", bakeAxisConversion);
            importBlendShapes = EditorGUILayout.Toggle("Import Blend Shapes", importBlendShapes);
            importDeformPercent = EditorGUILayout.Toggle("Import Deform Percent", importDeformPercent);
            importVisibility = EditorGUILayout.Toggle("Import Visibility", importVisibility);
            preserveHierarchy = EditorGUILayout.Toggle("Preserve Hierarchy", preserveHierarchy);
            sortHierarchyByName = EditorGUILayout.Toggle("Sort Hierarchy ByName", sortHierarchyByName);
        }

        public void showAddtionalMeshOptions()
        {
            meshCompression = (ModelImporterMeshCompression)EditorGUILayout.EnumPopup("Mesh Compression", meshCompression);
            optimizeMesh = (MeshOptimizationFlags)EditorGUILayout.EnumPopup("Optimize Mesh", optimizeMesh);
            generateColliders = EditorGUILayout.Toggle("Generate Colliders", generateColliders);
        }

        public void showAddtionalGeometoryOptions()
        {
            keepQuads = EditorGUILayout.Toggle("Keep Quads", keepQuads);
            weldVertices = EditorGUILayout.Toggle("Weld Vertices", weldVertices);
            _indexFormat = (ModelImporterIndexFormat)EditorGUILayout.EnumPopup("Index Format", _indexFormat);
            normalsMode = (ModelImporterNormalCalculationMode)EditorGUILayout.EnumPopup("Normals Mode", normalsMode);
            smoothingAngle = EditorGUILayout.Slider("Smoothing Angle", smoothingAngle, 0, 180);
            tangents = (ModelImporterTangents)EditorGUILayout.EnumPopup("Tangents", tangents);
            swapUvs = EditorGUILayout.Toggle("Swap Uvs", swapUvs);
            generateLightmapUvs = EditorGUILayout.Toggle(Utility.ToLabelName(nameof(generateLightmapUvs)), generateLightmapUvs);
            strictVertexDataChecks = EditorGUILayout.Toggle(Utility.ToLabelName(nameof(strictVertexDataChecks)), strictVertexDataChecks);
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
                prop.SetValue(modelImporter, legacyBlendShapeNomals);
            }
        }

        public bool GetLegacyBlendShapeNomals(ModelImporter modelImporter)
        {
            PropertyInfo prop = GetLegacyBlendShapeNomalsProp(modelImporter);
            bool value = (bool)prop.GetValue(modelImporter);
            return value;

        }

        internal void ChangeImportCameras(ModelImporter modelImporter)
        {
            modelImporter.importCameras = importCameras;
        }

        internal void ChangeImportLights(ModelImporter modelImporter)
        {
            modelImporter.importLights = importLights;
        }

        internal void ChangeIsReadable(ModelImporter modelImporter)
        {
            modelImporter.isReadable = isReadable;
        }

        internal void ChangeImportNormals(ModelImporter modelImporter)
        {
            modelImporter.importNormals = importNormals;
        }

        internal void ChangeImportBlendShapeNormals(ModelImporter modelImporter)
        {
            modelImporter.importBlendShapeNormals = importBlendShapeNormals;
        }
    }
}

