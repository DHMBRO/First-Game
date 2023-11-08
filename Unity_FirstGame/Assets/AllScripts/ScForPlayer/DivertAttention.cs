using UnityEngine;

public class DivertAttention : MethodsFromDevelopers
{
    [SerializeField] private GameObject RockForDiverAttention;
    [SerializeField] private GameObject SphereZoneNoice;

    [SerializeField] private SlotControler ControlerSlots;
    [SerializeField] private LineRenderer LineRender;
    [SerializeField] private Transform CameraPlayer;
    [SerializeField] private Transform HandPlayer;
    

    [SerializeField] float PowerToDrop;
    [SerializeField] int CountPoints = 10;
    [SerializeField] float CalcTime = 2.0f;
    float RadiusToNoise = 0.0f;

    [SerializeField] bool CanDivrtAttention;
    [SerializeField] GameObject Rock;
    Vector3 Point;

    private void Start()
    {
        ControlerSlots = GetComponent<SlotControler>();
        LineRender = GetComponent<LineRenderer>();
        //LineRender.enabled = false;
        LineRender.enabled = true;

        if (ControlerSlots && ControlerSlots.SlotHand) HandPlayer = ControlerSlots.SlotHand;
        else Debug.Log("Not set ControlerSlots");

    }

    public void SpawnRock()
    {
        if (!HandPlayer && ControlerSlots && !this.Rock) return;

        GameObject Rock;

        ControlerSlots.PutObjectInHand();

        Rock = Instantiate(RockForDiverAttention);
        
        Rock.AddComponent<SoundCreatorScript>();

        PutObjects(Rock.transform, HandPlayer, true);

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
        if (SoundScript) RadiusToNoise = SoundScript.NoiseRadius;
        
        LineRender.positionCount = CountPoints;
        SphereZoneNoice.SetActive(true);

        if (RadiusToNoise <= 0.0f) RadiusToNoise = Rock.GetComponent<SoundCreatorScript>().NoiseRadius;
        SphereZoneNoice.transform.localScale = new Vector3(RadiusToNoise, RadiusToNoise, RadiusToNoise);

        SphereZoneNoice.transform.position = Points[Points.Length - 1];
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
        SphereZoneNoice.SetActive(false);

        Trajectory = ((CameraPlayer.position + CameraPlayer.forward * 10.0f) - Rock.transform.position).normalized * PowerToDrop;
        RIGRock.AddRelativeForce(Trajectory, ForceMode.Impulse);
         
        Destroy(Rock, 10.0f);
        ControlerSlots.GetObjectInHand();
       
    }
       
    

    
    
}
