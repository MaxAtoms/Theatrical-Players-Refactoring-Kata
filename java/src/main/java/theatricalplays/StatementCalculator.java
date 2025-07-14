package theatricalplays;

import java.util.List;
import java.util.Map;

public class StatementCalculator {
	private StatementCalculator() {}

	public record PlayStatementInfo(String name, int amount, int audience) {}

	public static List<PlayStatementInfo> getPlayInfos(List<Performance> performances, Map<String, Play> plays) {
		return performances.stream().map(p -> {
			var play = plays.get(p.playID());
			return new PlayStatementInfo(play.name(), play.getAmount(p.audience()), p.audience());
		}).toList();
	}

	public static int calculateVolumeCredits(List<Performance> performances, Map<String, Play> plays) {
		return performances.stream()
				.mapToInt(p -> plays.get(p.playID()).getVolumeCredit(p.audience()))
				.sum();
	}
}
