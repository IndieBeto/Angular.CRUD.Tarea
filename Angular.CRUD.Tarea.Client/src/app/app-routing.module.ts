import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticuloComponent } from './components/articulos/add-articulo/add-articulo.component';
import { EditArticuloComponent } from './components/articulos/edit-articulo/edit-articulo.component';
import { TablaArticulosComponent } from './components/articulos/tabla-articulos/tabla-articulos.component';

const routes: Routes = [
  { path: 'create',  component: AddArticuloComponent },
  { path: 'edit/:id', component: EditArticuloComponent },
  { path: '', component: TablaArticulosComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
