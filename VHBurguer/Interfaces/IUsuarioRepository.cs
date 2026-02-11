using VHBurguer.Domains;

namespace VHBurguer.Interfaces
{
    public interface IUsuarioRepository
    {

        List<Usuario> Listar()

        // pode ser que não venha nenhum usuário, por isso
        Usuario? ObterPorId(int id);

        Usuario? OberPorEmail(string email);

        bool EmailExiste(string email);

        void Adicionar(Usuario usuario);

        void Atualizar(Usuario usuario);

        void Remover(int id);

    }
}
