using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Pickable
{
    public GameObject[] ammoPrefabs;

    public Transform canon;

    public KeyCode nextAmmoKey = KeyCode.Alpha2;
    public KeyCode prevAmmoKey = KeyCode.Alpha1;

    public float fireRatio = 0.5f;
    public float switchRatio = 0.8f;
    public bool canFire;
    private bool canSwitch;
    private float t,ts;

    public int ammoIdx;

    public Bullet b;
    private void Update()
    {
        if (picked)
        {
            t += Time.deltaTime;
            ts += Time.deltaTime;
            if (t > fireRatio)
                canFire = true;
            if (ts > switchRatio)
                canSwitch = true;
            Vector3 pointToLook = player.head.GetChild(0).position;
            transform.LookAt(pointToLook );
        }

    }
    // VIRTUALS
    public override void OnPickedPrincipalMouseButtonAction()
    {
        if (canFire)
        {
            Shoot(ammoPrefabs[ammoIdx]);
            t = 0;
            canFire = false;
        }
        
    }

    /*
    public override void OnPickedSecondaryMouseButtonAction()
    {
        
    }
    */
    public override void OnPickedKeyboardAction(KeyCode pKey)
    {
        if (canSwitch)
        {
            if (pKey == nextAmmoKey)
                ammoIdx++;
            else if (pKey == prevAmmoKey)
                ammoIdx--;

            if (ammoIdx < 0)
                ammoIdx = 0;
            else if (ammoIdx >= ammoPrefabs.Length)
                ammoIdx = ammoPrefabs.Length - 1;

            ts = 0;
            canSwitch = false;
        }
        

    }

    private void Shoot(GameObject bullet)
    {
        b = Instantiate(bullet, canon.position, canon.rotation).GetComponent<Bullet>();
        
        b.Shoot(2000);
        t = 0;
    }
}
