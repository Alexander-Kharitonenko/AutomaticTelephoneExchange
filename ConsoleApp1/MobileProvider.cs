using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{

    public delegate ulong AssignsNumberClient();//делегат генератора номеров 

    public delegate void MessageHandler(object o, MobileProviderEventArgs e); //делегат обработки 


    public class MobileProvider  : ConnectionPort, ITarifSwollenEar, ITarifTiredTongue //предоставляет порты для подключения , выдаёт кажому клиенту номер , заключить договор
    {
        

        AssignsNumberClient numberClient;

        public event MessageHandler Notify;

        private ulong Numbet { get; set; } // номер

        

        


       public List<(uint, string)> InformationCall = new List<(uint, string)>();



        private ulong GetNumber() // генератор номеров
        {
            
            ulong RegionТumber = 37529;
            Random NumberGeneration = new Random();
            Numbet = ulong.Parse(RegionТumber.ToString() + NumberGeneration.Next(1000000,8000000).ToString());
            return Numbet;

        }



        public void SignContract(Client person) // Заключаем контракт
        {
            

            if (Clients.All(i => i.Key.Name != person.Name & person.Age >= 18 & person != null))
            {

                numberClient += GetNumber;
                Clients.Add(person, numberClient());
                Notify?.Invoke(this,new MobileProviderEventArgs($"The contract with the client has been successfully concluded"));


            }
            else
            {
                
                Notify?.Invoke(this, new MobileProviderEventArgs($"The contract with the client has been successfully concluded"));

            }

           
        }



        public (bool , ulong) GetClient(Client person) // Ищим клиента в сети
        {
           
            ulong Number=0;
            if (Clients.Any( i => i.Key == person) && person != null)
            {

                Number = Clients[person];
                return (true, Number);
               

            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs($"No contract with this client"));
                return (false, Number);
               

            }
          
        }



        // ТАРИФНЫЕ ПЛАНЫ

        uint ITarifSwollenEar.СostMinutes(Client client) // стоимость тариф за месяц
        {
            uint result = 0;
            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    InformationCall.Add(CallDateTime());

                }
                InformationCall.OrderBy(i => i.Item1);
            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs("Specify a client"));

            }

            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    result += InformationCall[i].Item1;
                }

                result *= 4;
            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs("Specify a client"));

            }

            Console.WriteLine($"{client.Name} cost of calls per month - {result}P");
            return result;
        }

        void ITarifSwollenEar.PaymentOfTariff(Client client, ITarifSwollenEar tariff)//оплата тарифа
        {

            uint prise = tariff.СostMinutes(client);
            if (prise != 0)
            {
                Console.WriteLine($"cost of calls equally {prise} want to pay Y - Yes N - No ?");
                string Key = Console.ReadLine();
                try
                {
                    if (Key == "Y")
                    {
                        prise = 0;
                        Console.WriteLine("calls payment");

                    }
                    else if (Key == "N")
                    {
                        Console.WriteLine($"you refused to pay calls you owe a debt {prise}P");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }


        uint ITarifTiredTongue.СostMinutes(Client client) // стоимость тариф за месяц
        {
            uint result = 0;
            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    InformationCall.Add(CallDateTime());

                }
                InformationCall.OrderBy(i => i.Item1);
            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs("Specify a client"));

            }

            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    result += InformationCall[i].Item1;
                }

                result *= 2;
            }
            else
            {

                Notify?.Invoke(this, new MobileProviderEventArgs("Specify a client"));

            }

            Console.WriteLine($"{client.Name} cost of calls per month - {result}P");
            return result;
        }

        void ITarifTiredTongue.PaymentOfTariff(Client client, ITarifTiredTongue tariff)//оплата тарифа
        {

            uint prise = tariff.СostMinutes(client);
            if (prise != 0)
            {
                Console.WriteLine($"cost of calls equally {prise} want to pay Y - Yes N - No ?");
                string Key = Console.ReadLine();
                try
                {
                    if (Key == "Y")
                    {
                        prise = 0;
                        Console.WriteLine("calls payment");

                    }
                    else if (Key == "N")
                    {
                        Console.WriteLine($"you refused to pay calls you owe a debt {prise}P");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }


        }

    }
}
