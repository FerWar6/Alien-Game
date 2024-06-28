using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeedX = 10f;
    public float rotationSpeedY = 5f;

    void Update()
    {
        // Rotate the sphere around the X and Y axes
        transform.Rotate(Vector3.right, rotationSpeedX * Time.deltaTime);
        transform.Rotate(Vector3.up, rotationSpeedY * Time.deltaTime);
    }
}
