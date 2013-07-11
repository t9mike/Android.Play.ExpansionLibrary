using System;
using LicenseVerificationLibrary.Policy;

namespace LicenseVerificationLibrary
{
	public class OnlineOnlyPolicy : IPolicy
	{
        private PolicyServerResponse? Last_Response = null;

        public void ProcessServerResponse(PolicyServerResponse response, ResponseData rawData)
        {
            Last_Response = response;
        }

        public bool BeforeServerCheckAllowAccess()
        {
            // Do not cache responses
            return false;
        }

        public bool AfterServerCheckAllowAccess()
        {
            // Should not really be possible to be null given this is called after 
            // ProcessServerConnectionSuccess() or ProcessServerConnectionError() is called
            return Last_Response.GetValueOrDefault(PolicyServerResponse.Retry) != PolicyServerResponse.NotLicensed;
        }

        public void ProcessServerConnectionSuccess(PolicyServerResponse response, ResponseData rawData)
        {
            Last_Response = response;
        }

        public void ProcessServerConnectionError(PolicyServerResponse response, ResponseData rawData)
        {
            Last_Response = PolicyServerResponse.Retry;
        }
	}
}

