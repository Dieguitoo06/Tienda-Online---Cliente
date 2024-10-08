namespace Biblioteca;

public class Cliente
{
    public string Nombre { get; set;}
    public string Apellido { get; set; }
    public string Email { get; set; }    
    public string Usuario { get; set; } 
    public string Contraseña { get; set; }

    public Cliente (string nombre, string apellido, string email, string usuario, string contraseña)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        Usuario = usuario;
        Contraseña = contraseña;
    }
}
