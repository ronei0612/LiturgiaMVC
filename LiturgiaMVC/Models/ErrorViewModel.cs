namespace LiturgiaMVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? Mensagem { get; set; }
        public string? Titulo { get; set; }
    }
}