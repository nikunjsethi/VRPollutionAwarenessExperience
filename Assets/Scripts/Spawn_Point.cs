using UnityEngine;
using UnityEngine.SceneManagement;

public class VRSceneSpawn : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Find the spawn point in the new scene
        GameObject spawnPoint = GameObject.Find("SpawnPoint");

        if (spawnPoint != null)
        {
            // 2. Move the entire Rig to the spawn point
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;

            // 3. (Optional) Fix the rotation so the player faces the right way
            // This rotates the Rig to match the spawn point, correcting for where the user is looking
            MatchPlayerRotation(spawnPoint.transform);
        }
    }

    private void MatchPlayerRotation(Transform target)
    {
        // Get the camera's current local rotation (headset look direction)
        float currentYRotation = Camera.main.transform.localEulerAngles.y;
        // Calculate the difference needed to face the target direction
        float rotationDelta = target.eulerAngles.y - currentYRotation;
        // Apply that difference to the Rig
        transform.Rotate(0, rotationDelta, 0);
    }
}