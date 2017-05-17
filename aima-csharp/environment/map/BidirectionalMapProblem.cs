using System;
using System.Collections.Generic;
using aima.core.search.framework.problem;

namespace aima.core.environment.map
{
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class BidirectionalMapProblem : Problem, IBidirectionalProblem
    {
	Map map;

	Problem reverseProblem;

	public BidirectionalMapProblem(Map map, String initialState, String goalState): this(map, initialState, goalState, new DefaultGoalTest(goalState))
	{
	    
	}

	public BidirectionalMapProblem(Map map, String initialState, String goalState, IGoalTest goalTest) :  base(initialState, MapFunctionFactory.getActionsFunction(map), MapFunctionFactory.getResultFunction(),
			    goalTest, new MapStepCostFunction(map))
	{ 
	    this.map = map;

	    reverseProblem = new Problem(goalState, MapFunctionFactory.getReverseActionsFunction(map),
			    MapFunctionFactory.getResultFunction(), new DefaultGoalTest(initialState),
			    new MapStepCostFunction(map));
	}

	// START Interface BidrectionalProblem
	public Problem GetOriginalProblem()
	{
	    return this;
	}

	public Problem GetReverseProblem()
	{
	    return reverseProblem;
	}
	// END Interface BirectionalProblem
    }
}