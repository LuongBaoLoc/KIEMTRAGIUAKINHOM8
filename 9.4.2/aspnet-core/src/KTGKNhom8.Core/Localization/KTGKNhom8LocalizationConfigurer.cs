using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace KTGKNhom8.Localization
{
    public static class KTGKNhom8LocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(KTGKNhom8Consts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(KTGKNhom8LocalizationConfigurer).GetAssembly(),
                        "KTGKNhom8.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
