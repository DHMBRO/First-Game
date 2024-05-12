using UnityEngine;

public class DivertAttention : MethodsFromDevelopers
{
    [SerializeField] private GameObject RockForDiverAttention;
    [SerializeField] private GameObject ZoneNoise;
    [SerializeField] private GameObject LocalZoneNoise;

    [SerializeField] private SlotControler ControlerSlots;
    [SerializeField] private LineRenderer LineRender;
    [SerializeField] private Transform CameraPlayer;
    [SerializeField] private Transform HandPlayerForThrow;
    

    [SerializeField] float PowerToDrop;
    [SerializeField] int CountPoints = 10;
    [SerializeField] float CalcTime = 2.0f;
    [SerializeField] float RadiusToNoise = 10.0f;

    [SerializeField] bool CanDivrtAttention;
    [SerializeField] GameObject Rock;
    Vector3 Point;

    private void Start()
    {
        ControlerSlots = GetComponent<SlotControler>();
        LineRender = GetComponent<LineRenderer>();
        //LineRender.enabled = false;
        LineRender.enabled = true;

        if (!HandPlayerForThrow) Debug.Log("Not set HandPlayerForThrow");

    }

    public void SpawnRock()
    {
        if (!HandPlayerForThrow && ControlerSlots && !this.Rock) return;

        GameObject Rock;

        ControlerSlots.PutObjectInHand();

        Rock = Instantiate(RockForDiverAttention);
        
        Rock.AddComponent<SoundCreatorScript>();

        PutObjects(Rock.transform, HandPlayerForThrow, true);

        this.Rock = Rock;
        LineRender.enabled = true;


    }


    public void AimingToDrop()
    {
        Vector3 CameraTarget = CameraPlayer.position + CameraPlayer.forward * 10.0f;
        
        if (CountPoints <= 0) return;
        Vector3[] Points = new Vector3[CountPoints];
        float DeltaT;
        DeltaT = CalcTime / CountPoints;
        

        //GravityV = 0.0f;
        
        int i = 0;
        bool TouchPlane = false;
        for (float CurrentTime = 0.0f; CurrentTime <= CalcTime && i < Points.Length; CurrentTime += DeltaT, i++)
        {
            Points[i] = GetPointByTime(CurrentTime, PowerToDrop, Rock.transform.position, CameraTarget - Rock.transform.position);


            if (!TouchPlane && i >= 1)
            {
                if (Physics.Raycast(Points[i-1], (Points[i] - Points[i-1]).normalized, out RaycastHit RayResult, (Points[i-1] - Points[i]).magnitude))
                {
                    //Debug.Log(RayResult.collider.gameObject.name);
                    TouchPlane = true;
                    Points[i] = RayResult.point;
                                
                }

            }
            else if(i >= 1) Points[i] = Points[i-1]; 
        }

        SoundCreatorScript SoundScript = Rock.GetComponent<SoundCreatorScript>();
        //if (SoundScript) SoundScript.CurrentNoiceRadiuse = RadiusToNoise;
        
        //Debug.Log(SoundScript.CurrentNoiceRadiuse + "\t" + RadiusToNoise);

        LineRender.positionCount = CountPoints;

        if(!LocalZoneNoise) LocalZoneNoise = Instantiate(ZoneNoise);

        LocalZoneNoise.SetActive(true);
        LocalZoneNoise.transform.position = Points[Points.Length - 1];
        LocalZoneNoise.transform.localScale = new Vector3(RadiusToNoise, RadiusToNoise, RadiusToNoise) * 2.0f;

        LineRender.SetPositions(Points);
        
    }

    Vector3 GetPointByTime(float CurrentTime, float PowerDrop, Vector3 StartPosition, Vector3 StartDirection)
    {
        Vector3 Velocity = StartDirection.normalized * PowerDrop;

        Vector3 Point = new Vector3();

        Point.x = StartPosition.x + Velocity.x * CurrentTime;
        Point.z = StartPosition.z + Velocity.z * CurrentTime;
        
        Point.y = StartPosition.y + Velocity.y * CurrentTime + (Physics.gravity.y * Mathf.Pow(CurrentTime, 2.0f)) / 2;

        this.Point = Point;
        return Point;
        
    }

    public void DropRock()
    {
        
        Vector3 Trajectory;
        Rigidbody RIGRock;

        SoundCreatorScript SoundScript;
        ExecutoreScriptToRock ExecutorScript;

        LineRender.enabled = false;

        Rock.transform.SetParent(null);
        Rock.transform.rotation = CameraPlayer.transform.rotation;

        RIGRock = Rock.AddComponent<Rigidbody>();
        RIGRock.rotation = Quaternion.identity;

        SoundScript = Rock.GetComponent<SoundCreatorScript>();
        ExecutorScript = Rock.AddComponent<ExecutoreScriptToRock>();
        LocalZoneNoise.SetActive(false);

        Trajectory = ((CameraPlayer.position + CameraPlayer.forward * 10.0f) - Rock.transform.position).normalized * PowerToDrop;
        RIGRock.AddRelativeForce(Trajectory, ForceMode.Impulse);
         
        Destroy(Rock, 10.0f);
        ControlerSlots.GetObjectInHand();
       
    }
       
    

    
    
}
