using Microsoft.AspNetCore.Mvc;
using Asset___Vuln_Manager_.API.Data;
using Asset___Vuln_Manager_.API.Models;
using Asset___Vuln_Manager_.API.DTOs.Asset;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Asset___Vuln_Manager_.API.Controllers
{
    [Route("api/[controller]")] // esse atributo define a rota para os endpoints do controlador, onde [controller] é um placeholder que será substituído pelo nome do controlador (neste caso, "assets"), resultando em uma rota base de "api/assets" para os endpoints definidos neste controlador.
    [ApiController] // validação automática de modelo, vinculação de dados e formatação de resposta para os endpoints do controlador, simplificando o desenvolvimento de APIs RESTful.
    //[Authorize]
    public class AssetsController : ControllerBase // a classe AssetsController herda de ControllerBase, que é uma classe base para controladores de API no ASP.NET Core. Isso permite que o controlador responda a solicitações HTTP e retorne respostas apropriadas, como JSON ou status HTTP.
    {
        private readonly AppDbContext _context; // define um campo privado readonly do tipo AppDbContext chamado _context, que é usado para acessar o banco de dados e realizar operações relacionadas aos ativos.
        public AssetsController(AppDbContext context)// o construtor da classe AssetsController recebe um parâmetro do tipo AppDbContext chamado context e atribui esse valor ao campo _context. Isso é feito para permitir que o controlador acesse o banco de dados e execute operações relacionadas aos ativos.
        {
            _context = context; //
        }
        [HttpGet] // o atributo HttpGet indica que este método é um endpoint de leitura (GET) e pode ser acessado por meio de uma solicitação HTTP GET.
        public async Task<ActionResult<IEnumerable<AssetResponseDto>>> GetAssets() // o método GetAssets é um endpoint que retorna uma lista de ativos. Ele é assíncrono e retorna um ActionResult contendo uma coleção de objetos do tipo Asset.
        {
            var assets = await _context.Assets 
                .Select(a => new AssetResponseDto// o método Select é usado para projetar cada ativo (a) em um novo objeto do tipo AssetResponseDto, que é uma classe de transferência de dados (DTO) usada para formatar a resposta da API. Ele extrai as propriedades relevantes do ativo e as atribui às propriedades correspondentes do DTO, preparando-o para ser enviado como resposta. 
                {
                    Id = a.Id, 
                    Name = a.Name,
                    IpAddress = a.IpAddress,
                    LastSeen = a.LastSeen,
                    Criticality = a.Criticality
                })
                .ToListAsync();
            return Ok(assets);

        }
        [HttpPost] // o atributo HttpPost indica que este método é um endpoint de criação (POST) e pode ser acessado por meio de uma solicitação HTTP POST.
        public async Task<ActionResult<AssetResponseDto>> PostAsset(CreateAssetDto dto)  //o dto é o objeto criado para guardar os dados recebido no modelo do CreatAssetDto.
        {
            var asset = new Asset // criasse um objeto "asset" que faz referencia ao modelo Asset(definiçao teorica das regras no arquivo Asset.cs), pra que ao executar, instancia a classe propriamente num espaço de memoria.
            {
                Name = dto.Name, // dto recebe o dado, transcreve para o asset e depois o asset é adicionado ao banco de dados. 
                IpAddress = dto.IpAddress, 
                LastSeen = dto.LastSeen,
                Criticality = dto.Criticality
            };
            _context.Assets.Add(asset); // o método Add é usado para adicionar o novo ativo ao contexto do banco de dados, preparando-o para ser salvo posteriormente.
            await _context.SaveChangesAsync(); 

            var responseDto = new AssetResponseDto  
            {
                Id = asset.Id, 
                Name = asset.Name,
                IpAddress = asset.IpAddress,
                LastSeen = asset.LastSeen,
                Criticality = asset.Criticality
            };            
            return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, responseDto); // o método CreatedAtAction é usado para retornar uma resposta HTTP 201 Created, indicando que um novo recurso foi criado com sucesso. Ele também inclui a localização do novo recurso (usando o nome do método GetAsset e o ID do ativo) e o objeto de resposta DTO contendo os detalhes do ativo criado.
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetResponseDto>> GetAsset(int id)// o método GetAsset é um endpoint que retorna um ativo específico com base no ID fornecido. Ele é assíncrono e retorna um ActionResult contendo um objeto do tipo AssetResponseDto. O parâmetro id é usado para identificar o ativo que deve ser recuperado do banco de dados.
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound("Ativo não encontrado"); 
            }
            var dto = new AssetResponseDto // oque esse dto faz aqui? 
            { // o objeto dto é criado para mapear os dados do ativo encontrado para um formato específico de resposta (AssetResponseDto) que será retornado ao cliente. Ele extrai as propriedades relevantes do ativo e as atribui às propriedades correspondentes do DTO, preparando-o para ser enviado como resposta.
                Id = asset.Id,
                Name = asset.Name,
                IpAddress = asset.IpAddress,
                LastSeen = asset.LastSeen,
                Criticality = asset.Criticality
            };
            return Ok(dto); 

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, UpdateAssetDto dto)
        {
            // Busca o ativo no banco pelo ID da URL
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
                return NotFound("Ativo não encontrado.");

            // Atualiza apenas os campos permitidos pelo DTO.
            // Por que não usar dto.Id? Para evitar que o cliente troque a chave primária,
            // o que corromperia relacionamentos no banco.
            // A regra: o ID vem sempre da URL, nunca do body.
            asset.Name = dto.Name;
            asset.IpAddress = dto.IpAddress;
            asset.LastSeen = dto.LastSeen;
            asset.Criticality = dto.Criticality;

            // O EF Core detecta automaticamente que o objeto mudou (change tracking)
            // e executa um UPDATE no banco apenas para os campos alterados.
            await _context.SaveChangesAsync();

            // HTTP 204 No Content — padrão REST para PUT bem-sucedido.
            // Não retornamos o objeto porque o cliente já o enviou e sabe o que foi salvo.
            return NoContent();
        }

        // ─────────────────────────────────────────────
        // DELETE /api/assets/{id}
        // Remove um ativo permanentemente.
        // Uso: um servidor foi aposentado e deve sair do inventário.
        // ATENÇÃO: esta ação é irreversível no SQLite sem transações de backup.
        // Em produção, considere "soft delete" (campo IsDeleted = true) em vez de remover.
        // ─────────────────────────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
                return NotFound("Ativo não encontrado.");

            _context.Assets.Remove(asset); // marca para deleção no tracking do EF Core
            await _context.SaveChangesAsync(); // executa DELETE no banco

            // HTTP 204 No Content — padrão REST para DELETE bem-sucedido.
            return NoContent();
        }

    }
}