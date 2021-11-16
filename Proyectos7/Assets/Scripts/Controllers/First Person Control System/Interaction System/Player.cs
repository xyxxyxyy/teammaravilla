using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(MoveControl))]
// con esta instruccion obligamos al objeto a agregarse (si no lo tiene)
// una componente tipo Player_Controller
[System.Serializable]
public class Player : MonoBehaviour
{

    // INFO DEL PLAYER

    public Transform player;
    public Transform body;
    public Transform head;
    public Transform hand;



    public Transform objInHand;
    // CONTROLADOR
    public PlayerController input_controller;
    public MoveControl move_controller;

    private void Start()
    {
        input_controller = GetComponent<PlayerController>();
        input_controller.player_info = this;
        input_controller.head = head;

        move_controller = GetComponent <MoveControl>();
        move_controller.body = body;
        move_controller.head = head;
        move_controller.speed = 2;

    }
    private void Update()
    {
        if (hand.childCount > 0)
            objInHand = hand.GetChild(0);
        else
            objInHand = null;
    }

}
