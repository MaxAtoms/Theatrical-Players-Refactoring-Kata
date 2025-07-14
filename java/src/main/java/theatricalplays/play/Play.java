package theatricalplays.play;

public abstract class Play {
	private final String name;

	protected Play(String name) {
		this.name = name;
	}

	public String name() {
		return name;
	}

	public abstract int getAmount(int audience);

	public int getVolumeCredit(int audience) {
		return Math.max(audience - 30, 0);
	}
}

