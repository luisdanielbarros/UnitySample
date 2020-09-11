using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class baseStat
{
    public statsList statName;
    [SerializeField]
    public int baseValue;
    public List<statBonus> statBonuses { get; set; }
    private int finalValue { get; set; }
    public baseStat(statsList _statName, int _baseValue)
    {
        statName = _statName;
        baseValue = _baseValue;
        statBonuses = new List<statBonus>();
        calcFinalValue();
    }
    public void addStatBonus(statBonus _statBonus)
    {
        statBonuses.Add(_statBonus);
        calcFinalValue();
    }
    public void removeStatBonus(statBonus _statBonus)
    {
        statBonuses.Remove(statBonuses.Find(currentStatBonus => currentStatBonus.bonusValue == _statBonus.bonusValue));
        calcFinalValue();
    }
    private void calcFinalValue()
    {
        finalValue = baseValue;
        statBonuses.ForEach(currentStatBonus => finalValue += currentStatBonus.bonusValue);
    }
    public int getFinalValue()
    {
        return finalValue;
    }
}
