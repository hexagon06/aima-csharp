using System;
using System.Collections.Generic;

namespace aima.core.search.framework
{
    /// <summary>
    /// Stores key-value pairs for efficiency analysis.
    /// </summary>
    /// <remarks>
    /// Artificial Intelligence A Modern Approach (3rd Edition): page 78.
    /// </remarks>
    /// <author>Ciaran O'Reilly</author>
    public class PathCostFunction
    {
        public PathCostFunction()
        {

        }

        /// <summary>
        /// Returns the cost, traditionally denoted by g(n), of the path from the
        /// initial state to the node, as indicated by the parent pointers.</summary>
        /// <returns>
        /// the cost, traditionally denoted by g(n), of the path from the
        /// initial state to the node, as indicated by the parent pointers.
        /// </returns>
        public double G(Node n)
        {
            return n.GetPathCost();
        }
    }
}