using UnityEngine;

namespace Scenes.Swarm
{
    public class Swarmer : MonoBehaviour
    {
        public float speed = 0.1f;

        float rotationSpeed = 6.0f;

        Vector2 averageHeading;
        Vector2 centerSwarmPos;
        float maxNeighbourDistance = 2.0f;

        bool turning;
        // Start is called before the first frame update
        void Start()
        {
            speed = Random.Range(5.0f, 10.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, Vector3.zero) >= SwarmBehavior.areaSize)
                turning = true;
            else
                turning = false;
            if (turning)
            {
                var transform1 = this.transform;
                Vector3 direction = Vector3.zero - transform1.position;
                this.transform.rotation = Quaternion.Slerp(transform1.rotation,
                    Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
            else
                if (Random.Range(0, 5) < 1)
                        BehaveInSwarm();
            transform.Translate(0,0,Time.deltaTime * speed);
        }

        void BehaveInSwarm()
        {
            GameObject[] swarm = SwarmBehavior.swarm;

            Vector3 vCenter = Vector3.zero;
            Vector3 vAvoid = Vector3.zero;
            float globalSpeed = 0.1f;

            Vector3 goalPos = SwarmBehavior.goalPos;

            float dist;
        
            int groupSize = 0;
            foreach (GameObject entity in swarm)
            {
                if (entity != this.gameObject)
                {
                    dist = Vector3.Distance(this.transform.position, entity.transform.position);
                    if (dist <= maxNeighbourDistance)
                    {
                        vCenter += entity.transform.position;
                        groupSize++;

                        if (dist < 1.0f)
                        {
                            vAvoid += (this.transform.position - entity.transform.position);
                        }

                        Swarmer neighbourSwarmer = entity.GetComponent<Swarmer>();
                        globalSpeed += neighbourSwarmer.speed;
                    }
                }
            }

            if (groupSize > 0)
            {
                var transform1 = transform;
                var position = transform1.position;
                vCenter = vCenter / groupSize + (goalPos - position);
                speed = globalSpeed / groupSize;

                Vector3 direction = (vCenter + vAvoid) - position;

                if (direction != Vector3.zero)
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
