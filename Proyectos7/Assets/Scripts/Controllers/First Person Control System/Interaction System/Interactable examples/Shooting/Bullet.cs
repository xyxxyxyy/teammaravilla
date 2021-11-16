using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float t;

    public Rigidbody _rigidbody;
    private bool shooted;
    // Start is called before the first frame update
    void Start()
    {

        _rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (shooted)
        {
            t += Time.deltaTime;

            if (t > 10)
            {
                Destroy(this.gameObject);
            }
        }

    }

    public void Shoot(float force)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * force);
        shooted = true;
    }
}
