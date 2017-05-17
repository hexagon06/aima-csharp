using System;
using System.Collections.Generic;
using aima.core.agent;

namespace aima.core.search.framework
{
    /// <summary>
    /// Interface for all search algorithms which store at least a part of the
    /// exploration history as search tree and return a list of actions which lead
    /// from the initial state to a goal state.
    /// </summary>
    /// <author>Ruediger Lunde</author>
    public interface ISearchForActions : ISearch
    {
        NodeExpander GetNodeExpander();
    }
}