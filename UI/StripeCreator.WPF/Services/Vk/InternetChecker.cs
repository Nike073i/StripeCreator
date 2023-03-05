using System.Runtime.InteropServices;

namespace StripeCreator.WPF
{
    public class InternetChecker
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool IsConnectedToInternet() => InternetGetConnectedState(out _, 0);
    }
}
