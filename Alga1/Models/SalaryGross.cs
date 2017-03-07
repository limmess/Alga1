using System;

namespace Alga1.Models
{
    public class SalaryGross
    {
        public static decimal Gross(decimal salaryNet, int childrenNo, bool raisesAlone)
        {
            decimal pnpd = 100;
            if (raisesAlone == true)
            {
                pnpd = 200;
            }

            decimal x = (salaryNet - 75 - 0.15m * childrenNo * pnpd) / 0.685m;

            decimal npd = 310 - 0.5m * (x - 380m);
            if (npd >= 310m) npd = 310m;
            if (npd <= 0m) npd = 0m;

            decimal s = (0.15m * salaryNet - 0.1365m * (npd + pnpd * childrenNo)) / 0.76m;
            if (s <= 0m) s = 0m;

            decimal gross = (salaryNet + s) / 0.91m;
            decimal grossRound = Math.Round(gross, 2, MidpointRounding.AwayFromZero);
            return grossRound;
        }
    }
}


//Gyventojui taikytino NPD dydžio formulė(2017):

//NPD = Pagrindinis NPD + Papildomas NPD(PNPD)

//Pagrindinis NPD apskaičiuojamas taip:

//1) Jei asmens darbo užmokestis(„ant popieriaus“) yra didesnis negu 380 EUR(MMA, galiojantis einamųjų metų pirmąją dieną), tuomet neapmokestinamų pajamų dydis  skaičiuojamas pagal formulę:
//pagrindinis NPD = 310 EUR - 0,5 x(‘Mėnesinis darbo užmokestis‘ - 380 EUR)

//2) Jei mėnesinis darbo užmokestis „ant popieriaus“ bus didesnis nei 1 000 EUR, pagrindinis NPD bus lygus 0 (kitaip sakant, jis negali būti neigiamas).

//3) Jei mėnesinis darbo užmokestis(„ant popieriaus“) yra ne didesnis nei 380 EUR(MMA, galiojantis einamųjų metų pirmąją dieną), tuomet taikomas mėnesio NPD yra lygus 310 EUR.


//    'Papildomas neapmokestinamų pajamų' (PNPD) apskaičiuojamas taip:

//PNPD = (200 EUR x 'Auginamų vaikų skaičius‘) / 'Augintojų skaičius‘