using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float moveP;
    [SerializeField]
    float jumpP;
    [SerializeField]
    float mexSpeed;

    Rigidbody rb;

    bool isGraund = false;
    public bool isRopeing = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");


        rb.AddRelativeForce(new Vector3(xMove, 0, zMove).normalized * moveP * Time.deltaTime);

        if (isGraund && !isRopeing)
        { 
            rb.AddForce(0, Input.GetAxis("Jump") * jumpP * Time.deltaTime, 0);
        }

        //if (isRopeing)
        //{
        //    rb.AddRelativeForce(rb.velocity.normalized * 1000f * Time.deltaTime);
        //}
    }

    public void StartRope()
    {
        rb.velocity /= 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Graund")
        {
            isGraund = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Graund")
        {
            isGraund = false;
        }
    }
}