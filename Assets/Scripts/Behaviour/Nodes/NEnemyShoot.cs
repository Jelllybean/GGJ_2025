using UnityEngine;

public class NEnemyShoot : ANode
{
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if(bb.TryGet("common_agent", out Enemy agent))
        {
            agent.GetComponent<EnemyProjectileShooter>().Shoot();
            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.ERROR;
    }
}
