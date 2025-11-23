using UnityEngine;

public class Question_handler : MonoBehaviour
{
    public GameObject question1;
    public GameObject question2;
    public GameObject question3;
    public GameObject question4;
    public int questionIndex = 0;

    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(question1.GetComponent<PopupTrigger>().questionAnswered == true)
        {
            questionIndex = 1;
        }

        if(question2.GetComponent<PopupTrigger>().questionAnswered == true)
        {
            questionIndex = 2;
        }

        if(question3.GetComponent<PopupTrigger>().questionAnswered == true)
        {
            questionIndex = 3;
        }

        if(question4.GetComponent<PopupTrigger>().questionAnswered == true)
        {
            questionIndex = 4;
        }
    }
    public void CorrectAnswer()
    {
       score += 1;
    }


}
