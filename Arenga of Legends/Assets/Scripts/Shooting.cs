using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    public int projectileSpeed = 50;
    private float timeOfLastShot = 0;
    public float cooldownOfShot = 1000; //time in milliseconds

    public GameObject projectile;
    public GameObject Character;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyShooting();
    }

    void EnemyShooting()
    {
        bool isReady = Time.time * 1000 > timeOfLastShot + cooldownOfShot;
        Vector3 thisPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 characterPosition = new Vector3(Character.transform.position.x, Character.transform.position.y, Character.transform.position.z);
        characterPosition = characterPosition - thisPosition;
        characterPosition = characterPosition.normalized;
        Debug.Log(characterPosition);

        if (isReady)
        {
            timeOfLastShot = Time.time * 1000;
            Vector3 startingpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject newProjectile = Instantiate(projectile, startingpoint, Quaternion.identity);
            Rigidbody2D newProjectileRigibody = newProjectile.GetComponent<Rigidbody2D>();
            newProjectileRigibody.AddForce(characterPosition * projectileSpeed, ForceMode2D.Impulse);
            newProjectileRigibody.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
