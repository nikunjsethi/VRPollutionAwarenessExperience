using UnityEngine;
using UnityEngine.SceneManagement;

public class Question_handler : MonoBehaviour
{
    public int currentIndex = 0;

    public int score = 0;
    private Dialogue_Player dialogue_Player;
    public Scene_Transition scene_Transition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = 0;
        dialogue_Player = GetComponent<Dialogue_Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore()
    {
       score += 1;
    }

    public bool CanTrigger(int questionIndex)
    {
        if(currentIndex == questionIndex-1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateIndex()
    {
        currentIndex += 1;
        dialogue_Player.PlayIntro(currentIndex);
        scene_Transition.OnIndexUpdated(currentIndex, score);
    }


}
