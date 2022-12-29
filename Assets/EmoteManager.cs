using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (!animator) animator = gameObject.GetComponent<Animator>();
    }
    public void ProcessAnEmote(EmoteType emote) => animator.Play(emote.ToString());
}

public enum EmoteType
{
    Idle, Halo, Veins, Sweat 
}