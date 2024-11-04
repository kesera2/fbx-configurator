using System.IO;
using UnityEditor;
using UnityEngine;

namespace kesera2.FBXOptionsManager
{
    public class Localization : ScriptableSingleton<Localization>
    {
        // 言語選択の列挙型
        public enum SelectedLanguage
        {
            Jp,
            En
        }

        // 日・英のアセット名
        private const string AssetNameJa = "JP";

        private const string AssetNameEn = "EN";

        // 言語アセットフォルダへのパス
        private const string LangAssetFolderPath = "Language";

        // サポートされている言語
        internal static readonly string[] Languages = { "日本語", "English" };

        // 現在選択されている言語のハッシュ
        public static LanguageHash Lang { get; private set; }

        public void OnEnable()
        {
            Lang = LoadLanguage((SelectedLanguage)FBXOptionsManagerView.SelectedLanguage);
        }

        public static void Localize()
        {
            Lang = LoadLanguage((SelectedLanguage)FBXOptionsManagerView.SelectedLanguage);
        }

        private static LanguageHash LoadLanguage(SelectedLanguage selectedLanguage)
        {
            return selectedLanguage switch
            {
                SelectedLanguage.Jp => Resources.Load<LanguageHash>(Path.Combine(LangAssetFolderPath, AssetNameJa)),
                SelectedLanguage.En => Resources.Load<LanguageHash>(Path.Combine(LangAssetFolderPath, AssetNameEn)),
                _ => Lang
            };
        }
    }
}