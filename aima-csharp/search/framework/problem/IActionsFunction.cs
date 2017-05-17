using System.Collections.Generic;
using aima.core.agent;

namespace aima.core.search.framework.problem
{
    /// <summary>
    /// Given a particular state s, ACTIONS(s) returns the set of actions that can be
    /// executed in s. We say that each of these actions is <b>applicable</b> in s.
    /// </summary>
    /// <remarks>Artificial Intelligence A Modern Approach (3rd Edition): page 67.</remarks>
    /// <author>Ciaran O'Reilly</author>
    public interface IActionsFunction
    {
        /// <summary>
        /// Given a particular state s, returns the set of actions that can be executed in s.
        /// </summary>
        /// <param name="s">a particular state.</param>
        /// <returns>the set of actions that can be executed in s.</returns>
        HashSet<Action> Actions(System.Object s);
    }
}