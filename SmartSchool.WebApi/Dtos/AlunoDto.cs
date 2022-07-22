using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebApi.Dtos
{

    ///<summary>
    ///Este é o DTO de Aluno para o Get.
    ///</summary>
    public class AlunoDto
    {
        ///<summary>
        ///Identificador e chave do Banco
        ///</summary>
        public int Id { get; set; }

        ///<summary>
        ///Chave do aluno , para outros negocios na Instituição
        ///</summary>
        public int Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public int? Idade { get; set; }
        public DateTime? DataIni { get; set; }
        public bool? Ativo { get; set; }
    }
}