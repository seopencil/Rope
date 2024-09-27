using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    GameObject player;
    SpringJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0) && !player.GetComponent<PlayerMove>().isRopeing)
        {
            Swing();
            player.GetComponent<PlayerMove>().isRopeing = true;
        }
        if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
        {
            Destroy(joint);
            player.GetComponent<PlayerMove>().isRopeing = false;
        }
    }

    public void Swing()
    {
        player.GetComponent<PlayerMove>().StartRope();

        joint = player.AddComponent<SpringJoint>();

        GameObject temp = new GameObject();
        temp.transform.rotation = transform.rotation;
        temp.transform.position = transform.position;
        temp.transform.localPosition += transform.TransformDirection(new Vector3(0, 10f, 10f));

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = temp.transform.position;

        joint.spring = 10f;
        joint.damper = 5f;

        float dis = Vector3.Distance(temp.transform.position, transform.position);

        joint.minDistance = dis;
        joint.maxDistance = dis * 0.7f;
        joint.massScale = 5f;

        player.GetComponent<Rigidbody>().AddRelativeForce((Vector3.forward + Vector3.down).normalized * 2000);

        joint.breakForce = 10000000;
        joint.breakTorque = 10000000;

        Destroy(temp.gameObject);
    }
}
