using CutMe.Communication;
using CutMe.Exceptions;
using CutMe.Storage.Repositories;

namespace CutMe.Services;

public interface IRedirectService
{
    Task<string> GetFullUrlAsync(string shortcut);
    Task SetRedirectUrlAsync(SetRedirectRequest request);
}

public class RedirectService : IRedirectService
{
    private readonly IRedirectRepository _redirectRepository;

    public RedirectService(IRedirectRepository redirectRepository)
    {
        _redirectRepository = redirectRepository;
    }
    
    public async Task<string> GetFullUrlAsync(string shortcut)
    {
        if (string.IsNullOrWhiteSpace(shortcut))
        {
            throw new ArgumentException($"Shortcut can not be empty");
        }

        var redirectInformation = await _redirectRepository.GetFullUrlAsync(shortcut);
        if (redirectInformation is null)
        {
            throw new ResourceNotFoundException($"Resource with shortcut {shortcut} not found.");
        }

        return redirectInformation.FullUrl;
    }

    public async Task SetRedirectUrlAsync(SetRedirectRequest request)
    {
        if (!request.IsValid())
        {
            throw new ArgumentException("Invalid request.");
        }
        
        var redirectInformation = await _redirectRepository.GetFullUrlAsync(request.Shortcut);
        if (redirectInformation != null)
        {
            throw new ConflictException($"Resource with shortcut {request.Shortcut} is exist.");
        }

        await _redirectRepository.SetRedirectAsync(request.Shortcut, request.FullUrl);
    }
}