package theatricalplays;

public class Comedy extends Play {
	protected Comedy(String name) {
		super(name);
	}

	@Override
	public int getAmount(int audience) {
		var audienceExtra = audience > 20
				? 10_000 + 500 * (audience - 20)
				: 0;
		return 30_000 + 300 * audience + audienceExtra;
	}

	@Override
	public int getVolumeCredit(int audience) {
		return Math.max(audience - 30, 0) + (audience / 5);
	}
}
