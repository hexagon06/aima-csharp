using System.Collections.Generic;
using aima.core.agent;
using aima.core.agent.impl;
using aima.core.util;
using aima.core.search.framework.problem;

namespace aima.core.search.framework
{
    /// <summary>
    /// A simple problem-solving agent. It first formulates a goal and a
    /// problem, searches for a sequence of actions that would solve the problem, and
    /// then executes the actions one at a time. When this is complete, it formulates
    /// another goal and starts over.
    /// </summary>
    /// <remarks>
    /// Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.1, page 67.
    /// 
    /// 
    /// <pre>
    /// function SIMPLE-PROBLEM-SOLVING-AGENT(percept) returns an action
    ///   persistent: seq, an action sequence, initially empty
    ///               state, some description of the current world state
    ///               goal, a goal, initially null
    ///               problem, a problem formulation
    ///           
    ///   state &lt;- UPDATE-STATE(state, percept)
    ///   if seq is empty then
    ///     goal    &lt;- FORMULATE-GOAL(state)
    ///     problem &lt;- FORMULATE-PROBLEM(state, goal)
    ///     seq     &lt;- SEARCH(problem)
    ///     if seq = failure then return a null action
    ///   action &lt;- FIRST(seq)
    ///   seq &lt;- REST(seq)
    ///   return action
    /// </pre>
    /// 
    /// Figure 3.1 A simple problem-solving agent. It first formulates a goal and a
    /// problem, searches for a sequence of actions that would solve the problem, and
    /// then executes the actions one at a time. When this is complete, it formulates
    /// another goal and starts over.
    /// </remarks>
    /// <author>Ciaran O'Reilly</author>
    /// <author>Mike Stampone</author>
    public abstract class SimpleProblemSolvingAgent : AbstractAgent
    {
        /// <summary>
        /// seq, an action sequence, initially empty
        /// </summary>
        private List<Action> seq = new List<Action>();

        private bool formulateGoalsIndefinitely = true;

        private int maxGoalsToFormulate = 1;

        private int goalsFormulated = 0;

        /// <summary>
        /// Constructs a simple problem solving agent which will formulate goals
        /// indefinitely.</summary>
        public SimpleProblemSolvingAgent()
        {
            formulateGoalsIndefinitely = true;
        }
        
        /// <summary>
        /// Constructs a simple problem solving agent which will formulate, at
        /// maximum, the specified number of goals.</summary>
        /// <param name="maxGoalsToFormulate">the maximum number of goals this agent is to formulate.</param>
        public SimpleProblemSolvingAgent(int maxGoalsToFormulate)
        {
            formulateGoalsIndefinitely = false;
            this.maxGoalsToFormulate = maxGoalsToFormulate;
        }

        /// <summary>
        /// function SIMPLE-PROBLEM-SOLVING-AGENT(percept) returns an action
        /// </summary>
        /// <param name="p">perception of the environment</param>
        /// <returns>an action</returns>
        public override Action Execute(Percept p)
        {
            Action action = NoOpAction.NO_OP;

            // state <- UPDATE-STATE(state, percept)
            UpdateState(p);
            // if seq is empty then do
            if (0 == seq.Count)
            {
                if (formulateGoalsIndefinitely
                        || goalsFormulated < maxGoalsToFormulate)
                {
                    if (goalsFormulated > 0)
                    {
                        NotifyViewOfMetrics();
                    }
                    // goal <- FORMULATE-GOAL(state)
                    System.Object goal = FormulateGoal();
                    goalsFormulated++;
                    // problem <- FORMULATE-PROBLEM(state, goal)
                    Problem problem = FormulateProblem(goal);
                    // seq <- SEARCH(problem)
                    seq.AddRange(Search(problem));
                    if (0 == seq.Count)
                    {
                        // Unable to identify a path
                        seq.Add(NoOpAction.NO_OP);
                    }
                }
                else
                {
                    // Agent no longer wishes to
                    // achieve any more goals
                    setAlive(false);
                    NotifyViewOfMetrics();
                }
            }

            if (seq.Count > 0)
            {
                // action <- FIRST(seq)
                action = Util.first(seq);
                // seq <- REST(seq)
                seq = Util.rest(seq);
            }

            return action;
        }

        // PROTECTED METHODS

        protected abstract State UpdateState(Percept p);

        protected abstract System.Object FormulateGoal();

        protected abstract Problem FormulateProblem(System.Object goal);

        protected abstract List<Action> Search(Problem problem);

        protected abstract void NotifyViewOfMetrics();
    }
}