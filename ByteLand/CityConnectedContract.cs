using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteLand
{
    public class CityConnectedContract
    {
        public int CityId { get; set; }
        public bool IsCityConnected { get; set; }
        public int CityConnectTo { get; set; }
        public List<CityConnectedContract> CityAvaliableRoadList {get; set;}
    }
}
