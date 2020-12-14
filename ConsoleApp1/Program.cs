using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
       
        static void Main(string[] args)
        {
            MobileProvider mobileProvider = new MobileProvider();
            mobileProvider.Notify += mobileProvider.Message;
            mobileProvider.NotifyPort += mobileProvider.Message;

            Phone<MobileProvider> samsung = new Phone< MobileProvider > (mobileProvider, true);// можно включать выключать симку true/false
            Phone<MobileProvider> IPhone11 = new Phone<MobileProvider>(mobileProvider, true);// можно включать выключать симку true/false
            Phone<MobileProvider> Huawei = new Phone<MobileProvider>(mobileProvider, true);// можно включать выключать симку true/false
            samsung.Notify += samsung.Message;
            IPhone11.Notify += IPhone11.Message;
            Huawei.Notify += Huawei.Message;

            Client client1 = new Client("Вася", "Пупкин", 18, samsung); //создаём клиента
            Client client2 = new Client("Баба Валя", "Пупкина", 18, IPhone11);//создаём клиента
            Client client3 = new Client("Иришка", "Пупкина", 18, Huawei);//создаём клиента




            
            mobileProvider.SignContract(client1); // заключаем контракты с Вася
            mobileProvider.SignContract(client2); // заключаем контракты с Баобой валей
            mobileProvider.SignContract(client3); // заключаем контракты с Иришкой


            client1.Phone.AddContacts(client2);// Вася добавил контакт Баба Валя
            client2.Phone.AddContacts(client1);// Баба Валя добавил контакт Вася
            client3.Phone.AddContacts(client1);// Иришка добавил контакт Вася
            client3.Phone.AddContacts(client2);// Иришка добавил контакт Баба Валя

            

            ITariffSwollenEar tarif1 = samsung.SimCard;
            tarif1.PaymentOfTariff(client1, tarif1); //оплата первого тарифа

            ITarifTiredTongue tarif2 = IPhone11.SimCard;
            tarif2.PaymentOfTariff(client2, tarif2);// оплата второго тарифа


            client1.Phone.GetAllCalls(client1); // Все звонки Васи

            mobileProvider.ClientConnectionPort(client1, client2, client3); // порт по которому созваниваються


            Console.ReadKey();

        }
    }
}
