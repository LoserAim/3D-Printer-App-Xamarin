#define OFFLINE_SYNC_ENABLED

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Helper
{
    public class AzureAppServiceHelper
    {

        public static async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await App.MobileService.SyncContext.PushAsync();
                await App.requestsTable.PullAsync("AllRequest", "");
                await App.statusesTable.PullAsync("AllStatus", "");
                await App.printersTable.PullAsync("AllPrinter", "");
                await App.printColorsTable.PullAsync("AllPrintColor", "");
            }
            catch(MobileServicePushFailedException mspfe)
            {
                if (mspfe.PushResult != null)
                    syncErrors = mspfe.PushResult.Errors;
            }
            catch(Exception ex)
            {

            }

            if(syncErrors != null)
            {
                foreach(var error in syncErrors)
                {
                    if(error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }

                }
            }
        }

    }
}
