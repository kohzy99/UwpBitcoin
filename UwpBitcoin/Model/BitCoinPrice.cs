using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
//using Windows.Web.Http;

namespace UwpBitcoin.Model
{
    /// <summary>
    /// BitFlyerのAPIからTicker情報取得するModelクラス
    /// </summary>
    public class BitCoinPrice
    {
        // HttpClientの生成
        private static HttpClient httpClient = new HttpClient();

        // ネットワークの接続状態

        // APIエンドポイントURI
        const string EndPointUri = "https://api.bitflyer.jp/v1/ticker";

        // デストラクタ
        ~BitCoinPrice()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }

        /// <summary>
        /// BTCJPYのTicker情報を非同期に取得します
        /// </summary>
        /// <returns>取得したTicker情報</returns>
        public async Task<List<Ticker>> GetTickerAsync()
        {
            // 返却するリスト
            var result = new List<Ticker>();

            // Marketsリストに保持されている通貨をすべて取得する
            foreach (var market in Market.Markets)
            {
                // Ticker情報を格納する変数
                Ticker ticker = new Ticker();

                // API用クエリの作成
                var builder = new UriBuilder(EndPointUri);
                builder.Port = -1;
                builder.Query = string.Format("product_code={0}", market.Product_code);

                try
                {
                    // GET
                    var response = await httpClient.GetStringAsync(builder.ToString());

                    //Windows.Web.Http 用の実装
                    //var response = await httpClient.GetStringAsync(new Uri(builder.ToString()));

                    // Jsonのパース
                    ticker = JsonConvert.DeserializeObject<Ticker>(response);
                    ticker.IsError = false;

                }
                catch (Exception ex)
                {
                    ticker.IsError = true;
                    ticker.ErrorMessage = ex.Message;
                    //throw;
                }
                finally
                {

                    System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
                    // 追加情報を埋め込む
                    ticker.DisplayName = market.DisplayName;
                    ticker.ChartUrl = market.ChartUrl;
                    ticker.ImageFile = market.ImageFile;
                    ticker.LastPrice = ticker.ltp.ToString("C",ci);

                    // 1件追加
                    result.Add(ticker);
                }

            }

            // 返却
            return result;

        }
    }

}
