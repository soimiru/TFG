using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody myRigidbody;
    private float jumpForce;
    private float movementSpeed;
    private SphereCollider myCollider;
    private Animator characterAnimator;

    private UIManager uiManager;

   
    public List<GameObject> lifeSprites;

    public int lifes = 3;

    private Color activeHeart = new Color(1f, 1f, 1f);
    private Color deactivatedHeart = new Color(0.5f, 0.5f, 0.5f);

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<SphereCollider>();
        characterAnimator = GetComponentInChildren<Animator>();
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            Jump();
        }
        //rigidbody.velocity = new Vector3(1f, rigidbody.velocity.y);

        //this.transform.position += Vector3.right * movementSpeed * Time.timeScale;
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = new Vector2(movementSpeed, myRigidbody.velocity.y);
    }

    public void SetInitialLifes(int lifes) {
        this.lifes = lifes;
        if (lifes == 1) {
            transform.Find("Life").gameObject.SetActive(false);
        }
    }

    void Jump() {
        SoundManager.Instance.PlaySFX("Jump");
        characterAnimator.SetTrigger("Jumping");
        myRigidbody.velocity = Vector3.up * jumpForce;
        StartCoroutine(CheckIsGrounded());
    }

    IEnumerator CheckIsGrounded() {
        bool onAir = true;
        while (onAir) {
            if (IsGrounded()) {
                onAir = false;
            }
            yield return new WaitForSeconds(1f);
        }
        characterAnimator.SetTrigger("Landing");
    }

    private bool IsGrounded() {
        //Returns True when in air - False when grounded
        //Debug.Log(Physics.BoxCast(collider.bounds.center, collider.bounds.size, Vector3.down * .1f, Quaternion.identity, 1f, platformLayerMask));
        return Physics.BoxCast(myCollider.bounds.center, myCollider.bounds.size, Vector3.down, Quaternion.identity, 0.5f, platformLayerMask);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Equals("DyingZone"))
        {
            Vector3 resetPosition = other.gameObject.transform.GetChild(0).gameObject.transform.position;
            this.gameObject.transform.position = resetPosition;
            ReduceLife();
        }
        else if (other.gameObject.name.Equals("heart")) {
            IncreaseLife(other.gameObject);
        }
    }

    private void ReduceLife() {
        lifes--;
        if (lifes == 0)
        {
            Die();
        }
        lifeSprites[lifes].GetComponent<SpriteRenderer>().color = deactivatedHeart;

    }

    private void IncreaseLife(GameObject other) {
        if (lifes < 3)
        {
            lifes++;
            lifeSprites[lifes - 1].GetComponent<SpriteRenderer>().color = activeHeart;
            Destroy(other);
        }
    } 

    private void Die() {
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
        uiManager.GameOver();
    }
}
