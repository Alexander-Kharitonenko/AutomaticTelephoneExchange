using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    
     public delegate void MessageHandlerPhone(object o , MobileProviderEventArgs e); //делегат обработки событий

   public class Phone<T> : ITariffSwollenEar, ITiredTongue
                 where T : MobileProvider  
    {
       

        public Phone(T SimCard, bool UseSim) 
        {
            this.SimCard = SimCard;
            this.UseSim = UseSim;
        }

        private readonly string Vote1 = @"C:\Users\Александр\Desktop\С#\Проект26 - ДЗ Делегаты\ConsoleApp1\ConsoleApp1\Data\Голос парня1.mp3";
        private readonly string Vote2 = @"C:\Users\Александр\Desktop\С#\Проект26 - ДЗ Делегаты\ConsoleApp1\ConsoleApp1\Data\Голос бабушки1.mp3";

        public bool UseSim;

        public T SimCard;

        Dictionary<Client, ulong> Contacts = new Dictionary<Client, ulong>();

        List<(uint, string)> InformationCall = new List<(uint, string)>();


        public event MessageHandlerPhone Notify;




        public void AddContacts(Client client) // добавичь человека в список контактов каждый человек соответствует определённому номеру
        {


                (bool, ulong) Resuly = SimCard.GetClient(client);

                if (Resuly.Item1 & Contacts.All(i => i.Value != Resuly.Item2))
                {
                   
                    Contacts.Add(client, Resuly.Item2);
                    Notify?.Invoke(this, new MobileProviderEventArgs($"{client.Name} added to your contact list ")); //Евент
                }
                else
                {
                    Notify?.Invoke(this, new MobileProviderEventArgs($"Contact name { client.Name} not added to contact list ")); //Евент
                }
            
           
        }

        public void RemoveContacts(Client person) // удалить человека из списков контактов 
        {
  
            if (Contacts.Any(i => i.Key.Name == person.Name & i.Key.Surname == person.Surname))
            {
                Contacts.Remove(person);
               
            }          
            else
            {
                Notify?.Invoke(this, new MobileProviderEventArgs("The client does not exist"));
            }
            Contacts.OrderBy(i => i.Key.Name);
        }

        public void GetNumberAddContacts(Client client) //получить одного клиента из списка контактов или все записанные в телефоне номера
        {
            ulong Number;
            if (client != null & Contacts.Any(i => i.Key.Name == client.Name))
            {
                Number = Contacts[client];
                Notify?.Invoke(this, new MobileProviderEventArgs($"Name - {client.Name} Phome - {Number}"));
            }
            else 
            {
                Console.WriteLine($"Contact not found, here is a list of all contacts");
                foreach (var i in Contacts) 
                {
                    Notify?.Invoke(this, new MobileProviderEventArgs($"Name - {i.Key.Name} Phone - {i.Value}"));
                }
            }
            Contacts.OrderBy(i => i.Key.Name);
        }

      

        public void Call1 (Client Contact ) 
        {

            (bool, ulong) Resuly = SimCard.GetClient(Contact);

            if (Resuly.Item1 && UseSim)
            {
                
               
                Contact.Thread1.Start(Vote1);
                Thread.Sleep(2000);





                SimCard.CallDateTime();// длительность разговора 
                Notify?.Invoke(this, new MobileProviderEventArgs($"Call in progress { SimCard.CallDateTime().Item2} subscriber {Contact.Name}"));



            }
            else
            {
                Notify?.Invoke(this, new MobileProviderEventArgs($"Connection with {Contact.Name} failed, maybe the SIM card is not included or the contract is not concluded")); //Евент
            }

            
        }
        public void Call2(Client Contact)
        {

            (bool, ulong) Resuly = SimCard.GetClient(Contact);

            if (Resuly.Item1 && UseSim)
            {
                
                Contact.Thread2.Start(Vote2);
               
               
                




                SimCard.CallDateTime();// длительность разговора 
                Notify?.Invoke(this, new MobileProviderEventArgs($"Call in progress { SimCard.CallDateTime().Item2} subscriber {Contact.Name}"));



            }
            else
            {
                Notify?.Invoke(this, new MobileProviderEventArgs($"Connection with {Contact.Name} failed, maybe the SIM card is not included or the contract is not concluded")); //Евент
            }


        }





        // ТАРИФНЫЕ ПЛАНЫ

        uint ITariffSwollenEar.СostMinutes(Client client) // стоимость тариф за месяц
        {
            uint result = 0;
            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    InformationCall.Add(SimCard.CallDateTime());

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

        void ITariffSwollenEar.PaymentOfTariff(Client client , ITariffSwollenEar tariff)//оплата тарифа
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


        uint ITiredTongue.СostMinutes(Client client) // стоимость тариф за месяц
        {
            uint result = 0;
            if (client != null)
            {
                for (int i = 0; i < 30; i++)
                {
                    InformationCall.Add(SimCard.CallDateTime());

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

        void ITiredTongue.PaymentOfTariff(Client client, ITiredTongue tariff)//оплата тарифа
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

        public void GetAllCalls(Client client)
        {
            
            foreach (var i in InformationCall)
            {
                Console.WriteLine($"{client.Name} call date { i.Item2} - duration {i.Item1} minutes ");
            }
        }
    }
}
