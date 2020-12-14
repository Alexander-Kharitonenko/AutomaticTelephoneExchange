using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{

    public delegate void MessageHandlerPort(object o, MobileProviderEventArgs e); //делегат обработки 

    public class ConnectionPort
    {
        public Dictionary<Client, ulong> Clients = new Dictionary<Client, ulong>(); // список клиентов

        public event MessageHandlerPort NotifyPort;

        public byte Minutes { get; set; }
        public string DateCall { get; set; }


        public (uint, string) CallDateTime()//длительность звонка и дата
        {

            (uint, string) Result = (0, null);


            Random GetNumber = new Random();
            Minutes = (byte)GetNumber.Next(1, 61);

            DateTime GetDateCall = new DateTime(2020, new Random().Next(1, 13), new Random().Next(1, 28) + 1, new Random().Next(1, 12), new Random().Next(1, 55), new Random().Next(1, 55));
            DateCall = GetDateCall.ToString();

            Result = (Minutes, DateCall);



            return Result;
        }

        public void ClientConnectionPort(Client client1, Client client2, Client client3 = null) // порт соединения и разговора клиентов
        {

            ulong Number1;
            ulong Number2;


            if (client1 == null && client1 == null)
            {
                NotifyPort?.Invoke(this, new MobileProviderEventArgs($"There is nothing to connect to"));
            }
            else if (client1 != null && client2 != null && client2 == null)
            {
                Number1 = Clients[client1];// null если клиент не заключил контракт с АТС ,  пока не знаю как решить
                Number2 = Clients[client2];

                if ((Clients.Any(i => i.Value == Number1) && client1.Phone.UseSim) && (Clients.Any(i => i.Value == Number2) && client2.Phone.UseSim))//проверяем есть ли в клиенской базе номера и включена ли симка
                {
                    client1.Phone.Call1(client1); // даём голос нашим клиентам 
                    client2.Phone.Call2(client2);

                    NotifyPort?.Invoke(this, new MobileProviderEventArgs($"{client1.Name} called in { client2.Name}"));

                }
                else
                {
                    NotifyPort?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));
                }

            }
            else if (client1 != null && client2 == null)
            {


                NotifyPort?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

            }
            else if (client1 == null && client2 != null)
            {


                NotifyPort?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

            }
            else if (client1 != null && client2 != null && client3 != null)
            {
                Number1 = Clients[client1];// null если клиент не заключил контракт с АТС ,  пока не знаю как решить
                Number2 = Clients[client2];
                if ((Clients.Any(i => i.Value == Number1) && client1.Phone.UseSim) && (Clients.Any(i => i.Value == Number2) && client2.Phone.UseSim))//проверяем есть ли в клиенской базе номера и включена ли симка
                {
                    client1.Phone.Call1(client1); // даём голос нашим клиентам 
                    client2.Phone.Call2(client2);
                    NotifyPort?.Invoke(this, new MobileProviderEventArgs($"{client3.Name} trying to call { client1.Name} but the line is busy"));

                }
                else
                {


                    NotifyPort?.Invoke(this, new MobileProviderEventArgs($"The subscriber you are calling is temporarily unavailable"));

                }

                Console.WriteLine($"Call ended, call duration { CallDateTime().Item1}");//Евент

            }

        }

        public void Message(object o, MobileProviderEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
