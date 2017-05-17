using System.Collections.Generic;
using aima.core.agent;

namespace aima.core.search.framework.problem
{
    /// <summary>
    /// Returns one for every action.
    /// </summary>
    /// <author>Ravi Mohan</author>
    public class DefaultStepCostFunction : IStepCostFunction
    {
        public double Calculate(System.Object stateFrom, Action action, System.Object stateTo)
        {
            return 1;
        }
    }
}