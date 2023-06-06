import { Component } from '@angular/core';

@Component({
  selector: 'app-modal-component',
  templateUrl: './modal-component.component.html',
  styleUrls: ['./modal-component.component.css']
})
export class ModalComponentComponent {

  quantidade: number = 0;
  flEntrega: boolean = false;

  adicionarItem() {
    // Lógica para adicionar o item

    // Incrementar a quantidade
    this.quantidade++;
  }

  removerItem() {
    // Lógica para remover o item

    // Decrementar a quantidade, assegurando que não seja menor que zero
    if (this.quantidade > 0) {
      this.quantidade--;
    }
  }

  listaFake: any[] = [
    { id: 1, nome: '1293s', valor: 100 },
    { id: 2, nome: 'Gustavo Koslovski', valor: 200 },
    { id: 3, nome: 'Item 3', valor: 300 },
    { id: 4, nome: 'Item 3', valor: 300 },
    { id: 5, nome: 'Item 3', valor: 300 },
    { id: 6, nome: 'Item 3', valor: 300 },
    { id: 6, nome: 'Item 3', valor: 300 },
    { id: 6, nome: 'Item 3', valor: 300 },
    { id: 6, nome: 'Item 3', valor: 300 },
    { id: 6, nome: 'Item 3', valor: 300 },
    { id: 6, nome: 'Item 3', valor: 300 },
    { id: 6, nome: 'Item 3', valor: 300 },
    // ...
  ];

  setEntrega(){
    if(!this.flEntrega){
      this.flEntrega = true;
    }else{
      this.flEntrega = false;
    } 
    console.log(this.flEntrega)
  }

}
