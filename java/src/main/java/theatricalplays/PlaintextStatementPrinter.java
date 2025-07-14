package theatricalplays;

import theatricalplays.play.Play;

import java.text.NumberFormat;
import java.util.List;
import java.util.Locale;

public class PlaintextStatementPrinter implements StatementPrinter {
	static NumberFormat frmt = NumberFormat.getCurrencyInstance(Locale.US);

	public String print(List<Performance> performances, String customer, int volumeCredits) {
		StringBuilder result = new StringBuilder(String.format("Statement for %s%n", customer));

        for (var performance : performances) {
            result.append(String.format("  %s: %s (%s seats)%n", performance.play().name(), frmt.format(performance.getAmount() / 100), performance.audience()));
        }

		var totalAmount = performances.stream().mapToInt(Performance::getAmount).sum();
        result.append(String.format("Amount owed is %s%n", frmt.format(totalAmount / 100)));

        result.append(String.format("You earned %s credits%n", volumeCredits));
        return result.toString();
    }
}
