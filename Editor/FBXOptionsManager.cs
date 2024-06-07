using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class FBXOptionsManager : EditorWindow
    {
        public const string TOOL_NAME = "FBX Options Manager";
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
        internal FbxOptions options = new FbxOptions();

        private bool[] targets = { };
        private int WINDOW_WIDTH = 500;
        private int WINDOW_HEIGHT = 150;

        [MenuItem("Tools/" + TOOL_NAME)]
        public static void ShowWindow()
        {
            GetWindow<FBXOptionsManager>(TOOL_NAME);
        }

        private void OnEnable()
        {
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
            EditorGUILayout.LabelField(TOOL_NAME);
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

        private void showSelectTargets()
        {
            targetFoldOut = EditorGUILayout.Foldout(targetFoldOut, "FBXファイル");

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
            if (GUILayout.Button("全てのチェックを外す"))
            {
                Utility.toggleArrayChecks(targets, false);
            }
        }

        private void CheckAllCheckboxes()
        {
            if (GUILayout.Button("全てにチェックを入れる"))
            {
                Utility.toggleArrayChecks(targets, true);
            }
        }

        private void containsAllFbx()
        {
            processAllFBXFiles = EditorGUILayout.ToggleLeft("全てを対象にする", processAllFBXFiles);
            if (processAllFBXFiles)
            {
                Utility.toggleArrayChecks(targets, true);
            }
        }

        private void showFolderPath()
        {
            GUILayoutOption[] options = { GUILayout.ExpandWidth(true) };
            EditorGUILayout.LabelField("選択中のフォルダ:");
            EditorGUILayout.LabelField(folderPath, EditorStyles.wordWrappedLabel, options);
            if (GUILayout.Button("Open"))
            {
                folderPath = EditorUtility.OpenFolderPanel("フォルダを選択", folderPath, string.Empty);
                RefreshFBXFileList();
            }
        }

        private void showExecute()
        {
            using (new EditorGUI.DisabledGroupScope(!canExecute()))
            {

                if (GUILayout.Button("実行"))
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
                            //modelImporter.SaveAndReimport();
                            //AssetDatabase.SaveAssets();
                            Debug.Log($"{fbxFile}のオプションを変更しました。");
                        }
                    }
                    Debug.Log($"{TOOL_NAME} : 実行が完了しました。");
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
                EditorGUILayout.HelpBox("通常の場合、オプションを変更する必要はありません。", MessageType.Info);
            }
        }
        private void RefreshFBXFileList()
        {

#if UNITY_2019_4_31
            // Assetsからの相対パスを取得
            relativePath = folderPath.Substring(folderPath.IndexOf("Assets/"));
#elif UNITY_2019_4_OR_NEWER
            relativePath = Path.GetRelativePath(projectPath, folderPath);
#endif
            fbxFiles = Utility.GetFBXFiles(relativePath);
        }



        private void showHelp()
        {
            EditorGUILayout.HelpBox("Assets配下の全てのFBXのオプションを推奨設定に変更します。", MessageType.Info);
        }

        private void showWarning()
        {
            if (!canExecute())
            {
                EditorGUILayout.HelpBox("対象のFBXが存在しません。", MessageType.Warning);
            }
        }

        private void showDebug()
        {
            if (GUILayout.Button("Debug"))
            {
                Debug.Log($"対象ファイル数: {fbxFiles.Count}");
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