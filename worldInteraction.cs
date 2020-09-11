using UnityEngine;
using UnityEngine.AI;
public class worldInteraction : MonoBehaviour
{
    public static worldInteraction Instance { get; set; }

    public NavMeshAgent playerAgent;
    public charController playerController;
    private Interactable currentInteraction;
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    void Update()
    {
        //Get the interaction
        if (gameState.Instance.getGameState() == gameStates.World && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            getInterraction();
        }
        //Start the interaction
        else if (playerAgent != null && !playerAgent.pathPending && playerAgent.remainingDistance <= playerAgent.stoppingDistance && gameState.Instance.getGameState() == gameStates.movingToInteraction) 
            gameState.Instance.startInteraction(currentInteraction.interactionDialogue);
        //Proceed the interaction
        else if (gameState.Instance.getGameState() == gameStates.Interaction && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")))
        {
            //gameState.Instance.proceedInteraction();
        }
    }
    private void getInterraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;

            //Dialogue Interaction
            //If the Player has clicked on an Interaction
            if (gameState.Instance.getGameState() == gameStates.World && interactedObject.tag == "Interaction")
            {
                currentInteraction = interactedObject.GetComponent<Interactable>();
                playerInteractWith(currentInteraction.gameObject);
            }
            //Else if the Player has clicked on a social Entity
            else if (gameState.Instance.getGameState() == gameStates.World && interactedObject.transform.parent.tag == "socialEntity")
            {
                interactedObject.transform.parent.GetComponent<interactableEntity>().Interact(playerController);
                currentInteraction = interactedObject.transform.parent.GetComponent<interactableEntity>();
                playerInteractWith(currentInteraction.gameObject);
            }
            //Else the Player just clicked on an empty spot intending to walk
            else
            {
                playerAgent.stoppingDistance = 0;
                playerAgent.SetDestination(interactionInfo.point);
            }

            //Profile Interaction
            //If the Player has clicked on itself or on a User
            if (gameState.Instance.getGameState() == gameStates.World && (interactedObject.tag == "Player" || interactedObject.tag == "User"))
                gameState.Instance.openProfile(interactedObject.GetComponent<charController>());
            else gameState.Instance.closeProfile();
        }
    }
    private void playerInteractWith(GameObject _currentInteraction)
    {
        gameState.Instance.setGameState(gameStates.movingToInteraction);
        playerAgent.stoppingDistance = 3f;
        playerAgent.SetDestination(_currentInteraction.transform.position);
    }

    public void InteractWPlayer(Interactable _currentInteraction)
    {
        currentInteraction = _currentInteraction;
        gameState.Instance.startInteraction(currentInteraction.interactionDialogue);
    }

}