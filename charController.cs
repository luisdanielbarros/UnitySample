using UnityEngine;
using UnityEngine.AI;

public class charController : MonoBehaviour
{
    public NavMeshAgent charAgent;
    public characterNeeds charNeeds { get; set; }
    public characterGender charGender { get; set; }
    public characterAge charAge { get; set; }
    public characterRace charRace { get; set; }
    public characterPhysic charPhysic { get; set; }
    public characterPersonality charPersonality { get; set; }
    public characterTags[] charTags { get; set; }
    void Awake()
    {
        charNeeds = GetComponent<characterNeeds>();
    }
    private void Start()
    {
    }
    void Update()
    {
    }
    public string getName()
    {
        return charNeeds.Name;
    }
}