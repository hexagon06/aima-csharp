using System;
using System.Collections.Generic;

namespace aima.core.search.framework.problem
{
    /// <summary>
    /// A problem can be defined formally by five components: 
    /// <ul>
    /// <li>The<b> initial state</b> that the agent starts in.</li>
    /// <li>A description of the possible<b> actions</b> available to the agent.
    /// Given a particular state s, ACTIONS(s) returns the set of actions that can be
    /// executed in s.</li>
    /// <li>A description of what each action does; the formal name for this is the
    /// <b>transition model, specified by a function RESULT(s, a) that returns the
    /// state that results from doing action a in state s.</b></li>
    /// <li>The <b>goal test</b>, which determines whether a given state is a goal
    /// state.</li>
    /// <li>A <b>path cost</b> function that assigns a numeric cost to each path. The
    /// problem-solving agent chooses a cost function that reflects its own
    /// performance measure. The <b>step cost</b> of taking action a in state s to
    /// reach state s' is denoted by c(s,a,s')</li>
    /// </ul>
    /// </summary>
    /// <remarks>Artificial Intelligence A Modern Approach (3rd Edition): page 66.</remarks>
    /// <author>Ravi Mohan</author>
    /// <author>Ciaran O'Reilly</author>
    /// <author>Mike Stampone</author>
    public class Problem
    {
        protected Object initialState;

        protected IActionsFunction actionsFunction;

        protected IResultFunction resultFunction;

        protected IGoalTest goalTest;

        protected IStepCostFunction stepCostFunction;
        
        /// <summary>
        /// Constructs a problem with the specified components, and a default step cost function (i.e. 1 per step).
        /// </summary>
        /// <param name="initialState">the initial state that the agent starts in.</param>
        /// <param name="actionsFunction">a description of the possible actions available to the agent.</param>
        /// <param name="resultFunction">a description of what each action does; the formal name for
        /// this is the transition model, specified by a function
        /// RESULT(s, a) that returns the state that results from doing
        /// action a in state s.</param>
        /// <param name="goalTest">test determines whether a given state is a goal state.</param>
        public Problem(Object initialState, IActionsFunction actionsFunction,
                IResultFunction resultFunction, IGoalTest goalTest)
            : this(initialState, actionsFunction, resultFunction, goalTest,
                new DefaultStepCostFunction())
        {

        }
        
        /// <summary>
        /// Constructs a problem with the specified components, which includes a step
	    /// cost function.
        /// </summary>
        /// <param name="initialState">the initial state of the agent.</param>
        /// <param name="actionsFunction">a description of the possible actions available to the agent.</param>
        /// <param name="resultFunction">
        /// a description of what each action does; the formal name for
	    /// this is the transition model, specified by a function
	    /// RESULT(s, a) that returns the state that results from doing
	    /// action a in state s.</param>
        /// <param name="goalTest">test determines whether a given state is a goal state.</param>
        /// <param name="stepCostFunction">
        /// a path cost function that assigns a numeric cost to each path.
	    /// The problem-solving-agent chooses a cost function that
	    /// reflects its own performance measure.</param>
        public Problem(Object initialState, IActionsFunction actionsFunction,
               IResultFunction resultFunction, IGoalTest goalTest,
               IStepCostFunction stepCostFunction)
        {
            this.initialState = initialState;
            this.actionsFunction = actionsFunction;
            this.resultFunction = resultFunction;
            this.goalTest = goalTest;
            this.stepCostFunction = stepCostFunction;
        }
        
        /// <summary>
        /// Returns the initial state of the agent.
        /// </summary>
        /// <returns>the initial state of the agent.</returns>
        public Object GetInitialState()
        {
            return initialState;
        }
        
        /// <summary>
        /// Returns <code>true</code> if the given state is a goal state.
        /// </summary>
        /// <param name="state">a particular state</param>
        /// <returns><code>true</code> if the given state is a goal state.</returns>
        public bool IsGoalState(Object state)
        {
            return goalTest.IsGoalState(state);
        }
        
        /// <summary>
        /// Returns the goal test.
        /// </summary>
        /// <returns>the goal test.</returns>
        public IGoalTest GetGoalTest()
        {
            return goalTest;
        }
        
        /// <summary>
        /// Returns the description of the possible actions available to the agent.
        /// </summary>
        /// <returns>the description of the possible actions available to the agent.</returns>
        public IActionsFunction GetActionsFunction()
        {
            return actionsFunction;
        }

        /// <summary>
        /// Returns the description of what each action does.
        /// </summary>
        /// <returns>the description of what each action does.</returns>
        public IResultFunction GetResultFunction()
        {
            return resultFunction;
        }

        /// <summary>
        /// Returns the path cost function.
        /// </summary>
        /// <returns>the path cost function.</returns>
        public IStepCostFunction GetStepCostFunction()
        {
            return stepCostFunction;
        }

        // PROTECTED METHODS

        protected Problem()
        {

        }
    }
}