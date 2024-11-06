using UnityEditor;
using UnityEngine;
namespace kesera2.FBXConfigurator
{
    public readonly struct DisabledColorScope : System.IDisposable
    {
        private readonly Color _originalColor;

        public DisabledColorScope(Color color, bool enable)
        {
            _originalColor = GUI.color;
            if (!enable) return;
            EditorGUI.BeginDisabledGroup(true);
            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = _originalColor;
            EditorGUI.EndDisabledGroup();
        }
    }
}
