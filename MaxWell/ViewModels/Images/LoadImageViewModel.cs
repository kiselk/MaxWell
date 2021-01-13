﻿using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using MaxWell.Models;
using Xamarin.Forms;

using MaxWell.Shared;

namespace MaxWell.ViewModels.Images
{
    public class LoadImageViewModel : BaseViewModel
    {
        #region Constant Fields
        const int _downloadImageTimeoutInSeconds = 15;
        readonly Lazy<HttpClient> _clientHolder = new Lazy<HttpClient>(() => CreateHttpClient(TimeSpan.FromSeconds(10)));
        #endregion

        #region Fields
        bool _isImageDownloading, _isImageVisible, _isLoadImageButtonEnabled;
        string _imageUrlEntryText = @"https://blobstoragesampleapp.blob.core.windows.net/photos/Punday";
        string _downloadImageButtonText;
        ImageSource _downloadedImageSource;
        ICommand _loadImageButtonTapped, _clearImageButtonCommand, _initializeViewModelCommand;
        List<Models.RemoteImage> _imageDatabaseModelList;
        #endregion

        #region Constructors
        public LoadImageViewModel() => InitializeViewModelCommand?.Execute(null);
        #endregion

        #region Events
        public event EventHandler<string> ImageDownloadFailed;
        #endregion

        #region Properties
        public ICommand LoadImageButtonCommand => _loadImageButtonTapped ??
            (_loadImageButtonTapped = new Command(async () => await ExecuteLoadImageButtonCommand(DownloadImageButtonText, ImageUrlEntryText)));

        public ICommand ClearImageButtonCommand => _clearImageButtonCommand ??
            (_clearImageButtonCommand = new Command(ExecuteClearImageButtonCommand));

        public ICommand InitializeViewModelCommand => _initializeViewModelCommand ??
            (_initializeViewModelCommand = new Command(async () => await ExecuteInitializeViewModelCommand()));

        public bool IsImageDownloading
        {
            get => _isImageDownloading;
            set => SetProperty(ref _isImageDownloading, value);
        }

        public bool AreImageAndClearButtonVisible
        {
            get => _isImageVisible;
            set => SetProperty(ref _isImageVisible, value);
        }

        public string ImageUrlEntryText
        {
            get => _imageUrlEntryText;
            set => SetProperty(ref _imageUrlEntryText, value, async () => await UpdateDownloadButtonText(ImageUrlEntryText));
        }

        public ImageSource DownloadedImageSource
        {
            get => _downloadedImageSource;
            set => SetProperty(ref _downloadedImageSource, value);
        }

        public string DownloadImageButtonText
        {
            get => _downloadImageButtonText;
            set => SetProperty(ref _downloadImageButtonText, value);
        }

        public bool IsLoadImageButtonEnabled
        {
            get => _isLoadImageButtonEnabled;
            set => SetProperty(ref _isLoadImageButtonEnabled, value);
        }

        public List<Models.RemoteImage> DownloadedImageModelList
        {
            get => _imageDatabaseModelList;
            set => SetProperty(ref _imageDatabaseModelList, value);
        }

        HttpClient Client => _clientHolder.Value;
        #endregion

        #region Methods
        static HttpClient CreateHttpClient(TimeSpan timeout)
        {
            HttpClient client;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                    client = new HttpClient();
                    break;
                default:
                    client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip });
                    break;

            }
            client.Timeout = timeout;
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return client;
        }

        Task ExecuteInitializeViewModelCommand() => UpdateDownloadButtonText(ImageUrlEntryText);

        Task ExecuteLoadImageButtonCommand(string downloadImageButtonText, string imageUrlEntryText)
        {
            switch (downloadImageButtonText)
            {
                case LoadImageButtonTextConstants.LoadImageFromDatabaseButtonText:
                    return LoadImageFromDatabaseAsync(imageUrlEntryText);

                default:
                    return DownloadImage(imageUrlEntryText);
            }
        }

        void ExecuteClearImageButtonCommand()
        {
            AnalyticsServices.TrackTime(AnalyticsConstants.ClearButtonTapped);

            AreImageAndClearButtonVisible = false;
        }

        async Task UpdateDownloadButtonText(string imageUrlEntryText)
        {
            await RefreshDownloadedImageModelList().ConfigureAwait(false);

            if (IsUrInDatabase(imageUrlEntryText))
                DownloadImageButtonText = LoadImageButtonTextConstants.LoadImageFromDatabaseButtonText;
            else
                DownloadImageButtonText = LoadImageButtonTextConstants.DownloadImageFromUrlButtonText;

            IsLoadImageButtonEnabled = true;
        }

        async Task LoadImageFromDatabaseAsync(string imageUrl)
        {
         //   AnalyticsServices.Track(AnalyticsConstants.LoadImageFromDatabase, new Dictionary<string, string>
        //    {
        //        { AnalyticsConstants.ImageUrl, imageUrl }
        //    });

            var downloadedImageModel = await App.RemoteImageManager.GetTaskAsync(1).ConfigureAwait(false);

            DownloadedImageSource = downloadedImageModel.DownloadedImageAsImageStream;

            AreImageAndClearButtonVisible = true;
        }

        async Task DownloadImage(string imageUrl)
        {
            if (!imageUrl.Contains("https"))
            {
                OnImageDownloadFailed("URL must use https");
                return;
            }

            byte[] downloadedImage;

            SetIsImageDownloading(true);

            try
            {
                using (var httpResponse = await Client.GetAsync(imageUrl).ConfigureAwait(false))
                {
                    if (httpResponse.StatusCode is HttpStatusCode.OK)
                    {
                        downloadedImage = await httpResponse.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                        var downloadedImageModel = new Models.RemoteImage
                        {
                            //ImageUrl = imageUrl,
                            DownloadedImageBlob = downloadedImage
                        };

                        await App.RemoteImageManager.SaveTaskAsync(downloadedImageModel).ConfigureAwait(false);

                        DownloadedImageSource = downloadedImageModel.DownloadedImageAsImageStream;
                        AreImageAndClearButtonVisible = true;

                        AnalyticsServices.TrackTime(AnalyticsConstants.DownloadImage, new Dictionary<string, string>
                        {
                            { AnalyticsConstants.ImageDownloadSuccessful, imageUrl }
                        });
                    }
                    else
                    {
                        AnalyticsServices.TrackTime(AnalyticsConstants.DownloadImage, new Dictionary<string, string>
                        {
                            { AnalyticsConstants.ImageDownloadFailed, imageUrl }
                        });
                        OnImageDownloadFailed("Invalid Url");
                    }
                }
            }
            catch (Exception e)
            {
                AnalyticsServices.TrackTime(AnalyticsConstants.DownloadImage, new Dictionary<string, string>
                {
                    { AnalyticsConstants.ImageDownloadFailed, imageUrl }
                });
                OnImageDownloadFailed(e.Message);
            }
            finally
            {
                SetIsImageDownloading(false);
                await UpdateDownloadButtonText(imageUrl);
            }
        }

        void SetIsImageDownloading(bool isImageDownloading)
        {
            IsImageDownloading = isImageDownloading;
            IsLoadImageButtonEnabled = !isImageDownloading;
        }

        bool IsUrInDatabase(string url) => false;//DownloadedImageModelList.Any(x => x.ImageUrl.ToUpper().Equals(url.ToUpper()));

        async Task RefreshDownloadedImageModelList() => DownloadedImageModelList = await App.Database2.GetAllDownloadedImagesAsync();

        void OnImageDownloadFailed(string failureMessage) => ImageDownloadFailed?.Invoke(this, failureMessage);
        #endregion
    }
}
