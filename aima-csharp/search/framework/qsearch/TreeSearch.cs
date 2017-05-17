using System.Collections.Generic;
using aima.core.search.framework;
using aima.core.search.framework.problem;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    /// This implementation is based on the template method
    /// <see cref="QueueSearch.Search(Problem, Queue{Node})"/>
    /// and provides implementations for the needed primitive operations.
    /// </summary>
    /// <remarks>
    /// Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.7, page 77.
    /// 
    /// function TREE-SEARCH(problem) returns a solution, or failure
    ///     initialize the frontier using the initial state of the problem
    ///     loop do
    ///         if the frontier is empty then return failure
    ///         choose a leaf node and remove it from the frontier
    ///         if the node contains a goal state then return the corresponding solution
    ///         expand the chosen node, adding the resulting nodes to the frontier
    /// </remarks>
    /// <author>Ravi Mohan</author>
    /// <author>Ruediger Lunde</author>
    public class TreeSearch : QueueSearch
    {
        public TreeSearch() : this(new NodeExpander())
        {

        }

        public TreeSearch(NodeExpander nodeExpander) : base(nodeExpander)
        {

        }

        /// <summary>
        /// Inserts the node at the tail of the frontier.
        /// </summary>
        protected override void AddToFrontier(Node node)
        {
            frontier.Enqueue(node);
            UpdateMetrics(frontier.Count);
        }

        /// <summary>
        /// Removes and returns the node at the head of the frontier.
        /// </summary>
        /// <returns>the node at the head of the frontier.</returns>
        protected override Node RemoveFromFrontier()
        {
            Node result = frontier.Dequeue();
            UpdateMetrics(frontier.Count);
            return result;
        }

        /// <summary>
        /// Checks whether the frontier contains not yet expanded nodes.
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