using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private const int TRAGEDY_BASE_AMOUNT = 40000;
        private const int TRAGEDY_AUDIENCE_THRESHOLD = 30;
        private const int TRAGEDY_ADDITIONAL_RATE = 1000;
        
        private const int COMEDY_BASE_AMOUNT = 30000;
        private const int COMEDY_AUDIENCE_THRESHOLD = 20;
        private const int COMEDY_ADDITIONAL_FIXED = 10000;
        private const int COMEDY_ADDITIONAL_RATE = 500;
        private const int COMEDY_AUDIENCE_RATE = 300;
        
        private const int VOLUME_CREDITS_THRESHOLD = 30;
        private const int COMEDY_VOLUME_CREDITS_DIVISOR = 5;
        
        private const int AMOUNT_DIVISOR = 100;
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var performance in invoice.Performances) 
            {
                var play = plays[performance.PlayID];
                var amount = AmountFor(performance, play);
                
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
                total += AmountFor(performance, play);
            }
            return total;
        }

        private int TotalVolumeCredits(Invoice invoice, Dictionary<string, Play> plays)
        {
            var total = 0;
            foreach(var performance in invoice.Performances) 
            {
                var play = plays[performance.PlayID];
                total += VolumeCreditsFor(performance, play);
            }
            return total;
        }

        private decimal FormatAsCurrency(int amount)
        {
            return Convert.ToDecimal(amount / AMOUNT_DIVISOR);
        }

        private int VolumeCreditsFor(Performance perf, Play play)
        {
            var volumeCredits = Math.Max(perf.Audience - VOLUME_CREDITS_THRESHOLD, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / COMEDY_VOLUME_CREDITS_DIVISOR);
            return volumeCredits;
        }

        private int AmountFor(Performance perf, Play play)
        {
            var amount = 0;
            switch (play.Type) 
            {
                case "tragedy":
                    amount = TRAGEDY_BASE_AMOUNT;
                    if (perf.Audience > TRAGEDY_AUDIENCE_THRESHOLD) {
                        amount += TRAGEDY_ADDITIONAL_RATE * (perf.Audience - TRAGEDY_AUDIENCE_THRESHOLD);
                    }
                    break;
                case "comedy":
                    amount = COMEDY_BASE_AMOUNT;
                    if (perf.Audience > COMEDY_AUDIENCE_THRESHOLD) {
                        amount += COMEDY_ADDITIONAL_FIXED + COMEDY_ADDITIONAL_RATE * (perf.Audience - COMEDY_AUDIENCE_THRESHOLD);
                    }
                    amount += COMEDY_AUDIENCE_RATE * perf.Audience;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            return amount;
        }
    }
}
