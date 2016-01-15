function SvgPanel(params) {

    'use strict';

    //[Meta]
    var self = this;
    self.SvgPanel = true;
    var parent = params.parent;
    var key = params.key + '_svg';

    //[UI]
    self.svg = self.svg;
    var controls = {};
    var size = {
        width: params.width,
        height: 1000
    };
    var position = {

    };

    //[Quotations].
    //var quotations = params.quotations;


    //[Initialize functions].
    function initialize() {
        loadControls();
        assignEvents();
        createRaphaelCanvas();
        testPrint();
    }

    function loadControls() {
        controls.container = $('<div/>', {
            'class': 'chart-svg-container',
            id: key
        }).css({
            'height': size.height + 'px',
            'width': size.width + 'px'
        }).appendTo(params.container);
    }

    function assignEvents() {

    }

    function createRaphaelCanvas() {
        self.svg = Raphael(key);
    }

    function testPrint() {
        var circle = self.svg.circle(50, 50, 10);
        circle.attr({
            'stroke': '#222',
            'stroke-width': 1,
            'fill': '#FFF'
        });
    }



    function reset() {
        //Kasuje properties i notowania.

    }


    initialize();


    //Public API.
    self.bind = function (e) {
        $(self).bind(e);
    }
    self.trigger = function (e) {
        $(self).trigger(e);
    }
    self.initialize = initialize;
    //self.loadProperties = loadProperties;
    //self.loadQuotations = loadQuotations;
    self.reset = reset;

}
