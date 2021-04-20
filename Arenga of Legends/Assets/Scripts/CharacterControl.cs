using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public  int projectileSpeed = 50;
    public float movementSpeed = 5;
    private float timeOfLastShot = 0;
    public float cooldownOfShot = 1000; //time in milliseconds
    
    public GameObject projectile;    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Shooting();

    }

    void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (inputX < 0) inputX = -1;
        else if (inputX > 0) inputX = 1;

        if (inputY < 0) inputY = -1;
        else if (inputY > 0) inputY = 1;

        transform.position += new Vector3(inputX, inputY, 0) * Time.deltaTime * movementSpeed;
    }

    void Shooting()
    {
        bool isReady = Time.time * 1000 > timeOfLastShot + cooldownOfShot;
        bool isShooting = Input.GetButton("Fire1");

        if (isShooting && isReady)
        {
            timeOfLastShot = Time.time * 1000;
            Vector3 startingpoint = new Vector3(transform.position.x, transform.position.y + 0.08f * 2, transform.position.z);
            GameObject newProjectile = Instantiate(projectile, startingpoint, Quaternion.identity);
            Rigidbody2D newProjectileRigibody = newProjectile.GetComponent<Rigidbody2D>();
            newProjectileRigibody.AddForce(new Vector2(0, 1) * projectileSpeed, ForceMode2D.Impulse);
            newProjectileRigibody.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == projectile.name)
        {
            Destroy(collision.gameObject);
        }
    }
}
