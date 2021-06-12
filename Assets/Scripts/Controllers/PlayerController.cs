using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General player controller
/// </summary>
[RequireComponent(typeof(InputReader))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InputReader inputReader;
    [SerializeField]
    MouseOrbit camController;

    public List<PartController> playerParts;
    int curIndex = 0;

    private void Start()
    {
        InitializeCurrentParts();
        EnableCurrentPart();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSwitchController();
    }

    private void InitializeCurrentParts()
    {
        foreach(PartController controller in playerParts)
        {
            Debug.Log(controller.name);
            controller.inputReader = inputReader;
        }
    }

    private void HandleSwitchController()
    {
        if (Input.GetKeyDown(inputReader.switchKey))
        {
            if(playerParts.Count > 1)
            {
                DisableCurrentPart();
                IncrementIndex();
                EnableCurrentPart();
            }
        }
    }

    private void DisableCurrentPart()
    {
        playerParts[curIndex].DisablePart();
    }

    private void EnableCurrentPart()
    {
        playerParts[curIndex].EnablePart();
        camController.setTarget(playerParts[curIndex].transform);
    }

    private void IncrementIndex()
    {
        if(curIndex < playerParts.Count - 1)
        {
            curIndex++;
        }
        else
        {
            curIndex = 0;
        }
    }

    private void DecrementIndex()
    {
        if(curIndex > 1)
        {
            curIndex--;
        }
        else
        {
            curIndex = playerParts.Count - 1;
        }
    }

    public void AddPart(PartController partController)
    {
        if (!playerParts.Contains(partController))
        {
            partController.inputReader = inputReader;
            playerParts.Add(partController);
        }
        else
        {
            Debug.LogError("Part has already been added to the list: " + partController.gameObject.name);
        }      
    }

    public void RemovePart(PartController partController)
    {
        if (playerParts.Contains(partController))
        {
            playerParts.Remove(partController);
        }
        else
        {
            Debug.LogError("Part was not in the list to be removed: " + partController.gameObject.name);
        }
    }
}
