using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody rigidbody;
    private float jumpForce;
    private float movementSpeed;
    private SphereCollider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5f;
        jumpForce = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() ) {
            Jump();
        }
        //rigidbody.velocity = new Vector3(1f, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(movementSpeed, rigidbody.velocity.y);
        //this.transform.position += Vector3.right * movementSpeed * Time.timeScale;
    }
    

    void Jump(){
        rigidbody.velocity = Vector3.up * jumpForce;
    }

    private bool IsGrounded() {
        //Returns True when in air - False when grounded
        //Debug.Log(Physics.BoxCast(collider.bounds.center, collider.bounds.size, Vector3.down * .1f, Quaternion.identity, 1f, platformLayerMask));
        return Physics.BoxCast(collider.bounds.center, collider.bounds.size, Vector3.down, Quaternion.identity, 0.5f, platformLayerMask);
    }
}
