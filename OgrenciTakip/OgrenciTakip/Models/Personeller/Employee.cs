using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciTakip.Models.Personeller
{
    public class Employee
    {
        public int PersonelId { get; set; }
        public string AdiveSoyadi { get; set; }
        public DateTime DogumTarihi {get; set;}
        public string NICNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Cep { get; set; }
        public string Telefon { get; set; }
        public int CinsiyetId { get; set; }
        public DateTime IsTarihi { get; set; }
        public int BolumId { get; set; }
        public byte[] Photo { get; set; }

        public string AddressLine { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string PostCode { get; set; }

        public int JobTitleId { get; set; }
        public decimal CurrentSalary { get; set; }
        public decimal StartingSalary { get; set; }

        public bool HasLeft { get; set; }
        public DateTime DateLeft { get; set; }
        public int ReasonLeftId { get; set; }
        public string Comments { get; set; }

        public string CreatedBy { get; set; }

    }
}
