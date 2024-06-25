using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class FBXOptionsManagerView : EditorWindow
    {
        private Vector2 _scrollPosition = Vector2.zero;         // スクロール位置

        private string projectPath;
        internal string relativePath;
        private List<string> fbxFiles;
        private bool processAllFBXFiles = true;
        private bool targetFoldOut = false;
        internal bool optionFoldOut = false;
        private bool additionalOptionFoldOut = false;
        // Options
        private string folderPath;
        internal FbxOptions options;

        private bool[] targets = { };
        private int WINDOW_WIDTH = 500;
        private int WINDOW_HEIGHT = 150;

        [MenuItem(Settings.TOOL_MENU_PLACE + "/" + Settings.TOOL_NAME)]
        public static void ShowWindow()
        {
            GetWindow<FBXOptionsManagerView>(Settings.TOOL_NAME);
        }

        private void OnEnable()
        {
            Localization.Localize();
            options = new FbxOptions();
            // フォルダパス初期化
            folderPath = Application.dataPath;
            projectPath = Path.GetDirectoryName(Application.dataPath);
            RefreshFBXFileList();
            // targetsの初期化
            if (targets == null || targets.Length != fbxFiles.Count)
            {
                targets = new bool[fbxFiles.Count];
                Utility.toggleArrayChecks(targets, true);
            }
        }

        private void setWindowSize()
        {
            Vector2 vector = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
            this.minSize = vector;
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // ラベル幅の調整
            EditorGUIUtility.labelWidth = 200; // TODO: call at once
            showSelectLangage();
            setWindowSize();
            showTargetList();
            showFolderPath();
            showSelectTargets();
            showOptionFoldOut();
            showDebug();
            showExecute();
            showWarning();
            EditorGUILayout.EndScrollView();
        }

        internal static int selectedLanguage;

        private void showSelectLangage()
        {
            selectedLanguage = GUILayout.Toolbar(selectedLanguage, Localization.languages);
        }

        private void showSelectTargets()
        {
            targetFoldOut = EditorGUILayout.Foldout(targetFoldOut, Localization.lang.foldoutTargetFbxFiles);

            if (targetFoldOut)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    containsAllFbx();
                }
                using (new EditorGUI.DisabledGroupScope(processAllFBXFiles))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        CheckAllCheckboxes();
                        UncheckAllCheckboxes();
                    }
                }
                if (fbxFiles != null)
                {
                    using (new EditorGUILayout.VerticalScope("box"))
                    {
                        for (int i = 0; i < fbxFiles.Count; i++)
                        {
                            string fbxFile = fbxFiles[i];
                            using (new EditorGUI.DisabledGroupScope(processAllFBXFiles))
                            {
                                targets[i] = EditorGUILayout.ToggleLeft(fbxFile, targets[i]);
                            }

                        }
                    }
                }
            }
        }

        private void UncheckAllCheckboxes()
        {
            if (GUILayout.Button(Localization.lang.buttonUncheckAll))
            {
                Utility.toggleArrayChecks(targets, false);
            }
        }

        private void CheckAllCheckboxes()
        {
            if (GUILayout.Button(Localization.lang.checkSelectAll))
            {
                Utility.toggleArrayChecks(targets, true);
            }
        }

        private void containsAllFbx()
        {
            processAllFBXFiles = EditorGUILayout.ToggleLeft(Localization.lang.buttonCheckAll, processAllFBXFiles);
            if (processAllFBXFiles)
            {
                Utility.toggleArrayChecks(targets, true);
            }
        }

        private void showFolderPath()
        {
            GUILayoutOption[] options = { GUILayout.ExpandWidth(true) };
            EditorGUILayout.LabelField(Localization.lang.labelTargetDirectory);
            EditorGUILayout.LabelField(folderPath, EditorStyles.wordWrappedLabel, options);
            if (GUILayout.Button(Localization.lang.buttonOpenDirectory))
            {
                folderPath = EditorUtility.OpenFolderPanel(Localization.lang.windowLabelSelectFolder, folderPath, string.Empty);
                RefreshFBXFileList();
            }
        }

        private void showExecute()
        {
            using (new EditorGUI.DisabledGroupScope(!canExecute()))
            {

                if (GUILayout.Button(Localization.lang.buttonExecute))
                {
                    for (int i = 0; i < fbxFiles.Count; i++)
                    {
                        if (!targets[i])
                        {
                            continue;
                        }
                        string fbxFile = fbxFiles[i].ToString();
                        ModelImporter modelImporter = AssetImporter.GetAtPath(fbxFile) as ModelImporter;
                        if (modelImporter != null)
                        {
                            options.execute(modelImporter);
                            modelImporter.SaveAndReimport();
                            AssetDatabase.SaveAssets();
                            Debug.Log(string.Format(Localization.lang.logOptionChanged, fbxFile));
                        }
                    }
                    Debug.Log(string.Format(Localization.lang.logExecuted, Settings.TOOL_NAME));
                }
            }
        }

        private void showTargetList()
        {
            using (new EditorGUILayout.HorizontalScope())
            {

            }
        }

        private void showOptionFoldOut()
        {
            optionFoldOut = EditorGUILayout.Foldout(optionFoldOut, "Options");

            if (optionFoldOut)
            {
                options.showOptions();
                EditorGUILayout.HelpBox(Localization.lang.helpboxInfoNeedlessToChangeOptions, MessageType.Info);
            }
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
            relativePath = Path.GetRelativePath(projectPath, folderPath);
#endif
            fbxFiles = Utility.GetFBXFiles(relativePath);
        }

        private void showWarning()
        {
            if (!canExecute())
            {
                EditorGUILayout.HelpBox(Localization.lang.helpboxWarningTargetFbxNotFound, MessageType.Warning);
            }
        }

        private void showDebug()
        {
            if (GUILayout.Button("Debug"))
            {
                Debug.Log($"対象ファイル数: {fbxFiles.Count}");
                Debug.Log($"folderPath: {folderPath} relativePath:  {relativePath} projectPath: {projectPath}");
                for (int i = 0; i < fbxFiles.Count; i++)
                {
                    if (!targets[i])
                    {
                        continue;
                    }
                    string fbxFile = fbxFiles[i].ToString();
                    string folderPath = Path.GetDirectoryName(fbxFile);
                    ModelImporter model = AssetImporter.GetAtPath(fbxFile) as ModelImporter;
                    Debug.Log("model is null : " + model == null + ", file path : " + fbxFile);
                    if (model != null)
                    {

                        Debug.Log(folderPath + fbxFile + " importCameras " + model.importCameras);
                        Debug.Log(folderPath + fbxFile + " importLights " + model.importLights);
                        Debug.Log(folderPath + fbxFile + " isReadable " + model.isReadable);
                        Debug.Log(folderPath + fbxFile + " importNormals " + model.importNormals);
                        Debug.Log(folderPath + fbxFile + " importBlendShapeNormals " + model.importBlendShapeNormals);
                        Debug.Log(folderPath + fbxFile + " LegacyBlendShapeNomals " + options.GetLegacyBlendShapeNomals(model));
                        options.GetLegacyBlendShapeNomals(model);
                    }
                }
            }
        }

        private bool canExecute()
        {
            return fbxFiles.Count > 0 && !targets.All(c => c == false);
        }
    }
}