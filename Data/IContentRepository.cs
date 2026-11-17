using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Data;

public interface IContentRepository
{
    IReadOnlyList<TransportService> GetServices();
    TransportService? GetServiceById(int id);
    IReadOnlyList<ProcessStep> GetProcessSteps();
}
