using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPetManager : MonoBehaviour
{
    private static PlayerPetManager instance;
    public static PlayerPetManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerPetManager>();
            return instance;
        }
    }

    [SerializeField] PetContainer petContainer;
    [SerializeField] int petAmountMax = 20;

    private void Start()
    {
        VisualizePet();
    }

    public void VisualizePet()
    {
        petContainer.VisualizePet();
    }

    public void AddPet(PetData petData)
    {
        petContainer.AddPet(petData);
    }

    public bool CheckAmount()
    {
        return petContainer.petDatas.Count < petAmountMax;
    }
}
