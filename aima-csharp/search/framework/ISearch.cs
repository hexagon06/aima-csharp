using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework.problem;

namespace aima.core.search.framework
{
    /// <author>Ravi Mohan</author>
    /// <author>Mike Stampone</author>
    public interface ISearch
    {
        /// <summary>
        /// Returns a list of actions to the goal if the goal was found, a list
        /// containing a single NoOp Action if already at the goal, or an empty list
        /// if the goal could not be found.
        /// </summary>
        /// <param name="p">the search problem</param>
        /// <returns>
        /// a list of actions to the goal if the goal was found, a list
        /// containing a single NoOp Action if already at the goal, or an
        /// empty list if the goal could not be found.
        /// </returns>
        List<Action> Search(Problem p);

        /// <summary>
        /// Returns all the metrics of the search.
        /// </summary>
        /// <returns>all the metrics of the search.</returns>
        Metrics GetMetrics();
    }
}