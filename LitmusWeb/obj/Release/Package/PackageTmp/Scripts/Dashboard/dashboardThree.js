let backGroundColorForChart;
$(document).ready(function () {
    // Get the height of the first chart
    var firstChartHeight = $('#chart1').height();

    // Set the height of the other charts
    $('#chart2, #chart3, #chart4').height(firstChartHeight);
});
async function fetchData(historyDay) {
    return new Promise((resolve, reject) => {
        let param = JSON.stringify({
            "user_code": "",
            "company_code": null,
            "season_code": 24,
            "history_days": historyDay
        });
        $.ajax({
            url: '/api/DashBoardApi/GetDashboardDaysummary',
            type: 'POST',
            contentType: "application/json",
            data: param,
            dataType: "json",
            success: (response) => {
                var units = response.map(u => u.unit_name);
                backGroundColorForChart = units.map(() => getRandomColor());
                console.log(backGroundColorForChart);
                resolve(response);
            },
            error: (error) => {
                reject(error)
            }
        });
    });
}
function createChart(data) {
    const unitNames = data.map(u => u.unit_name);
    const unitData = data.map(u => u.results);
    //backGroundColorForChart = unitNames.map(() => getRandomColor());

    var d = {
        labels: unitNames,
        datasets: [{
            data: unitData.map(r => r[0].value),
            backgroundColor: backGroundColorForChart,
        }]
    };

    var ctx1 = document.getElementById("chart1").getContext('2d');

    // Destroy the existing chart with ID 'chart4' if it exists
    destroyChart('chart1');

    var mydoughnutChart = new Chart(ctx1, {
        type: 'doughnut',
        data: d,
        showDatapoints: true,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            tooltips: {
                enabled: true
            },
            plugins: {
                legend: {
                    position: 'right',
                },
                title: {
                    display: true,
                    text: 'Unit Wise Cane Crushed'
                }
            }
        }
    });
}

function lineChartForHistoricalData(data) {
        var unitLablels = data.map(u => u.unit_name);
        var unitDatasets = data.map(u => ({
            label: u.unit_name,
            backgroundColor: backGroundColorForChart,
            borderColor: 'rgba(' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ', 1)',
            borderWidth: 2,
            data: u.results.map(r => parseInt(r.value))
        }));

    const dateLabels = data[0].results.map(r => {
        const datetime = new Date(r.entry_date);
        return datetime.toLocaleDateString();
    });
    const lineChartData = {
        lables: dateLabels,
        datasets: unitDatasets
    };

        var options = {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                legend: {
                    position: 'bottom',
                },
                title: {
                    display: true,
                    text : 'Cane Crushed (historical data)'
                }
            }
        };

    var ctx1 = document.getElementById('chart1').getContext('2d');
    // Destroy the existing chart with ID 'chart4' if it exists
    destroyChart('chart1');

   
        var myLineChart = new Chart(ctx1, {
            type: 'line',
            data: {
                labels: data[0].results.map(r => r.entry_date),
                datasets: unitDatasets
            },
            //data : lineChartData,
            options: options,
            responsive: false // Disable responsive feature
        });
}

