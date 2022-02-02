import { Component, OnInit, Output, EventEmitter, Input  } from '@angular/core';
import { EditArticuloModel } from '../../../models/editArticuloModel';
import { Router, ActivatedRoute } from "@angular/router";
import { CategoriaModel } from 'src/app/models/categoriaModel';
import { ArticulosService } from '../../../service/articulos.service';
import { ToastrService } from 'ngx-toastr';
import { CreateArticuloModel } from 'src/app/models/createArticuloModel';
;


@Component({
  selector: 'app-add-articulo',
  templateUrl: './add-articulo.component.html',
  styleUrls: ['./add-articulo.component.css']
})
export class AddArticuloComponent implements OnInit {
  id_articulo: number = 0;
  codigo_articulo: string = '';
  marca_articulo: string = '';
  modelo_articulo: string = '';
  id_tipo_insumo: number;
  categorias: CategoriaModel[];
  descripcion_articulo: string = '';
  id_categoria_articulo: number = 0;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private articuloService: ArticulosService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.articuloService.getCategorias()
    .subscribe((response) => {
      this.categorias = response;
    })
  }

  public selectCategoria(deviceValue: Event){
    let idCategoriaSeleccionada = Number(((deviceValue.target as HTMLTextAreaElement).value));
    this.id_categoria_articulo = idCategoriaSeleccionada;
  }

  public validate(): boolean {
    if (
      !this.codigo_articulo || 
      !this.codigo_articulo ||
      !this.marca_articulo || 
      !this.modelo_articulo || 
      !this.id_tipo_insumo || 
      !this.descripcion_articulo || 
      this.id_categoria_articulo == 0
      ){
        this.toastr.warning('Completa todos los campos.', 'Atención', {
          closeButton: true,
          positionClass: 'toast-bottom-right'
        });
        return false;
    }
    return true;
  }

  public createArticulo() {
    if (!this.validate()){
      return;
    }
    const nuevoArticulo = {
      codigo_articulo: this.codigo_articulo,
      marca_articulo: this.marca_articulo,
      modelo_articulo: this.modelo_articulo,
      descripcion_articulo: this.descripcion_articulo,
      id_tipo_insumo: this.id_tipo_insumo,
      id_categoria_articulo: this.id_categoria_articulo
    }
    this.articuloService.createArticulo(nuevoArticulo as CreateArticuloModel)
      .subscribe((response) => {
        window.location.href = "/";
      })
  }

}
