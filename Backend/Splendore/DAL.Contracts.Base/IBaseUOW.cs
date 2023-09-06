namespace DAL.Contracts.Base;

public interface IBaseUOW
{
    Task<int> SaveChangesAsync();
    
    // how to contain and create repositories
}