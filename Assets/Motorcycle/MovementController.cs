using System.Diagnostics.SymbolStore;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject skmbike;
    private float movespeedMaxW = 2000f; private float movespeednow = 0f; private float movespeedMaxS = 500f; 
    private float rotationspeed = 80f; private float slantmax = 25f; private float slantnow = 0f; private float leanSpeed = 20f;
    private Rigidbody rbbike;
    private bool tens = false;
    private float moveDirection { get { return InputWS().y * movespeednow; } }
    private float rotationDirection { get { return InputAD().x * rotationspeed; } }


    private void StartPosition()
    {
        skmbike.transform.position = new Vector3(0, 4, 0);
        skmbike.transform.rotation = Quaternion.Euler(0, 0, 0);
        tens = false;
        TensorControl();
    }
    private void Awake()
    {
        rbbike = skmbike.GetComponent<Rigidbody>();
        rbbike.maxAngularVelocity = Vector2.one.magnitude * slantmax;
    }
    private void Start()
    {
        StartPosition();
    }
    private void FixedUpdate()
    {
        //rbbike.linearVelocity = (skmbike.transform.forward * moveDirection * Time.fixedDeltaTime) + (Vector3.up * rbbike.linearVelocity.y);
        //skmbike.transform.localRotation = Quaternion.Euler(0, skmbike.transform.localRotation.eulerAngles.y, -slantnow); //наклон
        //skmbike.transform.Rotate(0f, rotationDirection * Time.fixedDeltaTime, 0f, Space.World); //разворот
        rbbike.linearVelocity = (skmbike.transform.forward * moveDirection * Time.fixedDeltaTime) + (Vector3.up * rbbike.linearVelocity.y);
        Quaternion targetRotation = Quaternion.Euler(0, skmbike.transform.localRotation.eulerAngles.y, -slantnow);
        Quaternion yawRotation = Quaternion.Euler(0f, rotationDirection * Time.fixedDeltaTime, 0f);
        Quaternion finalRotation = targetRotation * yawRotation;
        rbbike.MoveRotation(finalRotation);
        TensorControl();

    }

    private void Update()
    {
        Restart();
    }
    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log("столкновение с объектом " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("wall"))
        {
            movespeednow = 0f;
            Debug.Log("врезание в стену");
        }
       
    }

    private Vector2 InputWS()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            movespeednow += 2f;
            if (movespeednow > movespeedMaxW) movespeednow = movespeedMaxW;
            tens = true;
            return Vector2.up;
        }
        else if (Keyboard.current.sKey.isPressed)
        {  
            movespeednow++;
            if (movespeednow > movespeedMaxS) movespeednow = movespeedMaxS;
            tens = true;
            return Vector2.down;
        }
        else
        {
            movespeednow--;
            if (movespeednow < 0) movespeednow = 0;
            tens = false;
            return Vector2.zero;
        }
    }
    private Vector2 InputAD()
    {
        if (Keyboard.current.dKey.isPressed && (Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed))
        {
            slantnow += leanSpeed * Time.deltaTime;
            slantnow = Mathf.Clamp(slantnow, -slantmax, slantmax);

            return Vector2.right;
        }

        if (Keyboard.current.aKey.isPressed && (Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed))
        {
            slantnow -= leanSpeed * Time.deltaTime;
            slantnow = Mathf.Clamp(slantnow, -slantmax, slantmax);

            return Vector2.left;
        }

        slantnow = Mathf.MoveTowards(
            slantnow,
            0,
            leanSpeed * Time.deltaTime);

        return Vector2.zero;
    }
    private void Restart()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            StartPosition();
        }
    }
    private void TensorControl()
    {
        if (tens)
        {
            rbbike.automaticInertiaTensor = false;
            rbbike.inertiaTensor = new Vector3(10, 10, 1);
        }
        else
        {
            rbbike.automaticInertiaTensor = true;
        }
    }

}


