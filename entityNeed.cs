using UnityEngine;
[System.Serializable]
public class entityNeed
{
    public Needs Need;
    [SerializeField]
    public float maxCapacity, Value, Increment;
    public entityNeed(Needs _Need, int _maxCapacity, int _Value, int _Decrement)
    {
        Need = _Need;
        maxCapacity = _maxCapacity;
        Value = _Value;
        Increment = _Decrement;
    }
    // Update is called once per frame
    public void Update()
    {
        Value += Time.deltaTime * Increment;
        if (Value <= 0) Value = 0;
    }
    public void satisfyNeed(float _Value)
    {
        Value += _Value;
        if (Value > maxCapacity) Value = maxCapacity;
    }
}