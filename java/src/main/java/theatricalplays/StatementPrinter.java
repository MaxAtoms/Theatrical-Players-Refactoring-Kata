package theatricalplays;

import java.text.NumberFormat;
import java.util.Locale;
import java.util.Map;

import static theatricalplays.StatementCalculator.getPlayInfos;
import static theatricalplays.StatementCalculator.calculateVolumeCredits;

public class StatementPrinter {
	static NumberFormat frmt = NumberFormat.getCurrencyInstance(Locale.US);

    public String print(Invoice invoice, Map<String, Play> plays) {
		var amounts = getPlayInfos(invoice.performances(), plays);
		StringBuilder result = new StringBuilder(String.format("Statement for %s%n", invoice.customer()));

        for (var o : amounts) {
            result.append(String.format("  %s: %s (%s seats)%n", o.name(), frmt.format(o.amount() / 100), o.audience()));
        }

		var totalAmount = amounts.stream().mapToInt(StatementCalculator.PlayStatementInfo::amount).sum();
        result.append(String.format("Amount owed is %s%n", frmt.format(totalAmount / 100)));

		var volumeCredits = calculateVolumeCredits(invoice.performances(), plays);
        result.append(String.format("You earned %s credits%n", volumeCredits));
        return result.toString();
    }
}
