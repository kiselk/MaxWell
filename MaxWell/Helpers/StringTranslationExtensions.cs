using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using FFImageLoading;
using MaxWell.Resources;

namespace MaxWell.Helpers
{
    public static class StringTranslationExtensions
    {
        /// <summary>
        /// Translate the text automatically
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Translate(this string text)
        {
            if (text != null)
            {
                var assembly = typeof(AppResources).GetTypeInfo().Assembly;
                var assemblyName = assembly.GetName();
                ResourceManager resourceManager = new ResourceManager($"MaxWell.Resources.AppResources", assembly);
                var lg = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                return resourceManager.GetString(text, new CultureInfo(lg));
            }

            return null;
        }
    }
}
