class LiveChart {

	constructor(elementId, max, args = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]) {
		this._chart = this._createChart(elementId, max, args);
	}

	_createChart(elementId, max, args) {
		let context = document.querySelector(elementId);
		let gradient = context.getContext('2d').createLinearGradient(0, 0, 0, 450);
		gradient.addColorStop(0, 'rgba(17, 153, 142, 0.95)');
		gradient.addColorStop(0.4, 'rgba(56, 239, 125, 0.15)');
		gradient.addColorStop(1, 'rgba(255, 255, 255, 0)');
		return new Chart(context, {
			type: 'line',
			data: {
				labels: ['', '', '', '', '', '', '', '', '', ''],
				datasets: [{
					data: args,
					borderColor: "#11998e",
					backgroundColor: gradient,
					pointBackgroundColor: 'white',
					fill: true
				}]
			},
			options: {
				scales: {
					yAxes: [{
						ticks: {
							suggestedMin: 0,
							suggestedMax: max
						}
					}]
				},
				responsive: true,
				legend: {
					display: false
				}
			}
		});
	}

	update(val) {
		this._chart.data.datasets[0].data.shift();
		this._chart.data.datasets[0].data.push(val);
		this._chart.update();
	}
}