namespace Resunet.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetCurrentUserId();
    }
}

