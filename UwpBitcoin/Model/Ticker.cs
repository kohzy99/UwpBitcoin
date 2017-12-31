using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpBitcoin.Model
{
    /// <summary>
    /// 取得したTicker情報のJSONをデシリアライズした結果を格納するクラス
    /// </summary>
    public class Ticker
    {
        public string product_code { get; set; }
        public DateTime timestamp { get; set; }
        public int tick_id { get; set; }
        public float best_bid { get; set; }
        public float best_ask { get; set; }
        public float best_bid_size { get; set; }
        public float best_ask_size { get; set; }
        public float total_bid_depth { get; set; }
        public float total_ask_depth { get; set; }
        public float ltp { get; set; }
        public float volume { get; set; }
        public float volume_by_product { get; set; }
        // 追加プロパティ
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string DisplayName { get; set; }
        public string ChartUrl { get; set; }
        public string ImageFile { get; set; }
        public string LastPrice { get; set; }
    }
}
