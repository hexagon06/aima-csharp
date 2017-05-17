using System.Collections.Generic;
using aima.core.agent;
namespace aima.core.search.framework.problem
{
    /// <summary>
    /// The <b>step cost</b> of taking action a in state s to reach state s' is
    /// denoted by c(s, a, s').
    /// </summary>
    /// <remarks>Artificial Intelligence A Modern Approach (3rd Edition): page 68.</remarks>
    /// <author>Ravi Mohan</author>
    /// <author>Ciaran O'Reilly</author>
    public interface IStepCostFunction
    {
        /// <summary>
        /// Calculate the step cost of taking action a in state s to reach state s'.
        /// </summary>
        /// <param name="s">the state from which action a is to be performed.</param>
        /// <param name="a">the action to be taken.</param>
        /// <param name="sDelta">the state reached by taking the action.</param>
        /// <returns>the cost of taking action a in state s to reach state s'.</returns>
        double Calculate(System.Object s, Action a, System.Object sDelta);
    }
}