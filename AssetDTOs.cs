using System;
using System.ComponentModel.DataAnnotations;
using Asset___Vuln_Manager_.API.DTOs.Asset;

namespace Asset___Vuln_Manager_.API.DTOs.Asset
{ 
	public class CreateAssetDto // Define a classe CreateAssetDTO, que é um Data Transfer Object (DTO) usado para transferir dados relacionados à criação de um ativo. Essa classe contém propriedades que correspondem aos campos necessários para criar um novo ativo, como Name e IpAddress, e inclui atributos de validação para garantir que os dados fornecidos sejam válidos antes de serem processados pelo controlador.
    {	
		[Required(ErrorMessage = "O campo Name é obrigatório.")]
		[StringLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "O campo IpAddress é obrigatório.")]
		[RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
			ErrorMessage = "IP Inválido! Use o formato 0.0.0. até 255.255.255.255")]
		public string IpAddress { get; set; } = string.Empty;

		public DateTime LastSeen { get; set; }
		public int Criticality { get; set; }
    }

	public class UpdateAssetDto
	{
		[Required(ErrorMessage = "O campo Name é obrigatório.")]
		[StringLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "O campo IpAddress é obrigatório.")]
		[RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
			ErrorMessage = "IP Inválido! Use o formato 0.0.0. até 255.255.255.255")]
        public string IpAddress { get; set; } = string.Empty;

        public DateTime LastSeen { get; set; }

        [Range(1, 5, ErrorMessage = "Criticality deve ser entre 1 e 5.")]
        public int Criticality { get; set; }
    }

    public class AssetResponseDto
    {
        public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string IpAddress { get; set; } = string.Empty;
		public DateTime LastSeen { get; set; }
		public int Criticality { get; set; }
    }
}