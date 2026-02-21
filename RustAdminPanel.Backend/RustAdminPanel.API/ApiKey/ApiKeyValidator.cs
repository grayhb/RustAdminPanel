namespace RustAdminPanel.API.ApiKey
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }

    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(string apiKey)
        {
            return apiKey == _configuration["ApiKey"];
        }
    }
}
