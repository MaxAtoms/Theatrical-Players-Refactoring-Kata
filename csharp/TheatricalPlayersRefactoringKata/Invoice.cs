using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata
{
    public record Invoice(string Customer, List<Performance> Performances);
}
