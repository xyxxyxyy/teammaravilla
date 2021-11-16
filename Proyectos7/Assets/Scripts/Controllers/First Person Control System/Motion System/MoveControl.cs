using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class MoveControl : MonoBehaviour
{
    public Transform body; // Transform para almacenar al cuerpo
    public Transform head;
    public float speed; // float de velocidad para mover al personaje
    public float speedYaw = 2.0f; // float de velocidad para ver hacia arriba y abajo
    public float speedPitch = 2.0f; // float de velocidad para ver hacia la derecha o la izquierda
    private float yaw = 0.0f; // float de posición con respecto al movimiento arriba y abajo de la cabeza
    private float pitch = 0.0f; // float de posición con respecto al movimiento izquierda y derecha de la cabeza

    public bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (body == null) // si en el inspector no asignamos el Transform del cuerpo
        {
            body = this.transform; // entonces la variable body será el Transform que tenga este código
        }
    }


    // Update is called once per frame
    void Update()
    {

        float translationZ = Input.GetAxis("Vertical") * speed; // Obtener el valor del Eje Vertical para movernos hacia adelante (-1,0,1)
        translationZ *= Time.deltaTime; // normalizar la velocidad con respecto al tiempo de procesamiento de este equipo

        body.position += body.forward * translationZ;
        
        
        float translationX = Input.GetAxis("Horizontal") * speed;

        translationX *= Time.deltaTime;
        body.position += body.right * translationX;

        if (translationX != 0 || translationZ != 0)
            isMoving = true;
        else
            isMoving = false;
        

        yaw += speedYaw * Input.GetAxis("Mouse X"); // obtenemos el valor de rotación en X
        pitch += speedPitch * Input.GetAxis("Mouse Y");

        head.localEulerAngles = new Vector3(-pitch, 0f, 0f);
        body.eulerAngles = new Vector3(0.0f, yaw, 0.0f); // le asignamos el valor de rotación en X al cuerpo


    }
}
