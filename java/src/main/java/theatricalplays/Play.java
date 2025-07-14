package theatricalplays;

public abstract class Play implements PlayInvoice {
	private final String name;

	protected Play(String name) {
		this.name = name;
	}

	public String name() {
		return name;
	}
}

