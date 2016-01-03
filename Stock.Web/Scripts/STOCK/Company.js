function CompanyManager() {

    'use strict';

    var items = mielk.hashTable(null);


    function loadCompany(id) {
        var objects = mielk.db.fetch('Company', 'GetCompany', { 'id': id }, {});
        var company = new Company(objects);
        items.setItem(company.id, company);
        return company;
    }


    function getList() {
        var data = mielk.hashTable(null);
        mielk.db.fetch('Company', 'FilterCompanies', {
            q: '',
            limit: 100
        }, {
            async: false,
            callback: function (r) {
                data = r.items;
            }
        });

        return data;

    }

    function getAll() {
        return items.values();
    }

    function getCompany(id) {
        return items.getItem(id) || loadCompany(id);
    }


    return {
        getAll: getAll,
        getCompany: getCompany,
        getList: getList
    };

};




/*
 * Company object used in whole application.
 */
function Company(params) {
    var self = this;
    this.Company = true;

    //[Company data].
    this.id = params.Id || 0;
    this.name = params.Name || '';
    this.idMarket = params.IdMarket || 0;
    this.symbol = params.Short || params.Name;
    this.isFx = params.IsFx || false;
    this.displayed = this.name + (this.isFx ? '' : ' (' + this.symbol + ')');

    //[Status & Timestamp variables].
    this.lastUpdate = params.LastPriceUpdate;
    this.lastCalculation = params.LastCalculation;
    this.lastTrendlinesReview = params.LastTrendlinesReview;
    this.pricesChecked = params.PricesChecked;

    //[Quotations].
    this.dataSets = {};

}

Company.prototype = {
    bind: function(e){
        $(this).bind(e);
    },
    trigger: function (e) {
        $(this).trigger(e);
    },
    toString: function () {
        return this.name + ' (' + this.id + ')';
    },
    getDataSet: function (timeband) {
        //Create reference to this [Company] object.
        var self = this;

        //Check if the sub-object for this timeband already exists.
        var dataSet = self.dataSets[timeband.symbol];

        //If [dataSet] for this timeband doesn't exist, 
        //create it, add to the collection of data sets...
        if (!dataSet) {
            dataSet = new QuotationSet({ company: self, timeband: timeband });
            self.dataSets[timeband.symbol] = dataSet;
        }

        return dataSet;

    }
};


function QuotationSet(params) {
    //[Meta].
    var self = this;
    self.QuotationSet = true;
    var company = params.company;
    var timeband = params.timeband;

    //[Loading status].
    var quotationsLoaded = false;
    var propertiesLoaded = false;

    //[Properties].
    var quotations = {};
    var firstDate;
    var lastDate;
    var minLevel;
    var maxLevel;
    var actualQuotationsCounter;
    var realQuotationsCounter;
    

    function loadQuotations(fn, counter, force) {

        if (!propertiesLoaded) loadProperties(null);

        if (!quotationsLoaded || force) {

            mielk.db.fetch(
                'Company',
                'GetFxQuotations',
                { pairSymbol: company.symbol, timeband: timeband.symbol, count: $.isNumeric(counter) ? counter : 0 },
                {

                    async: true,
                    callback: function (res) {

                        //Populate quotations collection.
                        assignQuotations(res);

                        //If function has been passed as a parameter, call it.
                        if (mielk.objects.isFunction(fn)) {
                            fn(quotations);
                        }

                    }
                }
            );

        }

    }

    function assignQuotations(data) {
        for (var i = 0; i < data.length; i++) {
            var date = mielk.dates.fromCSharpDateTime(data[i].Date);
            var dataItem = quotations[date];

            //There are some quotations in the database with Saturday or Sunday date.
            //For such quotations the operation above will return null, since there are no
            //items in [quotations] object with Saturday or SUnday date as a key.
            if (dataItem) {
                var quotation = new Quotation(data[i]);
                dataItem.quotation = quotation;
            }

        }
    }



    //Funkcja pobierająca właściwości dla danego timebandu z bazy danych.
    function loadProperties(fn) {

        var properties = null;

        mielk.db.fetch(
            'Company',
            'GetDataSetProperties',
            { pairSymbol: company.symbol, timeband: timeband.symbol },
            {
                async: true,
                callback: function (res) {
                    firstDate = mielk.dates.fromCSharpDateTime(res.firstDate);
                    lastDate = mielk.dates.fromCSharpDateTime(res.lastDate);
                    minLevel = res.minPrice;
                    maxLevel = res.maxPrice;
                    actualQuotationsCounter = res.counter;
                    
                    //Create object for quotations (with slot for each date
                    //between [firstDate] and [lastDate].
                    createQuotationsObject();
                    realQuotationsCounter = Object.keys(quotations).length;

                    //Flag this data set as having properties already loaded.
                    propertiesLoaded = true;

                    //Create [properties] object to be returns.
                    properties = {
                        firstDate: firstDate,       //The date of the first quotation
                        lastDate: lastDate,         //The date of the last quotation
                        minLevel: minLevel,         //The minimum level of the price
                        maxLevel: maxLevel,         //The maximum level of the price
                        actualQuotationsCounter: actualQuotationsCounter,   //The number of quotations in the database
                        realQuotationsCounter: realQuotationsCounter        //The expected number of quotations
                    };

                    //If function has been passed as a parameter, call it.
                    if (mielk.objects.isFunction(fn)) {
                        fn(properties);
                    }

                }
            }
        );

    }

    function createQuotationsObject() {

        //Check if properties are already loaded.
        //If not, this function cannot continue.
        //if (!propertiesLoaded) return;   -- commented out; this method is now invoked together with loading properties.

        //Initial values.
        quotations = {};
        var index = 0;
        var date = new Date(firstDate);

        while (date <= lastDate) {
            quotations[date] = {
                index: index++,
                date: mielk.dates.toString(date, true),
                quotation: null
            }

            //Calculate the next date, using method [next] of Timeband object.
            date = timeband.next(date);

        }

    }


    //Public API.
    self.loadQuotations = loadQuotations;
    self.loadProperties = loadProperties;

}



