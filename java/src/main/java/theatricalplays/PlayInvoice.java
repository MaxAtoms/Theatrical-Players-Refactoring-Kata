package theatricalplays;

public interface PlayInvoice {
	int getAmount(int audience);

	default int getVolumeCredit(int audience) {
		return Math.max(audience - 30, 0);
	}
}
