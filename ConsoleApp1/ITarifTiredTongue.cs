using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    interface ITarifTiredTongue
    {
        uint СostMinutes(Client client);// расчёт стоймости звонка тарифного плана за месяц 

        void PaymentOfTariff(Client client, ITarifTiredTongue tariff); // оплтатить тариф
    }
}
