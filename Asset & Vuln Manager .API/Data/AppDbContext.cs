using Microsoft.EntityFrameworkCore;
using Asset___Vuln_Manager_.API.Models; 
namespace Asset___Vuln_Manager_.API.Data
{
    public class AppDbContext : DbContext
    {
        //define o construtor da classe AppDbContext, que recebe um objeto DbContextOptions<AppDbContext> como parâmetro e o passa para a classe base DbContext. Isso é necessário para configurar o contexto do banco de dados com as opções apropriadas, como a string de conexão e o provedor de banco de dados.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //Cria uma tabela chamada Assets no banco de dados, onde cada linha representa um ativo e as colunas correspondem às propriedades da classe Asset (Id, Name, IpAddress, LastSeen, Criticality).
        public DbSet<Asset> Assets { get; set; }
    }
}