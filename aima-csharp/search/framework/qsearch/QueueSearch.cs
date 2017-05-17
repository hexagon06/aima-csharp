using System.Collections.Generic;
using System.Threading;
using aima.core.agent;
using aima.core.util;
using aima.core.search.framework.problem;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    /// Base class for queue-based search implementations, especially for 
    /// <see cref="TreeSearch"/>,
    /// <see cref="GraphSearch"/>
    /// and 
    /// <see cref="GraphSearchBFS"/>
    /// </summary>
    /// <author>Ravi Mohan</author>
    /// <author>Ciaran O'Reilly</author>
    /// <author>Mike Stampone</author>
    /// <author>Ruediger Lunde</author>
    public abstract class QueueSearch
    {
        public const System.String METRIC_NODES_EXPANDED = "nodesExpanded";
        public const System.String METRIC_QUEUE_SIZE = "queueSize";
        public const System.String METRIC_MAX_QUEUE_SIZE = "maxQueueSize";
        public const System.String METRIC_PATH_COST = "pathCost";

        protected readonly NodeExpander nodeExpander;
        protected Queue<Node> frontier;
        protected bool earlyGoalCheck = false;
        protected Metrics metrics = new Metrics();

        protected QueueSearch(NodeExpander nodeExpander)
        {
            this.nodeExpander = nodeExpander;
        }

        public virtual NodeExpander GetNodeExpander()
        {
            return nodeExpander;
        }

        /// <summary>
        /// Returns a list of actions to the goal if the goal was found, a list
        /// containing a single NoOp Action if already at the goal, or an empty list
        /// if the goal could not be found.This template method provides a base for
    	/// tree and graph search implementations.It can be customized by overriding
    	/// some primitive operations, especially 
        /// <see cref="AddToFrontier"/>,
        /// <see cref="RemoveFromFrontier"/>
        /// and
        /// <see cref="IsFrontierEmpty"/>
        /// </summary>
        /// <param name="problem">the search problem</param>
        /// <param name="frontier">the collection of nodes that are waiting to be expanded</param>
        /// <returns>
        /// a list of actions to the goal if the goal was found, a list
    	/// containing a single NoOp Action if already at the goal, or an
        /// empty list if the goal could not be found.</returns>
        public virtual List<Action> Search(Problem problem, Queue<Node> frontier)
        {
            this.frontier = frontier;
            ClearInstrumentation();
            // initialize the frontier using the initial state of the problem
            Node root = nodeExpander.CreateRootNode(problem.GetInitialState());
            if (earlyGoalCheck)
            {
                if (SearchUtils.IsGoalState(problem, root))
                {
                    return GetSolution(root);
                }
            }
            AddToFrontier(root);
            while (!(frontier.Count == 0))
            {
                // choose a leaf node and remove it from the frontier
                Node nodeToExpand = RemoveFromFrontier();
                // Only need to check the nodeToExpand if have not already
                // checked before adding to the frontier
                if (!earlyGoalCheck)
                {
                    // if the node contains a goal state then return the
                    // corresponding solution
                    if (SearchUtils.IsGoalState(problem, nodeToExpand))
                    {
                        return GetSolution(nodeToExpand);
                    }
                }
                // expand the chosen node, adding the resulting nodes to the
                // frontier
                foreach (Node successor in nodeExpander.Expand(nodeToExpand, problem))
                {
                    if (earlyGoalCheck)
                    {
                        if (SearchUtils.IsGoalState(problem, successor))
                        {
                            return GetSolution(successor);
                        }
                    }
                    AddToFrontier(successor)
;
                }
            }
            // if the frontier is empty then return failure
            return SearchUtils.Failure();
        }

        /// <summary>
        /// Primitive operation which inserts the node at the tail of the frontier.
        /// </summary>
        protected abstract void AddToFrontier(Node node);

        /// <summary>
        /// Primitive operation which removes and returns the node at the head of the frontier.
        /// </summary>
        /// <returns>the node at the head of the frontier.</returns>
        protected abstract Node RemoveFromFrontier();
        
        /// <summary>
        /// Primitive operation which checks whether the frontier contains not yet
        /// expanded nodes.
        /// </summary>
        protected abstract bool IsFrontierEmpty();
        
        /// <summary>
        /// Enables optimization for FIFO queue based search, especially breadth
        /// first search.
        /// </summary>
        /// <param name="state"></param>
        public void SetEarlyGoalCheck(bool state)
        {
            this.earlyGoalCheck = state;
        }

        /// <summary>
        /// Returns all the search metrics.
        /// </summary>
        /// <returns>all the search metrics.</returns>
        public virtual Metrics GetMetrics()
        {
            metrics.Set(METRIC_NODES_EXPANDED, nodeExpander.GetNumOfExpandCalls());
            return metrics;
        }

        /// <summary>
        /// Sets all metrics to zero.
        /// </summary>
        public void ClearInstrumentation()
        {
            nodeExpander.ResetCounter();
            metrics.Set(METRIC_NODES_EXPANDED, 0);
            metrics.Set(METRIC_QUEUE_SIZE, 0);
            metrics.Set(METRIC_MAX_QUEUE_SIZE, 0);
            metrics.Set(METRIC_PATH_COST, 0);
        }

        protected void UpdateMetrics(int queueSize)
        {
            metrics.Set(METRIC_QUEUE_SIZE, queueSize);
            int maxQSize = metrics.GetInt(METRIC_MAX_QUEUE_SIZE);
            if (queueSize > maxQSize)
            {
                metrics.Set(METRIC_MAX_QUEUE_SIZE, queueSize);
            }
        }

        private List<Action> GetSolution(Node node)
        {
            metrics.Set(METRIC_PATH_COST, node.GetPathCost());
            return SearchUtils.GetSequenceOfActions(node);
        }
    }
}
