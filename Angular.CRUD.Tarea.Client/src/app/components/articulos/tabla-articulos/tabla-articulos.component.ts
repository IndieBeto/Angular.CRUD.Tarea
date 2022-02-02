import { Component, OnInit } from '@angular/core';
import { ArticuloModel } from '../../../models/articuloModel'
import { EditArticuloModel } from '../../../models/editArticuloModel'
import { ArticulosService } from '../../../service/articulos.service'
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-tabla-articulos',
  templateUrl: './tabla-articulos.component.html',
  styleUrls: ['./tabla-articulos.component.css']
})
export class TablaArticulosComponent implements OnInit {
  articulos: ArticuloModel[] = [];
  editArticuloModel: boolean = false;

  constructor(private articulosService: ArticulosService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.articulosService.getArticulos()
      .subscribe((response) => {
        this.articulos = response;
        console.log(this.articulos);

      })
  }
  deleteArticulo(id: number){
    var index = this.articulos.map(x => {
      return x.id_articulo;
    }).indexOf(id);
    this.articulosService.deleteArticulo(id)
      .subscribe((response) => {
        console.log(response);
        this.articulos.splice(index, 1);
        this.toastr.success('Artículo borrado correctamente', 'Éxito', {
          closeButton: true,
          positionClass: 'toast-bottom-right'
        });
      });    
  }
  
  selectArticuloToEdit(editArticuloModel: EditArticuloModel){
    console.log(editArticuloModel);
  }

}
