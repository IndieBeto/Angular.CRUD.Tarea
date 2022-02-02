import { Component, OnInit, Output, Input } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ArticuloModel } from 'src/app/models/articuloModel';
import { CategoriaModel } from 'src/app/models/categoriaModel';
import { ArticulosService } from 'src/app/service/articulos.service';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-edit-articulo',
  templateUrl: './edit-articulo.component.html',
  styleUrls: ['./edit-articulo.component.css']
})
export class EditArticuloComponent implements OnInit {
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
    private toastr: ToastrService ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get("id");
    this.articuloService.getArticulo(Number(id))
      .subscribe((response) => {
        this.id_articulo = Number(id);
        this.codigo_articulo = response.codigo_articulo;
        this.marca_articulo = response.marca_articulo;
        this.modelo_articulo = response.modelo_articulo;
        this.id_tipo_insumo = response.id_tipo_insumo;
        this.descripcion_articulo = response.descripcion_articulo;
        console.log(this.id_categoria_articulo);
      })
    this.articuloService.getCategorias()
      .subscribe((response) => {
        this.categorias = response;
      })
  }

  public updateArticulo(){
    if (this.id_categoria_articulo == 0){
      this.toastr.warning('Por favor selecciona una categoría.', 'Atención', {
        closeButton: true,
        positionClass: 'toast-bottom-right'
      });
        return;
    }
    const articuloEditado = {
      id_articulo: this.id_articulo,
      codigo_articulo: this.codigo_articulo,
      marca_articulo: this.marca_articulo,
      modelo_articulo: this.modelo_articulo,
      descripcion_articulo: this.descripcion_articulo,
      id_tipo_insumo: this.id_tipo_insumo,
      id_categoria_articulo: this.id_categoria_articulo
  }
  this.articuloService.updateArticulo(articuloEditado)
    .subscribe((response) => {
      this.toastr.success('Artículo actualizado correctamente.', '¡Éxito!', {
        closeButton: true,
        positionClass: 'toast-bottom-right'
      });
      window.location.href = '/';
    })
  }

  public selectCategoria(deviceValue: Event){
    let idCategoriaSeleccionada = Number(((deviceValue.target as HTMLTextAreaElement).value));
    this.id_categoria_articulo = idCategoriaSeleccionada;
  }
}
