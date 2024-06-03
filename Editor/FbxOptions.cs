using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{

    public class FbxOptions
    {
        // Scenes
        private float scaleFactor = 1.0f;
        private bool convertUnits = true;
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
        private ModelImporterIndexFormat indexFormat = ModelImporterIndexFormat.Auto;
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
            ImportCameras = EditorGUILayout.Toggle(new GUIContent("Import Cameras", "これを有効にすると.FBXファイルからカメラをインポートできます。"), ImportCameras);
            ImportLights = EditorGUILayout.Toggle("Import Lights", ImportLights);
            IsReadable = EditorGUILayout.Toggle("Read/Write", IsReadable);
            ImportNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup("Nomals", ImportNormals);
            ImportBlendShapeNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup("Blend Shape Nomals", ImportBlendShapeNormals);
            LegacyBlendShapeNomals = EditorGUILayout.Toggle("Legacy BlendShape Nomals", LegacyBlendShapeNomals);
        }

        public void showAdditoinalSceneOptions()
        {
            ScaleFactor = EditorGUILayout.FloatField("Scale Factor", ScaleFactor);
            ConvertUnits = EditorGUILayout.Toggle("Convert Units", ConvertUnits);
            BakeAxisConversion = EditorGUILayout.Toggle("Bake Axis Conversion", BakeAxisConversion);
            ImportBlendShapes = EditorGUILayout.Toggle("Import Blend Shapes", ImportBlendShapes);
            ImportDeformPercent = EditorGUILayout.Toggle("Import Deform Percent", ImportDeformPercent);
            ImportVisibility = EditorGUILayout.Toggle("Import Visibility", ImportVisibility);
            PreserveHierarchy = EditorGUILayout.Toggle("Preserve Hierarchy", PreserveHierarchy);
            SortHierarchyByName = EditorGUILayout.Toggle("Sort Hierarchy ByName", SortHierarchyByName);
        }

        public void showAddtionalMeshOptions()
        {
            MeshCompression = (ModelImporterMeshCompression)EditorGUILayout.EnumPopup("Mesh Compression", MeshCompression);
            OptimizeMesh = (MeshOptimizationFlags)EditorGUILayout.EnumPopup("Optimize Mesh", OptimizeMesh);
            GenerateColliders = EditorGUILayout.Toggle("Generate Colliders", GenerateColliders);
        }

        public void showAddtionalGeometoryOptions()
        {
            KeepQuads = EditorGUILayout.Toggle("Keep Quads", KeepQuads);
            WeldVertices = EditorGUILayout.Toggle("Weld Vertices", WeldVertices);
            IndexFormat = (ModelImporterIndexFormat)EditorGUILayout.EnumPopup("Index Format", IndexFormat);
            NormalsMode = (ModelImporterNormalCalculationMode)EditorGUILayout.EnumPopup("Normals Mode", NormalsMode);
            SmoothingAngle = EditorGUILayout.Slider("Smoothing Angle", SmoothingAngle, 0, 180);
            Tangents = (ModelImporterTangents)EditorGUILayout.EnumPopup("Tangents", Tangents);
            SwapUvs = EditorGUILayout.Toggle("Swap Uvs", SwapUvs);
            GenerateLightmapUvs = EditorGUILayout.Toggle(Utility.ToLabelName(nameof(GenerateLightmapUvs)), GenerateLightmapUvs);
            StrictVertexDataChecks = EditorGUILayout.Toggle(Utility.ToLabelName(nameof(StrictVertexDataChecks)), StrictVertexDataChecks);
        }

        public PropertyInfo getLegacyBlendShapeNomalsProp(ModelImporter modelImporter)
        {
            return modelImporter.GetType().GetProperty("legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        public void setLegacyBlendShapeNomals(ModelImporter modelImporter, bool legacyBlendShapeNomals)
        {
            PropertyInfo prop = getLegacyBlendShapeNomalsProp(modelImporter);
            if (prop != null)
            {
                prop.SetValue(modelImporter, legacyBlendShapeNomals);
            }
        }

        public bool getLegacyBlendShapeNomals(ModelImporter modelImporter)
        {
            PropertyInfo prop = getLegacyBlendShapeNomalsProp(modelImporter);
            bool value = (bool)prop.GetValue(modelImporter);
            return value;

        }


        public float ScaleFactor { get => scaleFactor; set => scaleFactor = value; }
        public bool ConvertUnits { get => convertUnits; set => convertUnits = value; }
        public bool BakeAxisConversion { get => bakeAxisConversion; set => bakeAxisConversion = value; }
        public bool ImportBlendShapes { get => importBlendShapes; set => importBlendShapes = value; }
        public bool ImportDeformPercent { get => importDeformPercent; set => importDeformPercent = value; }
        public bool ImportVisibility { get => importVisibility; set => importVisibility = value; }
        public bool ImportCameras { get => importCameras; set => importCameras = value; }
        public bool ImportLights { get => importLights; set => importLights = value; }
        public bool PreserveHierarchy { get => preserveHierarchy; set => preserveHierarchy = value; }
        public bool SortHierarchyByName { get => sortHierarchyByName; set => sortHierarchyByName = value; }
        public ModelImporterMeshCompression MeshCompression { get => meshCompression; set => meshCompression = value; }
        public bool IsReadable { get => isReadable; set => isReadable = value; }
        public MeshOptimizationFlags OptimizeMesh { get => optimizeMesh; set => optimizeMesh = value; }
        public bool GenerateColliders { get => generateColliders; set => generateColliders = value; }
        public bool KeepQuads { get => keepQuads; set => keepQuads = value; }
        public bool WeldVertices { get => weldVertices; set => weldVertices = value; }
        public ModelImporterIndexFormat IndexFormat { get => indexFormat; set => indexFormat = value; }
        public bool LegacyBlendShapeNomals { get => legacyBlendShapeNomals; set => legacyBlendShapeNomals = value; }
        public ModelImporterNormals ImportNormals { get => importNormals; set => importNormals = value; }
        public ModelImporterNormals ImportBlendShapeNormals { get => importBlendShapeNormals; set => importBlendShapeNormals = value; }
        public ModelImporterNormalCalculationMode NormalsMode { get => normalsMode; set => normalsMode = value; }
        public ModelImporterNormalSmoothingSource SmoothnessSource { get => smoothnessSource; set => smoothnessSource = value; }
        public float SmoothingAngle { get => smoothingAngle; set => smoothingAngle = value; }
        public ModelImporterTangents Tangents { get => tangents; set => tangents = value; }
        public bool SwapUvs { get => swapUvs; set => swapUvs = value; }
        public bool GenerateLightmapUvs { get => generateLightmapUvs; set => generateLightmapUvs = value; }
        public bool StrictVertexDataChecks { get => strictVertexDataChecks; set => strictVertexDataChecks = value; }
    }
}

