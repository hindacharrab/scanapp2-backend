 
using ScanApp2.DTOs;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace ScanApp2.Services
{
    public class GoogleSheetsStorageService
    {
        private readonly SheetsService _service;
        private readonly string _spreadsheetId = "1-0Rl8o5LTJcv26ttAia_Z7bJYWJVPheyxYkiz1CID4g"; // remplace par ton ID

        public GoogleSheetsStorageService()
        {
            var credential = GoogleCredential.FromFile("Credentials/service-account.json")
     .CreateScoped(SheetsService.Scope.Spreadsheets);

            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "ScanApp"
            });
        }

        public async Task AddScanAsync(ScanInputDto scan, string user, string site)
        {
            var range = "scans!A:D"; // adapte si nécessaire
            var valueRange = new ValueRange
            {
                Values = new List<IList<object>>
                {
                    new List<object>

                    {
                        scan.NumeroBL,
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                        user ?? "Utilisateur inconnu",
                        site ?? "Site non défini"
                    }
                }
            };

            var appendRequest = _service.Spreadsheets.Values.Append(valueRange, _spreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
            await appendRequest.ExecuteAsync();
        }

        public async Task<IEnumerable<string>> GetScansAsync()
        {
            var range = "scans!A:A"; // ici on récupère juste le NuméroBL
            var request = _service.Spreadsheets.Values.Get(_spreadsheetId, range);
            var response = await request.ExecuteAsync();

            var result = new List<string>();
            if (response.Values != null)
            {
                foreach (var row in response.Values)
                {
                    if (row.Count > 0)
                        result.Add(row[0].ToString());
                }
            }
            return result;
        }
    }
}
