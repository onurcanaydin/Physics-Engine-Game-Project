using UnityEngine;

namespace cyclone
{
    public class Particle
    {
        private Vector3 position;
        private Vector3 velocity;
        private Vector3 acceleration;
        private Vector3 forceAccum;
        private float damping;
        private float inverseMass;

        public Particle()
        {
            position = new Vector3();
            velocity = new Vector3();
            acceleration = new Vector3();
            forceAccum = new Vector3();
        }

        public void Integrate(float duration)
        {
            if (inverseMass <= 0)
            {
                return;
            }

            if (duration <= 0)
            {
                return;
            }

            position.AddScaledVector(velocity, duration);
            /*should be uncommented for games with short acceleration bursts
            position.AddScaledVector(acceleration, duration * duration * 0.5f);*/

            Vector3 resultingAcc = new Vector3(acceleration.x, acceleration.y, acceleration.z);
            resultingAcc.AddScaledVector(forceAccum, inverseMass);

            velocity.AddScaledVector(resultingAcc, duration);

            velocity *= Mathf.Pow(damping, duration);

            ClearAccumulator();
        }

        public void ClearAccumulator()
        {
            forceAccum.x = 0;
            forceAccum.y = 0;
            forceAccum.z = 0;
        }

        public void SetDamping(float damp)
        {
            damping = damp;
        }

        public void SetMass(float mass)
        {
            inverseMass = mass;
        }

        public void SetPosition(float x, float y, float z)
        {
            position.x = x;
            position.y = y;
            position.z = z;
        }

        public void SetForceAccum(float x, float y, float z)
        {
            forceAccum.x = x;
            forceAccum.y = y;
            forceAccum.z = z;
        }

        public void AddForce(float x, float y, float z)
        {
            forceAccum.x += x;
            forceAccum.y += y;
            forceAccum.z += z;
        }

        public void SetAcceleration(float x, float y, float z)
        {
            acceleration.x = x;
            acceleration.y = y;
            acceleration.z = z;
        }

        public void SetVelocity(float x, float y, float z)
        {
            velocity.x = x;
            velocity.y = y;
            velocity.z = z;
        }

        public void ZeroY()
        {
            acceleration.y = 0;
            velocity.y = 0;
        }

        public Vector3 GetVelocity()
        {
            return new Vector3(velocity.x, velocity.y, velocity.z);
        }

        public Vector3 GetPosition()
        {
            return new Vector3(position.x, position.y, position.z);
        }
    }
}

