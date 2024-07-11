﻿using Cody.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cody.Core.Settings
{
    internal class SettingsService : ISettingsService
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly ILog log;

        public SettingsService(ISettingsProvider settingsProvider, ILog log)
        {
            this.settingsProvider = settingsProvider;
            this.log = log;
        }

        private string GetOrDefault(string settingName, string defaultValue)
        {
            if (settingsProvider.SettingExists(settingName))
                return settingsProvider.GetSetting(settingName);

            return defaultValue;
        }

        private void Set(string settingName, string value)
        {
            settingsProvider.SetSetting(settingName, value);
            log.Info($"Value for the {settingName} setting has been changed.");
        }

        public string ServerEndpoint
        {
            get => GetOrDefault(nameof(ServerEndpoint), "https://sourcegraph.com/");
            set => Set(nameof(ServerEndpoint), value);
        }

        public string AccessToken
        {
            get
            {
                var token = Environment.GetEnvironmentVariable("SourcegraphCodyToken");
                if (token != null)
                {
                    log.Warn("You are using a access token from environment variables!");
                    return token;
                }

                return GetOrDefault(nameof(AccessToken), null);
            }
            set
            {
                Set(nameof(AccessToken), value);
            }
        }
    }
}
