import { Component } from '@angular/core';
import { Meta } from 'src/app/model/meta';
import { MetaService } from 'src/app/service/meta-service/meta.service';

@Component({
  selector: 'app-modal-meta',
  templateUrl: './modal-meta.component.html',
  styleUrls: ['./modal-meta.component.css']
})
export class ModalMetaComponent {

  public meta! : Meta;
  metas: Meta[] = [];
  constructor(private metaService: MetaService){}
  
  ngOnInit(): void {
    this.listarMetas();
    this.meta = new Meta();
  }
  
  public addMeta(): void {
    this.metaService.cadastrar(this.meta);
  }

   listarMetas(): void {
  this.metaService.findAll()
    .then((metas: Meta[]) => {
      this.metas = metas; // Armazena a lista completa de metas
    })
    .catch((error) => {
      console.error('Erro ao obter as metas:', error);
    });
}

}
