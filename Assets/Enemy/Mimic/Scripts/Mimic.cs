using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    public class Mimic : MonoBehaviour
    {
        [Header("Animation")]
        public GameObject legPrefab;

        [Range(2, 20)]
        public int numberOfLegs = 5;
        [Tooltip("The number of splines per leg")]
        [Range(1, 10)]
        public int partsPerLeg = 4;
        int maxLegs;

        public int legCount;
        public int deployedLegs;
        [Range(0, 19)]
        public int minimumAnchoredLegs = 2;
        public int minimumAnchoredParts;

        [Tooltip("Minimum duration before leg is replaced")]
        public float minLegLifetime = 5;
        [Tooltip("Maximum duration before leg is replaced")]
        public float maxLegLifetime = 15;

        public Vector3 legPlacerOrigin = Vector3.zero;
        [Tooltip("Leg placement radius offset")]
        public float newLegRadius = 3;

        public float minLegDistance = 4.5f;
        public float maxLegDistance = 6.3f;

        [Range(2, 50)]
        [Tooltip("Number of spline samples per legpart")]
        public int legResolution = 40;

        [Tooltip("Minimum lerp coeficient for leg growth smoothing")]
        public float minGrowCoef = 4.5f;
        [Tooltip("MAximum lerp coeficient for leg growth smoothing")]
        public float maxGrowCoef = 6.5f;

        [Tooltip("Minimum duration before a new leg can be placed")]
        public float newLegCooldown = 0.3f;

        bool canCreateLeg = true;

        List<GameObject> availableLegPool = new List<GameObject>();

        [Tooltip("This must be updates as the Mimic moves to assure great leg placement")]
        public Vector3 velocity;

        void Start()
        {
            ResetMimic();
        }

        private void OnValidate()
        {
            ResetMimic();
        }

        private void ResetMimic()
        {
            foreach (Leg g in GameObject.FindObjectsOfType<Leg>())
            {
                if (g.transform.parent == transform) // Ensure only legs belonging to this mimic are affected
                {
                    Destroy(g.gameObject);
                }
            }
            legCount = 0;
            deployedLegs = 0;

            maxLegs = numberOfLegs * partsPerLeg;
            float rot = 360f / maxLegs;
            Vector2 randV = Random.insideUnitCircle;
            velocity = new Vector3(randV.x, 0, randV.y);
            minimumAnchoredParts = minimumAnchoredLegs * partsPerLeg;
            maxLegDistance = newLegRadius * 2.1f;
        }

        IEnumerator NewLegCooldown()
        {
            canCreateLeg = false;
            yield return new WaitForSeconds(newLegCooldown);
            canCreateLeg = true;
        }

        void Update()
        {
            if (!canCreateLeg)
                return;

            // New leg origin is placed in front of the mimic
            legPlacerOrigin = transform.position + velocity.normalized * newLegRadius;

            if (legCount <= maxLegs - partsPerLeg)
            {
                Vector2 offset = Random.insideUnitCircle * newLegRadius;
                Vector3 newLegPosition = legPlacerOrigin + new Vector3(offset.x, 0, offset.y);
                // Further adjustments...

                RaycastHit hit;
                if (Physics.Raycast(newLegPosition + Vector3.up * 10f, -Vector3.up, out hit))
                {
                    Vector3 myHit = hit.point;
                    if (Physics.Linecast(transform.position, hit.point, out hit))
                        myHit = hit.point;

                    float lifeTime = Random.Range(minLegLifetime, maxLegLifetime);

                    StartCoroutine("NewLegCooldown");
                    for (int i = 0; i < partsPerLeg; i++)
                    {
                        RequestLeg(myHit, legResolution, maxLegDistance, Random.Range(minGrowCoef, maxGrowCoef), this, lifeTime);
                        if (legCount >= maxLegs)
                            return;
                    }
                }
            }
        }

        void RequestLeg(Vector3 footPosition, int legResolution, float maxLegDistance, float growCoef, Mimic myMimic, float lifeTime)
        {
            GameObject newLeg;
            if (availableLegPool.Count > 0)
            {
                newLeg = availableLegPool[availableLegPool.Count - 1];
                availableLegPool.RemoveAt(availableLegPool.Count - 1);
            }
            else
            {
                newLeg = Instantiate(legPrefab, transform.position, Quaternion.identity);
            }
            newLeg.SetActive(true);
            newLeg.GetComponent<Leg>().Initialize(footPosition, legResolution, maxLegDistance, growCoef, myMimic, lifeTime);
            newLeg.transform.SetParent(myMimic.transform);
            legCount++;
        }

        public void RecycleLeg(GameObject leg)
        {
            if (leg.transform.parent == transform) // Ensure only legs belonging to this mimic are recycled
            {
                availableLegPool.Add(leg);
                leg.SetActive(false);
                legCount--;
            }
        }
    }
}
