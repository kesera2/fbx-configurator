using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionOptimizer
{
    public class FBXOptionOptimizer : EditorWindow
    {
        private const string TOOL_NAME = "FBX Option Optimizer";
        private Vector2 _scrollPosition = Vector2.zero;         // スクロール位置

        private List<string> fbxFiles;
        private bool processAllFBXFiles = true;
        private bool targetFoldOut = false;
        private bool optionFoldOut = false;
        // Options
        private string folderPath = Application.dataPath;
        private bool importCameras = false;
        private bool importLights = false;
        private bool isReadable = true;
        private ModelImporterNormals importNormals = ModelImporterNormals.Import;
        private ModelImporterNormals importBlendShapeNormals = ModelImporterNormals.None;
        private bool legacyBlendShapeNomals = false;
        private bool[] targets = { };

        [MenuItem("Tools/" + TOOL_NAME)]
        public static void ShowWindow()
        {
            GetWindow<FBXOptionOptimizer>(TOOL_NAME);
        }

        private void OnEnable()
        {
            RefreshFBXFileList();
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
            showDebug();
            EditorGUILayout.EndScrollView();
        }

        private void showSelectTargets()
        {
            targetFoldOut = EditorGUILayout.Foldout(targetFoldOut, "FBXファイル");
            // 初期化されていない場合
            if (targets == null || targets.Length != fbxFiles.Count)
            {
                targets = new bool[fbxFiles.Count];
                for (int i = 0; i < fbxFiles.Count; i++)
                {
                    targets[i] = true; // デフォルトはチェック
                }
            }
            if (targetFoldOut)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    processAllFBXFiles = EditorGUILayout.ToggleLeft("全てを対象にする", processAllFBXFiles);
                    if (processAllFBXFiles)
                    {
                        FBXOptionOptimizerUtility.toggleArrayChecks(targets, true);
                    }
                }
                using (new EditorGUI.DisabledGroupScope(processAllFBXFiles))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button("全てにチェックを入れる"))
                        {
                            FBXOptionOptimizerUtility.toggleArrayChecks(targets, true);
                        }
                        if (GUILayout.Button("全てのチェックを外す"))
                        {
                            FBXOptionOptimizerUtility.toggleArrayChecks(targets, false);
                        }
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
                            modelImporter.importCameras = importCameras;
                            modelImporter.importLights = importLights;
                            modelImporter.isReadable = isReadable;
                            modelImporter.importNormals = importNormals;
                            modelImporter.importBlendShapeNormals = importBlendShapeNormals;
                            setLegacyBlendShapeNomals(modelImporter, legacyBlendShapeNomals);
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
                            importCameras = EditorGUILayout.Toggle(importCameras, GUILayout.Width(20));
                            importLights = EditorGUILayout.Toggle(importLights);
                            isReadable = EditorGUILayout.Toggle(isReadable);
                            importNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup(importNormals);
                            importBlendShapeNormals = (ModelImporterNormals)EditorGUILayout.EnumPopup(importBlendShapeNormals);
                            legacyBlendShapeNomals = EditorGUILayout.Toggle(legacyBlendShapeNomals, GUILayout.ExpandWidth(true));
                        }
                    }
                }
                EditorGUILayout.HelpBox("通常の場合、オプションを変更する必要はありません。", MessageType.Info);
            }
        }

        private void RefreshFBXFileList()
        {
            string projectPath = Path.GetDirectoryName(Application.dataPath);
            string relativePath = Path.GetRelativePath(projectPath, folderPath);
            fbxFiles = GetFBXFiles(relativePath);
        }

        private List<string> GetFBXFiles(string folderPath)
        {
            List<string> fbxFiles = new List<string>();
            string[] files = Directory.GetFiles(folderPath, "*.fbx", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                fbxFiles.Add(file);
            }
            return fbxFiles;
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