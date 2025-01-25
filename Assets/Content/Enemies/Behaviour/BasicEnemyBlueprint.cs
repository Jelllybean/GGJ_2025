using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/BasicEnemy", order = -10000)]
public class BasicEnemyBlueprint : BehaviourBlueprint
{
    private const string BB_TARGET = "enemy_target";
    
    public override INode BuildTree()
    {
        return new NCSequence(new INode[]
        {
            new NGetGameObjectWithTag("Player", BB_TARGET),
            new NMoveTowards(BB_TARGET, PositionReadMode.GAME_OBJECT)
        });
    }
}
