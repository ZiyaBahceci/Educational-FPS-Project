using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, gravityModifier, jumpPower;

    public CharacterController charCon;

    private Vector3 moveInput;

    public Transform camTrans;

    public float mouseSensitivity;  
    public bool invertX;            
    public bool invertY;

    private bool canJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    public GameObject bullet;
    public Transform firePoint;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
                
    }


    void Update()
    {

        float yStore = moveInput.y; //Store y velocity

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical"); //player moves vertically 
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal"); //player moves horizontally    

        moveInput = horiMove + vertMove;    //Character Motion (X and Y)
       // moveInput = Vector3.ClampMagnitude(moveInput, 1f); //To keep the movement speed stable

            moveInput = moveInput * moveSpeed;
        

        moveInput.y = yStore;

        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;  //Gravity 

        if(charCon.isGrounded)                                                  //Falling 
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0; //determine if player can jump (is player on ground)

        if(Input.GetKeyDown(KeyCode.Space) && canJump)     //Handle jumping
        {
            moveInput.y = jumpPower;
        }


        charCon.Move(moveInput * Time.deltaTime);   //Move speed control

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") , Input.GetAxisRaw("Mouse Y")) * mouseSensitivity; //camera rotation control


        if (invertX)                        //Invert X axis control
        {                                   
            mouseInput.x = -mouseInput.x;   
        }                                   
        if(invertY)                         //Invert Y axis control
        {                                   
            mouseInput.y = -mouseInput.y;   
        }                                   
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z); //Camera Control X
       
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));                                                   //Camera Control Y

        if(Input.GetMouseButtonDown(0))                                        //Controls firing
        {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                if(Vector3.Distance(camTrans.position, hit.point) > 4f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }

            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }

        anim.SetFloat("moveSpeed", moveInput.magnitude);    //Walking/Running Animation
        anim.SetBool("onGround", canJump);                  //This exists so moving animation won't work midair
        
       
    }
}
