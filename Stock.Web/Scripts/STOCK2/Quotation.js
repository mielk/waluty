function Quotation(params) {
    var self = this;
    self.Quotation = true;
    self.date = mielk.dates.fromCSharpDateTime(params.Date);
    self.assetId = params.AssetId;
    self.open = params.Quotation.Open;
    self.high = params.Quotation.High;
    self.low = params.Quotation.Low;
    self.close = params.Quotation.Close;
    self.volume = params.Quotation.Volume;
    self.priceGap = params.Price ? params.Price.PriceGap : 0;
    self.peakByClose = params.Price ? params.Price.PeakByClose : 0;
    self.peakByHigh = params.Price ? params.Price.PeakByHigh : 0;
    self.troughByClose = params.Price ? params.Price.TroughByClose : 0;
    self.troughByLow = params.Price ? params.Price.TroughByLow : 0;
    self.macd = (params.Macd ? new Macd(params.Macd) : null);
    
}

function Macd(params){

    'use strict';

    var self = this;
    self.Macd = true;
    self.id = params.MacdId;
    self.histogram = params.Histogram;
    self.signal = params.SignalLine;
    self.macd = params.MacdLine;

    self.strMacd = function () {
        return self.macd.toFixed(4);
    };
    self.strSignal = function () {
        return self.signal.toFixed(4);
    };
    self.strHistogram = function () {
        return self.histogram.toFixed(4);
    };

}
