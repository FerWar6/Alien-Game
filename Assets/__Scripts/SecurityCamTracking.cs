using UnityEngine;

public class SecurityCamTracking : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    public float minXRotation = -95f;
    public float maxXRotation = -70f;
    public float minDistance = 1f; // Minimum distance to prevent snapping issues
    public float maxDistance = 20f; // Maximum distance at which the camera adjusts its x rotation

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned.");
            return;
        }

        // Calculate the direction from the camera to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Calculate the distance to the player
        float distanceToPlayer = directionToPlayer.magnitude;

        // Ignore the vertical component of the direction for y-axis rotation
        directionToPlayer.y = 0;

        // Calculate the target rotation for the y-axis
        Quaternion targetYRotation = Quaternion.LookRotation(directionToPlayer);

        // Adjust the x rotation based on the distance to the player
        float t = Mathf.Clamp01((distanceToPlayer - minDistance) / (maxDistance - minDistance));
        float targetXRotation = Mathf.Lerp(maxXRotation, minXRotation, t);

        // Apply the target rotation to the camera, with the adjusted x rotation
        transform.rotation = Quaternion.Euler(targetXRotation, targetYRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
