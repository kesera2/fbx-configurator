using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class FBXOptionsManager : EditorWindow
    {
        public const string TOOL_NAME = "FBX Options Manager";
        internal Vector2 _scrollPosition = Vector2.zero;         // スクロール位置

        internal string projectPath;
        internal string relativePath;
        internal List<string> fbxFiles;
        internal bool processAllFBXFiles = true;
        internal bool targetFoldOut = false;
        internal bool optionFoldOut = false;
        // Options
        internal string folderPath;
        FbxOptions options = new FbxOptions();

        internal bool[] targets = { };

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
            if (targets == null || targets.Length != fbxFiles.Count)
            {
                targets = new bool[fbxFiles.Count];
                Utility.toggleArrayChecks(targets, true);
            }
        }

        private void setWindowSize()
        {
            Vector2 vector = new Vector2(400, 150);
            this.minSize = vector;
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            EditorGUILayout.LabelField(TOOL_NAME);
            setWindowSize();
            showHelp();
            showFolderPath();
            showSelectTargets();
            showExecute();
            showWarning();
            showTargetList();
            showOptionFoldOut();
            showAdditionalOptionFoldOut();
            showDebug();
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
                    foreach (string fbxFile in fbxFiles)
                    {
                        ModelImporter modelImporter = AssetImporter.GetAtPath(fbxFile) as ModelImporter;
                        if (modelImporter != null)
                        {
                            modelImporter.importCameras = options.ImportCameras;
                            modelImporter.importLights = options.ImportLights;
                            modelImporter.isReadable = options.IsReadable;
                            modelImporter.importNormals = options.ImportNormals;
                            modelImporter.importBlendShapeNormals = options.ImportBlendShapeNormals;
                            setLegacyBlendShapeNomals(modelImporter, options.LegacyBlendShapeNomals);
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
                using (new EditorGUILayout.HorizontalScope())
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        using (new EditorGUILayout.VerticalScope())
                        {
                            EditorGUILayout.LabelField("Import Cameras");
                            EditorGUILayout.LabelField("Import Lights");
                            EditorGUILayout.LabelField("Read/Write");
                            EditorGUILayout.LabelField("Nomals");
                            EditorGUILayout.LabelField("Blend Shape Nomals");
                            EditorGUILayout.LabelField("Legacy BlendShape Nomals");
                        }
                        using (new EditorGUILayout.VerticalScope())
                        {
                            options.ImportCameras = EditorGUILayout.Toggle(options.ImportCameras, GUILayout.Width(20));
                            options.ImportLights = EditorGUILayout.Toggle(options.ImportLights);
                            options.IsReadable = EditorGUILayout.Toggle(options.IsReadable);
                            options.ImportNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup(options.ImportNormals);
                            options.ImportBlendShapeNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup(options.ImportBlendShapeNormals);
                            options.LegacyBlendShapeNomals = EditorGUILayout.Toggle(options.LegacyBlendShapeNomals, GUILayout.ExpandWidth(true));
                        }
                    }
                }
                EditorGUILayout.HelpBox("通常の場合、オプションを変更する必要はありません。", MessageType.Info);
            }
        }

        private void showAdditionalOptionFoldOut()
        {

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



        private PropertyInfo getLegacyBlendShapeNomalsProp(ModelImporter modelImporter)
        {
            return modelImporter.GetType().GetProperty("legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        private void setLegacyBlendShapeNomals(ModelImporter modelImporter, bool legacyBlendShapeNomals)
        {
            PropertyInfo prop = getLegacyBlendShapeNomalsProp(modelImporter);
            if (prop != null)
            {
                prop.SetValue(modelImporter, legacyBlendShapeNomals);
            }
        }

        private bool getLegacyBlendShapeNomals(ModelImporter modelImporter)
        {
            PropertyInfo prop = getLegacyBlendShapeNomalsProp(modelImporter);
            bool value = (bool)prop.GetValue(modelImporter);
            return value;

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
                foreach (string fbx in fbxFiles)
                {
                    string folderPath = Path.GetDirectoryName(fbx);
                    ModelImporter model = AssetImporter.GetAtPath(fbx) as ModelImporter;
                    Debug.Log("model is null : " + model == null + ", file path : " + fbx);
                    if (model != null)
                    {
                        Debug.Log(folderPath + fbx + " importCameras " + model.importCameras);
                        Debug.Log(folderPath + fbx + " importLights " + model.importLights);
                        Debug.Log(folderPath + fbx + " isReadable " + model.isReadable);
                        Debug.Log(folderPath + fbx + " importNormals " + model.importNormals);
                        Debug.Log(folderPath + fbx + " importBlendShapeNormals " + model.importBlendShapeNormals);
                        Debug.Log(folderPath + fbx + " LegacyBlendShapeNomals " + getLegacyBlendShapeNomals(model));
                        getLegacyBlendShapeNomals(model);
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