document.addEventListener('DOMContentLoaded', function () {
    fetch('/Home/GetChartData')
        .then(response => response.json())
        .then(data => {
            // Format line chart labels
            const formattedLabels = data.dailyOutages.labels.map(dateStr => {
                const date = new Date(dateStr);
                return date.toLocaleDateString('en-US', { month: 'short', day: 'numeric' });
            });

            // Line Chart
            new Chart(document.getElementById('lineChart').getContext('2d'), {
                type: 'line',
                data: {
                    labels: formattedLabels,
                    datasets: [{
                        label: 'Daily Nuclear Outage (MW)',
                        data: data.dailyOutages.values,
                        borderColor: 'rgba(75, 192, 192, 1)',
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        tension: 0.3,
                        fill: true
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { position: 'top' },
                        tooltip: {
                            callbacks: {
                                title: (context) => data.dailyOutages.labels[context[0].dataIndex]
                            }
                        }
                    },
                    scales: {
                        y: { beginAtZero: true }
                    }
                }
            });

            // Bar Chart
            new Chart(document.getElementById('barChart').getContext('2d'), {
                type: 'bar',
                data: {
                    labels: data.generatorOutages.labels,
                    datasets: [{
                        label: 'Total Outage by Generator (MW)',
                        data: data.generatorOutages.values,
                        backgroundColor: '#00543C'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { position: 'top' },
                        tooltip: {
                            callbacks: {
                                label: (context) => {
                                    const value = context.raw;
                                    const total = context.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                                    const percent = ((value / total) * 100).toFixed(1);
                                    return `${context.label}: ${value} MW (${percent}%)`;
                                }
                            }
                        }
                    },
                    scales: {
                        x: {
                            ticks: {
                                callback: function (value) {
                                    const label = this.getLabelForValue(value);
                                    return label.length > 15 ? label.slice(0, 12) + '...' : label;
                                }
                            }
                        },
                        y: { beginAtZero: true }
                    }
                }
            });

            // Pie Chart
            new Chart(document.getElementById('pieChart').getContext('2d'), {
                type: 'pie',
                data: {
                    labels: data.generatorFrequency.labels,
                    datasets: [{
                        data: data.generatorFrequency.values,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.7)', 'rgba(54, 162, 235, 0.7)', 'rgba(255, 206, 86, 0.7)',
                            'rgba(75, 192, 192, 0.7)', 'rgba(153, 102, 255, 0.7)', 'rgba(255, 159, 64, 0.7)',
                            'rgba(199, 199, 199, 0.7)', 'rgba(83, 102, 255, 0.7)', 'rgba(255, 102, 153, 0.7)',
                            'rgba(102, 255, 102, 0.7)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { position: 'right' },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    const value = context.raw;
                                    const total = context.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                                    const percent = ((value / total) * 100).toFixed(1);
                                    return `${context.label}: ${value} (${percent}%)`;
                                }
                            }
                        }
                    }
                }
            });
        })
        .catch(error => {
            console.error('Error fetching chart data:', error);
        });
});
