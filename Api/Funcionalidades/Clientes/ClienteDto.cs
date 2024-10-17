namespace Api.Funcionalidaades.Clientes;

public class UsuariosQueryDto
{
    public int Dni { get; set; }
    public required string Usuario { get; set; }

}

public class UsuariosCommandDto
{
    public required string Usuario { get; set; }

}
