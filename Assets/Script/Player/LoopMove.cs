using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LoopMove : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float ropeMaxDistance;
    [SerializeField]
    float swingMinDistance;
    [SerializeField]
    float swingMaxDistance;
    [SerializeField]
    GameObject player;


    SpringJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = Vector3.Distance(transform.position, player.transform.position);

        if (!player.GetComponent<PlayerMove>().isRopeing)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<PlayerMove>().isRopeing)
        {
            joint.spring = 50f;
            joint.damper = 0.1f;
            joint.minDistance = 0.1f;
        }

        //if (player.GetComponent<PlayerMove>().isRopeing && temp < joint.minDistance)
        //{
        //    joint.minDistance = temp;
        //}

        if (temp > ropeMaxDistance && !player.GetComponent<PlayerMove>().isRopeing)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            player.GetComponent<PlayerMove>().isRopeing = true;
            Hold();
        }
    }
    void Hold()
    {
        //player.GetComponent<PlayerMove>().StartRope();

        if (player.GetComponent<SpringJoint>() != null)
        {
            return;
        }
        
        joint = player.AddComponent<SpringJoint>();

        float dis = Vector3.Distance(transform.position, player.transform.position);

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = transform.position;

        joint.spring = 10f;
        joint.massScale = 5f;

        joint.maxDistance = 10;

        if (dis < swingMinDistance)
        {
            dis = swingMinDistance;
        }
        if (dis > swingMaxDistance)
        {
            dis = swingMaxDistance;
        }

        joint.minDistance = dis;
        joint.damper = dis;


        joint.breakForce = 10000000;
        joint.breakTorque = 10000000;

        //joint.minDistance = Vector3.Distance(transform.position, player.transform.position);
    }
}
