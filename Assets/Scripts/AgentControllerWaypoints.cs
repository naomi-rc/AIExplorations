using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentControllerWaypoints : MonoBehaviour
{
    public GameObject[] wayPoints;
    private int currentPoint = 0;
    public float speed = 0.8f;
    public float rotSpeed = 0.8f;
    public float precision = 10f;

    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject cameraSocket;

    bool hasPath = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasPath) return;
        mainCamera.transform.position = cameraSocket.transform.position;
        mainCamera.transform.rotation = cameraSocket.transform.rotation;
        if (Vector3.Distance(this.transform.position, wayPoints[currentPoint].transform.position) < precision)
            currentPoint++;
        if (currentPoint == wayPoints.Length)
            currentPoint = 0;

        Quaternion lookAt = Quaternion.LookRotation(wayPoints[currentPoint].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, rotSpeed * Time.deltaTime);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }


    public void SetPath(GameObject[] pathPoints)
    {
        wayPoints = pathPoints;
        Invoke("StartMoving", 3);

    }

    void StartMoving()
    {        
        hasPath = true;
    }
}
