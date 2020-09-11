using System.Collections.Generic;
using UnityEngine;

public class waypointRange : Waypoint
{
    //Debug
    private Color debugRangeColor = Color.green;
    //Range
    public float rangeRadius;
    //Connections
    private List<GameObject> Connections;
    private int currentConnection = -1;
    //Debug
    public override void OnDrawGizmos()
    {
        Gizmos.color = debugRangeColor;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
    void Start()
    {
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        Connections = new List<GameObject>();
        for (int i = 0; i < allWaypoints.Length; i++)
        {
            GameObject currentWaypoint = allWaypoints[i].gameObject;
            if (currentWaypoint != null)
            {
                if (Vector3.Distance(this.transform.position, currentWaypoint.transform.position) <= rangeRadius)
                {
                    Connections.Add(currentWaypoint);
                }
            }
        }
    }

    public Vector3 getNextWaypoint()
    {
        int nextWaypoint = -1;
        do
        {
            nextWaypoint = Random.Range(0, Connections.Count);
        } while (nextWaypoint == currentConnection);
        return Connections[nextWaypoint].transform.position;
    }
}
