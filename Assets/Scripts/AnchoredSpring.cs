using UnityEngine;
using cyclone;
using Vector3 = cyclone.Vector3;

public class AnchoredSpring : MonoBehaviour
{
    private Vector3 anchor;
    private float springConstant = 2f;
    private float restLength = 0f;
    private float forceCoeff = 40f;

    public void ApplySpringForce(Particle particle, Vector3 mouseUpVector)
    {
        Vector3 force = new Vector3();
        force = particle.GetPosition() - mouseUpVector;
        float magnitude = force.Magnitude();
        magnitude = Mathf.Abs(magnitude - restLength);
        magnitude *= springConstant;
        force.Normalize();
        force *= magnitude;
        force *= forceCoeff;
        particle.AddForce(force.x, force.y, force.z);
    }
}
