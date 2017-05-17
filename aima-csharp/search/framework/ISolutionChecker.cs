using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework.problem;

namespace aima.core.search.framework
{
    /// <summary>
    /// A specialization of the GoalTest interface so that it is possible to check
    /// the solution once a Goal has been identified to determine if it is
    /// acceptable. 
    /// </summary>
    /// <remarks>
    /// This allows you to continue searching for alternative solutions
    /// without having to restart the search.
    /// 
    /// However, care needs to be taken when doing this as it does not always make
    /// sense to continue with a search once an initial goal is found, for example if
    /// using a heuristic targeted at a single goal.
    /// </remarks>
    /// <author>Ciaran O'Reilly</author>
    public interface ISolutionChecker : IGoalTest
    {
        /// <summary>
        /// This method is only called if 
        /// <see cref="IGoalTest.IsGoalState(object)"/>
        /// returns true.
        /// </summary>
        /// <param name="actions">the list of actions to get to the goal state.</param>
        /// <param name="goal">the goal the list of actions will reach.</param>
        /// <returns>
        /// true if the solution is acceptable, false otherwise, which
        /// indicates the search should be continued.
        /// </returns>
        bool IsAcceptableSolution(List<Action> actions, System.Object goal);
    }
}