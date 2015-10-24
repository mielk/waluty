function Quotation(params) {
    var self = this;
    self.Quotation = true;
    self.date = new Date(params.Date.year, params.Date.month - 1, params.Date.day);
    self.assetId = params.AssetId;
    self.open = params.Quotation.Open;
    self.high = params.Quotation.High;
    self.low = params.Quotation.Low;
    self.close = params.Quotation.Close;
    self.volume = params.Quotation.Volume;
    self.peakByClose = params.Price ? params.Price.PeakByClose : 0;
    self.peakByHigh = params.Price ? params.Price.PeakByHigh : 0;
    self.troughByClose = params.Price ? params.Price.TroughByClose : 0;
    self.troughByLow = params.Price ? params.Price.TroughByLow : 0;
}


function QuotationUI(quotation) {
    var self = this;
    self.QuotationUI = true;
    self.quotation = quotation;

}





function QuotationManager() {

    'use strict';

    var drawParams = {};

    function resetDrawParams() {
        drawParams = {

        };
    }

    function convertToQuotations(items) {
        var quotations = [];
        for (var i = 0; i < items.length; i++) {
            var quotation = new Quotation(items[i]);
            quotations.push(quotation);
        }

        return quotations;

    }

    function createUIObjects() {

    }


    function initialize() {
        resetDrawParams();
    }



    //Initializing functions.
    initialize();


    return {
        convertToQuotations: convertToQuotations,
        createUIObjects: createUIObjects
    };

}


//Add the instance of quotation manager as an item of STOCK library.
$(function () {
    STOCK.QUOTATIONS = QuotationManager();
});
