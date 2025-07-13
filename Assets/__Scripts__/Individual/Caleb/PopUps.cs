using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject howToPlayPopUp;
    public GameObject controlsPopUp;
    private bool isControlsOpen = false;
    private bool isHowToPlayOpen = false;

    public void HowToPlay()
    {
        if (isControlsOpen)
        {
            return;
        }
        Vector3 newPosition = howToPlayPopUp.transform.position;
        newPosition.x = 430f;
        howToPlayPopUp.transform.position = newPosition;
        isHowToPlayOpen = true;
    }

    public void Controls()
    {
        if (isHowToPlayOpen)
        {
            return;
        }
        Vector3 newPosition = controlsPopUp.transform.position;
        newPosition.x = 430f;
        controlsPopUp.transform.position = newPosition;
        isControlsOpen = true;
    }

    public void Close()
    {
        isControlsOpen = false;
        isHowToPlayOpen = false;
        Vector3 newPosition = controlsPopUp.transform.position;
        Vector3 newPosition2 = howToPlayPopUp.transform.position;
        newPosition.x = -827f;
        newPosition2.x = 1566f;
        controlsPopUp.transform.position = newPosition;
        howToPlayPopUp.transform.position = newPosition2;
    }
}
