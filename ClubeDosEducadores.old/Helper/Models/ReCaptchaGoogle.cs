namespace Helper.Models
{
    public class ReCaptchaGoogle
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
