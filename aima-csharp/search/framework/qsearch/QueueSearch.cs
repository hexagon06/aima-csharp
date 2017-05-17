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

        public virtual NodeExpander getNodeExpander()
        {
            return nodeExpander;
        }

        /// <summary>
        /// Returns a list of actions to the goal if the goal was found, a list
        /// containing a single NoOp Action if already at the goal, or an empty list
        /// if the goal could not be found.This template method provides a base for
    	/// tree and graph search implementations.It can be customized by overriding
    	/// some primitive operations, especially 
        /// <see cref="addToFrontier"/>,
        /// <see cref="removeFromFrontier"/>
        /// and
        /// <see cref="isFrontierEmpty"/>
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
            clearInstrumentation();
            // initialize the frontier using the initial state of the problem
            Node root = nodeExpander.createRootNode(problem.GetInitialState());
            if (earlyGoalCheck)
            {
                if (SearchUtils.isGoalState(problem, root))
                {
                    return getSolution(root);
                }
            }
            addToFrontier(root);
            while (!(frontier.Count == 0))
            {
                // choose a leaf node and remove it from the frontier
                Node nodeToExpand = removeFromFrontier();
                // Only need to check the nodeToExpand if have not already
                // checked before adding to the frontier
                if (!earlyGoalCheck)
                {
                    // if the node contains a goal state then return the
                    // corresponding solution
                    if (SearchUtils.isGoalState(problem, nodeToExpand))
                    {
                        return getSolution(nodeToExpand);
                    }
                }
                // expand the chosen node, adding the resulting nodes to the
                // frontier
                foreach (Node successor in nodeExpander.expand(nodeToExpand, problem))
                {
                    if (earlyGoalCheck)
                    {
                        if (SearchUtils.isGoalState(problem, successor))
                        {
                            return getSolution(successor);
                        }
                    }
                    addToFrontier(successor)
;
                }
            }
            // if the frontier is empty then return failure
            return SearchUtils.failure();
        }

        /// <summary>
        /// Primitive operation which inserts the node at the tail of the frontier.
        /// </summary>
        protected abstract void addToFrontier(Node node);

        /// <summary>
        /// Primitive operation which removes and returns the node at the head of the frontier.
        /// </summary>
        /// <returns>the node at the head of the frontier.</returns>
        protected abstract Node removeFromFrontier();
        
        /// <summary>
        /// Primitive operation which checks whether the frontier contains not yet
        /// expanded nodes.
        /// </summary>
        protected abstract bool isFrontierEmpty();
        
        /// <summary>
        /// Enables optimization for FIFO queue based search, especially breadth
        /// first search.
        /// </summary>
        /// <param name="state"></param>
        public void setEarlyGoalCheck(bool state)
        {
            this.earlyGoalCheck = state;
        }

        /// <summary>
        /// Returns all the search metrics.
        /// </summary>
        /// <returns>all the search metrics.</returns>
        public virtual Metrics getMetrics()
        {
            metrics.set(METRIC_NODES_EXPANDED, nodeExpander.getNumOfExpandCalls());
            return metrics;
        }

        /// <summary>
        /// Sets all metrics to zero.
        /// </summary>
        public void clearInstrumentation()
        {
            nodeExpander.resetCounter();
            metrics.set(METRIC_NODES_EXPANDED, 0);
            metrics.set(METRIC_QUEUE_SIZE, 0);
            metrics.set(METRIC_MAX_QUEUE_SIZE, 0);
            metrics.set(METRIC_PATH_COST, 0);
        }

        protected void updateMetrics(int queueSize)
        {
            metrics.set(METRIC_QUEUE_SIZE, queueSize);
            int maxQSize = metrics.getInt(METRIC_MAX_QUEUE_SIZE);
            if (queueSize > maxQSize)
            {
                metrics.set(METRIC_MAX_QUEUE_SIZE, queueSize);
            }
        }

        private List<Action> getSolution(Node node)
        {
            metrics.set(METRIC_PATH_COST, node.getPathCost());
            return SearchUtils.getSequenceOfActions(node);
        }
    }
}
