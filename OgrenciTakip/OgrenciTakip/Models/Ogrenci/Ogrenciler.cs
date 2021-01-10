using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciTakip.Ekran.Ogrenci
{
    public class Ogrenciler
    {
        public int OgrenciNo { get; set; }
        public string AdiveSoyadi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string TcNo { get; set; }
        public string EmailAddress { get; set; }
        public string Cep { get; set; }
        public string Telefon { get; set; }
        public int CinsiyetId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int BolumId { get; set; }
        public byte[] Photo { get; set; }

        public string AddressLine { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string PostCode { get; set; }

        public string CreatedBy { get; set; }
    }
}
