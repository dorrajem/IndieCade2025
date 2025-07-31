using UnityEngine;

public class CameraController : MonoBehaviour
{
    // This script moves the camera in and out of play area

    public float back_posY = -13.5f;
    public float back_posZ = -10f;
    public float back_rotX = -60f;
    public float back_fov = 30;
    
    private float play_posY = 1f;
    private float play_posZ = -9f;
    private float play_rotX = 0f;
    private float play_fov = 50;

    public Camera mainCam;

    public float switchSpeed = 1f;

    private Vector3 target_pos;
    private Vector3 target_rot;
    private float target_fov;

    private bool CamForward = false;
    
    CardHover selectedCard = CardHover.currentlySelected;

    void Start()
    {
        mainCam = Camera.main;
        
        // Not working, needs rework
        // BackCam();
        
        target_pos = mainCam.transform.position;
        target_rot = mainCam.transform.eulerAngles;
        target_fov = mainCam.fieldOfView;
        PlayCam();
    }

    void Update()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null) return;
        
        CardHover selectedCard = CardHover.currentlySelected;
        
        if (selectedCard != null)
        {
            PlayCam();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CamForward)
            {
                BackCam();
            }
            else
            {
                PlayCam();
            }
        }
        
    }
    
    
    //Moves Camera into Play mode (top view of cards)
    public void PlayCam()
    {
        Vector3 pos = transform.position;
        pos.y = play_posY;
        pos.z = play_posZ;
        target_pos = pos;
        
        Vector3 rot = transform.eulerAngles;
        rot.x = play_rotX;
        target_rot = rot;

        target_fov = play_fov;
        
        CamForward = true;
    }
    
    //Moves Camera into Back Mode (full view of scene)
    public void BackCam()
    {
        Vector3 pos = transform.position;
        pos.y = back_posY;
        pos.z = back_posZ;
        target_pos = pos;
        
        Vector3 rot = transform.eulerAngles;
        rot.x = back_rotX;
        target_rot = rot;

        target_fov = back_fov;
        CamForward = false;
    }

    private void ForceCamBack()
    {
        transform.position = new Vector3(transform.position.x, back_posY, back_posZ);
        transform.eulerAngles = new Vector3(back_rotX, transform.eulerAngles.y, transform.eulerAngles.z);
        mainCam.fieldOfView = back_fov;
        CamForward = false;
    }
}
