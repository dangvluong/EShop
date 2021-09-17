//Chart for top selling products
$.post('/dashboard/statistic/GetBestSellingProducts', d => {
    var lab = new Array(d.length);
    var dat = new Array(d.length);
    for (var i in d) {
        lab[i] = d[i]['name'];
        dat[i] = d[i]['total'];
    }
    new Chart(topBestSellingProduct, {
        type: 'bar',
        data: {
            labels: lab,
            datasets: [{
                label: "Doanh thu ",
                backgroundColor: "rgba(2,117,216,0.5)",
                borderColor: "rgba(2,117,216,0.5)",
                data: dat,
            }],
        },
        options: {
            scales: {
                xAxes: {
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 6
                    }
                },
                yAxes: {
                    ticks: {
                        min: 0,
                        max: 15000,
                        maxTicksLimit: 5
                    },
                    gridLines: {
                        display: true
                    }
                },
            },
            legend: {
                display: false
            }
        }
    });

});
//Chart for revenue ratio by size
$.post('/dashboard/statistic/GetRevenueRatioBySize', (d) => {
    var lab = new Array(d.length);
    var dat = new Array(d.length);
    for (var i in d) {
        lab[i] = d[i]['name'];
        dat[i] = d[i]['ratio'];
    }
    new Chart(revenueRatioBySize, {
        type: 'pie',
        data: {
            labels: lab,
            datasets: [{
                data: dat,
                backgroundColor: ['#007bff', '#dc3545', '#ffc107', '#28a745', 'red', 'pink'],
            }],
        },
    });
});
//Chart for revenue by months
$.post('/dashboard/statistic/GetRenvenueByMonths', d => {
    var lab = new Array(d.length);
    var dat = new Array(d.length);
    for (var i in d) {
        lab[i] = d[i]['id'];
        dat[i] = d[i]['total'];
    }
    new Chart(revenueByMoths, {
        type: 'bar',
        data: {
            labels: lab,
            datasets: [{
                label: "Doanh thu ",
                backgroundColor: "rgba(2,117,216,0.5)",
                borderColor: "rgba(2,117,216,0.5)",
                data: dat,
            }],
        },
        options: {
            scales: {
                xAxes: [{
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 6
                    }
                }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 15000,
                        maxTicksLimit: 5
                    },
                    gridLines: {
                        display: true
                    }
                }],
            },
            legend: {
                display: false
            }
        }
    });

});
//chart for top 5 highest inventory products
$.post('/dashboard/statistic/GetTop5HighestInventoryProducts', d => {
    var lab = new Array(d.length);
    var dat = new Array(d.length);
    for (var i in d) {
        lab[i] = d[i]['name'];
        dat[i] = d[i]['total'];
    }
    var highestInventoryChart = new Chart(topHighestInventoryProducts, {
        type: 'bar',
        data: {
            labels: lab,
            datasets: [{
                label: "Số lượng tồn kho ",
                backgroundColor: "rgba(2,117,216,0.5)",
                borderColor: "rgba(2,117,216,0.5)",
                data: dat,
            }],
        },
        options: {
            scales: {
                xAxes: {
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 6
                    }
                },
                yAxes: {
                    ticks: {
                        min: 0,
                        max: 15000,
                        maxTicksLimit: 5
                    },
                    gridLines: {
                        display: true
                    }
                },
            },
            legend: {
                display: false
            }
        }
    });   
});
//Chart for revenue ratio by color
$.post('/dashboard/statistic/GetRevenueRatioByColor', (d) => {
    var lab = new Array(d.length);
    var dat = new Array(d.length);
    for (var i in d) {
        lab[i] = d[i]['name'];
        dat[i] = d[i]['ratio'];
    }
    new Chart(revenueRatioByColor, {
        type: 'pie',
        data: {
            labels: lab,
            datasets: [{
                data: dat,
                backgroundColor: ['#007bff', '#dc3545', '#ffc107', '#28a745', 'red', 'pink'],
            }],
        },
    });
});