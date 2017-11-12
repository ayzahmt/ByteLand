using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ByteLand
{
    public class ByteLandAlgorithm
    {
        //public static int[] CityArray = { 0, 1, 2, 0, 0, 3, 3 };
        public static int[] CityArray = { 0, 1, 1, 1, 1, 0, 2, 2 };
        //public static int[] CityArray = { 0, 1, 2 };

        public static int StepCount;

        public static int CityCount = CityArray.Length + 1;

        List<CityConnectedContract> CityConnectedList = new List<CityConnectedContract>();

        public static void Main(string[] args)
        {
            var ins = new ByteLandAlgorithm();            
            ins.CityConnectedList = ins.SetParameters();
            ins.CreateCityAvalilableRoads();

            ins.Process(ins.CityConnectedList);

            Console.Read();
        }

        internal List<CityConnectedContract> SetParameters()
        {
            for (var i = 0; i < CityCount; i++)
            {
                CityConnectedList.Add(new CityConnectedContract
                {
                    CityId = i, IsCityConnected = false, CityAvaliableRoadList = new List<CityConnectedContract>()
                });
            }
            return CityConnectedList;
        }

        internal void CreateCityAvalilableRoads()
        {
            var cityAvalilableRoads = new ArrayList();
            for (var i = 0; i < CityArray.Length; i++)
            {
                cityAvalilableRoads.Add(CityArray[i]);
                CityConnectedList[(int)cityAvalilableRoads[i]].CityAvaliableRoadList.Add(new CityConnectedContract { CityId = i + 1 });                
                CityConnectedList[i + 1].CityAvaliableRoadList.Add(CityConnectedList[(int)cityAvalilableRoads[i]]);                
            }
        }

        internal CityConnectedContract SelectAvailableCityToLink(List<CityConnectedContract> cityList,CityConnectedContract city)
        {
            var minCount = 999;
            var indisCity = 0;
            CityConnectedContract minCity = null;
            foreach (var item in city.CityAvaliableRoadList)
            {
                if (item.IsCityConnected) continue;

                for (var i = 0; i < cityList.Count; i++)
                {
                    if(item.CityId == cityList[i].CityId)
                    {
                        indisCity = i;
                        break;
                    }
                }

                var minItemCount = cityList[indisCity].CityAvaliableRoadList.Count;
                if (minItemCount < minCount)
                {
                    minCount = minItemCount;
                    minCity = cityList[indisCity];
                }
            }
            return minCity;
        }

        internal void Process(List<CityConnectedContract> cityList)
        {
            StepCount += 1;

            var newList = new List<CityConnectedContract>();

            foreach (var item in cityList)
            {
                if (item.IsCityConnected) continue;

                var cityConnect = SelectAvailableCityToLink(cityList,item);

                if (cityConnect != null)
                {
                    var tempSubCity = new CityConnectedContract {CityAvaliableRoadList = new List<CityConnectedContract>()};
                    tempSubCity.CityAvaliableRoadList.AddRange(cityConnect.CityAvaliableRoadList.Where(x => x.CityId != item.CityId).ToList());
                    tempSubCity.CityAvaliableRoadList.AddRange(item.CityAvaliableRoadList.Where(x => x.CityId != cityConnect.CityId).ToList());

                    newList.Add(new CityConnectedContract
                    {
                        CityId = item.CityId,
                        CityConnectTo = 0,
                        IsCityConnected = false,
                        CityAvaliableRoadList = tempSubCity.CityAvaliableRoadList
                    });

                    item.IsCityConnected = true;
                    item.CityConnectTo = cityConnect.CityId;
                    cityConnect.IsCityConnected = true;
                    cityConnect.CityConnectTo = item.CityId;
                }
                else
                {
                    newList.Add(new CityConnectedContract
                    {
                        CityId = item.CityId,
                        CityConnectTo = 0,
                        IsCityConnected = false,
                        CityAvaliableRoadList = item.CityAvaliableRoadList
                    });
                }
            }

            var isAllCityLinked = cityList.All(x => x.IsCityConnected) && newList.Count == 1;
            if(isAllCityLinked)
            {
                Console.WriteLine("City Count: " + CityCount);
                Console.Write("Minimum step count to link all city : " + StepCount);
            }
            else
            {
                Process(newList);
            }          
        }
    }
}
