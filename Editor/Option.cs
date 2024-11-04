﻿using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    internal class Option<T>
    {
        private const int OPTION_WIDTH = 350;
        private const float INTERVAL_WIDTH = 10;
        private static readonly GUILayoutOption[] optionsWidth = { GUILayout.Width(OPTION_WIDTH) };
        internal readonly int defaultSelected;
        internal string _fieldName;
        internal Toolbar _toolbar;

        internal Option(T value, int toolbarEnable, string label, string fieldName, string tooltip = "")
        {
            _value = value;
            ToolbarEnable = toolbarEnable;
            defaultSelected = toolbarEnable;
            Label = label;
            _fieldName = fieldName;
            Tooltip = tooltip;
        }

        private T _value { get; set; }

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public int ToolbarEnable { get; set; }

        public string Label { get; set; }

        public string Tooltip { get; }

        // Enable/Disableを切り替える共通部品
        private int drawToggleEnableToolbar(int currentSelection)
        {
            _toolbar = new Toolbar();
            return GUILayout.Toolbar(currentSelection, _toolbar.ToolbarLabels);
        }

        public void showOption()
        {
            ShowTOption(this);
        }

        public void showOption(Option<bool> option)
        {
            ShowTOption(option);
        }

        public void showOption(Option<MeshOptimizationFlags> option)
        {
            ShowTOption(option);
        }

        public void showOption(Option<ModelImporterMeshCompression> option)
        {
            ShowTOption(option);
        }

        public void showOption(Option<ModelImporterTangents> option)
        {
            ShowTOption(option);
        }

        public void showOption(Option<ModelImporterNormalCalculationMode> option)
        {
            ShowTOption(option);
        }

        public void showOption(Option<ModelImporterNormals> option)
        {
            ShowTOption(option);
        }

        public void showOption(Option<ModelImporterIndexFormat> option)
        {
            ShowTOption(option);
        }

        public void ShowTOption<T>(Option<T> option)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                var isDisabled = option.ToolbarEnable == (int)Toolbar.ToolbarState.Disable;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                        EditorGUILayout.LabelField(new GUIContent(option.Label, option.Tooltip), optionsWidth);
                    else
                        UpdateOptionValue(option);
                }

                GUILayout.Space(INTERVAL_WIDTH);
                var toolbar = new Toolbar();
                option.ToolbarEnable = drawToggleEnableToolbar(option.ToolbarEnable);
            }
        }

        private static void UpdateOptionValue<T>(Option<T> option)
        {
            if (typeof(T) == typeof(float))
                option.Value = (T)(object)EditorGUILayout.FloatField(new GUIContent(option.Label, option.Tooltip),
                    (float)(object)option._value, optionsWidth);
            else if (typeof(T) == typeof(bool))
                option.Value = (T)(object)EditorGUILayout.Toggle(new GUIContent(option.Label, option.Tooltip),
                    (bool)(object)option._value, optionsWidth);
            else if (typeof(T) == typeof(ModelImporterMeshCompression))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterMeshCompression)(object)option.Value, optionsWidth);
            else if (typeof(T) == typeof(MeshOptimizationFlags))
                option.Value = (T)(object)EditorGUILayout.EnumFlagsField(new GUIContent(option.Label, option.Tooltip),
                    (MeshOptimizationFlags)(object)option.Value, optionsWidth);
            else if (typeof(T) == typeof(ModelImporterIndexFormat))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterIndexFormat)(object)option.Value, optionsWidth);
            else if (typeof(T) == typeof(ModelImporterNormals))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterNormals)(object)option.Value, optionsWidth);
            else if (typeof(T) == typeof(ModelImporterNormalCalculationMode))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterNormalCalculationMode)(object)option.Value, optionsWidth);
            else if (typeof(T) == typeof(ModelImporterTangents))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterTangents)(object)option.Value, optionsWidth);
        }

        public void Update(ModelImporter modelImporter)
        {
            var propertyInfo = modelImporter.GetType().GetProperty(_fieldName);
            if (modelImporter == null || propertyInfo == null) return;
            // ツールバーがEnableの場合のみ変更
            if (ToolbarEnable == (int)Toolbar.ToolbarState.Enable) propertyInfo.SetValue(modelImporter, _value);
        }
    }
}