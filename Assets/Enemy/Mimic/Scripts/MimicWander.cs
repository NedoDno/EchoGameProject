using UnityEngine;
using UnityEngine.AI;

namespace MimicSpace
{
    public class MimicWander : MonoBehaviour
    {
        public NavMeshAgent agent;
        public float wanderRadius = 10f;
        public float wanderTimer = 5f;
        public float height = 0.8f;
        public LayerMask obstacleLayer; 
        public float playerDetectionRadius = 20f;
        public LayerMask playerLayer; 
        private Transform lightSource; 

        private Transform playerTransform;
        private float timer;
        private bool isInLitArea = false;
        Mimic myMimic;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
        }
        void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
            timer = wanderTimer;
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (PlayerDetected() && !IsObstacleBetween())
            {
                agent.SetDestination(playerTransform.position);
            }
            else if (isInLitArea)
            {
                MoveToDarkerArea();
            }
            else if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        }

        bool PlayerDetected()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, playerDetectionRadius, playerLayer);
            foreach (var hit in hits)
            {
                if (hit.gameObject.transform == playerTransform)
                {
                    return true;
                }
            }
            return false;
        }

        bool IsObstacleBetween()
        {
            if (playerTransform == null)
                return false;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, Vector3.Distance(transform.position, playerTransform.position), obstacleLayer))
            {
                return hit.transform != playerTransform;
            }
            return false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("LightSource"))
            {
                isInLitArea = true;
                lightSource = other.gameObject.transform;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("LightSource"))
            {
                isInLitArea = false;
            }
        }


        void MoveToDarkerArea()
        {
            Vector3 directionAwayFromLight = transform.position - lightSource.position;
            Vector3 darkPosition = transform.position + directionAwayFromLight.normalized * wanderRadius;
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(darkPosition, out navHit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(navHit.position);
                //transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
            }
        }

        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
            return navHit.position;
        }
    }
}
