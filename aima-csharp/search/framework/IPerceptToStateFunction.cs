using System;
using System.Collections.Generic;
using aima.core.agent;

namespace aima.core.search.framework
{
    /// <summary>
    /// This interface is to define how to Map a Percept to a State representation
    /// for a problem solver within a specific environment. 
    /// </summary>
    /// <remarks>
    /// This arises in the description of the Online Search algorithms from Chapter 4.
    /// </remarks>
    /// <author>Ciaran O'Reilly</author>
    public interface IPerceptToStateFunction
    {
        /// <summary>
        /// Get the problem state associated with a Percept.
        /// </summary>
        /// <param name="p">the percept to be transformed to a problem state.</param>
        /// <returns>a problem state derived from the Percept p.</returns>
        Object GetState(Percept p);
    }
}