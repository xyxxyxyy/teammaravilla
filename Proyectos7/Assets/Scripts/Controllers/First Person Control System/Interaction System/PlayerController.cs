using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    //info del player y raycast
    public Transform head; // en esta variable guardamos el Transform de la cabeza del personaje
    public Player player_info;

    public Interactable interactable_script;

    // ZONA RAYCAST
    RaycastHit hit; // una variable para almacenar toda la información del objeto con el que chocó mi rayo

    public int pMB;
    public KeyCode pKB;


    // Update is called once per frame
    void FixedUpdate()
    {
        test(); // de aquí obtenemos los valores de pMB y pKB

        //ESTO SIRVE PARA VER UN RAYO EN ESCENA "NADA MAS"
        Debug.DrawRay(head.position, head.forward * 100, Color.yellow);

        if (Physics.Raycast(head.position, head.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.GetComponent<Interactable>()) // si el objeto con el que chocó tiene un Interactable
                Action(pMB, pKB, hit.transform.GetComponent<Interactable>());

        }

        // SI TENEMOS ALGO EN LA MANO
        if (player_info.objInHand && player_info.objInHand.GetComponent<Interactable>()) // si lo que tenemos en la mano tiene interacciones las activamos
            Action(pMB, pKB, player_info.objInHand.GetComponent<Interactable>());
    }

    void Action(int pmb, KeyCode kmb, Interactable script) 
    {
        script.MouseButtonPressed(pmb, this);
        script.KBKeyPressed(kmb, this);
        
    }



    void test()
    {
        pMB = MouseCheck();
        pKB = KeyBoardCheck();
    }

    int MouseCheck() // checar y devolver el boton que se ha presionado del mouse
    {
        if (Input.GetMouseButton(0)) //
            return 0;

        else if (Input.GetMouseButton(1)) // 
            return 1;
        else if (Input.GetMouseButton(2))
            return 2;
        else
            return -1;
    }

    private KeyCode[] desiredKeys = { KeyCode.E, KeyCode.Space, KeyCode.Q, KeyCode.Alpha1, KeyCode.Alpha2 };
    KeyCode KeyBoardCheck()
    {
        
        foreach (KeyCode keyToCheck in desiredKeys) // para todos los elementos en desiredKeys
        {
            if (Input.GetKey(keyToCheck)) // preguntar si se presionó cada tecla
                return keyToCheck; // si si, devolver esa tecla
        }
        return 0;
    }
}
