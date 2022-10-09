using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = target.transform.position - this.transform.position;
        float angle = Vector3.SignedAngle(-1 *this.transform.forward, distance, this.transform.up);
        this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
    }
}
