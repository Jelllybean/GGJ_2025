using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/BasicEnemy", order = -10000)]
public class BasicEnemyBlueprint : BehaviourBlueprint
{
    public override INode BuildTree()
    {
        return new NCSequence(new INode[]
        {
            new NPrint(PrintMode.LOG, "Ik ben een enemy en ik doe dingen"),
            new NWait(1)
        });
    }
}
