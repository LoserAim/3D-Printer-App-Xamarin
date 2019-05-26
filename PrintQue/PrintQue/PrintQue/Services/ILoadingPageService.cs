using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrintQue.Services
{
    public interface ILoadingPageService
    {
        void InitLoadingPage
              (ContentPage loadingIndicatorPage = null);

        void ShowLoadingPage();

        void HideLoadingPage();
    }
}
