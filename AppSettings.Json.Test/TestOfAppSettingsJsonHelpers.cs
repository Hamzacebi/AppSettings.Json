using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using AppSettings.Json.Helpers;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AppSettings.Json.Test
{
    public class TestOfAppSettingsJsonHelpers
    {
        #region Private Properties
        private IConfigurationRoot ConfigurationRoot;
        #endregion Private Properties

        public TestOfAppSettingsJsonHelpers()
        {
            ConfigurationRoot = AppSettingsJsonHelpers.FetchAppSettingsJsonRoot(appSettingsJsonFileName: "json-of-test-project",
                                                                                pathToDirectoryContainingJsonFile: AppContext.BaseDirectory);
        }

        [Fact]
        public void FetchAppSettingsJsonRootByNullAppSettingsJsonFileName()
        {
            var configurationRoot = AppSettingsJsonHelpers.FetchAppSettingsJsonRoot(appSettingsJsonFileName: null,
                                                                                    pathToDirectoryContainingJsonFile: Directory.GetCurrentDirectory());
            Assert.Null(@object: configurationRoot);
        }


        [Fact]
        public void FetchAppSettingsJsonRootByNullDirectoryValue()
        {
            var configurationRoot = AppSettingsJsonHelpers.FetchAppSettingsJsonRoot(pathToDirectoryContainingJsonFile: null);
            Assert.Null(@object: configurationRoot);
        }

    }
}
