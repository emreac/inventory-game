using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    public VariableJoystick joystick;
    public CharacterController controller;
    public Canvas inputCanvas;
    public bool isJoystick;
    public float movementSpeed;
    public float rotationSpeed;
    public bool walking;


    private void Awake()
    {
        //Using Singleton
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        /*
        ItemWorld.SpawnItemWorld(new Vector3(4, 1, 3), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(2, 1, 4), new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(8, 1, 6), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        */
    }

    private void Start()
    {
       
        Time.timeScale = 1f;
        EnableJoystickInput();
    }

    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (isJoystick)
        {
            var movementDirection = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y);
            controller.SimpleMove(movementDirection * movementSpeed);

            if (movementDirection.sqrMagnitude <= 0)
            {

                return;
            }
           
            walking = true;
            var targetDirection = Vector3.RotateTowards(controller.transform.forward,
                movementDirection,rotationSpeed*Time.deltaTime,0f);
            controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

}
