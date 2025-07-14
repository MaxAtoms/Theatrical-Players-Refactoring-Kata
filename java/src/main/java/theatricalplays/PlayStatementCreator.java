package theatricalplays;

public class PlayStatementCreator {
	private final StatementPrinter printer;

	public PlayStatementCreator(StatementPrinter printer) {
		this.printer = printer;
	}

	public String createStatement(Invoice invoice) {
		var volumeCredits = invoice.performances().stream()
				.mapToInt(Performance::getVolumeCredit)
				.sum();

		return printer.print(invoice.performances(), invoice.customer(), volumeCredits);
	}
}
