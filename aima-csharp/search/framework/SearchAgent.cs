using System.Collections.Generic;
using aima.core.agent;
using aima.core.agent.impl;
using aima.core.search.framework.problem;

namespace aima.core.search.framework
{
    /**
     * @author Ravi Mohan
     * 
     */
    public class SearchAgent : AbstractAgent
    {
        protected List<Action> actionList;

        private List<Action>.Enumerator actionIterator;

        private Metrics searchMetrics;

        public SearchAgent(Problem p, ISearch search)
        {
            actionList = search.Search(p);
            actionIterator = actionList.GetEnumerator();
            searchMetrics = search.GetMetrics();
        }

        public override Action Execute(Percept p)
        {

            if (actionIterator.MoveNext())
            {
                return actionIterator.Current;
            }
            else
            {
                return NoOpAction.NO_OP;
            }
        }

        public bool IsDone()
        {
            return null != actionIterator.Current;
        }

        public List<Action> GetActions()
        {
            return actionList;
        }

        public Dictionary<string, string> GetInstrumentation()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            foreach (string key in searchMetrics.KeySet())
            {
                System.String value = searchMetrics.Get(key);
                retVal.Add(key, value);
            }
            return retVal;
        }
    }
}