using SCA.Model.Entidades;

namespace SCA.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario RecuperarUsuarioPorLoginSenha(string login, string password);
        Usuario CriarUsuarioAdmin();
    }
}
