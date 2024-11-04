using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class Localization : ScriptableSingleton<Localization>
    {
        private const string AssetNameJa = "JP";
        private const string AssetNameEn = "EN";
        private const string LangAssetFolderPath = "Language/";
        internal static readonly string[] Languages = { "日本語", "English" };
        public static LanguageHash Lang { get; private set; }

        public void OnEnable()
        {
            Lang = (SelectedLanguage)FBXOptionsManagerView.SelectedLanguage switch
            {
                SelectedLanguage.Jp => Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameJa),
                SelectedLanguage.En => Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameEn),
                _ => Lang
            };
        }

        public static void Localize()
        {
            Lang = (SelectedLanguage)FBXOptionsManagerView.SelectedLanguage switch
            {
                SelectedLanguage.Jp => Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameJa),
                SelectedLanguage.En => Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameEn),
                _ => Lang
            };
        }

        private enum SelectedLanguage
        {
            Jp,
            En
        }
    }
}