using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject skmbike;
    private float movespeed = 120f;
    private float rotationspeed = 100f;
    private Rigidbody rbbike;
    private bool tens = false;
    private float moveDirection
    {
        get
        {
            return InputWS().y * movespeed;
        }

    }
    private float rotationDirection
    {
        get
        {
            return InputAD().x * rotationspeed;
        }
    }
    private void StartPosition()
    {
        skmbike.transform.position = new Vector3(0, 1, 0);
        skmbike.transform.rotation = Quaternion.Euler(0, 0, 0);
        tens = false;
        TensorControl(tens);
    }
    private void Awake()
    {
        rbbike = skmbike.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        StartPosition();

    }
    private void FixedUpdate()
    {
        rbbike.linearVelocity = rbbike.transform.TransformDirection(new Vector3(0f, rbbike.linearVelocity.y, moveDirection * Time.fixedDeltaTime));
        rbbike.angularVelocity = new Vector3(0f, rotationDirection * Time.fixedDeltaTime, 0f);
        TensorControl(tens);
    }

    private void Update()
    {
        Restart();
    }

    private Vector2 InputWS()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            tens = true;
            return Vector2.up;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            tens = true;
            return Vector2.down;
        }
        else return Vector2.zero;
    }
    private Vector2 InputAD()
    {
        if (Keyboard.current.dKey.isPressed) return Vector2.right;
        if (Keyboard.current.aKey.isPressed) return Vector2.left;
        else return Vector2.zero;
    }
    private void Restart()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            StartPosition();
        }
    }
    private void TensorControl(bool t)
    {
        t = tens;
        if (t)
        {
            rbbike.automaticInertiaTensor = false;
            rbbike.inertiaTensor = new Vector3(10, 6, 1);

        }
        else
        {
            rbbike.automaticInertiaTensor = true;
        }
    }
    private void Acceleration(int m)
    {
        switch (m)
        {
            case (1):
                break;
            case (2):
                break;
            case (3):
                break;
        }
    }
}

