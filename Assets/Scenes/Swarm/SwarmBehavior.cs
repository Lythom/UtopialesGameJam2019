using UnityEngine;

namespace Scenes.Swarm
{
    public class SwarmBehavior : MonoBehaviour
    {
        public GameObject cellFishPrefab;
        public GameObject goalPrefab;

        public static Vector3 goalPos = Vector3.zero;
        
        private static int numEntity = 20;
        public static int areaSize = 10;

        public static GameObject[] swarm = new GameObject[numEntity];
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < numEntity; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-areaSize, areaSize),
                    Random.Range(-areaSize, areaSize), 0);
                swarm[i] = (GameObject) Instantiate(cellFishPrefab, pos, Quaternion.identity);
            }
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Random.Range(0, 10000) < 50)
            {
                goalPos = new Vector3(Random.Range(-areaSize, areaSize),
                    Random.Range(-areaSize, areaSize), 0);
                goalPrefab.transform.position = goalPos;
            }
        }
    }
}
