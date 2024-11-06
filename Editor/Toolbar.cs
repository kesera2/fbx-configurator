namespace kesera2.FBXConfigurator
{
    public class Toolbar
    {
        public enum ToolbarState : byte
        {
            Enable,
            Disable
        }

        public readonly string[] ToolbarLabels;

        public Toolbar()
        {
            ToolbarLabels = new[] { Localization.Lang.toolbarEnable, Localization.Lang.toolbarDisable };
        }
    }
}