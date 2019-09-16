using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Settings
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string FirebaseAuthJson
        {
            get => AppSettings.GetValueOrDefault(nameof(FirebaseAuthJson), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(FirebaseAuthJson), value);
        }

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(nameof(AccessToken), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(AccessToken), value);
        }

        public static string Email
        {
            get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Email), value);
        }
        public static string UserId
        {
            get => AppSettings.GetValueOrDefault(nameof(UserId), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserId), value);
        }

        public static string Tipo
        {
            get => AppSettings.GetValueOrDefault(nameof(Tipo), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Tipo), value);
        }
    }
}
