var Dashboard = (function () {

    //Chart 1
    var complaintsData = [
        { complaint: "Bilgisayarımı açamıyorum.", count: 780 },
        { complaint: "Mouse bozuldu", count: 120 },
        { complaint: "Batarya arızası", count: 52 },
        { complaint: "Sunucuya erişim sağlayamıyorum.", count: 1123 },
        { complaint: "Mail şifremle giriş yapamıyorum.", count: 321 },
        { complaint: "Bilgisayarımın formatlanması", count: 89 },
        { complaint: "Mail imzamın oluşturulması", count: 222 }
    ];

    var data = complaintsData.sort(function (a, b) {
        return b.count - a.count;
    }),
        totalCount = data.reduce(function (prevValue, item) {
            return prevValue + item.count;
        }, 0),
        cumulativeCount = 0,
        dataSource = data.map(function (item, index) {
            cumulativeCount += item.count;
            return {
                complaint: item.complaint,
                count: item.count,
                cumulativePercentage: Math.round(cumulativeCount * 100 / totalCount)
            };
        });

    //Chart 2
    var ticketData = [{
        arg: 2015,
        val: 19978
    }, {
        arg: 2016,
        val: 26306
    }, {
        arg: 2017,
        val: 31975
    }, {
        arg: 2018,
        val: 40078
    }, {
        arg: 2019,
        val: 48363
    }, {
        arg: 2020,
        val: 47261
    }, {
        arg: 2021,
        val: 57795
        }];

    var options = {
        startScaleValue: 0,
        endScaleValue: 35,
        tooltip: {
            customizeTooltip: function (arg) {
                return {
                    text: 'Cevap süresi: ' + arg.value + '<br>' + 'Ortalama: ' + arg.target
                };
            }
        }
    };

    var dataSourcePie = [{
        region: "MH1",
        val: 6293
    }, {
        region: "MH2",
        val: 6064
    }, {
        region: "MH3",
        val: 1520
    }, {
        region: "MH4",
        val: 6440
    }, {
        region: "MH5",
        val: 2222
    }, {
        region: "MH6",
        val: 756
    }, {
        region: "MHC",
        val: 7756
        }];

    function init() {
        $("#responsive-box").dxResponsiveBox({
            rows: [
                { ratio: 1 },
                { ratio: 2 },
                { ratio: 2, screen: "xs" },
                { ratio: 1 }
            ],
            cols: [
                { ratio: 1 },
                { ratio: 2, screen: "lg" },
                { ratio: 1 }
            ],
            singleColumnScreen: "sm",
            screenByWidth: function (width) {
                return (width < 700) ? 'sm' : 'lg';
            }
        });

        $("#chart1").dxChart({
            palette: "Harmony Light",
            dataSource: dataSource,
            title: "ASH - Arama Nedeni İstatistikleri",
            argumentAxis: {
                label: {
                    overlappingBehavior: "stagger"
                }
            },
            tooltip: {
                enabled: true,
                shared: true,
                customizeTooltip: function (info) {
                    var content = ["<div><div class='tooltip-header'></div>",
                        "<div class='tooltip-body'><div class='series-name'>",
                        "<span class='top-series-name'></span>",
                        ": </div><div class='value-text'>",
                        "<span class='top-series-value'></span>",
                        "</div><div class='series-name'>",
                        "<span class='bottom-series-name'></span>",
                        ": </div><div class='value-text'>",
                        "<span class='bottom-series-value'></span>",
                        "% </div></div></div>"].join("");

                    var htmlContent = $(content);

                    htmlContent.find(".tooltip-header").text(info.argumentText);
                    htmlContent.find(".top-series-name").text(info.points[0].seriesName);
                    htmlContent.find(".top-series-value").text(info.points[0].valueText);
                    htmlContent.find(".bottom-series-name").text(info.points[1].seriesName);
                    htmlContent.find(".bottom-series-value").text(info.points[1].valueText);

                    return {
                        html: $("<div>").append(htmlContent).html()
                    };
                }
            },
            valueAxis: [{
                name: "sıklık",
                position: "left",
                tickInterval: 300
            }, {
                name: "yüzde",
                position: "right",
                showZero: true,
                label: {
                    customizeText: function (info) {
                        return info.valueText + "%";
                    }
                },
                constantLines: [{
                    value: 80,
                    color: "#fc3535",
                    dashStyle: "dash",
                    width: 2,
                    label: { visible: false }
                }],
                tickInterval: 20,
                valueMarginsEnabled: false
            }],
            commonSeriesSettings: {
                argumentField: "complaint"
            },
            series: [{
                type: "bar",
                valueField: "count",
                axis: "sıklık",
                name: "Arama nedeni sıklığı",
                color: "#fac29a"
            }, {
                type: "spline",
                valueField: "cumulativePercentage",
                axis: "yüzde",
                name: "Kümülatif yüzde",
                color: "#6b71c3"
            }],
            legend: {
                verticalAlignment: "top",
                horizontalAlignment: "center"
            }
        });

        $("#chart2").dxChart({
            dataSource: ticketData,
            legend: {
                visible: false
            },
            series: {
                type: "bar"
            },
            argumentAxis: {
                tickInterval: 10,
                label: {
                    format: {
                        type: "decimal"
                    }
                }
            },
            title: "Yıllara Göre İş Emri Sayısı"
        });

        var junFirst = $.extend({ value: 23, target: 20, color: '#ebdd8f' }, options),
            julFirst = $.extend({ value: 27, target: 24, color: '#e8c267' }, options),
            augFirst = $.extend({ value: 20, target: 26, color: '#e55253' }, options);

        var junSecond = $.extend({ value: 24, target: 22, color: '#ebdd8f' }, options),
            julSecond = $.extend({ value: 28, target: 24, color: '#e8c267' }, options),
            augSecond = $.extend({ value: 30, target: 24, color: '#e55253' }, options);

        var junThird = $.extend({ value: 35, target: 24, color: '#ebdd8f' }, options),
            julThird = $.extend({ value: 24, target: 26, color: '#e8c267' }, options),
            augThird = $.extend({ value: 28, target: 22, color: '#e55253' }, options);

        var junFourth = $.extend({ value: 29, target: 25, color: '#ebdd8f' }, options),
            julFourth = $.extend({ value: 24, target: 27, color: '#e8c267' }, options),
            augFourth = $.extend({ value: 21, target: 21, color: '#e55253' }, options);

        $('.june-1').dxBullet(junFirst);
        $('.july-1').dxBullet(julFirst);
        $('.august-1').dxBullet(augFirst);

        $('.june-2').dxBullet(junSecond);
        $('.july-2').dxBullet(julSecond);
        $('.august-2').dxBullet(augSecond);

        $('.june-3').dxBullet(junThird);
        $('.july-3').dxBullet(julThird);
        $('.august-3').dxBullet(augThird);

        $('.june-4').dxBullet(junFourth);
        $('.july-4').dxBullet(julFourth);
        $('.august-4').dxBullet(augFourth);

        $("#pie").dxPieChart({
            type: "doughnut",
            palette: "Soft Pastel",
            dataSource: dataSourcePie,
            title: "ASH 2021 Kule Bazlı İş Emri Sayısı",
            tooltip: {
                enabled: true,
                customizeTooltip: function (arg) {
                    return {
                        text: arg.valueText + " - " + (arg.percent * 100).toFixed(2) + "%"
                    };
                }
            },
            legend: {
                horizontalAlignment: "right",
                verticalAlignment: "top",
                margin: 0
            },
            "export": {
                enabled: true
            },
            series: [{
                argumentField: "region",
                label: {
                    visible: true,
                    connector: {
                        visible: true
                    }
                }
            }]
        });
    };

    function dashboardLoad () {

    };

    return {
        init: init,
        dashboardLoad: dashboardLoad,
    }
})();

document.addEventListener("DOMContentLoaded", function documentReady() {
    this.removeEventListener("DOMContentLoaded", documentReady);
    Dashboard.init();
});
