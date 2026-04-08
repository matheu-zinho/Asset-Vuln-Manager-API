using System.ComponentModel.DataAnnotations; // necessário para usar as Data Annotations como [Required], [StringLength], etc.

// Os DTOs ficam em uma pasta separada de Models para deixar claro o papel de cada um:
// - Models/     → representa as tabelas do banco de dados (entidades do EF Core)
// - DTOs/Asset/ → representa os contratos de comunicação da API com o mundo externo
//
// Por que isso importa em segurança?
// Se você retornar o Model diretamente, pode vazar campos sensíveis que foram adicionados
// depois (ex: um campo "PasswordHash" que alguém adicionou ao Model sem pensar na API).
// Com DTOs, você define EXPLICITAMENTE o que cada operação aceita ou retorna.

namespace Asset___Vuln_Manager_.API.DTOs.Asset
{
    // ─────────────────────────────────────────────
    // CreateAssetDto — usado no POST /api/assets
    // Contém apenas os campos que o CLIENTE pode definir ao criar um ativo.
    // Note que Id NÃO está aqui: quem gera o Id é o banco de dados, não o cliente.
    // ─────────────────────────────────────────────
    public class CreateAssetDto
    {
        // [Required] → o campo é obrigatório; o ASP.NET retorna 400 se vier vazio.
        // [StringLength] → limita o tamanho para evitar que um texto enorme seja salvo no banco.
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        [StringLength(100, ErrorMessage = "Name deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        // [RegularExpression] → valida o formato do IP antes de qualquer lógica de negócio.
        // Isso evita que um valor malformado chegue ao banco e cause erros difíceis de rastrear.
        [Required(ErrorMessage = "O campo IpAddress é obrigatório.")]
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "IP inválido. Use o formato 0.0.0.0 até 255.255.255.255.")]
        public string IpAddress { get; set; } = string.Empty;

        // LastSeen é definido pelo cliente ao registrar um ativo.
        // Em um sistema real, você poderia definir isso automaticamente via agent/scanner.
        public DateTime LastSeen { get; set; }

        // [Range] → garante que criticidade esteja entre 1 (baixa) e 5 (crítica).
        // Sem isso, alguém poderia enviar 999 e quebrar sua lógica de priorização.
        [Range(1, 5, ErrorMessage = "Criticality deve ser entre 1 e 5.")]
        public int Criticality { get; set; }
    }

    // ─────────────────────────────────────────────
    // UpdateAssetDto — usado no PUT /api/assets/{id}
    // Identico ao CreateAssetDto neste caso, mas separado por uma razão importante:
    // no futuro, update e create podem ter campos diferentes.
    // Ex: no update, talvez você não permita mudar o IpAddress por razão de negócio.
    // Ter DTOs separados te dá essa flexibilidade sem quebrar código existente.
    // ─────────────────────────────────────────────
    public class UpdateAssetDto
    {
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        [StringLength(100, ErrorMessage = "Name deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo IpAddress é obrigatório.")]
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "IP inválido. Use o formato 0.0.0.0 até 255.255.255.255.")]
        public string IpAddress { get; set; } = string.Empty;

        public DateTime LastSeen { get; set; }

        [Range(1, 5, ErrorMessage = "Criticality deve ser entre 1 e 5.")]
        public int Criticality { get; set; }
    }

    // ─────────────────────────────────────────────
    // AssetResponseDto — usado nas respostas GET, POST e PUT
    // É o que a API RETORNA ao cliente. Mesmo que o Model mude internamente,
    // o contrato com o cliente permanece estável enquanto você não mudar este DTO.
    // Aqui SIM temos o Id, pois o cliente precisa saber o identificador do recurso criado/lido.
    // ─────────────────────────────────────────────
    public class AssetResponseDto
    {
        // Id é exposto na resposta para que o cliente possa referenciar o ativo
        // em chamadas futuras (ex: GET /api/assets/42 ou DELETE /api/assets/42)
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        // DateTime formatado em ISO 8601 pelo ASP.NET Core automaticamente (ex: "2026-04-03T12:00:00")
        public DateTime LastSeen { get; set; }

        public int Criticality { get; set; }
    }
}
