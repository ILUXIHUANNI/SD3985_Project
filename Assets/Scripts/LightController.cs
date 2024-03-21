using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Timeline;

public class LightController : MonoBehaviour
{
    Quaternion temp;
    public Rigidbody2D rb;
    Vector2 origin;
    Vector2 direction;
    public LayerMask a;
    private void FixedUpdate()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        temp = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = temp;
    }

    private void Update()
    {
        origin = rb.position + Vector2.up * 0.2f;
        direction = temp * Vector2.up;
        if (Input.GetKeyDown(KeyCode.X))
        {

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, direction.magnitude, LayerMask.GetMask("NPC"));
            if (hit.collider.gameObject != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(origin, origin + direction * 1.5f);
    }

}
