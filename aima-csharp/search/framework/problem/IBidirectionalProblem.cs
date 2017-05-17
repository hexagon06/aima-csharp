using System.Collections.Generic;

namespace aima.core.search.framework.problem
{
    /// <summary>
    /// An interface describing a problem that can be tackled from both directions at
    /// once (i.e InitialState<->Goal).
    /// </summary>
    /// <author>Ciaran O'Reilly</author>
    public interface IBidirectionalProblem
    {
        Problem GetOriginalProblem();

        Problem GetReverseProblem();
    }
}