function barChartForEstimatedSugarPercent(data) {
        const unitNames = data.map(u => u.unit_name);
        const unitDataResults = data.map(u => u.results);
    
        const backgroundColors = unitNames.map(() => getRandomColor());

        var d = {
            labels: unitNames,
            datasets: [{
                data: unitDataResults.map(r => parseFloat(r[0].estimated_sugar_percent_cane)),
                backgroundColor: backGroundColorForChart,
                borderColor: 'rgba(' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ', 1)',
                borderWidth: 1,
            }]
        };

        var ctx2 = document.getElementById("chart2").getContext('2d');

        // Destroy the existing chart with ID 'chart4' if it exists
        destroyChart('chart2');

        var mydoughnutChart = new Chart(ctx2, {
            type: 'bar',
            data: d,
            showDatapoints: true,
            options: {
                responsive: true,
                maintainAspectRatio: false,
                tooltips: {
                    enabled: true
                },
                plugins: {
                    legend: {
                        display: false,
                        position: 'right',
                    },
                    title: {
                        display: true,
                        text: 'Unit Wise - Sugar % Cane (Recovery)'
                    }
                }
            }
        });
}
function lineChartHistoricalforEstimatedSugarPercent(data) {
    var unitLablels = data.map(u => u.unit_name);
    const backgroundColors = unitLablels.map(() => getRandomColor());
    var unitDatasets = data.map(u => ({
        label: u.unit_name,
        backgroundColor: backGroundColorForChart,
        borderColor: 'rgba(' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ', 1)',
        borderWidth: 2,
        data: u.results.map(r => parseFloat(r.estimated_sugar_percent_cane))
    }));

    const dateLabels = data[0].results.map(r => {
        const datetime = new Date(r.entry_date);
        return datetime.toLocaleDateString();
    });
    const lineChartData = {
        lables: dateLabels,
        datasets: unitDatasets
    };

    var options = {
        scales: {
            y: {
                beginAtZero: true
            }
        },
        plugins: {
            legend: {
                position: 'bottom',
            },
            title: {
                display: true,
                text: 'Sugar % Cane (historical data)'
            }
        }
    };

    var ctx2 = document.getElementById('chart2').getContext('2d');
    // Destroy the existing chart with ID 'chart4' if it exists
    destroyChart('chart2');


    var myLineChart = new Chart(ctx2, {
        type: 'line',
        data: {
            labels: data[0].results.map(r => r.entry_date),
            datasets: unitDatasets
        },
        //data : lineChartData,
        options: options,
        responsive: false // Disable responsive feature
    });
}

function barChartForTotalSugarProductionQtls(data) {
    const unitNames = data.map(u => u.unit_name);
    const unitDataResults = data.map(u => u.results);

    const backgroundColors = unitNames.map(() => getRandomColor());

    var d = {
        labels: unitNames,
        datasets: [{
            //
            data: unitDataResults.map(r => parseFloat(r[0].total_sugar_bagged)),
            backgroundColor: backGroundColorForChart,
            borderColor: 'rgba(' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ', 1)',
            borderWidth: 1,
        }]

    };

    var ctx3 = document.getElementById('chart3').getContext('2d');
    // Destroy the existing chart with ID 'chart4' if it exists
    destroyChart('chart3');


    var myLineChart = new Chart(ctx3, {
        type: 'bar',
        data: d,
        showDatapoints: true,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            tooltips: {
                enabled: true
            },
            plugins: {
                legend: {
                    display: false,
                    position: 'right',
                },
                title: {
                    display: true,
                    text: 'Sugar bagged (Qtls.)'
                }
            }
        }
    });
}

function lineChartHistoricalForSugarProductionQtls(data) {
    var unitLablels = data.map(u => u.unit_name);
    const backgroundColors = unitLablels.map(() => getRandomColor());
    var unitDatasets = data.map(u => ({
        label: u.unit_name,
        backgroundColor: backGroundColorForChart,
        borderColor: 'rgba(' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ', 1)',
        borderWidth: 2,
        data: u.results.map(r => parseInt(r.total_sugar_bagged))
    }));

    const dateLabels = data[0].results.map(r => {
        const datetime = new Date(r.entry_date);
        return datetime.toLocaleDateString();
    });
    const lineChartData = {
        lables: dateLabels,
        datasets: unitDatasets
    };

    var options = {
        scales: {
            y: {
                beginAtZero: true
            }
        },
        plugins: {
            legend: {
                position: 'bottom',
            },
            title: {
                display: true,
                text: `Sugar bagged (Historical Data)`
            }
        }
    };

    var ctx3 = document.getElementById('chart3').getContext('2d');
    // Destroy the existing chart with ID 'chart4' if it exists
    destroyChart('chart3');


    var myLineChart = new Chart(ctx3, {
        type: 'line',
        data: {
            labels: data[0].results.map(r => r.entry_date),
            datasets: unitDatasets
        },
        //data : lineChartData,
        options: options,
        responsive: false // Disable responsive feature
    });
}

