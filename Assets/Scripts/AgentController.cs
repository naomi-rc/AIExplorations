using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Guidage
{
    Seek,
    Flee,
    Pursue,
    Evade,
    Stall,
    None
}

public class AgentController : MonoBehaviour
{
    public GameObject target;
    //public Vector3 velocity;
    public float speed = 1f;
    public float precision = 1f;
    public bool debug = false;
    [SerializeField] private Guidage guidage;
    public GameObject posPred;

    void Start()
    {
        //Vector3 distance = target.transform.position - this.transform.position;
        //this.transform.position += distance;
        //speed = velocity.magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(debug)
            DebugRays();
        switch (guidage)
        {
            case Guidage.Seek: Seek(); break; 
            case Guidage.Flee: Flee(); break; 
            case Guidage.Pursue: Pursue(); break; 
            case Guidage.Evade: Evade(); break;
            case Guidage.Stall: Stall();  break; 
            default: break; 
        }
    }

    void CalculateDistance()
    {
        Vector3 distance = target.transform.position - this.transform.position;
        Debug.Log("Distance (magnitude): " + distance.magnitude);
        Debug.Log("Distance (function): " + Vector3.Distance(this.transform.position, target.transform.position));
    }

    void DebugRays()
    {
        Vector3 distance = target.transform.position - this.transform.position;
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.green, 200);
        Debug.DrawRay(this.transform.position, distance, Color.red, 200);
        Debug.DrawRay(this.transform.position, this.transform.up * 10, Color.blue, 200);
    }

    void Stall()
    {
        Vector3 distance = target.transform.position - this.transform.position;        
        float angle = Vector3.SignedAngle(this.transform.forward, distance, this.transform.up);
        this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
    }

    void Seek()
    {

        Vector3 distance = target.transform.position - this.transform.position;
        if(distance.magnitude > precision)
        {
            float angle = Vector3.SignedAngle(this.transform.forward, distance, this.transform.up);
            this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
            this.transform.Translate(distance.normalized * speed * Time.deltaTime, Space.World);
        }
           
    }

    void Flee()
    {
        Vector3 distance = this.transform.position - target.transform.position;
        if (distance.magnitude < 10)
        {
            float angle = Vector3.SignedAngle(this.transform.forward, distance, this.transform.up);
            this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
            this.transform.Translate(distance.normalized * speed * Time.deltaTime, Space.World);
        }
    
    }

    void Pursue()
    {

        /*Vector3 distance = target.transform.position - this.transform.position;
        Vector3 distancePredite = (distance.magnitude / (this.transform.forward.magnitude * speed)) * target.transform.forward * speed;
        Vector3 positionPredite = target.transform.position + distancePredite;

        Debug.DrawRay(positionPredite, Vector3.up, Color.magenta, 2);

        Vector3 displacement = positionPredite - this.transform.position;
        if (displacement.magnitude > precision)
        {
            float angle = Vector3.SignedAngle(this.transform.forward, displacement, this.transform.up);
            this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
            this.transform.Translate(displacement.normalized * speed * Time.deltaTime, Space.World);
        }*/

        //Distance prédite
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / target.GetComponent<AgentController>().speed * target.transform.forward;

        //Position prédite
        Vector3 pos_predite = target.transform.position + dp;
        posPred.transform.position = pos_predite;
        Vector3 d = pos_predite - transform.position;
        float angle = Vector3.SignedAngle(this.transform.forward, d, this.transform.up);
        this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
        transform.Translate(d.normalized * speed * Time.deltaTime, Space.World);
    }

    void Evade()
    {

        /*Vector3 distance = target.transform.position - this.transform.position;
        Vector3 distancePredite = (distance.magnitude / this.transform.forward.magnitude * speed) * target.transform.forward * speed;
        Vector3 positionPredite = target.transform.position + distancePredite;
        //Debug.DrawRay(positionPredite, Vector3.up, Color.magenta, 2);
        posPred.transform.position = positionPredite;
        Vector3 destination = this.transform.position + (this.transform.position - target.transform.position);

        Vector3 displacement = destination - this.transform.position;  //Vector3 displacement = this.transform.position - target.transform.position;
                                                                      
        float angle = Vector3.SignedAngle(this.transform.forward, this.transform.position - positionPredite, this.transform.up);
        this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
        this.transform.Translate(displacement.normalized * speed * Time.deltaTime, Space.World);*/

        /*Vector3 pj = target.transform.position;
        Vector3 vj = target.GetComponent<AgentController>().speedVector;
        Vector3 pn = this.transform.position;
        Vector3 vn = speedVector;

        float dist = (pj - pn).magnitude;
        Vector3 dp = (dist / vn.magnitude) * vj;
        Vector3 pp = pj + dp;
        Vector3 d = pn - pp;
        Vector3 f = vn;
        Vector3 dest = pn + d;
        float o = Vector3.SignedAngle(f, d, this.transform.up);
        this.transform.Rotate(0, o * speed * Time.deltaTime, 0);
        this.transform.Translate((dest - pn).normalized * speed * Time.deltaTime, Space.World);*/

        //Distance prédite
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / target.GetComponent<AgentController>().speed * target.transform.forward;

        //Position prédite
        Vector3 pos_predite = target.transform.position + dp;
        posPred.transform.position = pos_predite;
        Vector3 d = transform.position - pos_predite;
        float angle = Vector3.SignedAngle(this.transform.forward, d, this.transform.up);
        this.transform.Rotate(0, angle * speed * Time.deltaTime, 0);
        transform.Translate(d.normalized * speed * Time.deltaTime, Space.World);
    }
}
