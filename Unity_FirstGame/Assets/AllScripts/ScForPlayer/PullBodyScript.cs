using UnityEngine;

public class PullBodyScript : MonoBehaviour
{
    [SerializeField] PlayerControler ControlerPlayer;
    [SerializeField] HingeJoint PlayerHingeJoint;

    private void Start()
    {
        ControlerPlayer = GetComponent<PlayerControler>();
        
    }

    public void SearchEnemyBody()
    {
        /*
        if (!ControlerPlayer.CameraPlayerF3)
        {
            Debug.Log("Not set CameraPlayerF3");
            return;
        }

        Vector3 Origin = transform.TransformPoint(ControlerPlayer.CameraPlayerF3.DesirableVector);
        Vector3 Direction = ControlerPlayer.CameraPlayerF3.transform.position = 
            ControlerPlayer.CameraPlayerF3.transform.forward * ControlerPlayer.CameraPlayerF3.CurrentMoveBackDistance;

        //Debug.DrawRay(Origin, Direction, Color.red);

        if (Physics.Raycast(Origin, Direction, out RaycastHit HitResult, ControlerPlayer.CameraPlayerF3.CurrentMoveBackDistance))
        {
            Debug.Log(HitResult.collider.name);

        }
        */
    }

}
