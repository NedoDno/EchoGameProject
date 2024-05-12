using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private float deltaY = 0f;

    private bool isCrouching;
    
    private bool isWalkSoundJustPlayed;
    private Vector3 originalCenter;
    private float originalHeight;

    private Vector3 originalTransform;
    private float originalMoveSpeed;
    private float originalVolume;
    private float coefTimerWalkSoung=12;

    private CharacterController charController;
    
    public AudioSource audioSource;
    [SerializeField] AudioClip walkSound;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        originalTransform = transform.localScale;
        originalMoveSpeed = speed;
        originalVolume =  audioSource.volume;
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        float dt = Time.deltaTime;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            audioSource.volume = audioSource.volume*2;
            movement = movement * 2;
            coefTimerWalkSoung = 16;
        }
        else{
            coefTimerWalkSoung = 12;
            audioSource.volume = originalVolume;
        }
        movement.y = gravity;
        movement *= dt;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
        if (charController.isGrounded && deltaY < 0)
        {
            deltaY = 0f;
        }
        //if (Input.GetButton("Jump") && charController.isGrounded)
        //{
        //    Jump(speed);
        //}
        deltaY += gravity * Time.deltaTime;
        movement = new Vector3(0, deltaY, 0);
        movement *= dt;
        
        charController.Move(movement);
        Crouch();
 
        if((deltaX != 0 || deltaZ != 0) && !isCrouching && !isWalkSoundJustPlayed &&  charController.isGrounded)
            StartCoroutine(WalkSoundPlay());
 
    }
    private IEnumerator WalkSoundPlay()
    {
        audioSource.PlayOneShot(walkSound);
        isWalkSoundJustPlayed = true;
        yield return new WaitForSeconds(speed/coefTimerWalkSoung);
        isWalkSoundJustPlayed = false;
    }
    //public void Jump(float jumpHeight)
    //{
    //    audioSource.PlayOneShot(jumpSound);
    //    deltaY = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    //}
    public void Crouch()
    {
        if (Input.GetButtonDown("Crouch") && charController.isGrounded)
        {
            transform.localScale = new Vector3(originalTransform.x, originalTransform.y / 2, originalTransform.z);
            speed = originalMoveSpeed / 2;
            isCrouching = true;
        }

        if (!Input.GetButton("Crouch") && isCrouching)
        {
            transform.localScale = originalTransform;
            speed = originalMoveSpeed;
            isCrouching = false;
        }
    }

    public void OnSpeedChange<T>(float _speed)
    {
        Debug.Log("New speed " + _speed.ToString());
        speed = _speed;
        originalMoveSpeed = speed;
    }
}