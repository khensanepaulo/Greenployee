import { Component, OnInit } from '@angular/core';
import { ViewChild } from '@angular/core';
import DatalabelsPlugin from 'chartjs-plugin-datalabels';
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Meta } from 'src/app/model/meta';
import { Pessoa } from 'src/app/model/pessoa';
import { PessoaMeta } from 'src/app/model/pessoaMeta';
import { MetaService } from 'src/app/service/meta.service';
import { PessoaService } from 'src/app/service/pessoa.service';
import { cloneDeep } from 'lodash';
import { UserDataService } from 'src/app/service/userDataService';
import { ComissoesPorPeriodo } from 'src/app/model/comissoesPorPeriodo';
import { OrdemServicoService } from 'src/app/service/ordem-servico.service';

@Component({
  selector: 'app-modal-relatorio',
  templateUrl: './modal-relatorio.component.html',
  styleUrls: ['./modal-relatorio.component.css']
})
export class ModalRelatorioComponent {

  comissoesPorData: ComissoesPorPeriodo[] = [];
  public quantidadeMetasConcluida!: number;
  public quantidadeMetasNaoConcluida!: number;
  verificaUser!: string;
  public pessoa!: Pessoa;
  public meta!: Meta;
  public pessoaMeta!: PessoaMeta;
  pessoas: Pessoa[] = [];
  metas: Meta[] = [];
  public listaNomesMes: string[] = [];
  public listaValores: number[] = [];

