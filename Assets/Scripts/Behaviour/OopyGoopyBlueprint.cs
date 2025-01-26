using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/OopyGoopy", order = -10000)]
public class OopyGoopyBlueprint: BehaviourBlueprint
{
    public float stepTime;
    public float rangedDistance;
    public LayerMask playerLayerMask;
    private const string BB_TARGET = "enemy_target";
    private const string BB_TARGET_DISTANCE = "enemy_target_distance";
    private const string BB_TARGET_WILL_SHOOT = "enemy_target_shoot";
    
    public override INode BuildTree()
    {
        return new NCSequence(new INode[]
        {
            new NGetGameObjectWithTag("Player", BB_TARGET),
            new NGetDistanceTo(BB_TARGET, BB_TARGET_DISTANCE, PositionReadMode.GAME_OBJECT),
            
            // if target is nearby, shoot
            new NCSelector(new INode[]
            {
                new NDComparison<float>(new NDHasLineOfSight(new NCSequence(new INode[]
                        {
                            new NPrint(PrintMode.LOG, "Shoot"),
                            new NEnemyShoot()
                        }), 
                        BB_TARGET, playerLayerMask),
                    BB_TARGET_DISTANCE, Comparator.GREATER, rangedDistance, true),
                
                new NCSequence(new INode[]
                {
                    new NMoveTowards(BB_TARGET, PositionReadMode.GAME_OBJECT),
                    new NPrint(PrintMode.LOG, "Move"),
                    new NEnemyStep(),
                    new NWait(stepTime)
                })
            }),
            
            
            
            new NBlackboardSet(BB_TARGET_WILL_SHOOT, false)
            
        });
    }
}
