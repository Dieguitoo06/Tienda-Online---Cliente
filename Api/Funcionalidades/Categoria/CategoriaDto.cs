using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Funcionalidades.Clientes.Categoria
{
    public class CategoriaDto
    {
      public int idCategoria  { get; set; }
      public required string Nombre  { get; set; }
    }
    public class CategoriaCommandDto
    {
        public int idCategoria  { get; set; }
        public required string Nombre { get; set; }

    }    
}