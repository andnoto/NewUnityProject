using UnityEngine;
using System.Collections;

public class BalloonBlueScript : MonoBehaviour
{
    public Transform FireworksPrefab;
    public float FireworksVectorUp = 1.0f;
    public AudioClip FireworkSound;
    public Transform Balloon_redPrefab;
    public float BalloonSecurityDistance = 2.0f;
    public int BalloonMaxRandomDistance = 3;
    public float BalloonMaxXPosition = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Create a new firework.
            var FireworksTransform = Instantiate(FireworksPrefab) as Transform;
            // Manages the location of the firework.
            FireworksTransform.position = transform.position + Vector3.up * FireworksVectorUp;
            // Start the firework sound
            AudioSource.PlayClipAtPoint(FireworkSound, new Vector3(1, 1, 0));
            // create a new balloon distant from "Player"
            Transform otherTransform = other.gameObject.GetComponent<Transform>();
            var BalloonTransform = Instantiate(Balloon_redPrefab) as Transform;
            // obtains the casual but safe position of the balloon with respect to the player (1 = right, 2 = high, 3 = left)
            int RandomBalloonPosition = Random.Range(1, 3);
            if (RandomBalloonPosition == 1)
                BalloonTransform.position =
                    otherTransform.position + Vector3.right * BalloonSecurityDistance + Vector3.up * Random.Range(0, BalloonMaxRandomDistance);
            if (RandomBalloonPosition == 2)
                BalloonTransform.position =
                    otherTransform.position + Vector3.up * BalloonSecurityDistance + Vector3.right * Random.Range(-BalloonMaxRandomDistance, BalloonMaxRandomDistance);
            if (RandomBalloonPosition == 3)
                BalloonTransform.position =
                    otherTransform.position + Vector3.left * BalloonSecurityDistance + Vector3.up * Random.Range(0, BalloonMaxRandomDistance);
            if (BalloonTransform.position.x > BalloonMaxXPosition)
                BalloonTransform.position =
                    otherTransform.position + Vector3.up * BalloonSecurityDistance;
            if (BalloonTransform.position.x < -BalloonMaxXPosition)
                BalloonTransform.position =
                    otherTransform.position + Vector3.up * BalloonSecurityDistance;
            // destroys the (old) balloon
            Destroy(gameObject);
        }
    }
}
