namespace LicenseVerificationLibrary.Policy
{
    /// <summary>
    /// The i policy.
    /// </summary>
    public interface IPolicy
    {
        #region Public Methods and Operators

        /// <summary>
        /// Before connecting to authentication service, this is called to see if we should skip the
        /// check and just allow access.
        /// </summary>
        /// <returns>
        /// The allow access.
        /// </returns>
        bool BeforeServerCheckAllowAccess();

        /// <summary>
        /// After ProcessServerConnectionSuccess() or ProcessServerConnectionError() have been called,
        /// this gets called to determine whether user should be allowed access.
        /// </summary>
        /// <returns>
        /// The allow access.
        /// </returns>
        bool AfterServerCheckAllowAccess();

        /// <summary>
        /// Called when LicenseChecker was able to connect to the license server. You typically
        /// do some interesting things with this data, and definately something that will be used
        /// with AfterServerCheckAllowAccess(), which will be called sometime after.
        /// 
        /// Provide results from contact with the license server. 
        /// Retry counts are incremented if the current value of response is 
        /// <see cref="PolicyServerResponse.Retry"/>. 
        /// Results will be used for any future policy decisions.
        /// </summary>
        /// <param name="response">
        /// The result from validating the server response
        /// </param>
        /// <param name="rawData">
        /// The raw server response data, can be null for 
        /// <see cref="PolicyServerResponse.Retry"/>
        /// </param>
        void ProcessServerConnectionSuccess(PolicyServerResponse response, ResponseData rawData);

        /// <summary>
        /// Similar to ProcessServerConnectionSuccess(), but called if there was an error connecting
        /// to the authentication service.
        /// </summary>
        void ProcessServerConnectionError(PolicyServerResponse response, ResponseData rawData);

        #endregion
    }
}