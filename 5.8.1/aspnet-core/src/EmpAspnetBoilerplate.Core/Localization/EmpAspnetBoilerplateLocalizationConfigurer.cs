using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace EmpAspnetBoilerplate.Localization
{
    public static class EmpAspnetBoilerplateLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(EmpAspnetBoilerplateConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(EmpAspnetBoilerplateLocalizationConfigurer).GetAssembly(),
                        "EmpAspnetBoilerplate.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
