using System;

namespace TheatricalPlayersRefactoringKata
{
    public abstract class Play
    {
        public string Name { get; set; }
        public abstract string Type { get; }

        public Play(string name)
        {
            Name = name;
        }

        public abstract int AmountFor(Performance performance);
        public abstract int VolumeCreditsFor(Performance performance);
    }

    public class TragedyPlay : Play
    {
        private const int TRAGEDY_BASE_AMOUNT = 40000;
        private const int TRAGEDY_AUDIENCE_THRESHOLD = 30;
        private const int TRAGEDY_ADDITIONAL_RATE = 1000;
        private const int VOLUME_CREDITS_THRESHOLD = 30;

        public override string Type => "tragedy";

        public TragedyPlay(string name) : base(name)
        {
        }

        public override int AmountFor(Performance performance)
        {
            var amount = TRAGEDY_BASE_AMOUNT;
            if (performance.Audience > TRAGEDY_AUDIENCE_THRESHOLD)
            {
                amount += TRAGEDY_ADDITIONAL_RATE * (performance.Audience - TRAGEDY_AUDIENCE_THRESHOLD);
            }
            return amount;
        }

        public override int VolumeCreditsFor(Performance performance)
        {
            return Math.Max(performance.Audience - VOLUME_CREDITS_THRESHOLD, 0);
        }
    }

    public class ComedyPlay : Play
    {
        private const int COMEDY_BASE_AMOUNT = 30000;
        private const int COMEDY_AUDIENCE_THRESHOLD = 20;
        private const int COMEDY_ADDITIONAL_FIXED = 10000;
        private const int COMEDY_ADDITIONAL_RATE = 500;
        private const int COMEDY_AUDIENCE_RATE = 300;
        private const int VOLUME_CREDITS_THRESHOLD = 30;
        private const int COMEDY_VOLUME_CREDITS_DIVISOR = 5;

        public override string Type => "comedy";

        public ComedyPlay(string name) : base(name)
        {
        }

        public override int AmountFor(Performance performance)
        {
            var amount = COMEDY_BASE_AMOUNT;
            if (performance.Audience > COMEDY_AUDIENCE_THRESHOLD)
            {
                amount += COMEDY_ADDITIONAL_FIXED + COMEDY_ADDITIONAL_RATE * (performance.Audience - COMEDY_AUDIENCE_THRESHOLD);
            }
            amount += COMEDY_AUDIENCE_RATE * performance.Audience;
            return amount;
        }

        public override int VolumeCreditsFor(Performance performance)
        {
            var volumeCredits = Math.Max(performance.Audience - VOLUME_CREDITS_THRESHOLD, 0);
            // add extra credit for every ten comedy attendees
            volumeCredits += (int)Math.Floor((decimal)performance.Audience / COMEDY_VOLUME_CREDITS_DIVISOR);
            return volumeCredits;
        }
    }
}
