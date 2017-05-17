using System.Collections.Generic;

namespace aima.core.search.framework.problem
{
    /// <summary>
    /// The goal test, which determines whether a given state is a goal state.
    /// </summary>
    /// <remarks>Artificial Intelligence A Modern Approach (3rd Edition): page 67.</remarks>
    /// <author>Ravi Mohan</author>
    /// <author>Mike Stampone</author>
    public interface IGoalTest
    {
        /// <summary>
        /// Returns <code>true</code> if the given state is a goal state.
        /// </summary>
        /// <param name="state">a particular state.</param>
        /// <returns><code>true</code> if the given state is a goal state.</returns>
        bool IsGoalState(System.Object state);
    }
}