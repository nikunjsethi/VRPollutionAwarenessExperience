using UnityEditor.UI;
using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    [Header("Setup")]

    public int questionIndex = 0;
    [Tooltip("The GameObject to show (e.g., a World Space Canvas).")]
    public GameObject popupContent;
    public Question_handler question_Handler;
    public Dialogue_Player dialogue_Player;


    [Tooltip("The tag checking for the player.")]
    private string playerTag = "Player";
    
    [Tooltip("If true, the popup will face the player when it appears.")]
    public bool facePlayerOnEnable = true;

    private void Start()
    {
        // Ensure the popup is hidden when the game starts
        if (popupContent != null)
        {
            popupContent.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && question_Handler.CanTrigger(questionIndex))
        {
            if (popupContent != null)
            {
                popupContent.SetActive(true);
                if (facePlayerOnEnable) LookAtPlayer(other.transform);
            }

            dialogue_Player.PlayDialogue(questionIndex);
        }
    }

    private void LookAtPlayer(Transform player)
    {
        // Simple billboard effect so the UI faces the user
        Vector3 direction = player.position - popupContent.transform.position;
        direction.y = 0; // Keep text upright
        if (direction != Vector3.zero)
        {
            popupContent.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}