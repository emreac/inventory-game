using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        if (mainCamera != null) {
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
     
    }
}
