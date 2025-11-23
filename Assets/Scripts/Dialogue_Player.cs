using UnityEngine;

public class Dialogue_Player : MonoBehaviour
{
    public Question_handler question_Handler;
    public AudioSource speaker;
    public AudioClip AudioClip1;
    public AudioClip AudioClip2;
    public AudioClip AudioClip3;
    public AudioClip AudioClip4;

    private bool clip1 = false;
    private bool clip2 = false;
    private bool clip3 = false;
    private bool clip4 = false;



    // Update is called once per frame
    void Update()
    {
        if(question_Handler.questionIndex == 0 && clip1 == false)
        {
            speaker.PlayOneShot(AudioClip1);
            clip1 = true;
        }
        if(question_Handler.questionIndex == 2 && clip2 == false)
        {
            speaker.PlayOneShot(AudioClip1);
            clip2 = true;
        }
        if(question_Handler.questionIndex == 3 && clip3 == false)
        {
            speaker.PlayOneShot(AudioClip1);
            clip3 = true;
        }
        if(question_Handler.questionIndex == 4 && clip4 == false)
        {
            speaker.PlayOneShot(AudioClip1);
            clip4 = true;
        }
    }
}
