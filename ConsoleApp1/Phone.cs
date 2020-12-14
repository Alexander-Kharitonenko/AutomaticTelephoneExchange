﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    
     public delegate void MessageHandlerPhone(object o , MobileProviderEventArgs e); //делегат обработки событий

   public class Phone<T> where T : MobileProvider  
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

 

        public void GetAllCalls(Client client)
        {
            
            foreach (var i in SimCard.InformationCall)
            {
                Console.WriteLine($"{client.Name} call date { i.Item2} - duration {i.Item1} minutes ");
            }
        }

        public void Message(object o, MobileProviderEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }

    
}
