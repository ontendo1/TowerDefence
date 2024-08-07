using System;
using UnityEngine;

public class ParabolicProjectile : Projectile
{
    [SerializeField] private float arcHeight;
    [HideInInspector] private float calculatedArcHeight;
    [HideInInspector] public Transform targetTransform;
    private float defaultDistance;
    private float defaultYPos;

    bool isFalling = false;
    protected override void Start()
    {
        base.Start();

        defaultYPos = rb.position.y;

        defaultDistance = Vector3.Distance(new(targetTransform.position.x, 0, targetTransform.position.z), new(transform.position.x, 0, transform.position.z));
    }

    void FixedUpdate()
    {
        //Destroys game object when target is dead by other weapons.
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        //Calculates target point regurarly because parabolic projectile have small precision.
        float targetPosX = targetTransform.position.x - transform.position.x;
        float targetPosZ = targetTransform.position.z - transform.position.z;

        direction = new Vector3(targetPosX, 0, targetPosZ).normalized;

        rb.velocity = direction * projectileSpec.speed;

        float distance = Vector3.Distance(new(targetTransform.position.x, 0, targetTransform.position.z), new(transform.position.x, 0, transform.position.z));

        float oldArcHeight = calculatedArcHeight;

        //Do parabolic effect with sin function.
        //parabolic effect is on the peak point when "distance / defaultDistance" = ~0.5f.
        
        calculatedArcHeight = Mathf.Sin(distance / defaultDistance * Mathf.PI) * arcHeight; 

        //Compares the arc height in current frame with the arch height of lastest frame for determine it's increasing or decreasing.
          
        if (Mathf.Abs(oldArcHeight) > Mathf.Abs(calculatedArcHeight) && !isFalling)
        {
            isFalling = true;
        }
        else if (isFalling && Mathf.Abs(oldArcHeight) < Mathf.Abs(calculatedArcHeight))
        {
            Destroy(gameObject);
        }

        rb.position = new Vector3(rb.position.x, defaultYPos + calculatedArcHeight, rb.position.z);
    }
}
