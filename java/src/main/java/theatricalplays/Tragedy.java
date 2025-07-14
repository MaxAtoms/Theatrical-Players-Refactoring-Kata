package theatricalplays;

public class Tragedy extends Play {
	protected Tragedy(String name) {
		super(name);
	}

	@Override
	public int getAmount(int audience) {
		var audienceExtra = audience > 30
				? 1_000 * (audience - 30)
				: 0;
		return 40_000 + audienceExtra;
	}
}
