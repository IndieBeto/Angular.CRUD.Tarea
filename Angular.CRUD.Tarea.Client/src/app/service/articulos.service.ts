import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ArticuloModel } from '../models/articuloModel';
import { Observable } from 'rxjs';
import { CategoriaModel } from '../models/categoriaModel';
import { EditArticuloModel } from '../models/editArticuloModel';
import { CreateArticuloModel } from '../models/createArticuloModel';

@Injectable({
  providedIn: 'root'
})
export class ArticulosService {
  baseUrl: string = "https://localhost:5001/api"

  constructor(private httpClient: HttpClient) { }

  public createArticulo(createArticuloModel: CreateArticuloModel): Observable<ArticuloModel>{
    return this.httpClient.post<ArticuloModel>(`${this.baseUrl}/articulos/`, createArticuloModel);
  }

  public getArticulos() : Observable<ArticuloModel[]>{
    return this.httpClient.get<ArticuloModel[]>(`${this.baseUrl}/articulos/getall`);
  }

  public getArticulo(id: number) : Observable<ArticuloModel>  {
    return this.httpClient.get<ArticuloModel>(`${this.baseUrl}/articulos/get/${id}`);
  }

  public deleteArticulo(id: number) {
    return this.httpClient.delete(`${this.baseUrl}/articulos/delete/${id}`);
  }

  public getCategorias(): Observable<CategoriaModel[]>{
    return this.httpClient.get<CategoriaModel[]>(`${this.baseUrl}/categorias/getall`)
  }

  public updateArticulo(editArticuloDto: any) : Observable<any>{
    return this.httpClient.put(`${this.baseUrl}/articulos/`, editArticuloDto);
  }
}
