using System;
using System.Threading.Tasks;

namespace BasketManagement.WebApi.FunctionalTest.Extensions
{
    public static class TestHelper
    {
        public static void WaitForAsyncProcess()
        {
            WaitForAsyncProcessAsync()
               .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static async Task WaitForAsyncProcessAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}