using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface ITariffSwollenEar // Безлимит во всей сети
    {
         uint СostMinutes(Client client);// расчёт стоймости звонка тарифного плана за месяц 

         void PaymentOfTariff(Client client, ITariffSwollenEar tariff); // оплтатить тариф

    }
}
