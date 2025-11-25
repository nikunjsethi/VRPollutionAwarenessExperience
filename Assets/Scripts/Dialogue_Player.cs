using System.Collections;
using UnityEngine;

public class Dialogue_Player : MonoBehaviour
{
    public Question_handler question_Handler;
    public float introWaitTime = 1f;
    public float dialogueWaitTime = 0.5f;
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
        PlayIntro(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialogue(int index)
    {
        StartCoroutine(DelayDialogue(index));
    }
    public void PlayIntro(int index)
    {

        StartCoroutine(DelayIntro(index));
        
    }

    IEnumerator DelayDialogue(int index)
    {
        yield return new WaitForSeconds(dialogueWaitTime);

        if(index == 1)
        {
            speaker.Stop();
            speaker.PlayOneShot(q1);
        }
        if(index == 2)
        {
            speaker.Stop();
            speaker.PlayOneShot(q2);
        }
        if (index == 3)
        {
            speaker.Stop();
            speaker.PlayOneShot(q3);
        }
        if(index == 4)
        {
            speaker.Stop();
            speaker.PlayOneShot(q4);
        }
    }

    IEnumerator DelayIntro(int index)
    {
        yield return new WaitForSeconds(introWaitTime);

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

    public void StopAudio()
    {
        speaker.Stop();
    }
}
