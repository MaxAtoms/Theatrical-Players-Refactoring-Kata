package theatricalplays;

import java.util.List;
import java.util.Map;

public class StatementCalculator {
	private StatementCalculator() {}

	public record PlayStatementInfo(String name, int amount, int audience) {}

	public static List<PlayStatementInfo> getPlayInfos(List<Performance> performances, Map<String, Play> plays) {
		return performances.stream().map(p -> {
			var play = plays.get(p.playID());
			return new PlayStatementInfo(play.name(), calculateSingleAmount(p, play), p.audience());
		}).toList();
	}

	private static int calculateSingleAmount(Performance perf, Play play) {
		switch (play.type()) {
			case TRAGEDY -> {
				var audienceExtra = perf.audience() > 30
						? 1_000 * (perf.audience() - 30)
						: 0;
				return 40_000 + audienceExtra;
			}
			case COMEDY -> {
				var audienceExtra = perf.audience() > 20
						? 10_000 + 500 * (perf.audience() - 20)
						: 0;
				return 30_000 + 300 * perf.audience() + audienceExtra;
			}
			default -> throw new Error("unknown type: ${play.type}");
		}
	}

	public static int calculateVolumeCredits(List<Performance> performances, Map<String, Play> plays) {
		return performances.stream()
				.mapToInt(p -> calculateSingleVolumeCredit(p, plays.get(p.playID())))
				.sum();
	}

	private static int calculateSingleVolumeCredit(Performance perf, Play play) {
		var comedyAttendeesExtra = play.type().equals(PlayType.COMEDY) ? (perf.audience() / 5) : 0;
		return Math.max(perf.audience() - 30, 0) + comedyAttendeesExtra;
	}
}
