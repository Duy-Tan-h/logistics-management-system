using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Services;

public class ContentService(IContentRepository repository) : IContentService
{
    public HomeViewModel GetHomeData() => new()
    {
        FeaturedServices = repository.GetServices().Take(3).ToList(),
        ProcessSteps = repository.GetProcessSteps()
    };

    public IReadOnlyList<TransportService> GetServices() => repository.GetServices();
    public TransportService? GetService(int id) => repository.GetServiceById(id);
    public IReadOnlyList<ProcessStep> GetProcess() => repository.GetProcessSteps();
}
