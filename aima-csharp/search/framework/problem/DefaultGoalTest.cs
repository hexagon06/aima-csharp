using System;
using System.Collections.Generic;

namespace aima.core.search.framework.problem
{
    /// <summary>
    /// Checks whether a given state equals an explicitly specified goal state.
    /// </summary>
    /// <author>Ruediger Lunde</author>
    public class DefaultGoalTest : IGoalTest
    {
        private Object goalState;

        public DefaultGoalTest(Object goalState)
        {
            this.goalState = goalState;
        }

        public bool IsGoalState(Object state)
        {
            return goalState.Equals(state);
        }
    }
}