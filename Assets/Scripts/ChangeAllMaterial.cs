using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAllMaterial : MonoBehaviour
{
    [SerializeField] Material mat;
    private void OnValidate()
    {
        if (mat != null)
        {
            SpriteRenderer[] spList = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < spList.Length; i++)
            {
                spList[i].material = mat;
            }
        }
    }
}