//Add the instance of company manager as an item of STOCK library.
$(function () {
    STOCK.COMPANIES = CompanyManager();
});

























//Company.prototype = {
//    toString: function () {
//        return this.name + ' (' + this.id + ')';
//    },
//    bind: function (e) {
//        $(this).bind(e);
//    },
//    trigger: function (e) {
//        $(this).trigger(e);
//    },
//    load: function (timeband, params) {
//        var self = this;
//        var results;
//        if (!params) params = {};

//        //Check if data are already loaded. If yes, there is no
//        //point to re-load them. In this case only callback 
//        //function is called.
//        if (this.checkData(timeband, params)) {
//            results = this.getQuotationsSet(timeband).getQuotations();
//            if (mielk.objects.isFunction(params.callback)) {
//                (params.callback)(results);
//            }
//            return;
//        }


//        //Otherwise, the data are loaded from the database.
//        results = mielk.db.fetch('Company',
//            self.isFx ? 'GetFxQuotations' : 'GetQuotations',
//            {
//                pairSymbol: self.isFx ? self.name : self.id,
//                timeband: self.isFx ? timeband.symbol : timeband.id,
//                count: params.count || 0
//            },
//            {
//                async: true,
//                callback: function (r) {
//                    var quotations = STOCK.QUOTATIONS.convertToQuotations(r);
//                    self.setQuotations(timeband, quotations);

//                    //Run additional callback functions if they are given.
//                    if (mielk.objects.isFunction(params.callback)) {
//                        (params.callback)(quotations);
//                    }

//                }
//            }
//        );
//    },
//    checkData: function (timeband, params) {
//        var set = this.getQuotationsSet(timeband);

//        if (!set) return false;

//        return set.containsData(params);

//    },
//    getQuotationsSet: function (timeband) {
//        if (this.quotations.hasOwnProperty(timeband.id)) {
//            return this.quotations[timeband.id];
//        } else {
//            return null;
//        }
//    },
//    setQuotations: function (timeband, quotations) {
//        var self = this;
//        var set = this.getQuotationsSet(timeband);

//        if (set) {

//            //If set already exists, it has to be checked if it
//            //already contains the given quotations.
//            set.appendQuotations(quotations);

//        } else {

//            //New quotations set is created ...
//            set = function (tb, q) {
//                var $timeband = tb;
//                var $quotations = q;

//                function startDate() {
//                    return $quotations[0].date;
//                }

//                function endDate() {
//                    return mielk.arrays.getLastItem($quotations).date;
//                }

//                function containsData(params) {
//                    if (!params) return false;

//                    //Sprawdza pod względem liczebności.
//                    var size = params.size || params.count || params.length || 0;
//                    if (size) {
//                        return $quotations.length >= size;
//                    }

//                    //Sprawdza zakres dat.
//                    var start = params.start || params.startDate || 0;
//                    var end = params.end || params.endDate || 0;

//                    return (startDate <= start && endDate >= end);

//                }

//                function appendQuotations($q) {

//                    //TODO zmienić implementację - zakresy dat mogą na siebie nachodzić w różny sposób - 
//                    //niekoniecznie jeden będzie się całkowicie zawierał w drugim.

//                    var start = $q[0].date;
//                    var end = mielk.arrays.getLastItem($q).date;

//                    if (start < startDate || end > endDate) {
//                        quotations = $q;

//                        //Trigger an event of data reloading.
//                        self.trigger({ type: 'reloaded' });

//                    }

//                }

//                function getQuotations(params) {

//                    //TODO zmienić implementację, żeby uwzględniała różne zakresy dat
//                    //oraz umożliwiała pobranie wybranej ilości ostatnich notowań.

//                    return quotations;

//                }

//                return {
//                    getQuotations: getQuotations,
//                    containsData: containsData,
//                    appendQuotations: appendQuotations
//                };

//            }(timeband, quotations);

//            //... and added to the collection of quotations sets.
//            this.quotations[timeband.id] = set;

//            //Trigger an event of data reloading.
//            self.trigger({ type: 'reloaded' });

//        }

//    },
//    getQuotations: function (timeband) {
//        var set = this.getQuotationsSet(timeband);
//        return set ? set.getQuotations() : [];
//    }

//};