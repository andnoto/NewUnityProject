//Script PhysicsObject
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhysicsObject : MonoBehaviour
{
    //
    public GameObject newSprite;
    public Text logText;
    //
    public float minGroundDistance = .65f;
    public float gravityFactor = 1f;
    //
    protected bool logDone;
    //
    protected Rigidbody2D rigidBody2D;
    protected ContactFilter2D contactFilter2D;
    protected RaycastHit2D[] hitArray = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitArrayList = new List<RaycastHit2D>(16);
    protected bool landed;
    //
    protected Vector2 speed;
    //
    protected Vector2 ultimateSpeed;
    //
    protected Vector2 groundNormal;
     
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;
    // This function is called when the object becomes enabled and active.
    // Use this for initialization
    void OnEnable()
    {
        //
        rigidBody2D = newSprite.GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // check if the object is on the ground using raycasting
        // refer to official document 
        contactFilter2D.useTriggers = false;
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useLayerMask = true;
        //
        Vector2 castDirection = Vector2.down;
        hitArrayList.Clear();
        int counter = rigidBody2D.Cast(castDirection, contactFilter2D, hitArray, shellRadius);
        for (int i = 0; i < counter; i++)
        {
            hitArrayList.Add(hitArray[i]);
        }
        for (int i = 0; i < hitArrayList.Count; i++)
        {
            // computes the base normal vector
            // In geometry, a normal is an object such as a line, ray, or vector that is perpendicular to a given object.
            // fonte wikipedia https://en.wikipedia.org/wiki/Normal_(geometry)
            Vector2 vectorNormal = hitArrayList[i].normal;
         }
    }
    // Update is called once per frame
    //    void Update()
    //    {
    //      
    //    }
    protected virtual void ComputeMovement()
    {
    }
    //
    public virtual void StartAction(string[] words)
    {
    }
    //
    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        ComputeMovement();
        landed = false;
        // recalculates the jump speed by reducing it due to the effect of gravity
        speed += gravityFactor * Physics2D.gravity * Time.deltaTime;
        // calculates the horizontal movement speed
        speed.x = ultimateSpeed.x;
        // computes the displacement vector
        Vector2 shiftVector = speed * Time.deltaTime;
        // calculates the horizontal displacement vector
        Vector2 moveNewSprite = Vector2.right * shiftVector.x;
        Movement(moveNewSprite, false);
        // calculates the vertical displacement vector whose y component will be positive if the game object is rising, negative if it is falling
        moveNewSprite = Vector2.up * shiftVector.y;
        Movement(moveNewSprite, true);
    }
    void Movement(Vector2 moveNewSprite, bool yMovement)
    {
        float distance = moveNewSprite.magnitude;
        if (distance > minMoveDistance)
        {
            //
            hitArrayList.Clear();
            int counter = rigidBody2D.Cast(moveNewSprite, contactFilter2D, hitArray, distance + shellRadius);
            for (int i = 0; i < counter; i++)
            {
                hitArrayList.Add(hitArray[i]);
            }
            for (int i = 0; i < hitArrayList.Count; i++)
            {
                // The normal vector of a surface hit by the ray 

                // In geometry, a normal is an object such as a line, ray, or vector that is perpendicular to a given object.
                // source wikipedia https://en.wikipedia.org/wiki/Normal_(geometry)
                Vector2 vectorNormal = hitArrayList[i].normal;
                // check if the game object is on the ground
                if (vectorNormal.y > minGroundDistance)
                // in this case the game object is definitely downhill and close to the base
                    {
                    landed = true;
                    }
                //
                // The distance from the ray origin to the impact point.
                // in the event of a fall, the maximum movement distance is obviously down to the ground
                float modifiedDistance = hitArrayList[i].distance - shellRadius;
                if (modifiedDistance < distance)
                    distance = modifiedDistance;
                // the previous statements can be summarized as follows (condition? consequent: alternative)
                // distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rigidBody2D.position = rigidBody2D.position + moveNewSprite.normalized * distance;
    }
}

