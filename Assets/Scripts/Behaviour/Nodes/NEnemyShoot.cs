using UnityEngine;

public class NEnemyShoot : ANode
{
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if(bb.TryGet("common_agent", out Enemy agent))
        {
            if(agent.TryGetComponent(out EnemyProjectileShooter shooter))
            {
                shooter.Shoot();
                return NodeReturnState.SUCCESS;
            }
            else return NodeReturnState.FAILED;
        }

        return NodeReturnState.ERROR;
    }
}
