using System;
using UnityEngine;
using UnityEngine.UI;
//An entity is whatever temporary object whose existence depends on the fulfillment of an array of needs.
//It can be an object, a living being or a cluster of matter.

//Living beings see the world through their needs, their most basic function is to detect and memorize their
//surrounding environment and how it can help satisfy their needs. The need is therefor the first dimension
//of complexity that composes the living entity, I'll explain how it is equally fitting for all non-living 
//entities.

//Even unsconscious objects unable to see or think have at least one need: the unsatiable need of Time, when
//this need is unfulfilled for too long (in the unavoidable passage of in-game time) their existence ceases:
//they're either destroyed or transformed into another object (akin to real-life decomposure).

//Regarding the destruction or transformation points at which an object's need's fulfillment value(s) causes
//it to cease, a fulfillment may either be a range of two values, minimum and maximum, or a single minimum 
//value.

//Examples:

//Excessive warmth turns carbon-based entities into ash, this Warmth is a need whose fulfillment should thus 
//be within a certain range. Even though excessive water in our system can thin the blood into dangerously
//fluid levels, that so scarcely happens, it's an ignorable exception and the Thirst need could simply have
//a dangerous minimum at 0 and a safe maximum clamped at 100.

public enum Needs { Time = 0, Thirst = 1, Hunger = 2, Warmth = 3, Sleep = 4, Safety = 5, Hygiene = 6, Relatedness = 7, Closeness = 8, Prestige = 9, selfActualization = 10 };
public enum entityLevel { A, B};
public class entityA : MonoBehaviour
{
    public string Name { get; set; }
    public string Description { get; set; }
    //currentLevel is the enumerator that classifies the level of complexity of the entity. Level A is used for
    //objects, level B for living beings.
    protected entityLevel currentLevel { get; set; }
    //primalNeeds is the array of needs that when not satisfied, causes the object to disappear (die) or
    //transform into another
    //satisfiesNeeds is the array of needs that the object can satisfy in other entities
    public entityNeed[] satisfiesNeeds;
    public entityNeed[] primalNeeds;
    protected int needsInterval = 1, needsTime = 0;
    //Debug output for development
    public Text debugOutput;
    public virtual void Awake()
    {
        currentLevel = entityLevel.A;
    }
    public virtual void Update()
    {
        //Update the needs
        for (int i = 0; i < primalNeeds.Length; i++)
        {
            primalNeeds[i].Update();
            if (primalNeeds[i].Value == 0) Destroy(this.gameObject);
        }
        for (int i = 0; i < satisfiesNeeds.Length; i++)
        {
            satisfiesNeeds[i].Update();
        }
        //Update the debug output
        if (debugOutput != null)
        {
            debugOutput.text = "";
            for (int i = 0; i < primalNeeds.Length; i++)
            {
                debugOutput.text += Enum.GetName(typeof(Needs), primalNeeds[i].Need) + ": " + primalNeeds[i].Value.ToString() + "\n";
            }
            for (int i = 0; i < satisfiesNeeds.Length; i++)
            {
                debugOutput.text += Enum.GetName(typeof(Needs), satisfiesNeeds[i].Need) + ": " + satisfiesNeeds[i].Value.ToString() + "\n";
            }
        }

    }
    public entityLevel getCurrentLevel()
    {
        return currentLevel;
    }
    public virtual bool satisfyNeed(Needs _Need)
    {
        for (int i = 0; i < satisfiesNeeds.Length; i++)
        {
            if (_Need == satisfiesNeeds[i].Need)
            {
                satisfiesNeeds[i].satisfyNeed(-1);
                return true;
            }
        }
        return false;
    }
}