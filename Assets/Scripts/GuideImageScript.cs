using UnityEngine;

public class GuideImageScript : MonoBehaviour
{
    public Transform userHead; // Assign the VR Camera (CenterEyeAnchor or Main Camera)
    public float distanceFromUser = 5.0f; // Distance in meters in front of the user
    public float heightOffset = 9.0f; // Adjust this value to raise the canvas higher

    void Start()
    {
        if (userHead != null)
        {
            // Start with the user's position
            Vector3 targetPosition = userHead.position;

            // Modify only the y-axis for height
            targetPosition.y += heightOffset;

            // Move the canvas forward
            targetPosition += userHead.forward * distanceFromUser;

            // Apply the final position
            transform.position = targetPosition;

            // Ensure it faces the user
            transform.LookAt(new Vector3(userHead.position.x, transform.position.y, userHead.position.z)); // Only rotate horizontally

            // Fix the mirrored issue by rotating 180 degrees around the Y-axis
            transform.Rotate(0, 180, 0);
        }
        else
        {
            Debug.LogError("ERROR: userHead (VR Camera) is not assigned!");
        }
    }
}
