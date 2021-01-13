using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.TextToSpeech;

namespace MaxWell.Services
{
    class VoiceService
    {
        public VoiceService()
        {
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            client.MaxResponseContentBufferSize = 10000000;
        }
        HttpClient client;
        private static VoiceService Instance;

        public static VoiceService getInstance()
        {
            if (Instance == null) Instance = new VoiceService();
            return Instance;
        }

        public async Task SpeakRu(string text)
        {


            var languagesRu = await CrossTextToSpeech.Current.GetInstalledLanguages();
            try
            {
                var languageRu = languagesRu.FirstOrDefault(l => l.Language == "ru-RU");
               await CrossTextToSpeech.Current.Speak(text, languageRu);
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);
            }

        }


        public async Task SpeakEn(string text)
        {
            var languagesEn = await CrossTextToSpeech.Current.GetInstalledLanguages();
            try
            {
                var languageEn = languagesEn.FirstOrDefault(l => l.Language == "en-US");
               await  CrossTextToSpeech.Current.Speak(text, languageEn);
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);
            }

        }
    }
}
