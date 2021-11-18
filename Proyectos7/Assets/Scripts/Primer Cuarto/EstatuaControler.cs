using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatuaControler : MonoBehaviour
{
    public GameObject puerta;
    public bool estatuauno;
    public bool estatuados;
    public bool estatuatres;

    public Animator puertanim;


    // Start is called before the first frame update
    void Start()
    {
        estatuauno = false;
        estatuados = false;
        estatuatres = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(estatuauno == true && estatuados == true && estatuatres == true)
        {
            puertanim.Play("PuertaBajando");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Estatua1")
        {
            estatuauno = true;
        }

        if (col.tag == "Estatua2")
        {
            estatuados = true;
        }

        if (col.tag == "Estatua3")
        {
            estatuatres = true;
        }
    }
}

