using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuohoVittuu : MonoBehaviour
{
    public void DisableColliders()
    {
        foreach (Transform child in transform)
        {
            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                childCollider.enabled = false;
            }
            DisableChildCollidersInHierarchy(child);
        }
    }

    private void DisableChildCollidersInHierarchy(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                childCollider.enabled = false;
            }
            DisableChildCollidersInHierarchy(child);
        }
    }
    private void Start()
    {
        DisableColliders();
    }
}
