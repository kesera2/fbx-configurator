using UnityEditor;
using UnityEngine;

namespace kesera2.FBXConfigurator
{
    internal class Option<T>
    {
        private const float IntervalWidth = 10;
        private const int OptionWidth = 350;
        private readonly GUILayoutOption[] _optionsWidth;
        internal readonly int DefaultSelected;
        internal readonly string FieldName;
        private Toolbar _toolbar;

        internal Option(T value, int toolbarEnable, string label, string fieldName, string tooltip = "")
        {
            Value = value;
            ToolbarEnable = toolbarEnable;
            DefaultSelected = toolbarEnable;
            Label = label;
            FieldName = fieldName;
            Tooltip = tooltip;
            _optionsWidth = new[] { GUILayout.Width(OptionWidth) };
        }

        public T Value { get; set; }


        public int ToolbarEnable { get; set; }

        public string Label { get; set; }

        public string Tooltip { get; }

        // Enable/Disableを切り替える共通部品
        private int DrawToggleEnableToolbar(int currentSelection)
        {
            _toolbar = new Toolbar();
            return GUILayout.Toolbar(currentSelection, _toolbar.ToolbarLabels);
        }

        public void ShowOption()
        {
            ShowTOption(this);
        }

        private void ShowTOption(Option<T> option)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                var isDisabled = option.ToolbarEnable == (int)Toolbar.ToolbarState.Disable;
                using (new DisabledColorScope(Color.gray, isDisabled))
                {
                    if (isDisabled)
                        EditorGUILayout.LabelField(new GUIContent(option.Label, option.Tooltip), _optionsWidth);
                    else
                        UpdateOptionValue(option);
                }

                GUILayout.Space(IntervalWidth);
                option.ToolbarEnable = DrawToggleEnableToolbar(option.ToolbarEnable);
            }
        }

        private static void UpdateOptionValue(Option<T> option)
        {
            if (typeof(T) == typeof(float))
                option.Value = (T)(object)EditorGUILayout.FloatField(new GUIContent(option.Label, option.Tooltip),
                    (float)(object)option.Value, option._optionsWidth);
            else if (typeof(T) == typeof(bool))
                option.Value = (T)(object)EditorGUILayout.Toggle(new GUIContent(option.Label, option.Tooltip),
                    (bool)(object)option.Value, option._optionsWidth);
            else if (typeof(T) == typeof(ModelImporterMeshCompression))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterMeshCompression)(object)option.Value, option._optionsWidth);
            else if (typeof(T) == typeof(MeshOptimizationFlags))
                option.Value = (T)(object)EditorGUILayout.EnumFlagsField(new GUIContent(option.Label, option.Tooltip),
                    (MeshOptimizationFlags)(object)option.Value, option._optionsWidth);
            else if (typeof(T) == typeof(ModelImporterIndexFormat))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterIndexFormat)(object)option.Value, option._optionsWidth);
            else if (typeof(T) == typeof(ModelImporterNormals))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterNormals)(object)option.Value, option._optionsWidth);
            else if (typeof(T) == typeof(ModelImporterNormalCalculationMode))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterNormalCalculationMode)(object)option.Value, option._optionsWidth);
            else if (typeof(T) == typeof(ModelImporterTangents))
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip),
                    (ModelImporterTangents)(object)option.Value, option._optionsWidth);
        }

        public void Update(ModelImporter modelImporter)
        {
            var propertyInfo = modelImporter.GetType().GetProperty(FieldName);
            if (!modelImporter || propertyInfo == null) return;
            // ツールバーがEnableの場合のみ変更
            if (ToolbarEnable == (int)Toolbar.ToolbarState.Enable) propertyInfo.SetValue(modelImporter, Value);
        }
    }
}