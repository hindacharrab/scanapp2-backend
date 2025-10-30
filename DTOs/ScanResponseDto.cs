namespace ScanApp2.DTOs
{

    public class ScanResponseDto
    {
        public bool Success { get; set; }      // true si le scan a été enregistré
        public string Message { get; set; }    // message à retourner à l'utilisateur
    }
}
