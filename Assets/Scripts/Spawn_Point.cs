using UnityEngine;

public class SetPlayerPosition : MonoBehaviour
{
    // Tag your VR Rig as "Player" in the inspector!
    public string playerTag = "Player"; 

    void Start()
    {
        // 1. Find the Player Rig in the scene (works for both persistent and new rigs)
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            // 2. Teleport the player to THIS object's position
            player.transform.position = transform.position;
            
            // 3. Match rotation (so you face the right way)
            player.transform.rotation = transform.rotation;
            
            Debug.Log("Player successfully moved to SpawnPoint!");
        }
        else
        {
            Debug.LogError("Could not find the Player! Did you forget to set the Tag?");
        }
    }
}