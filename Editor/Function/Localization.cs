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
            switch ((SelectedLanguage)FBXOptionsManagerView.SelectedLanguage)
            {
                case SelectedLanguage.Jp:
                    Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameJa);
                    break;
                case SelectedLanguage.En:
                    Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameEn);
                    break;
            }
        }

        public static void Localize()
        {
            switch ((SelectedLanguage)FBXOptionsManagerView.SelectedLanguage)
            {
                case SelectedLanguage.Jp:
                    Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameJa);
                    break;
                case SelectedLanguage.En:
                    Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameEn);
                    break;
            }
        }

        private enum SelectedLanguage
        {
            Jp,
            En
        }
    }
}