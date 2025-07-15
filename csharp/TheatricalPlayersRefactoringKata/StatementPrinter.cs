using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private const int AMOUNT_DIVISOR = 100;
        
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var performance in invoice.Performances) 
            {
                var play = plays[performance.PlayID];
                var amount = play.AmountFor(performance);
                
                // print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, FormatAsCurrency(amount), performance.Audience);
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", FormatAsCurrency(TotalAmount(invoice, plays)));
            result += String.Format("You earned {0} credits\n", TotalVolumeCredits(invoice, plays));
            return result;
        }

        private int TotalAmount(Invoice invoice, Dictionary<string, Play> plays)
        {
            var total = 0;
            foreach(var performance in invoice.Performances) 
            {
                var play = plays[performance.PlayID];
                total += play.AmountFor(performance);
            }
            return total;
        }

        private int TotalVolumeCredits(Invoice invoice, Dictionary<string, Play> plays)
        {
            var total = 0;
            foreach(var performance in invoice.Performances) 
            {
                var play = plays[performance.PlayID];
                total += play.VolumeCreditsFor(performance);
            }
            return total;
        }

        private decimal FormatAsCurrency(int amount)
        {
            return Convert.ToDecimal(amount / AMOUNT_DIVISOR);
        }
    }
}
