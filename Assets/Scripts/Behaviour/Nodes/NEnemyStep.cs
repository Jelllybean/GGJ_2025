using UnityEngine;

public class NEnemyStep : ANode
{
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if(bb.TryGet("common_agent", out Enemy agent))
        {
            agent.MovementStep();
            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.ERROR;
    }
}
