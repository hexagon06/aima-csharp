using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework;
using aima.core.search.framework.problem;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    /// This implementation is based on the template method
    /// <see cref="aima.core.search.framework.qsearch.QueueSearch.Search(Problem, Queue{Node})"/>
    /// and provides implementations for the needed primitive operations.It is the most
    /// efficient variant of graph search for breadth first search.But don't expect
    /// shortest paths in combination with priority queue frontiers.
    /// </summary>
    /// <remarks>
    /// Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.7, page 77.
    /// function GRAPH-SEARCH(problem) returns a solution, or failure
    ///     initialize the frontier using the initial state of problem
    ///     initialize the explored set to be empty
    ///     loop do
    ///         if the frontier is empty then return failure
    ///         choose a leaf node and remove it from the frontier
    ///         if the node contains a goal state then return the corresponding solution
    ///         add the node to the explored set
    ///         expand the chosen node, adding the resulting nodes to the frontier
    ///             only if not in the frontier or explored set
    ///             
    /// Figure 3.7 An informal description of the general graph-search algorithm.
    /// </remarks>
    /// <author>Ravi Mohan</author>
    /// <author>Ciaran O'Reilly</author>
    /// <author>Ruediger Lunde</author>
    public class GraphSearchBFS : QueueSearch
    {
        private HashSet<object> explored = new HashSet<object>();
        private HashSet<object> frontierStates = new HashSet<object>();

        public GraphSearchBFS() : this(new NodeExpander())
        {

        }

        public GraphSearchBFS(NodeExpander nodeExpander) : base(nodeExpander)
        {

        }

        /// <summary>
        /// Clears the set of explored states and calls the search implementation of
        /// <see cref="aima.core.search.framework.qsearch.QueueSearch.Search(Problem, Queue{Node})"/>
        /// </summary>
        /// <param name="problem">the search problem</param>
        /// <param name="frontier">the collection of nodes that are waiting to be expanded</param>
        /// <returns>
        /// a list of actions to the goal if the goal was found, a list
    	/// containing a single NoOp Action if already at the goal, or an
        /// empty list if the goal could not be found.</returns>
        public override List<Action> Search(Problem problem, Queue<Node> frontier)
        {
            // Initialize the explored set to be empty
            explored.Clear();
            frontierStates.Clear();
            return base.Search(problem, frontier);
        }

        /// <summary>
        /// Inserts the node at the tail of the frontier if the corresponding state
        /// is not already a frontier state and was not yet explored.
        /// </summary>
        /// <param name="node"></param>
        protected override void AddToFrontier(Node node)
        {
            if (!explored.Contains(node.GetState()) && !frontierStates.Contains(node.GetState()))
            {
                frontier.Enqueue(node);
                frontierStates.Add(node.GetState());
                UpdateMetrics(frontier.Count);
            }
        }

        /// <summary>
        /// Removes the node at the head of the frontier, adds the corresponding
        /// state to the explored set, and returns the node.
        /// </summary>
        /// <returns>the node at the head of the frontier.</returns>
        protected override Node RemoveFromFrontier()
        {
            Node result = frontier.Dequeue();
            explored.Add(result.GetState());
            frontierStates.Remove(result.GetState());
            UpdateMetrics(frontier.Count);
            return result;
        }

        /// <summary>
        /// Checks whether there are still some nodes left.
        /// </summary>
        protected override bool IsFrontierEmpty()
        {
            if (frontier.Count == 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
