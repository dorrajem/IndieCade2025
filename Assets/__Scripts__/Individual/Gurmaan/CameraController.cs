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
    
    CardHover selectedCard = CardHover.currentlySelected;

    void Start()
    {
        mainCam = Camera.main;
        target_pos = mainCam.transform.position;
        target_rot = mainCam.transform.eulerAngles;
        target_fov = mainCam.fieldOfView;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || selectedCard != null)
        {
            PlayCam();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && selectedCard == null)
        {
            BackCam();
        }
        
        transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime*switchSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target_rot), Time.deltaTime*switchSpeed*4);
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, target_fov, Time.deltaTime*switchSpeed/4);
    }
    
    
    //Moves Camera into Play mode (top view of cards)
    void PlayCam()
    {
        Vector3 pos = transform.position;
        pos.y = play_posY;
        pos.z = play_posZ;
        target_pos = pos;
        
        Vector3 rot = transform.eulerAngles;
        rot.x = play_rotX;
        target_rot = rot;

        target_fov = play_fov;
    }
    
    //Moves Camera into Back Mode (full view of scene)
    void BackCam()
    {
        Vector3 pos = transform.position;
        pos.y = back_posY;
        pos.z = back_posZ;
        target_pos = pos;
        
        Vector3 rot = transform.eulerAngles;
        rot.x = back_rotX;
        target_rot = rot;

        target_fov = back_fov;
    }
}
