using Microsoft.EntityFrameworkCore;

namespace EfCoreTut.Services;

public class MyAppService : IMyAppService
{
    private readonly DbContext _dbContext; 
    
    public MyAppService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRole()
    {
    }   
}