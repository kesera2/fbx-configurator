using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class FBXOptionOptimizer : EditorWindow
{
    private const string TOOL_NAME = "FBX Option Optimizer";
    private Vector2 _scrollPosition = Vector2.zero;         // �X�N���[���ʒu

    private List<string> fbxFiles;
    private bool processAllFBXFiles = true;
    private bool targetFoldOut = false;
    private bool optionFoldOut = false;
    // Options
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
        showDebug();
        showExecute();
        showFolderPath();
        showSelectTargets();
        showTargetList();
        showOptionFoldOut();
        EditorGUILayout.EndScrollView();
    }

    private void showSelectTargets()
    {
        targetFoldOut = EditorGUILayout.Foldout(targetFoldOut, "FBX�t�@�C��");
        // ����������Ă��Ȃ��ꍇ
        if (targets == null || targets.Length != fbxFiles.Count)
        {
            targets = new bool[fbxFiles.Count];
            for (int i = 0; i < fbxFiles.Count; i++)
            {
                targets[i] = true; // �f�t�H���g�̓`�F�b�N
            }
        }
        if (targetFoldOut)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                processAllFBXFiles = EditorGUILayout.ToggleLeft("�S�Ă�Ώۂɂ���", processAllFBXFiles);


            }
            using (new EditorGUI.DisabledGroupScope(processAllFBXFiles))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("�S�ĂɃ`�F�b�N������"))
                    {
                        for (int i = 0; i < targets.Length; i++)
                        {
                            targets[i] = true;
                        }
                    }
                    if (GUILayout.Button("�S�Ẵ`�F�b�N���O��"))
                    {
                        for (int i = 0; i < targets.Length; i++)
                        {
                            targets[i] = false;
                        }
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
        EditorUtility.OpenFolderPanel("/Assets");
    }

    private void showExecute()
    {
        if (GUILayout.Button("���s"))
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
                }
            }
            Debug.Log(string.Format("{0} : ���s���������܂����B", TOOL_NAME));
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
            EditorGUILayout.HelpBox("�ʏ�̏ꍇ�A�I�v�V������ύX����K�v�͂���܂���B", MessageType.Info);
        }
    }

    private void RefreshFBXFileList()
    {
        fbxFiles = GetFBXFiles("Assets/");
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
        EditorGUILayout.HelpBox("Assets�z���̑S�Ă�FBX�̃I�v�V�����𐄏��ݒ�ɕύX���܂��B", MessageType.Info);
    }

    private void showDebug()
    {
        if (GUILayout.Button("Debug"))
        {
            foreach (string fbx in fbxFiles)
            {
                string folderPath = Path.GetDirectoryName(fbx);
                ModelImporter model = AssetImporter.GetAtPath(fbx) as ModelImporter;
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
}