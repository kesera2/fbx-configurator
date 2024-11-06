using UnityEngine;

namespace kesera2.FBXConfigurator
{
    [CreateAssetMenu(menuName = "FBXConfigurator/LaguageData")]
    public class LanguageHash : ScriptableObject
    {
        [Header("Label")] public string labelTargetDirectory;

        // Toggle Toolbar
        public string labelToggleToolbar;
        public string labelToggleToolbarToEnable;
        public string labelToggleToolbarToDefault;
        public string labelToggleToolbarToDisable;

        // Scene
        public string labelSceneGroup;
        public string labelScaleFactor;
        public string labelConvertUnits;
        public string labelBakeAxisConversion;
        public string labelImportBlendShapes;
        public string labelImportDeformPercent;
        public string labelImportVisibility;
        public string labelImportCameras;
        public string labelImportLights;
        public string labelPreserveHierarchy;
        public string labelSortHierarchyByName;

        // Mesh
        public string labelMeshGroup;
        public string labelMeshCompression;
        public string labelIsReadable;
        public string labelOptimizeMesh;
        public string labelGenerateColliders;

        // Geometry
        public string labelGeometryGroup;
        public string labelKeepQuads;
        public string labelWeldVertices;
        public string labelIndexFormat;
        public string labelLegacyBlendShapeNomals;
        public string labelImportNormals;
        public string labelImportBlendShapeNormals;
        public string labelNormalsMode;
        public string labelSmoothnessSource;
        public string labelSmoothingAngle;
        public string labelTangents;
        public string labelSwapUvs;
        public string labelGenerateLightmapUvs;
        public string labelStrictVertexDataChecks;

        [Header("Foldout Label")] public string foldoutOptions;

        public string foldoutTargetFbxFiles;

        [Header("Button")] public string buttonOpenDirectory;

        public string buttonSelectAllFbx;
        public string buttonUnselectAllFbx;
        public string buttonAllEnable;
        public string buttonUseDefault;
        public string buttonAllDisable;
        public string buttonExecute;

        [Header("CheckBox")] public string checkboxSelectTargetFBX;

        [Header("Toolbar")] public string toolbarMenuGroupModel;

        public string toolbarMenuGroupOther;
        public string toolbarEnable;
        public string toolbarDisable;

        [Header("Dialog")] public string executeDialogMessage;


        [Header("Window Label")] public string windowLabelSelectFolder;

        [Header("Help Box")] public string helpboxInfoNeedlessToChangeOptions;

        public string helpboxWarningTargetFbxIsNotFound;

        [Header("Log")] public string logOptionChanged;

        public string logExecuted;
    }
}