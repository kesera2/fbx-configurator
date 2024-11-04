using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class Localization : ScriptableSingleton<Localization>
    {
        private static readonly string LangAssetFolderPath = "Language/";
        private static readonly string AssetNameJa = "JP";
        private static readonly string AssetNameEn = "EN";
        internal static readonly string[] Languages = { "日本語", "English" };
        public static LanguageHash Lang { get; private set; }

        public void OnEnable()
        {
            if (FBXOptionsManagerView.SelectedLanguage == (int)SelectedLanguage.JP)
                Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameJa);
            else
                Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameEn);
        }

        public static void Localize()
        {
            if (FBXOptionsManagerView.SelectedLanguage == (int)SelectedLanguage.JP)
                Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameJa);
            else
                Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameEn);
        }

        private enum SelectedLanguage
        {
            JP,
            EN
        }
    }
}