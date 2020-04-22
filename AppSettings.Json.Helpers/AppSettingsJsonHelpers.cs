using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppSettings.Json.Helpers
{
    /// <summary>
    /// Bu Class, appSettings.json dosyasi icerisinde olan Section'larin degerlerinin okunmasini saglamak amaciyla yazilmistir.
    /// </summary>
    public static class AppSettingsJsonHelpers
    {
        #region Private Static Properties
        private static string basePath;
        private static string defaultJsonFileName;
        #endregion Private Static Properties


        #region Constructor
        static AppSettingsJsonHelpers()
        {
            defaultJsonFileName = "appsettings.json";
            basePath = Directory.GetCurrentDirectory();
        }
        #endregion Constructor


        #region Private Static Functions
        private static IConfigurationRoot GenerateAppSettingsJsonRoot(string pathToDirectoryContainingJsonFile,
                                                                      string appSettingsJsonFileName = "appsettings.json")
        {
            appSettingsJsonFileName = (appSettingsJsonFileName ?? string.Empty).Trim();
            pathToDirectoryContainingJsonFile = (pathToDirectoryContainingJsonFile ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(value: appSettingsJsonFileName))
            {
                throw new ArgumentNullException(innerException: null,
                                                message: ".json filename to be read cannot be empty! Please entered a .json filename!");
            }
            if (string.IsNullOrEmpty(value: pathToDirectoryContainingJsonFile))
            {
                throw new ArgumentNullException(innerException: null,
                                                message: "The path to directory containing the .json file address cannot be empty! Please set a Directory!");
            }
            try
            {
                return new ConfigurationBuilder()
                    .SetBasePath(basePath: pathToDirectoryContainingJsonFile)
                    .AddJsonFile(optional: true,
                                 path: appSettingsJsonFileName)
                    .Build();
            }
            catch (Exception exceptionReason)
            {
                throw exceptionReason;
            }
        }


        private static bool CheckIfSectionNameExists(IConfigurationRoot configurationRoot, string sectionNameToCheck)
        {
            if (configurationRoot == null)
            {
                throw new ArgumentNullException(innerException: null,
                                                message: "ConfigurationRoot cannot be empty! Please set a ConfigurationRoot!");
            }

            sectionNameToCheck = (sectionNameToCheck ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(value: sectionNameToCheck))
            {
                throw new ArgumentNullException(innerException: null,
                                                message: "Section name to check cannot be empty! Please enter a Section name!");
            }

            return configurationRoot.GetSection(key: sectionNameToCheck)
                                    .Exists();

        }
        #endregion Private Static Functions


        #region Public Static Functions

        /// <summary>
        /// Farkli uygulamalar icerisinden, farkli isimlerde ki appsettings.json dosyalarina ait ConfigurationRoot 
        /// bilgilerini elde etmeye ve bu Root sayesinde icerisindeki Section'lara ait bilgileri okuyabilmeyi 
        /// saglayan fonksiyon
        /// </summary>
        /// <param name="pathToDirectoryContainingJsonFile">Root degerine ulasilmak istenilen appsettings.json dosyasinin bulundugu uygulamaya
        /// ait Full Path (Orn: Directory.GetCurrentDirectory(), AppContext.BaseDirectory vb.)</param>
        /// <param name="appSettingsJsonFileName">Icerisindeki Section'lara ait bilgilerin okunmak istenildigi Json dosyasina ait isim.
        /// (Orn: appsettings.json, json-of-test-project vb.)</param>
        /// <returns></returns>
        public static IConfigurationRoot FetchAppSettingsJsonRoot(string pathToDirectoryContainingJsonFile,
                                                                  string appSettingsJsonFileName = "appsettings.json")
        {
            appSettingsJsonFileName = (appSettingsJsonFileName ?? string.Empty).Trim();
            pathToDirectoryContainingJsonFile = (pathToDirectoryContainingJsonFile ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(value: appSettingsJsonFileName))
            {
                throw new ArgumentNullException(innerException: null,
                                                message: ".json filename to be read cannot be empty! Please entered a .json filename!");
            }
            if (string.IsNullOrEmpty(value: pathToDirectoryContainingJsonFile))
            {
                throw new ArgumentNullException(innerException: null,
                                                message: "The path to directory containing the .json file address cannot be empty! Please set a Directory!");
            }
            IConfigurationRoot configurationRoot = default;

            if (appSettingsJsonFileName != defaultJsonFileName ||
                pathToDirectoryContainingJsonFile != basePath)
            {
                basePath = pathToDirectoryContainingJsonFile;
                defaultJsonFileName = appSettingsJsonFileName;

                configurationRoot = GenerateAppSettingsJsonRoot(appSettingsJsonFileName: appSettingsJsonFileName,
                                                                pathToDirectoryContainingJsonFile: pathToDirectoryContainingJsonFile);
            }
            else
            {
                configurationRoot = GenerateAppSettingsJsonRoot(pathToDirectoryContainingJsonFile: basePath,
                                                                appSettingsJsonFileName: defaultJsonFileName);
            }
            return configurationRoot;
        }


        public static bool CheckIfTheSectionNameExistsInTheJsonFile(IConfigurationRoot configurationRoot, string sectionNameToCheck)
        {
            return CheckIfSectionNameExists(configurationRoot: configurationRoot,
                                            sectionNameToCheck: sectionNameToCheck);
        }

        #endregion Public Static Functions
    }
}
