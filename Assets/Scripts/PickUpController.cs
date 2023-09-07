using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject Gun;
    public Transform WeaponParent;
    public bool gunPicked=false;
    public GameObject crosshair;
    public static PickUpController Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assigning the instance to the static property
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }
    private void Start()
    {
        Gun.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        if(ManageControls.Instance.inputZ !=0 ||ManageControls.Instance.inputX !=0 )
        {
            ManageControls.Instance.animator.SetInteger("RunWithGun", 1);
        }
        else
        {
             ManageControls.Instance.animator.SetInteger("RunWithGun", 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Drop();
            ManageControls.Instance.animator.SetBool("HoldGun", false);
        }
    }
 
    void Drop()
    {
        WeaponParent.DetachChildren();
        Gun.transform.eulerAngles = new Vector3(Gun.transform.position.x, Gun.transform.position.y, Gun.transform.position.z);
        Gun.GetComponent<Rigidbody>().isKinematic = false;
        Gun.GetComponent<MeshCollider>().enabled = true;
        Gun.GetComponent<BoxCollider>().enabled = true;
        gunPicked = false;
        AudioManager.Instance.Play("ItemEquip");
        crosshair.SetActive(false);
    }
    void Equip()
    {
        Gun.GetComponent<Rigidbody>().isKinematic = true;
        
        Gun.transform.position = WeaponParent.transform.position;
        Gun.transform.rotation = WeaponParent.transform.rotation;

        Gun.GetComponent<BoxCollider>().enabled = false;
        Gun.GetComponent<MeshCollider>().enabled = false;
        Gun.transform.SetParent(WeaponParent);

        gunPicked = true;
        AudioManager.Instance.Play("ItemEquip");
        crosshair.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag== "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                Equip();
                ManageControls.Instance.animator.SetBool("HoldGun", true);
            }

        }
    }
   
}
