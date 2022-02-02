export interface ArticuloModel {
    id_articulo: number
    codigo_articulo: string
    marca_articulo: string
    modelo_articulo: string;
    descripcion_articulo: string
    id_tipo_insumo: number
    id_categoria_articulo: number
    glosa_categoria_articulo: string
    vigente_categoria_articulo: boolean
}