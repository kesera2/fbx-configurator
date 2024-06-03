using UnityEditor;

namespace kesera2.FBXOptionsManager
{

    public class FbxOptions
    {
        // Scenes
        private double scaleFactor = 1.0;
        private bool convertUnits = true;
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
        private ModelImporterNormalSmoothingSource smoothnessSource = ModelImporterNormalSmoothingSource.PreferSmoothingGroups;
        private int smoothingAngle = 60; // min:0 max:180
        private ModelImporterTangents tangents = ModelImporterTangents.CalculateMikk;
        private bool swapUvs = false;
        private bool generateLightmapUvs = false;

        public double ScaleFactor { get => scaleFactor; set => scaleFactor = value; }
        public bool ConvertUnits { get => convertUnits; set => convertUnits = value; }
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
        public ModelImporterNormalSmoothingSource SmoothnessSource { get => smoothnessSource; set => smoothnessSource = value; }
        public int SmoothingAngle { get => smoothingAngle; set => smoothingAngle = value; }
        public ModelImporterTangents Tangents { get => tangents; set => tangents = value; }
        public bool SwapUvs { get => swapUvs; set => swapUvs = value; }
        public bool GenerateLightmapUvs { get => generateLightmapUvs; set => generateLightmapUvs = value; }
    }
}