function barChartForEstimatedPolInCane(data) {
    const unitNames = data.map(u => u.unit_name);
    const unitDataResults = data.map(u => u.results);

    const backgroundColors = unitNames.map(() => getRandomColor());

    var d = {
        labels: unitNames,
        datasets: [{
            //label: unitNames,
            data: unitDataResults.map(r => parseFloat(r[0].pol_in_cane)),
            backgroundColor: backGroundColorForChart,
            borderColor: 'rgba(' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ', 1)',
            borderWidth: 1,
            datalabels: {
                align: 'end',
                anchor: 'start'
            }
        }]

    };

    var ctx4 = document.getElementById('chart4').getContext('2d');
    // Destroy the existing chart with ID 'chart4' if it exists
    destroyChart('chart4');


    var myLineChart = new Chart(ctx4, {
        type: 'bar',
        data: d,
        showDatapoints: true,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            tooltips: {
                enabled: true
            },
            plugins: {
                legend: {
                    display:false,
                    position: 'right',
                },
                title: {
                    display: true,
                    text: 'Pol in Cane %'
                },
                datalabels: {
                    color: 'white',
                    display: function (context) {
                        return context.dataset.data[context.dataIndex] > 15;
                    },
                    font: {
                        weight: 'bold'
                    },
                }
            }
            
        }
    });
}

function lineChartHistoricalForPolInCane(data) {
    var unitLablels = data.map(u => u.unit_name);
    const backgroundColors = unitLablels.map(() => getRandomColor());
    var unitDatasets = data.map(u => ({
        label: u.unit_name,
        backgroundColor: backGroundColorForChart,
        borderColor: 'rgba(' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ',' + Math.floor(Math.random() * 256) + ', 1)',
        borderWidth: 2,
        data: u.results.map(r => parseFloat(r.pol_in_cane))
    }));

    const dateLabels = data[0].results.map(r => {
        const datetime = new Date(r.entry_date);
        return datetime.toLocaleDateString();
    });
    const lineChartData = {
        lables: dateLabels,
        datasets: unitDatasets
    };

    var options = {
        scales: {
            y: {
                beginAtZero: true
            }
        },
        plugins: {
            legend: {
                position: 'bottom',
            },
            title: {
                display: true,
                text: 'Pol in Cane % (Historical Data)'
            }
        }
    };

    var ctx4 = document.getElementById('chart4').getContext('2d');
    // Destroy the existing chart with ID 'chart4' if it exists
    destroyChart('chart4');


    var myLineChart = new Chart(ctx4, {
        type: 'line',
        data: {
            labels: data[0].results.map(r => r.entry_date),
            datasets: unitDatasets
        },
        //data : lineChartData,
        options: options,
        responsive: false // Disable responsive feature
    });
}


function destroyChart(chartId) {
    // Check if the chart exists and destroy it
    var existingChart = Chart.getChart(chartId);
    if (existingChart) {
        existingChart.destroy();
    }
}

function getRandomColor() {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

window.onload = async function () {
    try {
        var apiData = await fetchData(1);
        createChart(apiData);
        barChartForEstimatedSugarPercent(apiData);
        barChartForTotalSugarProductionQtls(apiData);
            barChartForEstimatedPolInCane(apiData);
    } catch (error) {
        console.log(error);
    }
};

$("#historyDays").change(async function () {
    var selectedValue = $(this).val();
    try {
        if (selectedValue == 1) {
            var apiData = await fetchData(selectedValue);
            createChart(apiData);
            barChartForEstimatedSugarPercent(apiData);
            barChartForTotalSugarProductionQtls(apiData);
            barChartForEstimatedPolInCane(apiData);
        } else {
            var apiData = await fetchData(selectedValue);
            lineChartForHistoricalData(apiData);
            lineChartHistoricalforEstimatedSugarPercent(apiData);
            lineChartHistoricalForSugarProductionQtls(apiData);
            lineChartHistoricalForPolInCane(apiData);
        }
    } catch (error) {
        console.log(error);
    }
});