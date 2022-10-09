using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //public Node predecessor;
    public List<Node> neighbors;
    public float G = Mathf.Infinity;
    public float H;
    public float F;
    TextMesh textMesh;
    Material currentMaterial;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = $"{name} \ng = {G} \nh = {H} \nf={G+H}";
        currentMaterial = GetComponent<MeshRenderer>().material;
        
    }


    void Update()
    {
        textMesh.text = $"{name} \ng = {G} \nh = {H} \nf={G + H}";
        foreach (Node n in neighbors)
        {
            //Debug.DrawRay(this.transform.position, n.transform.position - this.transform.position, Color.blue);
            Debug.DrawLine(this.transform.position, n.transform.position, Color.blue);
        }
    }
    public void setCurrent()
    {
        currentMaterial.color = Color.black;
    }

    public void setOpen()
    {
        currentMaterial.color = Color.yellow;
    }

    public void setClosed()
    {
        currentMaterial.color = Color.red;
    }
}
