using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    internal class Option<T>
    {
        private class OptionT
        {
            public T value;
        }
        private T _value { get; set; }
        private int _toolbarEnable = 0;
        private string _label;
        private string _tooltip;
        private string _fieldName;
        private const int OPTION_WIDTH = 350;
        private const float INTERVAL_WIDTH = 10;
        private static GUILayoutOption[] optionsWidth = { GUILayout.Width(OPTION_WIDTH) };
        internal Option(T value, int toolbarEnable, string label, string fieldName, string tooltip = "")
        {
            _value = value;
            _toolbarEnable = toolbarEnable;
            _label = label;
            _fieldName = fieldName;
            _tooltip = tooltip;
        }
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public int ToolbarEnable
        {
            get { return _toolbarEnable; }
            set { _toolbarEnable = value; }
        }
        public string Label
        {
            get { return _label; }
        }
        public string Tooltip
        {
            get { return _tooltip; }
        }
        // Enable/Disableを切り替える共通部品
        private static int drawToggleEnableToolbar(int currentSelection)
        {
            return GUILayout.Toolbar(currentSelection, Toolbar.TOOLBAR_LABLE);
        }
        public static void showOption(Option<float> option)
        {
            ShowTOption(option);
        }
        public static void showOption(Option<bool> option)
        {
            ShowTOption(option);
        }
        public static void showOption(Option<MeshOptimizationFlags> option)
        {
            ShowTOption(option);
        }
        public static void showOption(Option<ModelImporterMeshCompression> option)
        {
            ShowTOption(option);
        }
        public static void showOption(Option<ModelImporterTangents> option)
        {
            ShowTOption(option);
        }
        public static void showOption(Option<ModelImporterNormalCalculationMode> option)
        {
            ShowTOption(option);
        }
        public static void showOption(Option<ModelImporterNormals> option)
        {
            ShowTOption(option);
        }
        public static void showOption(Option<ModelImporterIndexFormat> option)
        {
            ShowTOption(option);
        }
        public static void ShowTOption<T>(Option<T> option)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    bool isDisabled = option._toolbarEnable == (int)Toolbar.TOOLBAR.DISABLE;
                    using (new DisabledColorScope(Color.gray, isDisabled))
                    {
                        if (isDisabled)
                        {
                            EditorGUILayout.LabelField(new GUIContent(option.Label, option.Tooltip), optionsWidth);
                        }
                        else
                        {
                            UpdateOptionValue(option);
                        }
                    }
                    GUILayout.Space(INTERVAL_WIDTH);
                    option.ToolbarEnable = drawToggleEnableToolbar(option.ToolbarEnable);
                }
            }
        }
        private static void UpdateOptionValue<T>(Option<T> option)
        {
            if (typeof(T) == typeof(float))
            {
                option.Value = (T)(object)EditorGUILayout.FloatField(new GUIContent(option.Label, option.Tooltip), (float)(object)option._value, optionsWidth);
            }
            else if (typeof(T) == typeof(bool))
            {
                option.Value = (T)(object)EditorGUILayout.Toggle(new GUIContent(option.Label, option.Tooltip), (bool)(object)option._value, optionsWidth);
            }
            else if (typeof(T) == typeof(ModelImporterMeshCompression))
            {
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip), (ModelImporterMeshCompression)(object)option.Value, optionsWidth);
            }
            else if (typeof(T) == typeof(MeshOptimizationFlags))
            {
                option.Value = (T)(object)EditorGUILayout.EnumFlagsField(new GUIContent(option.Label, option.Tooltip), (MeshOptimizationFlags)(object)option.Value, optionsWidth);
            }
            else if (typeof(T) == typeof(ModelImporterIndexFormat))
            {
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip), (ModelImporterIndexFormat)(object)option.Value, optionsWidth);
            }
            else if (typeof(T) == typeof(ModelImporterNormals))
            {
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip), (ModelImporterNormals)(object)option.Value, optionsWidth);
            }
            else if (typeof(T) == typeof(ModelImporterNormalCalculationMode))
            {
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip), (ModelImporterNormalCalculationMode)(object)option.Value, optionsWidth);
            }
            else if (typeof(T) == typeof(ModelImporterTangents))
            {
                option.Value = (T)(object)EditorGUILayout.EnumPopup(new GUIContent(option.Label, option.Tooltip), (ModelImporterTangents)(object)option.Value, optionsWidth);
            }
        }

        public void Update(ModelImporter modelImporter)
        {
            var propertyInfo = modelImporter.GetType().GetProperty(_fieldName);
            if (modelImporter == null || propertyInfo == null) return;
            if (_toolbarEnable == (int)Toolbar.TOOLBAR.ENABLE)
            {
                propertyInfo.SetValue(modelImporter, _value);
            }
        }
    }
}
