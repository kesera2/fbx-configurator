using System.IO;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class Localization : ScriptableSingleton<Localization>
    {
        private static readonly string LangAssetFolderPath = "Language";
        private static readonly string AssetNameJa = "JA";
        private static readonly string AssetNameEn = "EN";
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
                default:
                    Lang = Resources.Load<LanguageHash>(LangAssetFolderPath + AssetNameJa);
                    break;
            }
        }

        public static void Localize()
        {
            switch ((SelectedLanguage)FBXOptionsManagerView.SelectedLanguage)
            {
                case SelectedLanguage.Jp:
                    Lang = Resources.Load<LanguageHash>(Path.Combine(LangAssetFolderPath, AssetNameJa));
                    break;
                case SelectedLanguage.En:
                    Lang = Resources.Load<LanguageHash>(Path.Combine(LangAssetFolderPath, AssetNameEn));
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