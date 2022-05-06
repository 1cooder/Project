using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartLocker : MonoBehaviour
{
    [SerializeField]
    Collider2D collider;

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && transform.TransformPoint(PlayerController.Instance.transform.position).x>0)
        {
            collider.isTrigger = false;
        }
    }
}
