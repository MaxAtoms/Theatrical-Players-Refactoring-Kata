package theatricalplays;

import org.junit.jupiter.api.Test;
import theatricalplays.play.Comedy;
import theatricalplays.play.Tragedy;

import java.util.List;

import static org.approvaltests.Approvals.verify;

class StatementPrinterTests {

    @Test
    void exampleStatement() {
        Invoice invoice = new Invoice("BigCo", List.of(
                new Performance(new Tragedy("Hamlet"), 55),
                new Performance(new Comedy("As You Like It"), 35),
                new Performance(new Tragedy("Othello"), 40)));

		PlayStatementCreator statementCreator = new PlayStatementCreator(new PlaintextStatementPrinter());
        var result = statementCreator.createStatement(invoice);

        verify(result);
    }

    //@Test
    //void statementWithNewPlayTypes() {
    //    Map<String, Play> plays = Map.of(
    //            "henry-v",  new Play("Henry V", PlayType.HISTORY),
    //            "as-like", new Play("As You Like It", PlayType.PASTORAL));

    //    Invoice invoice = new Invoice("BigCo", List.of(
    //            new Performance("henry-v", 53),
    //            new Performance("as-like", 55)));

    //    StatementPrinter statementPrinter = new StatementPrinter();
    //    Assertions.assertThrows(Error.class, () -> statementPrinter.print(invoice, plays));
    //}
}
