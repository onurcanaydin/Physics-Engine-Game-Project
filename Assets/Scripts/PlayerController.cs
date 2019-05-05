using UnityEngine;
using cyclone;
using Vector3 = cyclone.Vector3;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public Particle particle;
    public Image jetpackFill;

    [SerializeField]
    private bool jump = false;
    [SerializeField]
    private bool right = false;
    [SerializeField]
    private bool left = false;
    [SerializeField]
    private bool grounded = true;
    [SerializeField]
    private bool jetpacking = false;
    private bool downwardsGravity = true;
    private float jumpAcc = 100f;
    private float runAcc = 10f;
    private float accCoeff = 1f;
    private float gravity = -5f;
    private float jetpackFuel = 100f;
    private float jetpackAcc = 10f;
    private float jetpackDrainCoeff = 40f;
    private float jetpackFillCoeff = 20f;
    private Vector3 startPos;

    void Start()
    {
        startPos = new Vector3();
        startPos.x = transform.position.x;
        startPos.y = transform.position.y;
        startPos.z = transform.position.z;
        particle = new Particle();
        particle.SetMass(1f);
        particle.SetDamping(0.9f);
        particle.SetPosition(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            jump = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            jetpacking = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(downwardsGravity);
            downwardsGravity = !downwardsGravity;
            Debug.Log(downwardsGravity);
            if (downwardsGravity)
            {
                gravity = -5f;
            }
            else
            {
                gravity = 5f;
            }
        }
        Debug.Log(gravity);
    }

    private void FixedUpdate()
    {
        Vector3 accVector = new Vector3();
        if (jump == true)
        {
            jump = false;
            if(grounded == true)
            {
                if (downwardsGravity)
                {
                    accVector += new Vector3(0, jumpAcc, 0);
                }
                else
                {
                    accVector += new Vector3(0, -jumpAcc, 0);
                }
            }
        }
        if (right == true)
        {
            accVector += new Vector3(runAcc, 0, 0);
            right = false;
        }
        if (left == true)
        {
            accVector += new Vector3(-runAcc, 0, 0);
            left = false;
        }
        if (jetpacking == true && jetpackFuel > 0)
        {
            if (downwardsGravity)
            {
                accVector += new Vector3(0, jetpackAcc, 0);
            }
            else
            {
                accVector += new Vector3(0, -jetpackAcc, 0);

            }
            jetpackFuel -= jetpackDrainCoeff * Time.fixedDeltaTime;
            jetpacking = false;
        }
        if (grounded == true && jetpackFuel < 100f)
        {
            jetpackFuel += jetpackFillCoeff * Time.fixedDeltaTime;
        }
        jetpackFill.fillAmount = jetpackFuel / 100;
        accVector += new Vector3(0, gravity, 0);
        particle.SetForceAccum(accVector.x, accVector.y, accVector.z);
        particle.Integrate(Time.fixedDeltaTime);
        transform.position = particle.GetPosition().CycloneToUnity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        particle.ZeroY();
        gravity = 0f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        if (downwardsGravity)
        {
            gravity = -5f;
        }
        else
        {
            gravity = 5f;
        }
    }

    private void Reset()
    {
        particle.SetAcceleration(0, 0, 0);
        particle.SetVelocity(0, 0, 0);
        particle.ClearAccumulator();
        particle.SetPosition(startPos.x, startPos.y, startPos.z);
        jetpackFuel = 100f;
        downwardsGravity = true;
    }
}
