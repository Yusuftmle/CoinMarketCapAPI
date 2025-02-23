using System.Text.Json;
using static CoinMarketCapWebApp.Infrastructure.Models.CryptoApiModels;

namespace CoinMarketCapWebApp.Infrastructure.Services
{
    public class CoinMarketCapService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "29e332df-7b57-4fbe-91cd-3c39e78d1160"; // API anahtarını buraya ekle.
        private const string ProxyUrl = "https://cors-anywhere.herokuapp.com/"; // CORS Anywhere Proxy URL
        public CoinMarketCapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/");
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        #region 1. Kripto Para Fiyatları ve Piyasa Verileri

        // Tüm kripto para listesi (örnek: ilk 10 kripto para)
        public async Task<List<CryptoInfo>> GetCryptoListingsAsync(int limit)
        {
            string endpoint = $"v1/cryptocurrency/listings/latest?start=1&limit={limit}&convert=USD";
            string fullUrl = ProxyUrl + _httpClient.BaseAddress + endpoint; // Proxy ile full URL oluşturuluyor
            var response = await _httpClient.GetAsync(fullUrl); // Proxy üzerinden istek yapılıyor

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API çağrısı başarısız oldu: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<CryptoRoot>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return root.Data;
        }

        #endregion

        #region 2. Belirli Kripto Para Bilgisi (Meta Veriler)

        // Belirli bir kripto paranın detaylı bilgisi (örnek: Bitcoin)
        public async Task<CryptoInfo> GetCryptoInfoBySymbolAsync(string symbol)
        {
            string endpoint = $"v1/cryptocurrency/info?symbol={symbol}";
            string fullUrl = ProxyUrl + _httpClient.BaseAddress + endpoint; // Proxy ile full URL oluşturuluyor

            var response = await _httpClient.GetAsync(fullUrl); // Proxy üzerinden istek yapılıyor

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API çağrısı başarısız oldu: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<Dictionary<string, CryptoInfo>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return root[symbol];
        }



        #endregion

        #region 3. Piyasa Trendleri

        // CMC100 endeksi (En iyi 100 kripto para)
        public async Task<List<CryptoInfo>> GetCMC100IndexAsync()
        {
            return await GetCryptoListingsAsync(100);
        }

        #endregion

        #region 4. Borsa Verileri

        // Merkezi borsa (CEX) listesi
        public async Task<List<ExchangeInfo>> GetExchangeListingsAsync()
        {
            string endpoint = $"v1/exchange/map?crypto_id=1";
            string fullUrl = ProxyUrl + _httpClient.BaseAddress + endpoint; // Proxy ile full URL oluşturuluyor

            var response = await _httpClient.GetAsync(fullUrl); // Proxy üzerinden istek yapılıyor

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API çağrısı başarısız oldu: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<ExchangeRoot>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return root.Data;
        }
        #endregion
        #region 5. Kripto Para Logoları
        public async Task<Dictionary<string, CryptoMeta>> GetCryptoLogosAsync(string[] symbols)
        {
            string endpoint = $"v1/cryptocurrency/info?symbol={string.Join(",", symbols)}";
            var fullUrl = ProxyUrl + _httpClient.BaseAddress + endpoint;
            var response = await _httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API çağrısı başarısız oldu: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<CryptoMetaRoot>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return root.Data;
        }


        #endregion


        #region 6. korku ac gozluluk Index
        public async Task<FearAndGreedLatest> GetFearAndGreedLatests()
        {
            string endpoint = $"v3/fear-and-greed/latest";
            string fullUrl = ProxyUrl + _httpClient.BaseAddress + endpoint;

            var response = await _httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"API çağrısı başarısız oldu: {response.StatusCode}");

            var jsonResponse = await response.Content.ReadAsStringAsync();



            var root = JsonSerializer.Deserialize<FearAndGearRoot>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // Gelen yanıtı logla
            Console.WriteLine("API Yanıtı:");
            Console.WriteLine(jsonResponse);
            return root.Data;
        }
        #endregion

        public async Task<List<Cryptocurrency>?> GetLatestCryptocurrencyListings()
        {
            string endpoint = $"v1/cryptocurrency/listings/latest";
            string fullUrl = ProxyUrl + _httpClient.BaseAddress + endpoint;

            var response = await _httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"API çağrısı başarısız oldu: {response.StatusCode}");

            var jsonResponse = await response.Content.ReadAsStringAsync();



            var root = JsonSerializer.Deserialize<CryptocurrencyListingResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // Gelen yanıtı logla
            Console.WriteLine("API Yanıtı:");
            Console.WriteLine(jsonResponse);
            return root.Data;
        }




    }

}