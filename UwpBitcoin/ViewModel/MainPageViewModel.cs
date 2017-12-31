using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpBitcoin.Common;
using UwpBitcoin.Model;

namespace UwpBitcoin.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        #region コンストラクタ

        public MainPageViewModel()
        {
            this.WindowTitle = "ビットコイン価格チェッカー";
            SetPriceAsync();
        }

        #endregion

        #region プロパティ・フィールド

        /// <summary>
        /// Model の生成
        /// </summary>
        private BitCoinPrice bitCoinPrice = new BitCoinPrice();

        /// <summary>
        /// MainWindowsのタイトル
        /// </summary>
        public string WindowTitle { get; set; }

        private List<Ticker> tickers;
        /// <summary>
        /// 取得したTicker情報をリスト形式で保持するプロパティ
        /// </summary>
        public List<Ticker> Tickers
        {
            get { return this.tickers; }
            set
            {
                this.tickers = value;

                // 先頭の時刻を抜き出してJSTに変換する
                //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
                this.UpdateTime = tickers.First().timestamp.ToLocalTime().ToString("G");

                this.RaisePropertyChanged(nameof(Tickers));
            }
        }

        private string updateTime;
        /// <summary>
        /// Tickerリストを更新した時間を保持するプロパティ
        /// </summary>
        public string UpdateTime
        {
            get { return this.updateTime; }
            set
            {
                this.updateTime = "Last Update: " + value;
                this.RaisePropertyChanged(nameof(UpdateTime));
            }
        }


        //private Ticker tickerBTC;
        ///// <summary>
        ///// Ticker情報（BTC)
        ///// </summary>
        //public Ticker TickerBTC
        //{
        //    get { return this.tickerBTC; }
        //    set
        //    {
        //        this.tickerBTC = value;
        //        this.RaisePropertyChanged(nameof(TickerBTC));
        //    }
        //}

        //private Ticker tickerBTCFX;
        ///// <summary>
        ///// Ticker情報（BTCFX)
        ///// </summary>
        //public Ticker TickerBTCFX
        //{
        //    get { return this.tickerBTCFX; }
        //    set
        //    {
        //        this.tickerBTCFX = value;
        //        this.RaisePropertyChanged(nameof(TickerBTCFX));
        //    }
        //}

        private DelegateCommand updateTicker;
        /// <summary>
        /// 更新ボタンのコマンドを公開するプロパティ
        /// </summary>
        public DelegateCommand UpdateTicker
        {
            get
            {
                if (this.updateTicker == null)
                {
                    this.updateTicker = new DelegateCommand(SetPriceAsync, CanUpdateTicker);
                }
                return updateTicker;
            }
        }

        private DelegateCommand appExit;
        /// <summary>
        /// Applicationを終了します
        /// </summary>
        public DelegateCommand AppExit
        {
            get
            {
                if (this.appExit == null)
                {
                    Action a = () =>
                    {
                        App.Current.Exit();
                    };
                    this.appExit = new DelegateCommand(a,CanUpdateTicker);
                }
                return appExit;
            }
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 生成したModelオブジェクトの非同期メソッドを使用してTicker情報を取得し、プロパティに値をセットします。
        /// </summary>
        private async void SetPriceAsync()
        {
            this.Tickers = await bitCoinPrice.GetTickerAsync();

            /*
            var BTC = await bitCoinPrice.GetTickerAsync(bitCoinPrice.Url_BTC);
            var BTCFX = await bitCoinPrice.GetTickerAsync(bitCoinPrice.Url_BTCFX);

            if (BTC.isError == false && BTCFX.isError == false)
            {
                // UTC->JST変換
                BTC.timestamp = BTC.timestamp.ToLocalTime();
                BTCFX.timestamp = BTCFX.timestamp.ToLocalTime();
                // 取得した情報をプロパティにセット
                TickerBTC = BTC;
                TickerBTCFX = BTCFX;
            }
            else
            {
                BTC.ltp = 0;
                TickerBTC = BTC;

                BTCFX.ltp = 0;
                TickerBTCFX = BTCFX;
            }
            */
        }

        /// <summary>
        /// 更新ボタンの実行可否の判定を行います。
        /// </summary>
        /// <returns></returns>
        private bool CanUpdateTicker()
        {
            return true;
        }

        #endregion

    }
}
