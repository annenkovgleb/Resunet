namespace ResunetDAL.Interfaces;

public interface IUserToken
{
    Task<Guid> Create(int userId);
    
    Task<int?> Get(Guid tokenId);
}
