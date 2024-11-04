using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class FBXOptionsManagerView : EditorWindow
    {
        private const int WindowWidth = 500;
        private const int WindowHeight = 150;
        private const int LabelWidth = 200;

        public static int SelectedLanguage;
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
        }

        private void OnGUI()
        {
            Localization.Localize();
            Options = new FbxOptions();
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // ラベル幅の調整
            EditorGUIUtility.labelWidth = LabelWidth;

            ShowSelectLanguage();
            SetWindowSize();
            ShowFolderPath();
            ShowSelectTargets();
            ShowOptionFoldOut();
            ShowExecute();
            ShowWarning();

            EditorGUILayout.EndScrollView();
        }

        [MenuItem(Settings.ToolMenuPlace + "/" + Settings.ToolName)]
        public static void ShowWindow()
        {
            GetWindow<FBXOptionsManagerView>(Settings.ToolName);
        }

        private void SetWindowSize()
        {
            var size = new Vector2(WindowWidth, WindowHeight);
            minSize = size;
        }

        private void ShowSelectLanguage()
        {
            SelectedLanguage = GUILayout.Toolbar(SelectedLanguage, Localization.Languages);
            if (_selectedLanguage != SelectedLanguage)
            {
                // ここに選択言語変更時の処理を追加できます。
            }

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
        } // ReSharper disable Unity.PerformanceAnalysis

        private void ShowExecute()
        {
            using (new EditorGUI.DisabledGroupScope(!CanExecute()))
            {
                if (GUILayout.Button(Localization.Lang.buttonExecute)) ExecuteOptions();
                Debug.Log(string.Format(Localization.Lang.logExecuted, Settings.ToolName));
            }
        }

        private void ExecuteOptions()
        {
            for (var i = 0; i < _fbxFiles.Count; i++)
            {
                if (!_targets[i]) continue;

                var fbxFile = _fbxFiles[i];
                var modelImporter = AssetImporter.GetAtPath(fbxFile) as ModelImporter;
                if (modelImporter == null) continue;

                Options.Execute(modelImporter);
                modelImporter.SaveAndReimport();
                AssetDatabase.SaveAssets();
                Debug.Log(string.Format(Localization.Lang.logOptionChanged, fbxFile));
            }
        }

        private void ShowOptionFoldOut()
        {
            OptionFoldOut = EditorGUILayout.Foldout(OptionFoldOut, "Options");
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
            if (_targets == null || _targets.Length != _fbxFiles.Count)
            {
                _targets = new bool[_fbxFiles.Count];
                Utility.ToggleArrayChecks(_targets, true);
            }
        }

        private bool CanExecute()
        {
            return _fbxFiles.Count > 0 && _targets.Any(c => c);
        }
    }
}