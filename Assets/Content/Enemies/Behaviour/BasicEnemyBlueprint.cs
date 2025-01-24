using UnityEngine;

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
