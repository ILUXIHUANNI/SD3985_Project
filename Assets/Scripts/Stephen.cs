using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stephen : MonoBehaviour
{
    public List<GameObject> inrange;
    public List<GameObject> inrangeNoBlock;

    public GameObject target;
    private void Update()
    {
        for (int i = 0; i < inrange.Count; i++)
        {
            //raycast every inrange object
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "AAA")
        {
            inrange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AAA")
        {
            inrange.Remove(collision.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 dir = target.transform.position - transform.position;
        Gizmos.DrawLine(transform.position, transform.position + dir*3);
    }
}
