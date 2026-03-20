namespace Asset___Vuln_Manager_.API.Models
{
    public class Asset
    {
        //Id: Um identificador único para cada ativo, geralmente usado como chave primária no banco de dados.
        public int Id { get; set; }
        //Name: O nome do ativo, que pode ser usado para identificar o dispositivo ou sistema.
        public string Name { get; set; } = string.Empty;
        //IpAddress: O endereço IP do ativo, que pode ser usado para localizar o dispositivo na rede.
        public string IpAddress { get; set; } = string.Empty;
        //LastSeen: A data e hora em que o ativo foi visto pela última vez na rede, o que pode ser útil para monitorar a atividade do dispositivo e identificar possíveis problemas de segurança.
        public DateTime LastSeen { get; set; }
        //Criticality: Um valor numérico que indica a criticidade do ativo, onde valores mais altos indicam ativos mais críticos para a organização. Isso pode ser usado para priorizar a remediação de vulnerabilidades e alocar recursos de segurança de forma mais eficaz.
        public int Criticality { get; set; }
    }
}
