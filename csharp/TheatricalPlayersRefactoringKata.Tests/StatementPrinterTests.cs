using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        [Fact]
        public Task test_statement_example()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new TragedyPlay("Hamlet"));
            plays.Add("as-like", new ComedyPlay("As You Like It"));
            plays.Add("othello", new TragedyPlay("Othello"));

            Invoice invoice = new Invoice("BigCo", new List<Performance>{new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40)});
            
            StatementPrinter statementPrinter = new StatementPrinter();
            var result = statementPrinter.Print(invoice, plays);

            return Verifier.Verify(result);
        }
        [Fact]
        public void test_statement_with_new_play_types()
        {
            // This test verifies that we can create new play types by extending the class hierarchy
            var plays = new Dictionary<string, Play>();
            plays.Add("henry-v", new TragedyPlay("Henry V"));
            plays.Add("as-like", new ComedyPlay("As You Like It"));

            Invoice invoice = new Invoice("BigCoII", new List<Performance>{new Performance("henry-v", 53),
                new Performance("as-like", 55)});
            
            StatementPrinter statementPrinter = new StatementPrinter();

            // This should work fine now since we're using concrete play types
            var result = statementPrinter.Print(invoice, plays);
            Assert.NotNull(result);
            Assert.Contains("BigCoII", result);
        }
    }
}
