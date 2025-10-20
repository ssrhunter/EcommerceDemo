
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private HttpClient _http;
        private Uri baseAddress = new Uri("https://localhost:7192/", UriKind.Absolute);

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task GetCategories()
        {
            var requestURL = new Uri(baseAddress, "api/category");
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>(requestURL);
            if (result != null && result.Data != null)
            {
                Categories = result.Data;
            }
        }
    }
}
