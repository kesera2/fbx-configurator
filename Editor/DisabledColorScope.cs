using UnityEditor;
using UnityEngine;
namespace kesera2.FBXOptionsManager
{
    public struct DisabledColorScope : System.IDisposable
    {
        private Color originalColor;

        public DisabledColorScope(Color color, bool enable)
        {
            originalColor = GUI.color;
            if (enable)
            {
                EditorGUI.BeginDisabledGroup(true);
                GUI.color = color;
            }
        }

        public void Dispose()
        {
            GUI.color = originalColor;
            EditorGUI.EndDisabledGroup();
        }
    }
}
