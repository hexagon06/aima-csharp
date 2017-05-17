using System;
using System.Collections.Generic;

namespace aima.core.search.framework
{
    /// <summary>
    /// a heuristic function, denoted h(n): 
    /// h(n) = estimated cost of the cheapest path from the state at node n to a goal state.
    /// lowest evaluation is expanded first.
    /// Notice that h(n) takes a node as input, but, unlike g(n) it depends only on
    /// the state at that node.
    /// </summary>
    /// <remarks>
    /// Artificial Intelligence A Modern Approach (3rd Edition): page 92.
    /// </remarks>
    /// <author>Ciaran O'Reilly</author>
    public interface IHeuristicFunction
    {
        double H(Object state);
    }
}