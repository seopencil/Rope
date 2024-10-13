using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    float pawer;

    RaycastHit hit;
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, transform.GetComponentInParent<Enemy>().Range, layer))
        {
            hit.transform.GetComponent<PlayerMove>().PlayerKnockBack(transform.position, pawer);
            gameObject.SetActive(false);
        }
        Debug.DrawRay(transform.position, transform.forward * 80f, Color.blue, 0.3f);
    }
}
