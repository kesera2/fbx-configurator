﻿namespace kesera2.FBXOptionsManager
{
    public static class Toolbar
    {
        public enum TOOLBAR : byte
        {
            ENABLE,
            DISABLE
        }
        public static string[] TOOLBAR_LABLE = { Localization.lang.toolbarEnable, Localization.lang.toolbarDisable };
    }
}