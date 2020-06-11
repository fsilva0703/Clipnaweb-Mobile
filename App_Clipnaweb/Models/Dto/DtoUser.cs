using System;
using System.Collections.Generic;
using System.Text;

namespace App_Clipnaweb.Models.Dto
{
    public class DtoUser
    {
        public int userId { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string NomeBanco { get; set; }
        public string NomeCliente { get; set; }
    }
}
