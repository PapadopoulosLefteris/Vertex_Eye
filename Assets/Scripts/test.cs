using UnityEngine;

public class test : MonoBehaviour
{
    public Transform[] spheres;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;    
    }

    void Update()
    {
        if (mainCamera == null)
            return;

        // Get the position of the cursor in world space
        Vector3 cursorPosition = GetCursorPosition();

        // Rotate each sphere to look at the cursor position
        foreach (Transform sphere in spheres)
        {
            RotateTowards(sphere, cursorPosition);
        }
    }

    Vector3 GetCursorPosition()
    {
        // Raycast from the camera through the mouse position to get the cursor position in world space
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        else
        {
            // If the raycast doesn't hit anything, return a point far away from the camera
            return ray.GetPoint(8); // Adjust the distance as needed
        }
    }

    void RotateTowards(Transform target, Vector3 position)
    {
        // Calculate the rotation needed for the target to look at the position
        Quaternion targetRotation = Quaternion.LookRotation(position - target.position);
        // Apply the rotation to the target's transform
        target.rotation = targetRotation;
    }
}
