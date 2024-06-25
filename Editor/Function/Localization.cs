using kesera2.FBXOptionsManager;
using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class Localization : ScriptableSingleton<Localization>
    {
        public static LanguageHash lang { get; private set; }

        private readonly static string LANG_ASSET_FOLDER_PATH = "Language/";
        private readonly static string JP_ASSET_NAME = "JP";
        private readonly static string EN_ASSET_NAME = "EN";
        internal static string[] languages = new string[] { "“ú–{Œê", "English" };
        private enum SelectedLanguage { JP, EN };

        public void OnEnable()
        {
            if (FBXOptionsManagerView.selectedLanguage == (int)SelectedLanguage.JP)
            {
                lang = Resources.Load<LanguageHash>(LANG_ASSET_FOLDER_PATH + JP_ASSET_NAME);
            }
            else
            {
                lang = Resources.Load<LanguageHash>(LANG_ASSET_FOLDER_PATH + EN_ASSET_NAME);
            }

        }

        public static void Localize()
        {
            if (FBXOptionsManagerView.selectedLanguage == (int)SelectedLanguage.JP)
            {
                lang = Resources.Load<LanguageHash>(LANG_ASSET_FOLDER_PATH + JP_ASSET_NAME);
            }
            else
            {
                lang = Resources.Load<LanguageHash>(LANG_ASSET_FOLDER_PATH + EN_ASSET_NAME);
            }
        }

    }
}