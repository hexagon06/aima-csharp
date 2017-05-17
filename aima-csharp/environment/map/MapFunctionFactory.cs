using System.Collections.Generic;
using aima.core.agent;
using aima.core.agent.impl;
using aima.core.search.framework;
using aima.core.search.framework.problem;
using aima.core.util;

namespace aima.core.environment.map
{
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class MapFunctionFactory
    {
	private static IResultFunction resultFunction;
	private static IPerceptToStateFunction perceptToStateFunction;

	public static IActionsFunction getActionsFunction(Map map)
	{
	    return new MapActionsFunction(map, false);
	}

	public static IActionsFunction getReverseActionsFunction(Map map)
	{
	    return new MapActionsFunction(map, true);
	}

	public static IResultFunction getResultFunction()
	{
	    if (null == resultFunction)
	    {
		resultFunction = new MapResultFunction();
	    }
	    return resultFunction;
	}

	private class MapActionsFunction : IActionsFunction
	{
	    private Map map = null;
	    private bool reverseMode;

	    public MapActionsFunction(Map map, bool reverseMode)
	    {
		this.map = map;
		this.reverseMode = reverseMode;
	    }

	    public HashSet<Action> Actions(System.Object state)
	    {
		HashSet<Action> actions = new HashSet<Action>();
		System.String location = state.ToString();

		List<System.String> linkedLocations = reverseMode ? map.getPossiblePrevLocations(location)
					: map.getPossibleNextLocations(location);
		foreach (System.String linkLoc in linkedLocations)
		{
		    actions.Add(new MoveToAction(linkLoc));
		}
		return actions;
	    }
	}

	public static IPerceptToStateFunction getPerceptToStateFunction()
	{
	    if (null == perceptToStateFunction)
	    {
		perceptToStateFunction = new MapPerceptToStateFunction();
	    }
	    return perceptToStateFunction;
	}

	private class MapResultFunction : IResultFunction
	{
	    public MapResultFunction()
	    {
	    }

	    public System.Object Result(System.Object s, Action a)
	    {

		if (a is MoveToAction)
		{
		    MoveToAction mta = (MoveToAction)a;

		    return mta.getToLocation();
		}

		// The Action is not understood or is a NoOp
		// the result will be the current state.
		return s;
	    }
	}

	private class MapPerceptToStateFunction :
		IPerceptToStateFunction
	{
	    public System.Object GetState(Percept p)
	    {
		return ((DynamicPercept)p)
			.getAttribute(DynAttributeNames.PERCEPT_IN);
	    }
	}
    }
}