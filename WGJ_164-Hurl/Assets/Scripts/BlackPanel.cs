using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackPanel : MonoBehaviour
{
    [SerializeField] GameObject panel = null;
    [SerializeField] Animator animator = null;
    [SerializeField] Text text = null;

    [Tooltip("Wait time until the start event is called and players can move about.")]
    [SerializeField] float startTimer = 0;
    public static event Action OnFightBegin; //Input will listen to this method and enable input

    void Start()
    {
        UnitHealth.OnDeath += FadeToBlack;
        StartCoroutine(StartCombat());
    }
    void OnDisable() => UnitHealth.OnDeath -= FadeToBlack;
    
    IEnumerator StartCombat()
    {
        yield return new WaitForSeconds(startTimer);
        OnFightBegin?.Invoke();
    }
    void FadeToBlack(bool b)
    {
        panel.SetActive(true);
        animator.SetBool("isOver", true);
        text.text = b ? "Player Two wins!" : "Player One Wins!";
    }
}