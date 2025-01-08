namespace MSCaddie.Client.Utils;

public interface IHttpResponseHelper
{
    /// <summary>
    /// Checks if response is successful and generates notification accordingly
    /// </summary>
    /// <returns>True if response is successful, false otherwise</returns>
    Task<bool> CheckErrorCodeAndNotify(HttpResponseMessage response, string errorTitle, string successMessage);
}

public class HttpResponseHelper : IHttpResponseHelper
{
    //private readonly INotificationService _notificationService;

    //public HttpResponseHelper(INotificationService notificationService)
    //{
    //    _notificationService = notificationService;
    //}
    
    public async Task<bool> CheckErrorCodeAndNotify(HttpResponseMessage response, string errorTitle, string successMessage)
    {
        if (response.IsSuccessStatusCode)
        {
            //await _notificationService.Success(successMessage);
            return true;
        };
        
        var msg = await response.Content.ReadAsStringAsync();
        //await _notificationService.Error(msg, $"{response.StatusCode} - {errorTitle}", options => options.IntervalBeforeClose = 10000);

        return false;
    }
}