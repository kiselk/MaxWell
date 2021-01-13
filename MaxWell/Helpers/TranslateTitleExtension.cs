using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using Plugin.Multilingual;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Helpers
{
    [ContentProperty("Title")]
    public class TranslateTitleExtension : IMarkupExtension
    {
        const string ResourceId = "MaxWell.Resources.AppResources";

        static readonly Lazy<ResourceManager> resmgr =
            new Lazy<ResourceManager>(() =>
                new ResourceManager(ResourceId, typeof(TranslateTitleExtension)
                    .GetTypeInfo().Assembly));

        public string Title { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Title == null)
                return "";

            var ci = CrossMultilingual.Current.DeviceCultureInfo;
            var translation = resmgr.Value.GetString(Title, ci);

            if (translation == null)
            {
                translation = Title; // returns the key, which GETS DISPLAYED TO THE USER
            }
            return translation;
        }
    }
}
