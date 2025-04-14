namespace ResunetDAL.Interfaces;

public interface IUserTokenDAL
{
    Task<Guid> Create(int userId);
    
    Task<int?> Get(Guid tokenId);
    
    void AddSecure(string rememberMeCookieName, string toString, int rememberMeDays);
}
