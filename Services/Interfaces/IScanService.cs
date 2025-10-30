using ScanApp2.DTOs;

namespace ScanApp2.Services.Interfaces
{
    public interface IScanService
    {
        Task<ScanResponseDto> ProcessScanAsync(ScanInputDto scan);
        Task<IEnumerable<string>> GetAllScansAsync();

    }
}
