using CutMe.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CutMe.Storage.Repositories;

public interface IRedirectRepository
{
    Task<RedirectInformation?> GetFullUrlAsync(string shortcut);
    Task SetRedirectAsync(string shortcut, string fullUrl);
}

public class RedirectRepository : IRedirectRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly RedirectionDbContext _dbContext;

    public RedirectRepository(IMemoryCache memoryCache, RedirectionDbContext dbContext)
    {
        _memoryCache = memoryCache;
        _dbContext = dbContext;
    }
    
    public async Task<RedirectInformation?> GetFullUrlAsync(string shortcut)
    {
        RedirectInformation? redirectInformation = null;
        
        if (_memoryCache.TryGetValue(shortcut, out string fullUrl))
        {
            redirectInformation =  new RedirectInformation
            {
                Shortcut = shortcut,
                FullUrl = fullUrl
            };
        }

        return redirectInformation;
    }

    public async Task SetRedirectAsync(string shortcut, string fullUrl)
    {
        await _dbContext.AddAsync(new RedirectInformation
        {
            FullUrl = fullUrl,
            Shortcut = shortcut
        });

        _memoryCache.Set(shortcut, fullUrl);
    }
}