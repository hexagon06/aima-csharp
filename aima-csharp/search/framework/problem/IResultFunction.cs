using System.Collections.Generic;
using aima.core.agent;

namespace aima.core.search.framework.problem
{
    /// <summary>
    /// A description of what each action does; the formal name for this is the
    /// transition model, specified by a function RESULT(s, a) that returns the state
    /// that results from doing action a in state s. We also use the term successor
    /// to refer to any state reachable from a given state by a single action.
    /// </summary>
    /// <remarks>Artificial Intelligence A Modern Approach (3rd Edition): page 67.</remarks>
    /// <author>Ravi Mohan</author>
    /// <author>Ciaran O'Reilly</author>
    public interface IResultFunction
    {
        /// <summary>
        /// Returns the state that results from doing action a in state s
        /// </summary>
        /// <param name="s">a particular state.</param>
        /// <param name="a">an action to be performed in state s.</param>
        /// <returns>the state that results from doing action a in state s.</returns>
        System.Object Result(System.Object s, Action a);
    }
}