using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    bool mGrounded = true;
    float mJumpPower = 250;
    Player mPlayer;
    float mSpeed = 5;
    [SerializeField]
    Rigidbody mRigidBody;


    void Awake()
    {
        mPlayer = GetComponent<Player>();
    }

    public void Move(Vector2 direction)
    {

        Vector3 mMovement = new Vector3();
        Vector3 animationSpeed = new Vector3(direction.x, 0, direction.y);
        mMovement.Set(direction.x, 0f, direction.y);
        mMovement = new Vector3(direction.x, 0, direction.y) * mSpeed * Time.deltaTime;
        Vector3 nonNormalizedMovement = mMovement;
        mMovement.x *= Mathf.Abs(direction.x);
        mMovement.y *= Mathf.Abs(direction.y);

        var locVel = transform.InverseTransformDirection(animationSpeed);
        float speedZ = locVel.z;
        float speedX = locVel.x;


        mRigidBody.MovePosition(transform.position + mMovement);
    }

    void FixedUpdate()
    {
        if (!mGrounded && GetComponent<Rigidbody>().velocity.y <= 10)
        {
            mGrounded = true;
            Debug.Log("yolo");

        }
        if (Input.GetButton("Jump") && mGrounded == true)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * mJumpPower);
            mGrounded = false;
            Debug.Log("swag");
        }
    }
        void LateUpdate()
    {
        HandleInput();
    }
    public void HandleInput()
    {
        mPlayer.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));


    }

    public void HandleRotation()
    {
        float horMovement = Input.GetAxis("CameraRotateX");
        float verMovement = Input.GetAxis("CameraRotateY");
        Vector2 rightAnalogue = new Vector2(horMovement, verMovement);
        if (rightAnalogue.magnitude > 0.5f)

        {
            rightAnalogue.Normalize();
            transform.LookAt(new Vector3(transform.position.x + rightAnalogue.x, transform.position.y, transform.position.z + rightAnalogue.y));
        }
        else
        {
            Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (moveVector != Vector2.zero)
            {
                transform.LookAt(new Vector3(transform.position.x + moveVector.x, transform.position.y, transform.position.z + moveVector.y));
            }

        }
    }

}
