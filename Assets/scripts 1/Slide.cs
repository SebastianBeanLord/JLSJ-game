using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }

    public float SlideForce = 25f;
    public GameObject player;
    Vector3 slide = new Vector3(1f, 0.5f, 1f);
    Vector3 origin;
    Rigidbody rb;
    private Vector3 _previousPos;

    private Vector3 _currentPos;

    public Vector3 moveDirection
    {
        get
        {
            return (_currentPos - _previousPos).normalized;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        _previousPos = _currentPos;

        _currentPos = transform.position;
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            Sliding();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = Vector3.Lerp(slide, new Vector3(1, 1, 1), 1f);
        }
    }
    void Sliding()
    {
        if (Stamina.instance.currentStamina - 200 > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            transform.localScale = Vector3.Lerp(player.transform.position, slide, 1f);
            rb.AddForce(moveDirection * SlideForce, ForceMode.VelocityChange);
            Stamina.instance.UseStamina(200);
        }
    }
}
