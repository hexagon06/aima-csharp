using System;
using System.Collections.Generic;
using aima.core.agent.impl;

namespace aima.core.search.framework
{
    /// <summary>
    /// A NoOp action that indicates a CutOff has occurred in a search. Used
    /// primarily by DepthLimited and IterativeDeepening search routines.
    /// </summary>
    /// <author>Ciaran O'Reilly</author>
    public class CutOffIndicatorAction : DynamicAction
    {
        public static readonly CutOffIndicatorAction CUT_OFF = new CutOffIndicatorAction();

        // START-Action
        public bool IsNoOp()
        {
            return true;
        }

        // END-Action
        private CutOffIndicatorAction() : base("CutOff")
        {

        }
    }
}