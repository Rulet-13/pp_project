using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLogic : MonoBehaviour
{
    public GameObject skmbike;
    public Camera Camera; 
    void Awake()
    {
        SetCamera();
    }
    private void LateUpdate()
    {
        SetCamera();
    }
    void Update()
    {
       
    }
    public Vector3 GetPosition(GameObject obj)
    {
        return obj.transform.position;
    }
    public Quaternion GetRotation(GameObject obj)
    {
        return obj.transform.rotation;
    }
    public void SetCamera()
    {
        Camera.transform.position = GetPosition(skmbike) + new Vector3(0, 1.4f, 0);
        Camera.transform.rotation = GetRotation(skmbike) * Quaternion.Euler(5f, 1f, 1f);
    }
}
