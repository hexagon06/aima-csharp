using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework.problem;

namespace aima.core.search.framework
{
    /// <summary>
    /// Instances of this class are responsible for node creation and expansion. They
    /// compute path costs, support progress tracing, and count the number of
    /// <see cref="Expand(Node, Problem)"/> calls.
    /// </summary>
    /// <author>Ruediger Lunde</author>
    public class NodeExpander
    {
        // expanding nodes

        public Node CreateRootNode(System.Object state)
        {
            return new Node(state);
        }

        /// <summary>
        /// Computes the path cost for getting from the root node state via the
        /// parent node state to the specified state, creates a new node for the
        /// specified state, adds it as child of the provided parent, and returns it.
        /// </summary>
        public Node CreateNode(System.Object state, Node parent, Action action, double stepCost)
        {
            return new Node(state, parent, action, parent.GetPathCost() + stepCost);
        }

        /// <summary>
        /// Returns the children obtained from expanding the specified node in the specified problem.
        /// </summary>
        /// <param name="node">the node to expand</param>
        /// <param name="problem">the problem the specified node is within.</param>
        /// <returns>
        /// the children obtained from expanding the specified node in the
        /// specified problem.
        /// </returns>
        public List<Node> Expand(Node node, Problem problem)
        {
            List<Node> successors = new List<Node>();

            IActionsFunction actionsFunction = problem.GetActionsFunction();
            IResultFunction resultFunction = problem.GetResultFunction();
            IStepCostFunction stepCostFunction = problem.GetStepCostFunction();

            foreach (Action action in actionsFunction.Actions(node.GetState()))
            {
                System.Object successorState = resultFunction.Result(node.GetState(), action);

                double stepCost = stepCostFunction.Calculate(node.GetState(), action, successorState);
                successors.Add(CreateNode(successorState, node, action, stepCost));
            }

            foreach (INodeListener listener in nodeListeners)
            {
                listener.OnNodeExpanded(node);
            }
            counter++;
            return successors;
        }

        // progress tracing and statistical data

        /// <summary>
        /// Interface for progress Tracers
        /// </summary>
        public interface INodeListener
        {
            void OnNodeExpanded(Node node);
        }

        /// <summary>
        /// All node listeners added to this list get informed whenever a node is
        /// expanded.
        /// </summary>
        private List<INodeListener> nodeListeners = new List<INodeListener>();

        /// <summary>
        /// Counts the number of <see cref="Expand(Node, Problem)"/> calls.
        /// </summary>
        private int counter;

        /// <summary>
        /// Adds a listener to the list of node listeners. It is informed whenever a
        /// node is expanded during search.
        /// </summary>
        public void AddNodeListener(INodeListener listener)
        {
            nodeListeners.Add(listener);
        }

        /// <summary>
        /// Resets the counter for {@link #expand(Node, Problem)} calls.
        /// </summary>
        public void ResetCounter()
        {
            counter = 0;
        }

        /// <summary>
        /// Returns the number of <see cref="Expand(Node, Problem)"/> calls since the last
        /// counter reset.
        /// </summary>
        public int GetNumOfExpandCalls()
        {
            return counter;
        }
    }
}