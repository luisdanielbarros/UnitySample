using UnityEngine;
public class characterNeeds : entityA
{
    //psychologicalNeeds is the array of needs taken into consideration after the primalNeeds' array is satisfied. This
    //is the array of needs that causes social interaction, resulting in an eventual social hierarchy of entities.

    //fulfillmentNeeds is the array of needs taken into consideration after the psychologicalNeeds' array is satisfied.
    //This is the array of needs that takes place after complete social insertion. 

    //I'll be happy if the program reaches this point of complete social insertion. (19-08-11)
    public entityNeed[] psychologicalNeeds;
    public entityNeed[] fulfillmentNeeds;
    public override void Awake()
    {
        base.Awake();
        currentLevel = entityLevel.B;
    }
    public override void Update()
    {
        if (Time.time >= needsTime)
        {
            needsTime += needsInterval;
            base.Update();
            for (int i = 0; i < psychologicalNeeds.Length; i++)
            {
                psychologicalNeeds[i].Update();
            }
            for (int i = 0; i < fulfillmentNeeds.Length; i++)
            {
                fulfillmentNeeds[i].Update();
            }
        }
    }
}