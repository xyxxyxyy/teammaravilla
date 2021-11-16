using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Code : Interactable
{
    public override int MouseButtonPressed(int button, PlayerController player)
    {
        //AQUI VAMOS A COLOCAR LO QUE SE HARA CUANDO SE PRESIONE UN BOTON DEL MOUSE
        return button;
    }

    public override KeyCode KBKeyPressed(KeyCode pKey, PlayerController player)
    {
        //QUI VAMOS A COLOCAR LO QUE SE HARA CUANDO SE PRESIONE UNA TECLA DEL TECLADO
        return pKey;
    }

}
