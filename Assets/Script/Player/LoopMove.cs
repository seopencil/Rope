using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoopMove : MonoBehaviour
{
    [SerializeField]
    float speed;

    GameObject player;
    SpringJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float temp = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<PlayerMove>().isRopeing)
        {
            joint.spring = 8f;
            joint.minDistance = 0.1f;
        }

        if (player.GetComponent<PlayerMove>().isRopeing && Vector3.Distance(transform.position, player.transform.position) < joint.minDistance)
        {
            joint.minDistance = Vector3.Distance(transform.position, player.transform.position) - 0.1f;
        }

        /*if (temp > 10)
        {
            Destroy(this.gameObject);
        }*/
    }

    public void setPlayer(GameObject _player)
    {
        player = _player;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            speed = 0;
            player.GetComponent<PlayerMove>().isRopeing = true;
            Hold();
        }
    }
    private void OnDestroy()
    {
        player.GetComponentInChildren<Loop>().DesLoop();
        player.GetComponent<PlayerMove>().isRopeing = false;
        Destroy(joint);
    }

    void Hold()
    {
        //player.GetComponent<PlayerMove>().StartRope();

        joint = player.AddComponent<SpringJoint>();

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = transform.position;

        joint.spring = 5f;
        joint.damper = 3f;
        joint.massScale = 5f;

        float dis = Vector3.Distance(player.transform.position, transform.position);

        joint.minDistance = dis;
        joint.maxDistance = dis * 0.5f;

        joint.breakForce = 10000000;
        joint.breakTorque = 10000000;

        joint.minDistance = Vector3.Distance(transform.position, player.transform.position);
    }
}