  constructor(public metaService: MetaService,
    public pessoaService: PessoaService,
    public userDataService: UserDataService,
    public ordemServicoService: OrdemServicoService,) { }

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  ngOnInit(): void {


    this.listarMetas();
    this.listarPessoas();
    this.listaComissoesPorMes();
    this.barChartData.labels = this.listaNomesMes;

    // Atribuir os valores à lista de valores (data)
    this.barChartData.datasets[0].data = this.listaValores;
    this.pessoaMeta = new PessoaMeta();
    this.meta = new Meta();
    this.pessoa = new Pessoa();

  }

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: {
      x: {},
      y: {
        min: 10
      }
    },
    plugins: {
      legend: {
        display: true,
      },
      datalabels: {
        anchor: 'end',
        align: 'end'
      }
    }
  };
  public barChartType: ChartType = 'bar';
  public barChartPlugins = [
    DatalabelsPlugin
  ];

  public barChartData: ChartData<'bar'> = {
    labels: this.listaNomesMes,
    datasets: [
      { data: this.listaValores, label: 'Ordens Serviço' },
      // { data: [ 28, 48, 40, 19, 86, 27, 90 ], label: 'Series B' }
    ]
  };

  // events

  public randomize(): void {
    // Only Change 3 values
    this.barChartData.datasets[0].data = [
      Math.round(Math.random() * 100),
      59,
      80,
      Math.round(Math.random() * 100),
      56,
      Math.round(Math.random() * 100),
      40];

    this.chart?.update();
  }



  public pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'bottom',

      },
      datalabels: {
        formatter: (value: any, ctx: any) => {
          if (ctx.chart.data.labels) {
            return ctx.chart.data.labels[ctx.dataIndex];
          }
        },
        color: 'white', // Altera a cor do texto para branco
      },
      tooltip: {
        enabled: true,
      },
      title: {
        display: true,
        text: 'Metas',
        font: {
          size: 22,
        },
        color: 'black', // Altera a cor do texto para branco
      },
    },
    elements: {
      arc: {
        backgroundColor: ['rgb(54,130,94)', 'rgb(107, 25, 25)', '#FFCE56', '#4BC0C0', '#9966FF'],
        borderColor: '#FFFFFF',
      },
    },
  };

  public get pieChartData(): ChartData<'pie', number[], string | string[]> {
    return {
      labels: ['Concluídas', 'Não Concluídas'],
      datasets: [{
        data: [this.quantidadeMetasConcluida, this.quantidadeMetasNaoConcluida]
      }]
    };
  }
  public pieChartType: ChartType = 'pie';
  public pieChartPlugins = [DatalabelsPlugin];

  // events
  public chartClicked({ event, active }: { event: ChartEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public chartHoveredBar({ event, active }: { event: ChartEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public chartHoveredPie({ event, active }: { event: ChartEvent, active: {}[] }): void {
    console.log(event, active);
  }


  changeLabels(): void {
    const words = ['hen', 'variable', 'embryo', 'instal', 'pleasant', 'physical', 'bomber', 'army', 'add', 'film',
      'conductor', 'comfortable', 'flourish', 'establish', 'circumstance', 'chimney', 'crack', 'hall', 'energy',
      'treat', 'window', 'shareholder', 'division', 'disk', 'temptation', 'chord', 'left', 'hospital', 'beef',
      'patrol', 'satisfied', 'academy', 'acceptance', 'ivory', 'aquarium', 'building', 'store', 'replace', 'language',
      'redeem', 'honest', 'intention', 'silk', 'opera', 'sleep', 'innocent', 'ignore', 'suite', 'applaud', 'funny'];
    const randomWord = () => words[Math.trunc(Math.random() * words.length)];
    this.pieChartData.labels = new Array(3).map(_ => randomWord());

    this.chart?.update();
  }

  addSlice(): void {
    if (this.pieChartData.labels) {
      this.pieChartData.labels.push(['Line 1', 'Line 2', 'Line 3']);
    }

    this.pieChartData.datasets[0].data.push(400);

    this.chart?.update();
  }

  removeSlice(): void {
    if (this.pieChartData.labels) {
      this.pieChartData.labels.pop();
    }

    this.pieChartData.datasets[0].data.pop();

    this.chart?.update();
  }

  changeLegendPosition(): void {
    if (this.pieChartOptions?.plugins?.legend) {
      this.pieChartOptions.plugins.legend.position = this.pieChartOptions.plugins.legend.position === 'left' ? 'top' : 'left';
    }

    this.chart?.render();
  }

  toggleLegend(): void {
    if (this.pieChartOptions?.plugins?.legend) {
      this.pieChartOptions.plugins.legend.display = !this.pieChartOptions.plugins.legend.display;
    }

    this.chart?.render();
  }








  public resetMeta(): void {
    this.meta = new Meta();
    this.pessoaMeta = new PessoaMeta();
  }

  public listarMetas(): void {

    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions == 'Admin') {
      this.metaService.findAll().then((metas: Meta[]) => {
        this.metas = metas; // Armazena a lista completa de pessoas
        console.log(metas);
        this.contagemMetas();
      })
        .catch((error) => {
          console.error('Erro ao obter as pessoas:', error);
        });
    } else {
      this.metaService.findByUserId(parsedUserId).then((metas: Meta[]) => {
        this.metas = metas; // Armazena a lista completa de metas
        this.contagemMetas();
        console.log(metas);
      })
        .catch((error) => {
          console.error('Erro ao obter as metas:', error);
        });
    }
  }

  public contagemMetas(): void {
    const metasComDataNull = this.metas.filter(item => item.dtAtualizado === null);
    this.quantidadeMetasNaoConcluida = metasComDataNull.length;

    const metasComData = this.metas.filter(item => item.dtAtualizado != null);
    this.quantidadeMetasConcluida = metasComData.length;
  }



  public verificarUser(): boolean {

    this.verificaUser = this.userDataService.userCredentials.permissions;
    return this.verificaUser != 'Admin';

  }

  public verificarAdmin(): boolean {
    this.verificaUser = this.userDataService.userCredentials.permissions;
    return this.verificaUser != 'User';

  }

  public listarPessoas(): void {
    this.pessoaService.findAll()
      .then((pessoas: Pessoa[]) => {
        this.pessoas = pessoas; // Armazena a lista completa de metas
      })
      .catch((error) => {
        console.error('Erro ao obter as metas:', error);
      });
  }

  public resetItem(): void {
    this.pessoaMeta = new PessoaMeta();
    this.meta = new Meta();
  }

  public removeItem(sinal: string, index: number): void {
    if (sinal == '-') {
      this.meta.pessoasMeta.splice(index, 1);
    } else {
      return;
    }
  }

  public listaComissoesPorMes(): void {

    const userId = this.userDataService.userCredentials.userId;
    const parsedUserId = parseInt(userId, 10);
    if (this.userDataService.userCredentials.permissions == 'Admin') {
   
      this.ordemServicoService.FindByCommissionsByMonthAll().then((comissoesPorData: ComissoesPorPeriodo[]) => {
   
        this.comissoesPorData = comissoesPorData.slice(0, 10);
        this.listaNomesMes = comissoesPorData.map(comissao => comissao.nmMes.toString());
        this.listaValores = comissoesPorData.map(comissao => comissao.vlTotal);
        console.log(this.comissoesPorData);
      })
        .catch((error) => {
          console.error('Erro ao obter as Comissoes.');
        });
    } else {
   
      this.ordemServicoService.FindBycommissionsByMonthById(parsedUserId).then((comissoesPorData: ComissoesPorPeriodo[]) => {
        this.comissoesPorData = comissoesPorData.slice(0, 10);
        this.listaNomesMes = comissoesPorData.map(comissao => comissao.nmMes.toString());
        this.listaValores = comissoesPorData.map(comissao => comissao.vlTotal);
        console.log(this.comissoesPorData);
      })
        .catch((error) => {
          console.error('Erro ao obter as Comissoes.');
        });
    }
  }

}
