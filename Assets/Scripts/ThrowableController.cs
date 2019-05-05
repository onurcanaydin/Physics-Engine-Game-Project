using UnityEngine;
using cyclone;
using Vector3 = cyclone.Vector3;

public class ThrowableController : MonoBehaviour
{
    public Vector3 startPos;
    public Particle throwableParticle;
    public AnchoredSpring anchoredSpring;
    private float gravity = -5f;

    private void Awake()
    {
        throwableParticle = new Particle();
        throwableParticle.SetPosition(transform.position.x, transform.position.y, transform.position.z);
        startPos = new Vector3();
        startPos.x = transform.position.x;
        startPos.y = transform.position.y;
        startPos.z = transform.position.z;
        throwableParticle.SetMass(1f);
        throwableParticle.SetDamping(0.995f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    private void FixedUpdate()
    {
        Vector3 accVector = new Vector3();
        accVector += new Vector3(0, gravity, 0);
        throwableParticle.AddForce(accVector.x, accVector.y, accVector.z);
        throwableParticle.Integrate(Time.fixedDeltaTime);
        transform.position = throwableParticle.GetPosition().CycloneToUnity();
    }

    private void OnMouseUp()
    {
        UnityEngine.Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0;
        Debug.Log(mp);
        Vector3 mousePos = new Vector3(mp.x, mp.y, mp.z);
        anchoredSpring.ApplySpringForce(throwableParticle, mousePos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        throwableParticle.ZeroY();
        gravity = 0f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gravity = -9.8f;
    }

    public void Reset()
    {
        throwableParticle.SetAcceleration(0, 0, 0);
        throwableParticle.SetVelocity(0, 0, 0);
        throwableParticle.ClearAccumulator();
        throwableParticle.SetPosition(startPos.x, startPos.y, startPos.z);
    }
}
