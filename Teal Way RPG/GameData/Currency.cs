using System;
using System.Collections.Generic;

namespace Teal_Way_RPG.GameData
{
    public class Currency
    {
        public int SimpleCurrencyId { get; }
        // ## of implementation
        // C code
        // currency type code
        public string CurrencyId { get; }
        public string CurrencyName { get; }
        public string CurrencyMultipleName { get; }
        public ConsoleColor ColorFore { get; }
        //public ConsoleColor ColorBack { get; }
        public int CurrencyGoldBasedRate { get; }
        public bool IsExchangeable { get; }

        public static string BaseIdFormat = "####";

        public Currency(int simpleCurrencyId, string currencyId, string currencyName, string currencyMultipleName, ConsoleColor colorFore, int currencyGoldBasedRate = 0)
        {
            SimpleCurrencyId = simpleCurrencyId;
            CurrencyId = currencyId;
            CurrencyName = currencyName;
            CurrencyMultipleName = currencyMultipleName;
            ColorFore = colorFore;
            //ColorBack = colorBack;
            if (currencyGoldBasedRate != 0)
            {
                CurrencyGoldBasedRate = currencyGoldBasedRate;
            }
            else
            {
                IsExchangeable = false;
            }
        }

        public Currency(string currencyId, string currencyName)
        {
            CurrencyId = currencyId;
            CurrencyName = currencyName;
        }
    }

    public class Currencies
    {
        public static Currency DefaultCurrency = new Currency("00CN", "Default");
        public static Currency Gold = new Currency(1, "01CG", "Gold", "Gold", ConsoleColor.Yellow);
        public static Currency Shard = new Currency(2, "02CS", "Shard", "Shards", ConsoleColor.Cyan);
        public static Currency Blood = new Currency(3, "03CB", "Blood", "Blood", ConsoleColor.DarkRed);

        public static List<Currency> CurrencyList = new List<Currency>();

        public static List<string> CurrencyIdList = new List<string>();

        public static List<Currency> CurrencyListBuilder(params Currency[] currencies)
        {
            foreach (var currency in currencies)
            {
                CurrencyList.Add(currency);
            }

            return CurrencyList;
        }

        public static List<string> CurrencyIdListBuilder(params Currency[] currencies)
        {
            foreach (var currency in currencies)
            {
                CurrencyIdList.Add(currency.CurrencyId);
            }

            return CurrencyIdList;
        }

        public static Currency GetCurrencyById(string currencyId)
        {
            foreach (var currency in CurrencyList)
            {
                if (currency.CurrencyId == currencyId)
                    return currency;
            }

            //throw new Exception($"Unknown CurrencyId:{currencyId} was transferred!");
            Utils.TryCatch("GameData.Currencies.GetCurrencyById", currencyId);
            return DefaultCurrency;
        }

        public static Currency GetCurrencyByName(string currencyName)
        {
            foreach (var currency in CurrencyList)
            {
                if (currency.CurrencyName == currencyName)
                    return currency;
            }

            //throw new Exception($"Unknown CurrencyName:{currencyName} was transferred!");
            Utils.TryCatch("GameData.Currencies.GetCurrencyByName", currencyName);
            return DefaultCurrency;
        }
    }

    public class CurrencyProcessor
    {
        public static void ProcessCurrencyText(string currencyName)
        {
            var currency = Currencies.GetCurrencyByName(currencyName);
            Utils.SetColorsTo(currency.ColorFore);
            Utils.Cw($" {currencyName} ");
            Utils.SetColorsToDefault();
        }

        public static void CurrencyInitializer()
        {
            Currencies.CurrencyListBuilder(
                Currencies.DefaultCurrency,
                Currencies.Gold,
                Currencies.Shard,
                Currencies.Blood);

            Currencies.CurrencyIdListBuilder(
                Currencies.DefaultCurrency,
                Currencies.Gold,
                Currencies.Shard,
                Currencies.Blood);
        }
    }
}

