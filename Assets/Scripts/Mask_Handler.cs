using UnityEngine;

public class Mask_Handler : MonoBehaviour
{
    public GameObject floorMask;
    public GameObject playerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMask.SetActive(false);
    }

    public void WearMask()
    {
        playerMask.SetActive(true);
        floorMask.SetActive(false);
    }


}
