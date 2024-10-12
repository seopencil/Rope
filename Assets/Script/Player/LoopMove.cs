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
    GameObject player;

    SpringJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerMove>().isRopeing)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        float temp = Vector3.Distance(transform.position, player.transform.position);

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

        /*if (temp > 10)
        {
            Destroy(this.gameObject);
        }*/
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

        joint.spring = 150f;
        joint.damper = dis;
        joint.massScale = 5f;

        joint.maxDistance = dis * 0.5f;

        if (Vector3.Distance(transform.position, player.transform.position) > transform.position.y - 25f)
        {
            dis = transform.position.y - 25f;
            joint.maxDistance = dis;
        }
        
        joint.minDistance = dis;


        joint.breakForce = 10000000;
        joint.breakTorque = 10000000;

        //joint.minDistance = Vector3.Distance(transform.position, player.transform.position);
    }
}
