using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteLand
{
    public class Program
    {
        public static int[] CityArray = new int[] { 0, 1, 2, 0, 0, 3, 3 };
        public static int[,] CityMatrix = new int[CityArray.Count() + 1, CityArray.Count() + 1];
        public static int ConnectionCount;
        public static int CityCount = CityArray.Count() + 1;
        public static List<Tuple<int, int>> CityConnectedIndisList = new List<Tuple<int,int>>();
        public static List<Tuple<int, int, int>> CityConnectedCountList = new List<Tuple<int, int, int>>();

        List<CityConnectedContract> CityConnectedList = new List<CityConnectedContract>();

        public static void Main1(string[] args)
        {
            var ins = new Program();
            ins.CreateCityMatrix(CityArray, CityMatrix);
            ins.CityConnectedList = ins.SetParameters();

            Console.Read();

            for (int i = 0; i < CityArray.Count() + 1; i++)
            {
                if (ins.CityConnectedList[i].IsCityConnected == true) continue; // Bağlanmış yollar varsa geç!!

                #region En az bağımlı yolu seç ve bağla
                for (int j = 0; j < CityArray.Count() + 1; j++)
                {
                    if (ins.CityConnectedList[j].IsCityConnected == true) continue; // Bağlanmış yollar varsa geç!!

                    var cnCount = 0;
                    ConnectionCount = 0;
                    if (CityMatrix[i, j] == 1)
                    {
                        ConnectionCount = 1;
                        //cnCount = ins.SearchConnectionCounts(j); // i. yolun bağlı olduğu yolların bağlı olduğu yol sayısı :) !!
                        cnCount++; // i. yolun bağlı olduğu yolların bağlı olduğu yol sayısı :) !!
                        CityConnectedCountList.Add(new Tuple<int, int, int>(i, j, cnCount));
                    }                    
                }

                var minDependentCity = CityConnectedCountList.OrderBy(x => x.Item3).First();

                ins.CityConnectedList[i].CityId = i;
                ins.CityConnectedList[i].IsCityConnected = true;
                ins.CityConnectedList[i].CityConnectTo = minDependentCity.Item3;
                ins.CityConnectedList[minDependentCity.Item3].CityId = i;
                ins.CityConnectedList[minDependentCity.Item3].IsCityConnected = true;
                ins.CityConnectedList[minDependentCity.Item3].CityConnectTo = i;
                #endregion
            }

        }

        internal void CreateCityMatrix(int[] cityArray, int[,] cityMatrix)
        {
            
            for (int i = 0; i < cityArray.Count(); i++)
            {
                cityMatrix[cityArray[i], i + 1] = 1;
                cityMatrix[i + 1, cityArray[i]] = 1;
            }

            for (int i = 0; i < cityArray.Count() + 1; i++)
            {
                for (int j = 0; j < cityArray.Count() + 1; j++)
                {
                    Console.Write(cityMatrix[i, j]);
                }
                Console.Write('\n');
            }
            Console.Read();

        }

        internal int SearchConnectionCounts(int cityIndis)
        {            
            for (int i = cityIndis+1; i < CityCount; i++)
            {
                if(CityMatrix[cityIndis,i] == 1)
                {
                    ConnectionCount++;                    
                    SearchConnectionCounts(i);
                }
            }
            return ConnectionCount;
        }

        internal int SearchSubConnnectionCounts(int indis)
        {
            for (int j = indis+1; j < CityCount; j++)
            {
                if (CityMatrix[indis, j] == 1)
                {
                    ConnectionCount++;
                    SearchSubConnnectionCounts(j);
                }
            }
            return ConnectionCount;
        }

        internal List<CityConnectedContract> SetParameters()
        {
            for (int i = 0; i < CityCount; i++)
            {
                CityConnectedList[i].CityId = i;
                CityConnectedList[i].IsCityConnected = false;
                Console.WriteLine(CityConnectedList[i]);
            }
            return CityConnectedList;
        }
    }
}
