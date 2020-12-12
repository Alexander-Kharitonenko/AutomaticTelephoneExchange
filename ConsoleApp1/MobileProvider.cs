using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{

    public delegate ulong AssignsNumberClient();//делегат генератора номеров 


    public delegate void MessageHandler(object o , MobileProviderEventArgs e); //делегат обработки событий


    public class MobileProvider  //предоставляет порты для подключения , выдаёт кажому клиенту номер , заключить договор
    {
        

        AssignsNumberClient numberClient;

  
        public event MessageHandler Notify;

        public  string DateCall { get; set; }

        private ulong Numbet { get; set; } // номер

        public byte Minutes { get; set; }

        

        Dictionary<Client, ulong> Clients = new Dictionary<Client, ulong>(); // список клиентов



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

       


        public void ClientConnectionPort(Client client1, Client client2, Client client3 = null ) // порт соединения и разговора клиентов
        {

            ulong Number1;
            ulong Number2;


            if (client1 == null && client1 == null)
            {
                Notify?.Invoke(this, new MobileProviderEventArgs($"There is nothing to connect to"));
            }
            else if (client1 != null && client2 != null && client2 == null)
            {
                Number1 = Clients[client1];// null если клиент не заключил контракт с АТС ,  пока не знаю как решить
                Number2 = Clients[client2];

                if ((Clients.Any(i => i.Value == Number1) && client1.Phone.UseSim) && (Clients.Any(i => i.Value == Number2) && client2.Phone.UseSim))//проверяем есть ли в клиенской базе номера и включена ли симка
                {
                    client1.Phone.Call1(client1); // даём голос нашим клиентам 
                    client2.Phone.Call1(client2);

                    Notify?.Invoke(this, new MobileProviderEventArgs($"{client1.Name} called in { client2.Name}"));

                }
                else
                {
                    Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));
                }

            }
            else if (client1 != null && client2 == null)
            {
                
                    
               Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

            }
            else if (client1 == null && client2 != null)
            {


                Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

            }
            else if(client1 != null && client2 != null && client3 != null) 
            {
                Number1 = Clients[client1];// null если клиент не заключил контракт с АТС ,  пока не знаю как решить
                Number2 = Clients[client2];
                if ((Clients.Any(i => i.Value == Number1) && client1.Phone.UseSim) && (Clients.Any(i => i.Value == Number2) && client2.Phone.UseSim))//проверяем есть ли в клиенской базе номера и включена ли симка
                {
                    client1.Phone.Call1(client1); // даём голос нашим клиентам 
                    client2.Phone.Call2(client2);
                    Notify?.Invoke(this, new MobileProviderEventArgs($"{client3.Name} trying to call { client1.Name} but the line is busy"));

                }
                else
                {


                    Notify?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

                }

                Console.WriteLine($"Call ended, call duration { CallDateTime().Item1}");//Евент

            }


        }



        public (uint, string ) CallDateTime()//длительность звонка и дата
        {

            (uint, string) Result = (0, null);


                Random GetNumber = new Random();
                Minutes = (byte)GetNumber.Next(1, 61);

                DateTime GetDateCall = new DateTime(2020, new Random().Next(1, 13), new Random().Next(1, 28)+1, new Random().Next(1, 12), new Random().Next(1, 55), new Random().Next(1, 55));
                DateCall =  GetDateCall.ToString();

                Result = (Minutes, DateCall);
            

            
            return Result;
        }

        
       
        

       
    }
}
