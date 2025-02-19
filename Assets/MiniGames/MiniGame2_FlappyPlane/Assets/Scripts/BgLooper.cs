using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyPlane
{
    public class BgLooper : MonoBehaviour
    {
        public int obstarcleCount = 0;
        public Vector3 obstarcleLastPosition = Vector3.zero;
        public int numBgCount = 5;
        // Start is called before the first frame update
        void Start()
        {
            Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
            obstarcleLastPosition = obstacles[0].transform.position;
            obstarcleCount = obstacles.Length;

            for (int i = 0; i < obstarcleCount; i++)
            {
                obstarcleLastPosition = obstacles[i].SetRandomPlace(obstarcleLastPosition, obstarcleCount);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("BackGround"))
            {
                float widthOfBgobject = ((BoxCollider2D)collision).size.x;
                Vector3 pos = collision.transform.position;

                pos.x += widthOfBgobject * numBgCount;
                collision.transform.position = pos;
                return;
            }


            Obstacle obstacle = collision.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstarcleLastPosition = obstacle.SetRandomPlace(obstarcleLastPosition, obstarcleCount);
            }
        }
    }

}