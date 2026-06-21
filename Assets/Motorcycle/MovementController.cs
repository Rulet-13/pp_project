using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject skmbike;
    private float movespeed = 10f;
    private float rotationspeed = 100f;
    private void StartPosition()
    {
        skmbike.transform.position = new Vector3(0, 0, 0);
        skmbike.transform.rotation = Quaternion.Euler(0, 0, 0);
        skmbike.transform.localScale = new Vector3(1, 1, 1);
    }
    void Start()
    {
        StartPosition();
    }
    void Update()
    {
  
        float moveDirection = InputWS().y * movespeed * Time.deltaTime;
        skmbike.transform.Translate(0, 0, moveDirection);
        float rotationDirection = InputAD().x * rotationspeed * Time.deltaTime;
        skmbike.transform.Rotate(0, rotationDirection, 0);
    }
    private Vector2 InputWS()
    {
         if (Keyboard.current.wKey.isPressed)
            return Vector2.up;
         if (Keyboard.current.sKey.isPressed)
            return Vector2.down;
        else return Vector2.zero;
    }
    private Vector2 InputAD()
    {
        if (Keyboard.current.dKey.isPressed)
            return Vector2.right;
        if (Keyboard.current.aKey.isPressed)
           return Vector2.left;
        else return Vector2.zero;
    }
}
  
