using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private Uri baseAddress = new Uri("https://localhost:7192/", UriKind.Absolute);
        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set; } = new List<Product>();

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var requestURL = new Uri(baseAddress, $"api/product/{productId}");
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>(requestURL);
            return result;
        }

        public async Task GetProducts()
        {
            var requestURL = new Uri(baseAddress, "api/product");
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>(requestURL);
            if (result != null && result.Data != null)
            {
                Products = result.Data;
            }
        }
    }
}
