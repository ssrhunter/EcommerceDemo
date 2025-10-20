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

        // Event trigger for refreshing the products list.
        public event Action ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var requestURL = new Uri(baseAddress, $"api/product/{productId}");
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>(requestURL);
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var requestURL = categoryUrl == null
                ? new Uri(baseAddress, "api/product")
                : new Uri(baseAddress, $"api/product/category/{categoryUrl}");
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>(requestURL);
            if (result != null && result.Data != null)
            {
                Products = result.Data;
            }

            // Tell every component that is subscribed to
            // ProductsChanged that a change was made.
            ProductsChanged.Invoke();
        }
       
    }
}
