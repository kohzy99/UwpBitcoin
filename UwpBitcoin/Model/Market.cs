using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpBitcoin.Model
{
    /// <summary>
    /// Ticker情報を取得するマーケットのリストを保持するクラス
    /// </summary>
    public class Market
    {
        public string Product_code { get; set; }
        public string DisplayName { get; set; }
        public string ChartUrl { get; set; }
        public string ImageFile { get; set; }

        private static List<Market> markets = new List<Market>()
        {
            new Market() { Product_code = "BTC_JPY", DisplayName = "BTC", ChartUrl="https://cryptowat.ch/bitflyer/btcjpy", ImageFile="../Resources/opengraph.png" },
            new Market() { Product_code = "FX_BTC_JPY", DisplayName = "BTC-FX", ChartUrl="https://cryptowat.ch/bitflyer/btcfxjpy", ImageFile="../Resources/opengraph.png" },
        };

        public static List<Market> Markets
        {
            get { return markets; }
            //set { markets = value; }
        }
    }
}
