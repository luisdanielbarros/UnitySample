using UnityEngine;
using UnityEngine.AI;

public class entitySight : MonoBehaviour
{
    //The number of degrees centered in the forward direction the entity can see
    public float fieldOfViewAngle = 110f;
    //Whether the entity can see the player
    public bool playerInSight;
    //The last sighting of the player unique to an individual entity
    public Vector3 lastPersonalSighting;

    //The length of the path to the player as a measure of how far the entity can hear
    private NavMeshAgent nav;
    //It'll be compared to the radius of the sphere collider
    private SphereCollider col;

    //The Animation Controller has a playerInSight boolean parameter, which we need in order to set this script's playerInSight boolean.
    private Animator Anim;

    //The global last sighting of the player in inside the global sighting script

    //We'll be using some of the On Trigger enter functions in this script to detect the player entering the sphere collider, as such we need a reference to the player
    private GameObject playerGameObj;

    //Since the entity can hear the player when he's running or shouting we need a reference to the player's animator.
    private Animator playerAnim;

    //Stores the personal last sighting from the previous frame, so we can know if the position of the player has changed
    private Vector3 previousSighting;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        Anim = GetComponent<Animator>();
        playerGameObj = GameObject.FindGameObjectWithTag("Player");
        lastPersonalSighting = new Vector3(0, 0, 0);
        previousSighting = new Vector3(0, 0, 0);
    }
    void Start()
    {
    }
    void Update()
    {
        //If the global sighting of the player has changed update the personal sighting of the player
        if (globalPlayerSighting.Instance.position != previousSighting) lastPersonalSighting = globalPlayerSighting.Instance.position;
        //Change the previous position to be this frame's position
        previousSighting = globalPlayerSighting.Instance.position;
        //For the player in sight to be true, the player must satisfy three conditions
        //1. To be within the trigger zone
        //2. In front of the entity and inside his field of view
        //3. Nothing is blocking the entity's view of the player (if the later collider object hits the player)
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerGameObj)
        {
            //Sight
            playerInSight = false;
            Vector3 Direction = other.transform.position - transform.position;
            float Angle = Vector3.Angle(Direction, transform.position);
            if (Angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit Hit;
                if (Physics.Raycast(transform.position + transform.up, Direction.normalized, out Hit, col.radius))
                {
                    if (Hit.collider.gameObject == playerGameObj)
                    {
                        playerInSight = true;
                        lastPersonalSighting = playerGameObj.transform.position;
                        globalPlayerSighting.Instance.position = lastPersonalSighting;
                    }
                }
            }
            //Hearing
            if (calculatePathLength(playerGameObj.transform.position) <= col.radius)
            {
                playerInSight = true;
                lastPersonalSighting = playerGameObj.transform.position;
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerGameObj) playerInSight = false;
    }

    private float calculatePathLength(Vector3 targetPosition)
    {
        float pathLength = 0f;
        NavMeshPath navPath = new NavMeshPath();
        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, navPath);
            Vector3[] navPathAllPoints = new Vector3[navPath.corners.Length + 2];
            navPathAllPoints[0] = transform.position;
            navPathAllPoints[navPathAllPoints.Length - 1] = targetPosition;
            for (int i = 0; i < navPath.corners.Length; i++)
            {
                navPathAllPoints[i + 1] = navPath.corners[i];
            }
            for (int i = 0; i < navPathAllPoints.Length - 1; i++)
            {
                pathLength += Vector3.Distance(navPathAllPoints[i], navPathAllPoints[i + 1]);
            }
        }
        return pathLength;
    }
}
