using System.ComponentModel.DataAnnotations; //Adiciona a diretiva using para o namespace System.ComponentModel.DataAnnotations, que contém atributos de validação de dados usados para validar as propriedades da classe Asset, como [Required] ou [StringLength].
namespace Asset___Vuln_Manager_.API.Models
{
    public class Asset
    {
        //Id: Um identificador único para cada ativo, geralmente usado como chave primária no banco de dados.
        public int Id { get; set; }
        //Name: O nome do ativo, que pode ser usado para identificar o dispositivo ou sistema.
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        public string Name { get; set; } = string.Empty;
        //IpAddress: O endereço IP do ativo, que pode ser usado para localizar o dispositivo na rede.
        [Required(ErrorMessage = "O campo IpAddress é obrigatório.")]
        [RegularExpression(@"^(\d{1,3}\.){3}\d{1,3}$", ErrorMessage = "IP Inválido")]
        public string IpAddress { get; set; } = string.Empty;
        //LastSeen: A data e hora em que o ativo foi visto pela última vez na rede, o que pode ser útil para monitorar a atividade do dispositivo e identificar possíveis problemas de segurança.
        public DateTime LastSeen { get; set; }
        //Criticality: Um valor numérico que indica a criticidade do ativo, onde valores mais altos indicam ativos mais críticos para a organização. Isso pode ser usado para priorizar a remediação de vulnerabilidades e alocar recursos de segurança de forma mais eficaz.
        [Range(1,5, ErrorMessage = "Criticality deve ser um valor entre 1 e 5.")]
        public int Criticality { get; set; }
    }
}