using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    void Start()
    {
                
    }


    void Update()
    {

        float yStore = moveInput.y; //Store y velocity

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical"); //player moves vertically 
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal"); //player moves horizontally    

        moveInput = horiMove + vertMove;    //Character Motion (X and Y)
        //moveInput.Normalize();  //To make sure moving speed is always the same - not necessary in our game.
        moveInput = moveInput * moveSpeed;  //Character Speed (X and Y)

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
    }
}
