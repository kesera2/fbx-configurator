namespace kesera2.FBXOptionsManager
{
    public static class Toolbar
    {
        public enum ToolbarState : byte
        {
            Enable,
            Disable
        }
        public static readonly string[] ToolbarLabels = { Localization.lang.toolbarEnable, Localization.lang.toolbarDisable };
    }
}