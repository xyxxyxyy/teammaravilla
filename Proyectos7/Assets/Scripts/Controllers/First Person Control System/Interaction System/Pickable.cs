using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using InputController = *InserteSuClaseControladoraAqui*; 
using InputController = PlayerController;

[Serializable]
public class SelfInfo
{
    public Transform self;
    public Rigidbody rb;
    public Transform originalParent;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public List<ColInfo> colliders;

};

[Serializable]
public class ColInfo
{
    public Collider col;
    public bool isTrigger;
};

[Serializable]
public class DesiredButton
{
    public int left = 0;
    public int right = 1;
    public int center = 2;
};

public class Pickable : Interactable
{
    //////////////////////////
    public Player player;
    public KeyCode desiredKey = KeyCode.E;
    // public de fuerza
    public float throwForce = 1000;
    // Variables del ego


    [SerializeField]
    public SelfInfo objInfo;
    [SerializeField]
    public DesiredButton desiredButton;


    private Transform hand;

    public bool picked;
    public bool canPick;

    [Serializable]
    public struct PoseOnPick
    { 
        public Vector3 position; 
        public Vector3 rotation; 
    };
    [SerializeField]
    public PoseOnPick poseOnPick;

    private void Start()
    {
        objInfo.self = transform;
        objInfo.originalPosition = transform.position;
        objInfo.originalRotation = transform.rotation;
        objInfo.originalParent = transform.parent;
        if (objInfo.rb = GetComponent<Rigidbody>()) { }

        foreach (Collider col in GetComponentsInChildren<Collider>()){

            objInfo.colliders.Add(new ColInfo { col = col, isTrigger = col.isTrigger});
        } 
    }
    
    // OVERRIDES
    public override int MouseButtonPressed(int button, InputController playerController)
    {
        player = playerController.player_info;

        if (IsPickingMe(player) == true) // preguntamos si el Player ya tiene algo en la mano
        {
            if (button == desiredButton.left)
            {
                OnPickedPrincipalMouseButtonAction();
            }
            if (button == desiredButton.right)
            {
                OnPickedSecondaryMouseButtonAction();
            }
            if (button == desiredButton.center)
            {
                
            }
        }
        
        return button;
    }

    public override KeyCode KBKeyPressed(KeyCode pKey, InputController playerController)
    {
        player = playerController.player_info;

        if (IsPickingMe(player) == true) // preguntamos si el Player ya tiene algo en la mano
            OnPickedKeyboardAction(pKey);
        else
        {
            if (player.objInHand == null)
            {
                if (pKey == desiredKey)
                    PickMe(player);
            }
        }
        
        return pKey;
    }


    //PRIVATES
    private bool IsPickingMe(Player plyr)
    {
        if (plyr.objInHand)
            return (player.objInHand && player.objInHand == transform); // sabiendo la cantidad de hijos que tiene la mano podemos inferir si sostiene o no algo
        else
            return false;
    }

    private void PickMe(Player player_)
    {

        if (transform.GetComponent<Rigidbody>())
        {
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            transform.GetComponent<Rigidbody>().useGravity = false; // apagmos la gravedad del objeto para poder moverlo
        }
            

        foreach (Collider col in transform.GetComponentsInChildren<Collider>()) 
        { 
            col.isTrigger = true;
        }

        transform.parent = player_.hand;
        transform.localPosition = poseOnPick.position;
        transform.localRotation = Quaternion.Euler(poseOnPick.rotation);

        picked = true;
    }    
    
    private  void DropMe()
    {
        transform.parent = null;

        if (transform.GetComponent<Rigidbody>()) {
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.useGravity = true; // apagmos la gravedad del objeto para poder moverlo
            rb.isKinematic = false;
        }
        int i = 0;
        foreach (Collider col in transform.GetComponentsInChildren<Collider>()) { 
            col.isTrigger = objInfo.colliders[i].isTrigger;
            i++;
        }

        picked = false;
    }

    private void ThrowMe()
    {
        DropMe();
        transform.GetComponent<Rigidbody>().AddForce(player.head.forward * throwForce); // aviento el objeto
    }
    
    // VIRTUALS
    public virtual void OnPickedPrincipalMouseButtonAction() 
    {
        ThrowMe();
    }

    public virtual void OnPickedSecondaryMouseButtonAction()
    {
        DropMe();
    }

    public virtual void OnPickedKeyboardAction(KeyCode pKey)
    {

    }



}
