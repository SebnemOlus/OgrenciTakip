using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciTakip.Models.Bolumler
{
    public class Branch
    {
        public int BolumId { get; set; }
        public string BolumAdi { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Website { get; set; }
        public byte[] BolumResmi { get; set; }
        public string AdressLine { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string PostCode { get; set; }
        public string CreatedBy { get; set; }

        

    }
}
