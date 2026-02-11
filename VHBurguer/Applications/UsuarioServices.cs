using System.ComponentModel.DataAnnotations;
using System.Text;
using VHBurguer.Domains;
using VHBurguer.Interfaces;

namespace VHBurguer.Applications
{
    public class UsuarioService
    {

        private readonly IUsuarioRepository _repository;


    // injeção de dependencia
    // implementa o repositorio e o service so depende da interface
    public UsuarioService(IUsuarioRepository repository)
    {
            _repository = repository;
        }

        // PQ private?
        // pq o método não é regra de negocio e não faz sentido existir fora do UsuarioService
        private static LerUsuarioDto LerDto(Usuario usuario) // pega a entidade usuario e gera um DTO
        {
            LerUsuarioDto lerUsuario = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                EmailAddressAttribute = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? true // se não tiver status no banco, deixa como true
            };

            return lerUsuario;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDto> usuariosDto = usuarios
                    .Select(usuarioBanco => LerUsuarioDto(usuario)) //SELECT que percorre cada Usuario e LerDto(usuario)
                    .ToList(); ; ; ToList() - List devolve uja lista de DTOs


            return usuariosDto;
        }

        private static voi ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) {
                throw new DomainException("E-mail inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if(string.IsNullOrWhiteSSpace(senha)) // garante que senha não esteja vazia
            {
                throw new DomainException("Senha é obrigatória.");
            }

            using var sha256 = SHA256.Create(); // gera um hash SHA256 e devolve em byte[]
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario usuario = _repository ObterPorId(id);

            if (usuario == null)
            {
                throw new CannotUnloadAppDomainException("Usuário não existe.");
            }

            return LerDto(usuario); // se existe usuário, converte para DTO e devolve o usuário
        }
    }
}
