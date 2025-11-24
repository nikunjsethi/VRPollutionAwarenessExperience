using UnityEngine;

public class Dialogue_Player : MonoBehaviour
{
    public Question_handler question_Handler;
    public bool clipPlayed = false;
    private int currentIndex = 0;
    private AudioSource speaker;
    public AudioClip intro1;
    public AudioClip q1;
    public AudioClip q2;
    public AudioClip intro2;
    public AudioClip q3;
    public AudioClip intro3;
    public AudioClip q4;
    public AudioClip intro4;

    void Start()
    {
        speaker = GetComponent<AudioSource>();
        currentIndex = 0;
        PlayIntro(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialogue(int index)
    {
        if(index == 1)
        {
            speaker.PlayOneShot(q1);
        }
        if(index == 2)
        {
            speaker.PlayOneShot(q2);
        }
        if (index == 3)
        {
            speaker.PlayOneShot(q3);
        }
        if(index == 4)
        {
            speaker.PlayOneShot(q4);
        }
    }
    public void PlayIntro(int index)
    {
        if(index == 0)
        {
            speaker.PlayOneShot(intro1);
        }
        if(index == 2)
        {
            speaker.PlayOneShot(intro2);
        }
        if(index == 3)
        {
            speaker.PlayOneShot(intro3);
        }
        if(index == 4)
        {
            speaker.PlayOneShot(intro4);
        }
    }
}
