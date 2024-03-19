using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject target;
    private Vector3 targetPos;
    /*[SerializeField] private float speed;
    private float offset;*/
    void Start()
    {
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, -1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = targetPos;
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, -1f);
    }
}
