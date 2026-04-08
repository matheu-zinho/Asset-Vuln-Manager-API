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
                .Select(a => new AssetResponseDto
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
        public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAssets), new { id = asset.Id }, asset); // o método PostAsset é um endpoint que permite criar um novo ativo. Ele recebe um objeto do tipo Asset como parâmetro, adiciona esse ativo ao contexto do banco de dados (_context) e salva as alterações. Em seguida, retorna uma resposta HTTP 201 Created com a localização do novo recurso criado usando CreatedAtAction, que aponta para o método GetAssets para recuperar o ativo recém-criado.
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetResponseDto>> GetAsset(int id)// o método GetAsset é um endpoint que retorna um ativo específico com base no ID fornecido. Ele é assíncrono e retorna um ActionResult contendo um objeto do tipo Asset.
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound("Ativo não encontrado"); 
            }
            var dto = new AssetResponseDto 
            {
                Id = asset.Id,
                Name = asset.Name,
                IpAddress = asset.IpAddress,
                LastSeen = asset.LastSeen,
                Criticality = asset.Criticality
            };
            return Ok(dto); 

        }

    }
}