using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Input")] 
    public PlayerInput input;

    private InputInfo inputValues;
    
    [Header("Player Interaction")] 
    public PlayerInteraction interaction;

    private void Awake()
    {
        InitializeEntity();
    }
    
    private void InitializeEntity()
    {
        input = GetComponent<PlayerInput>();
        interaction = GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
        ManageInput();
        ManageInteraction(inputValues);
    }

    private void ManageInput()
    {
        input.GetInput();
        
        inputValues = input.ReturnInput();
    }

    private void ManageInteraction(InputInfo input)
    {
        if (CheckForShrinkInteractionInput(input))
        {
            EntityController hitEntity = interaction.SearchForEntity();

            if (hitEntity != null)
            {
                hitEntity.ShrinkEntitySize();
            }
        }
        else if (CheckForEnlargeInteractionInput(input))
        {
            EntityController hitEntity = interaction.SearchForEntity();

            if (hitEntity != null)
            {
                hitEntity.EnlargeEntitySize();
            }
        }
    }

    private bool CheckForShrinkInteractionInput(InputInfo input)
    {
        if (inputValues.ReturnCurrentButtonState("Interact_Shrink"))
        {
            return true;
        }

        return false;
    }
    
    private bool CheckForEnlargeInteractionInput(InputInfo input)
    {
        if (inputValues.ReturnCurrentButtonState("Interact_Enlarge"))
        {
            return true;
        }

        return false;
    }
}


