import { Component, OnInit } from '@angular/core';
import { Chart, ChartOptions } from 'chart.js';

@Component({
  selector: 'app-modal-relatorio',
  templateUrl: './modal-relatorio.component.html',
  styleUrls: ['./modal-relatorio.component.css']
})
export class ModalRelatorioComponent implements OnInit {

  ngOnInit(): void {
    this.createDonutChart();
  }

  createDonutChart(): void {
    const donutChartOptions: ChartOptions = {
      responsive: false,
      cutoutPercentage: 70 // Adjust the cutoutPercentage to control the size of the donut hole
    };

    const donutChartData = {
      labels: ['Label 1', 'Label 2', 'Label 3'],
      datasets: [{
        data: [10, 20, 30], // Replace with your data values
        backgroundColor: ['red', 'green', 'blue'] // Replace with your desired colors
      }]
    };

    const donutChartConfig = {
      type: 'doughnut',
      data: donutChartData,
      options: donutChartOptions
    };

    const donutChartCanvas = document.getElementById('donutChartCanvas') as HTMLCanvasElement | null;
    if (donutChartCanvas) {
      const context = donutChartCanvas.getContext('2d');
      if (context) {
        new Chart(context, donutChartConfig);
      } else {
        console.error('Could not retrieve 2D context for the canvas element.');
      }
    } else {
      console.error('Could not find the canvas element with the specified ID.');
    }
  }


}
