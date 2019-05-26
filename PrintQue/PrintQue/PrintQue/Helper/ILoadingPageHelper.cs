using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrintQue.Helper
{
    public interface ILoadingPageHelper
    {
        void InitLoadingPage
              (ContentPage loadingIndicatorPage = null);

        void ShowLoadingPage();

        void HideLoadingPage();
    }
}
