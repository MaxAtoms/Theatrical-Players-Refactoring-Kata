package theatricalplays;

import java.util.List;

public interface StatementPrinter {
	String print(List<Performance> performances, String customer, int volumeCredits);
}
