using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace kesera2.FBXConfigurator
{
    public class FBXConfiguratorView : EditorWindow
    {
        private const int WindowWidth = 500;
        private const int WindowHeight = 150;
        private const int LabelWidth = 200;

        private const string OptionsFoldoutLabel = "Options";

        public static int SelectedLanguage;
        private Object _currentSelectedObject;
        private List<string> _fbxFiles;

        // Tab system
        private int _currentTab = 0;

        // Copy Mode fields
        private GameObject _srcModel;
        private GameObject _destModel;

        // Options
        private string _folderPath;
        private bool _isSelectTargetFBX;
        private string _projectPath;
        private Vector2 _scrollPosition = Vector2.zero; // スクロール位置
        private int _selectedLanguage;
        private bool _targetFoldOut;
        private bool[] _targets = { };
        internal bool OptionFoldOut;
        internal FbxOptions Options;
        internal string RelativePath;


        private void OnEnable()
        {
            // フォルダパス初期化
            _folderPath = Application.dataPath;
            _projectPath = Path.GetDirectoryName(Application.dataPath);
            RefreshFBXFileList();

            // targetsの初期化
            InitializeTargets();
            UpdateOptions();
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            // ラベル幅の調整
            EditorGUIUtility.labelWidth = LabelWidth;
            DrawLogo();
            ShowSelectLanguage();
            SetWindowSize();

            // Tab system
            string[] tabNames = { Localization.Lang.tabBatchSettings, Localization.Lang.tabCopyMode };
            _currentTab = GUILayout.Toolbar(_currentTab, tabNames);
            EditorGUILayout.Space(10);

            if (_currentTab == 0)
            {
                // Batch Settings Tab
                ShowFolderPath();
                ShowSelectTargets();
                ShowOptionFoldOut();
                // ShowDebug();
                ShowExecute();
                ShowWarning();
            }
            else if (_currentTab == 1)
            {
                // Copy Mode Tab
                ShowCopyMode();
            }

            EditorGUILayout.EndScrollView();
        }

        private void UpdateOptions()
        {
            Localization.Localize();
            Options = new FbxOptions();
        }

        [MenuItem(Settings.ToolMenuPlace + "/" + Settings.ToolName)]
        public static void ShowWindow()
        {
            GetWindow<FBXConfiguratorView>(Settings.ToolName);
        }

        private void SetWindowSize()
        {
            var size = new Vector2(WindowWidth, WindowHeight);
            minSize = size;
        }

        private static void DrawLogo()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var logo = Resources.Load<Texture2D>("Icon/FBX_Configurator_Logo");
            EditorGUILayout.LabelField(new GUIContent(logo), GUILayout.Height(100), GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void ShowSelectLanguage()
        {
            SelectedLanguage = GUILayout.Toolbar(SelectedLanguage, Localization.Languages);
            if (_selectedLanguage != SelectedLanguage) UpdateOptions();

            _selectedLanguage = SelectedLanguage;
        }

        private void ShowSelectTargets()
        {
            _targetFoldOut = EditorGUILayout.Foldout(_targetFoldOut, Localization.Lang.foldoutTargetFbxFiles);
            if (!_targetFoldOut) return;

            using (new EditorGUILayout.HorizontalScope())
            {
                ShowSelectTargetFBXCheckbox();
            }

            ShowSelectAllButtons();

            if (_fbxFiles == null) return;

            using (new EditorGUILayout.VerticalScope("box"))
            {
                for (var i = 0; i < _fbxFiles.Count; i++)
                {
                    var fbxFile = _fbxFiles[i];
                    _targets[i] = EditorGUILayout.ToggleLeft(fbxFile, _targets[i]);
                }
            }
        }

        private void ShowSelectAllButtons()
        {
            using (new EditorGUI.DisabledGroupScope(!_isSelectTargetFBX))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(Localization.Lang.buttonSelectAllFbx))
                        Utility.ToggleArrayChecks(_targets, true);
                    if (GUILayout.Button(Localization.Lang.buttonUnselectAllFbx))
                        Utility.ToggleArrayChecks(_targets, false);
                }
            }
        }

        private void ShowSelectTargetFBXCheckbox()
        {
            _isSelectTargetFBX =
                EditorGUILayout.ToggleLeft(Localization.Lang.checkboxSelectTargetFBX, _isSelectTargetFBX);
            if (!_isSelectTargetFBX) Utility.ToggleArrayChecks(_targets, true);
        }

        private void ShowFolderPath()
        {
            GUILayoutOption[] options = { GUILayout.ExpandWidth(true) };
            EditorGUILayout.LabelField(Localization.Lang.labelTargetDirectory);
            EditorGUILayout.LabelField(_folderPath, EditorStyles.wordWrappedLabel, options);
            if (GUILayout.Button(Localization.Lang.buttonOpenDirectory))
            {
                _folderPath = EditorUtility.OpenFolderPanel(Localization.Lang.windowLabelSelectFolder, _folderPath,
                    string.Empty);
                RefreshFBXFileList();
            }
        }

        private void ShowExecute()
        {
            using (new EditorGUI.DisabledGroupScope(!CanExecute()))
            {
                if (!GUILayout.Button(Localization.Lang.buttonExecute)) return;
                if (!DisplayConfirmDialog()) return;
                ExecuteOptions();

                var message = string.Format(Localization.Lang.logExecuted, Settings.ToolName);
                Debug.Log(message);
                EditorUtility.DisplayDialog(
                    Settings.ToolName,
                    message,
                    "OK"
                );
            }
        }

        private void ExecuteOptions()
        {
            // NOTE: InspectorでFBXがアクティブになっているとSaveAssetsが適用されないため一旦退避する
            StashAndClearCurrentSelection();
            for (var i = 0; i < _fbxFiles.Count; i++)
            {
                if (!_targets[i]) continue;

                var fbxFile = _fbxFiles[i];
                var modelImporter = AssetImporter.GetAtPath(fbxFile) as ModelImporter;
                if (!modelImporter) continue;

                Options.Execute(modelImporter);
                modelImporter.SaveAndReimport();
                Debug.Log(string.Format(Localization.Lang.logOptionChanged, fbxFile));
            }

            // 選択オブジェクトの復元
            RestoreSelection();
            AssetDatabase.SaveAssets();
        }

        private bool DisplayConfirmDialog()
        {
            // メッセージボックスを表示
            return EditorUtility.DisplayDialog(
                Settings.ToolName, // ダイアログのタイトル
                string.Format(Localization.Lang.executeDialogMessage, _fbxFiles.Count), // メッセージ
                "OK",
                "Cancel"
            );
        }

        private void ShowOptionFoldOut()
        {
            OptionFoldOut = EditorGUILayout.Foldout(OptionFoldOut, OptionsFoldoutLabel);
            if (!OptionFoldOut) return;
            Options.ShowOptions();
            EditorGUILayout.HelpBox(Localization.Lang.helpboxInfoNeedlessToChangeOptions, MessageType.Info);
        }

        private void RefreshFBXFileList()
        {
#if UNITY_2019_4_31
            int index = folderPath.IndexOf("Assets");
            // Assetsからの相対パスを取得
            if (index > 0)
            {
                relativePath = folderPath.Substring(index);
            }
            else
            {
                relativePath = Application.dataPath;
            }
#elif UNITY_2019_4_OR_NEWER
            RelativePath = Path.GetRelativePath(_projectPath, _folderPath);
#endif
            _fbxFiles = Utility.GetFBXFiles(RelativePath);
        }

        private void ShowWarning()
        {
            if (!CanExecute())
                EditorGUILayout.HelpBox(Localization.Lang.helpboxWarningTargetFbxIsNotFound, MessageType.Warning);
        }

        private void InitializeTargets()
        {
            if (_targets != null && _targets.Length == _fbxFiles.Count) return;
            _targets = new bool[_fbxFiles.Count];
            Utility.ToggleArrayChecks(_targets, true);
        }

        private bool CanExecute()
        {
            return _fbxFiles.Count > 0 && _targets.Any(c => c);
        }

        private void ShowDebug()
        {
            if (GUILayout.Button("Debug"))
            {
                Debug.Log($"対象ファイル数: {_fbxFiles.Count}");
                Debug.Log($"folderPath: {_folderPath} relativePath:  {RelativePath} projectPath: {_projectPath}");
                for (var i = 0; i < _fbxFiles.Count; i++)
                {
                    if (!_targets[i]) continue;
                    var fbxFile = _fbxFiles[i];
                    var folderPath = Path.GetDirectoryName(fbxFile);
                    var model = AssetImporter.GetAtPath(fbxFile) as ModelImporter;
                    Debug.Log("model is null : " + model == null + ", file path : " + fbxFile);
                    if (model != null)
                    {
                        Debug.Log(folderPath + fbxFile + " importCameras " + model.importCameras);
                        Debug.Log(folderPath + fbxFile + " importLights " + model.importLights);
                        Debug.Log(folderPath + fbxFile + " isReadable " + model.isReadable);
                        Debug.Log(folderPath + fbxFile + " importNormals " + model.importNormals);
                        Debug.Log(folderPath + fbxFile + " importBlendShapeNormals " + model.importBlendShapeNormals);
                        Debug.Log(folderPath + fbxFile + " LegacyBlendShapeNomals " +
                                  Options.GetLegacyBlendShapeNormals(model));
                        Options.GetLegacyBlendShapeNormals(model);
                    }
                }
            }
        }

        private void StashAndClearCurrentSelection()
        {
            _currentSelectedObject = Selection.activeObject;

            // 例として新しいGameObjectを作成
            var newObject = new GameObject("New Object");

            // 選択オブジェクトを新しいオブジェクトに変更
            Selection.activeObject = newObject;

            // 新しいオブジェクトをすぐに削除
            DestroyImmediate(newObject);
        }

        private void RestoreSelection()
        {
            Selection.activeObject = _currentSelectedObject;
        }

        // Copy Mode Methods
        private void ShowCopyMode()
        {
            EditorGUILayout.LabelField(Localization.Lang.copyModeTitle, EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.HelpBox(
                Localization.Lang.copyModeDescription,
                MessageType.Info);

            EditorGUILayout.Space(10);

            // Source Model field
            _srcModel = (GameObject)EditorGUILayout.ObjectField(
                Localization.Lang.copyModeLabelSourceModel,
                _srcModel,
                typeof(GameObject),
                false);

            // Destination Model field
            _destModel = (GameObject)EditorGUILayout.ObjectField(
                Localization.Lang.copyModeLabelDestModel,
                _destModel,
                typeof(GameObject),
                false);

            EditorGUILayout.Space(10);

            // Copy button
            using (new EditorGUI.DisabledGroupScope(!CanCopy()))
            {
                if (GUILayout.Button(Localization.Lang.copyModeButtonCopy, GUILayout.Height(30)))
                {
                    CopyFBXSettings();
                }
            }

            // Warning message
            if (!CanCopy())
            {
                EditorGUILayout.HelpBox(
                    Localization.Lang.copyModeWarningSelectBoth,
                    MessageType.Warning);
            }
        }

        private bool CanCopy()
        {
            return _srcModel != null && _destModel != null &&
                   GetModelImporter(_srcModel) != null &&
                   GetModelImporter(_destModel) != null;
        }

        private ModelImporter GetModelImporter(GameObject obj)
        {
            if (obj == null) return null;

            var assetPath = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(assetPath)) return null;

            return AssetImporter.GetAtPath(assetPath) as ModelImporter;
        }

        private void CopyFBXSettings()
        {
            var srcImporter = GetModelImporter(_srcModel);
            var destImporter = GetModelImporter(_destModel);

            if (srcImporter == null || destImporter == null)
            {
                EditorUtility.DisplayDialog(
                    Localization.Lang.copyModeDialogTitle,
                    Localization.Lang.copyModeDialogError,
                    "OK");
                return;
            }

            // Confirmation dialog
            var srcPath = AssetDatabase.GetAssetPath(_srcModel);
            var destPath = AssetDatabase.GetAssetPath(_destModel);
            var message = string.Format(Localization.Lang.copyModeDialogMessage, srcPath, destPath);

            if (!EditorUtility.DisplayDialog(
                Localization.Lang.copyModeDialogTitle,
                message,
                "OK",
                "Cancel"))
            {
                return;
            }

            // Copy all settings
            CopyAllModelImporterSettings(srcImporter, destImporter);

            // Save and reimport
            destImporter.SaveAndReimport();
            AssetDatabase.SaveAssets();

            EditorUtility.DisplayDialog(
                Localization.Lang.copyModeDialogTitle,
                Localization.Lang.copyModeDialogSuccess,
                "OK");

            Debug.Log($"[FBX Configurator] Copied settings from '{srcPath}' to '{destPath}'");
        }

        private void CopyAllModelImporterSettings(ModelImporter src, ModelImporter dest)
        {
            // Model Tab Settings
            dest.globalScale = src.globalScale;
            dest.useFileScale = src.useFileScale;
            // Note: fileScale is read-only, cannot be set directly
            dest.meshCompression = src.meshCompression;
            dest.isReadable = src.isReadable;
            dest.optimizeMeshPolygons = src.optimizeMeshPolygons;
            dest.optimizeMeshVertices = src.optimizeMeshVertices;
            dest.addCollider = src.addCollider;

            dest.importBlendShapes = src.importBlendShapes;
            dest.importVisibility = src.importVisibility;
            dest.importCameras = src.importCameras;
            dest.importLights = src.importLights;
            dest.preserveHierarchy = src.preserveHierarchy;
            dest.sortHierarchyByName = src.sortHierarchyByName;
            dest.swapUVChannels = src.swapUVChannels;
            dest.generateSecondaryUV = src.generateSecondaryUV;
            dest.weldVertices = src.weldVertices;
            dest.keepQuads = src.keepQuads;
            dest.indexFormat = src.indexFormat;

            dest.importNormals = src.importNormals;
            dest.importBlendShapeNormals = src.importBlendShapeNormals;
            dest.normalCalculationMode = src.normalCalculationMode;
            dest.normalSmoothingSource = src.normalSmoothingSource;
            dest.normalSmoothingAngle = src.normalSmoothingAngle;
            dest.importTangents = src.importTangents;

            // Rig Tab Settings
            dest.animationType = src.animationType;
            dest.avatarSetup = src.avatarSetup;
            dest.optimizeGameObjects = src.optimizeGameObjects;

            if (src.avatarSetup == ModelImporterAvatarSetup.CopyFromOther && src.sourceAvatar != null)
            {
                dest.sourceAvatar = src.sourceAvatar;
            }

            // Animation Tab Settings
            dest.importAnimation = src.importAnimation;
            dest.bakeAxisConversion = src.bakeAxisConversion;
            dest.resampleCurves = src.resampleCurves;
            dest.animationCompression = src.animationCompression;
            dest.animationRotationError = src.animationRotationError;
            dest.animationPositionError = src.animationPositionError;
            dest.animationScaleError = src.animationScaleError;

            // Copy clip animations
            if (src.clipAnimations != null && src.clipAnimations.Length > 0)
            {
                dest.clipAnimations = src.clipAnimations;
            }

            // Materials Tab Settings
            // Note: importMaterials is obsolete, use materialImportMode instead
            dest.materialImportMode = src.materialImportMode;
            dest.materialLocation = src.materialLocation;
            dest.materialName = src.materialName;
            dest.materialSearch = src.materialSearch;

            Debug.Log("[FBX Configurator] All settings copied: Model, Rig, Animation, Materials");
        }
    }
}