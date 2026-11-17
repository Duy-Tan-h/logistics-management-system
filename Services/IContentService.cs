using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Services;

public interface IContentService
{
    HomeViewModel GetHomeData();
    IReadOnlyList<TransportService> GetServices();
    TransportService? GetService(int id);
    IReadOnlyList<ProcessStep> GetProcess();
}
