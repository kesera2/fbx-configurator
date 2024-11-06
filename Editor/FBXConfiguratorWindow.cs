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
            ShowFolderPath();
            ShowSelectTargets();
            ShowOptionFoldOut();
            // ShowDebug();
            ShowExecute();
            ShowWarning();

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
            var logo = Resources.Load<Texture2D>("Icon/Logo");
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
    }
}