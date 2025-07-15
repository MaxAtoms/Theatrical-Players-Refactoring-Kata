using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                var thisAmount = AmountFor(perf, play);
                
                // print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, FormatAsCurrency(thisAmount), perf.Audience);
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", FormatAsCurrency(TotalAmount(invoice, plays)));
            result += String.Format("You earned {0} credits\n", TotalVolumeCredits(invoice, plays));
            return result;
        }

        private int TotalAmount(Invoice invoice, Dictionary<string, Play> plays)
        {
            var total = 0;
            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                total += AmountFor(perf, play);
            }
            return total;
        }

        private int TotalVolumeCredits(Invoice invoice, Dictionary<string, Play> plays)
        {
            var total = 0;
            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                total += VolumeCreditsFor(perf, play);
            }
            return total;
        }

        private decimal FormatAsCurrency(int amount)
        {
            return Convert.ToDecimal(amount / 100);
        }

        private int VolumeCreditsFor(Performance perf, Play play)
        {
            var result = Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) result += (int)Math.Floor((decimal)perf.Audience / 5);
            return result;
        }

        private int AmountFor(Performance perf, Play play)
        {
            var thisAmount = 0;
            switch (play.Type) 
            {
                case "tragedy":
                    thisAmount = 40000;
                    if (perf.Audience > 30) {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    thisAmount = 30000;
                    if (perf.Audience > 20) {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            return thisAmount;
        }
    }
}
