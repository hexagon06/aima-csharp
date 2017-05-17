using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework.problem;

namespace aima.core.search.framework
{
    /**
     * Instances of this class are responsible for node creation and expansion. They
     * compute path costs, support progress tracing, and count the number of
     * {@link #expand(Node, Problem)} calls.
     * 
     * @author Ruediger Lunde
     *
     */
    public class NodeExpander
    {
        // expanding nodes

        public Node CreateRootNode(System.Object state)
        {
            return new Node(state);
        }

        /**
	 * Computes the path cost for getting from the root node state via the
	 * parent node state to the specified state, creates a new node for the
	 * specified state, adds it as child of the provided parent, and returns it.
	 */
         public Node CreateNode(System.Object state, Node parent, Action action, double stepCost)
        {
            return new Node(state, parent, action, parent.GetPathCost() + stepCost);
        }

        /**
	 * Returns the children obtained from expanding the specified node in the
    	 * specified problem.
    	 * 
    	 * @param node
    	 *            the node to expand
    	 * @param problem
    	 *            the problem the specified node is within.
    	 * 
    	 * @return the children obtained from expanding the specified node in the
    	 *         specified problem.
    	 */
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

            foreach (NodeListener listener in nodeListeners)
            {
                listener.onNodeExpanded(node);
            }
            counter++;
            return successors;
        }

        // progress tracing and statistical data

        /** Interface for progress Tracers */
        public interface NodeListener
        {
            void onNodeExpanded(Node node);
        }

        /**
	 * All node listeners added to this list get informed whenever a node is
	 * expanded.
	 */
        private List<NodeListener> nodeListeners = new List<NodeListener>();

        /** Counts the number of {@link #expand(Node, Problem)} calls. */
        private int counter;

        /**
    	 * Adds a listener to the list of node listeners. It is informed whenever a
	 * node is expanded during search.
	 */
        public void AddNodeListener(NodeListener listener)
        {
            nodeListeners.Add(listener);
        }

        /**
    	 * Resets the counter for {@link #expand(Node, Problem)} calls.
    	 */
        public void ResetCounter()
        {
            counter = 0;
        }

        /**
	 * Returns the number of {@link #expand(Node, Problem)} calls since the last
	 * counter reset.
	 */
        public int GetNumOfExpandCalls()
        {
            return counter;
        }
    }
}