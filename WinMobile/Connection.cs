using connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using WinMobile;

[assembly: Xamarin.Forms.Dependency(typeof(Connection))]
namespace WinMobile
{
    class Connection : IConnectivity
    {
        public bool NetworkConnected()
        {
            var rv = false;
            try
            {
                var InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                var profileInfo = GetConnectionProfile(InternetConnectionProfile);
                if (profileInfo.Contains("None"))
                    rv = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception thrown : {0}:{1}", ex.Message, ex.InnerException);
                rv = false;
            }
            return rv;
        }

        string GetConnectionProfile(ConnectionProfile connectionProfile)
        {
            string connectionProfileInfo = string.Empty;
            if (connectionProfile != null)
            {
                connectionProfileInfo = "Profile Name : " + connectionProfile.ProfileName + "\n";

                switch (connectionProfile.GetNetworkConnectivityLevel())
                {
                    case NetworkConnectivityLevel.None:
                        connectionProfileInfo += "Connectivity Level : None\n";
                        break;
                    case NetworkConnectivityLevel.LocalAccess:
                        connectionProfileInfo += "Connectivity Level : Local Access\n";
                        break;
                    case NetworkConnectivityLevel.ConstrainedInternetAccess:
                        connectionProfileInfo += "Connectivity Level : Constrained Internet Access\n";
                        break;
                    case NetworkConnectivityLevel.InternetAccess:
                        connectionProfileInfo += "Connectivity Level : Internet Access\n";
                        break;
                }

                switch (connectionProfile.GetDomainConnectivityLevel())
                {
                    case DomainConnectivityLevel.None:
                        connectionProfileInfo += "Domain Connectivity Level : None\n";
                        break;
                    case DomainConnectivityLevel.Unauthenticated:
                        connectionProfileInfo += "Domain Connectivity Level : Unauthenticated\n";
                        break;
                    case DomainConnectivityLevel.Authenticated:
                        connectionProfileInfo += "Domain Connectivity Level : Authenticated\n";
                        break;
                }

            }
            return connectionProfileInfo;
        }
    }
}
