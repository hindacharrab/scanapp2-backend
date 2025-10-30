using ScanApp2.DTOs;
using ScanApp2.Services.Interfaces;

namespace ScanApp2.Services
{
    public class ScanService : IScanService
    {
        private readonly GoogleSheetsStorageService _storageService;
        private readonly IUserContextService _userContextService;

        public ScanService(GoogleSheetsStorageService storageService, IUserContextService userContextService)
        {
            _storageService = storageService;
            _userContextService = userContextService;
        }

        public async Task<ScanResponseDto> ProcessScanAsync(ScanInputDto scan)
        {
            if (scan == null || string.IsNullOrWhiteSpace(scan.NumeroBL))
                return new ScanResponseDto { Success = false, Message = "Numéro BL manquant" };

            var user = _userContextService.GetCurrentUser();
            var site = _userContextService.GetCurrentUserSite();

            await _storageService.AddScanAsync(scan, user, site);

            return new ScanResponseDto
            {
                Success = true,
                Message = "Scan enregistré avec succès"
            };
        }

        public async Task<IEnumerable<string>> GetAllScansAsync()
        {
            return await _storageService.GetScansAsync();
        }
    }
}
