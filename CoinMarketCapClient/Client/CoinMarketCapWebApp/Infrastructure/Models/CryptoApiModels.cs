namespace CoinMarketCapWebApp.Infrastructure.Models
{
    public class CryptoApiModels
    {
        public class CryptoMetaRoot
        {
            public Dictionary<string, CryptoMeta> Data { get; set; }
        }

        public class CryptoMeta
        {
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Logo { get; set; }
            public string Description { get; set; }
        }



        #region Model Sınıfları

        // Kripto para model sınıfları
        public class CryptoRoot
        {
            public List<CryptoInfo> Data { get; set; }
        }

        public class CryptoInfo
        {
        
            public string Name { get; set; }
            public string Symbol { get; set; }
            public Quote Quote { get; set; }

            public decimal PriceUSD => Quote?.Usd?.Price ?? 0;
        }

        public class Quote
        {
            public Usd Usd { get; set; }
            public decimal Price { get; set; }
            public decimal Volume24h { get; set; }
            public decimal VolumeChange24h { get; set; }
            public decimal PercentChange1h { get; set; }
            public decimal percent_change_24h { get; set; }
            public decimal PercentChange7d { get; set; }
            public decimal market_cap { get; set; }
            public decimal MarketCapDominance { get; set; }
            public decimal FullyDilutedMarketCap { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        public class Usd
        {
            public decimal Price { get; set; }
        }

        // Borsa model sınıfları
        public class ExchangeRoot
        {
            public List<ExchangeInfo> Data { get; set; }
        }

        public class ExchangeInfo
        {
           
                public string ListingStatus { get; set; } = "active"; // Varsayılan olarak aktif borsaları getirir
                public string? Slug { get; set; } // Belirli borsaları filtrelemek için
                public int Start { get; set; } = 1; // Sayfalama için başlangıç
                public int Limit { get; set; } = 10; // Kaç sonuç alınacağını belirler (max 5000)
                public string Sort { get; set; } = "id"; // Varsayılan sıralama "id"
                public string? Aux { get; set; } // Ekstra bilgiler için
                public string? CryptoId { get; set; } // Belirli bir kripto para içeren borsaları filtrelemek için
            

        }

        #endregion

        public class FearAndGreedLatest
        {
            public int Value { get; set; }  // 0 ile 100 arasında bir değer (korku ve açgözlülük endeksi)
            public string value_classification { get; set; } // "Extreme Fear", "Fear", "Neutral", "Greed", "Extreme Greed"
            public string update_time { get; set; } // ISO 8601 formatında tarih
        }

        public class FearAndGearRoot
        {
            public FearAndGreedLatest Data { get; set; } // "data" objesi içinde tek bir obje var.
        }

        public class CryptocurrencyListingResponse
        {
            public Status? Status { get; set; }
            public List<Cryptocurrency>? Data { get; set; }
        }

        public class Status
        {
            public DateTime Timestamp { get; set; }
            public int ErrorCode { get; set; }
            public string? ErrorMessage { get; set; }
            public int Elapsed { get; set; }
            public int CreditCount { get; set; }
        }

        public class Cryptocurrency
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Symbol { get; set; }
            public string? Slug { get; set; }
            public int NumMarketPairs { get; set; }
            public DateTime DateAdded { get; set; }
            public List<string>? Tags { get; set; }
            public decimal? MaxSupply { get; set; }
            public decimal CirculatingSupply { get; set; }
            public decimal TotalSupply { get; set; }
            public int cmc_rank { get; set; }
            public DateTime LastUpdated { get; set; }
            public Dictionary<string, Quote>? Quote { get; set; }
        }

       


    }
}
