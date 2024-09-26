using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField]
    GameObject loop;

    GameObject _loop;

    bool haveLoop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !haveLoop)
        {
            haveLoop = true;
            ShootLoop();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(_loop);
        }
    }

    void ShootLoop()
    {
        _loop = Instantiate(loop, transform.position, transform.rotation, transform);

        _loop.transform.localPosition = Vector3.forward * 1;
        _loop.transform.SetParent(null);
        _loop.GetComponent<LoopMove>().setPlayer(transform.parent.gameObject);
    }

    

    public void DesLoop()
    {
        haveLoop = false;
    }
}
