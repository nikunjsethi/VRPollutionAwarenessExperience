using UnityEditor.UI;
using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("The GameObject to show (e.g., a World Space Canvas).")]
    public GameObject popupContent;
    public AudioSource Speaker;
    public AudioClip question_Clip;
    public bool questionAnswered = false;
    public int questionIndex = 0;
    public Question_handler question_Handler;
    private int questionPlayed = 0;     




    [Tooltip("The tag checking for the player.")]
    public string playerTag = "Player";

    [Header("Behavior")]
    [Tooltip("If true, the popup hides when you walk away.")]
    public bool hideOnExit = true;
    
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
        if(question_Handler.questionIndex == questionIndex - 1 && questionPlayed < questionIndex)
        {
            // Check if the object entering is the Player
            if (other.CompareTag(playerTag))
            {
                if (popupContent != null)
                {
                    popupContent.SetActive(true);
                    if (facePlayerOnEnable) LookAtPlayer(other.transform);
                }

                if(Speaker != null && question_Clip != null)
                {
                    Speaker.PlayOneShot(question_Clip);
                }

                questionPlayed = questionIndex;
            }
        }

        
    }

    private void Question_Answered()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (questionAnswered && other.CompareTag(playerTag))
        {
            if (popupContent != null)
            {
                popupContent.SetActive(false);
            }
        }
    }

    private void LookAtPlayer(Transform player)
    {
        // Simple billboard effect so the UI faces the user
        Vector3 direction = player.position - popupContent.transform.position;
        direction.y = 0; // Keep text upright
        if (direction != Vector3.zero)
        {
            popupContent.transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}