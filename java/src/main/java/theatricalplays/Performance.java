package theatricalplays;

import theatricalplays.play.Play;

public record Performance(Play play, int audience) {
	public int getVolumeCredit() {
		return this.play.getVolumeCredit(audience);
	}

	public int getAmount() {
		return this.play.getAmount(audience);
	}
}
