using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pistol", menuName = "Inventory/Pistol")]
public class pistol : inventoryItem
{
    // Bullet prefab
    [SerializeField] private GameObject bullet;
    // Time between shots
    [SerializeField] private float shootRate = 0.5f;
    // Shooting distance
    [SerializeField] private int shootDist = 10;
    // Shooting audio clip
    [SerializeField] private AudioClip shootSound;
    // Volume of the shooting sound
    [SerializeField] private float shootVol = 1.0f;
    [SerializeField] private int maxAmmo;

    private float lastShootTime = 0f;
    // Current ammo count
    private int ammoCur = 10;

    

    public override void Attack(Vector3 position, Quaternion rotation)
    {
        if (Time.time - lastShootTime < shootRate || ammoCur <= 0)
            return;

        // Instantiate bullet and shoot in the given direction
        GameObject projectile = Instantiate(bullet, position, rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = rotation * Vector3.forward * shootDist;
        }

        // Play shooting sound
        AudioSource.PlayClipAtPoint(shootSound, position, shootVol);

        // Decrease ammo count
        ammoCur--;
        lastShootTime = Time.time;
    }
    public void Reload(int amount)
    {
        ammoCur = Mathf.Min(ammoCur + amount, maxAmmo); 
    }
}
