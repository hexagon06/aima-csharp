using System;
using System.Collections.Generic;

namespace aima.core.search.framework
{
    /// <summary>
    /// The evaluation function is construed as a cost estimate, so the node with the
    /// lowest evaluation is expanded first.
    /// </summary>
    /// <remarks>
    /// Artificial Intelligence A Modern Approach (3rd Edition): page 92.
    /// </remarks>
    /// <author>Ciaran O'Reilly</author>
    public interface IEvaluationFunction
    {
        double F(Node n);
    }
}