using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] GameObject panel = null;
    [SerializeField] float transitionTime = 1f;
    
    void Start()
    {
        StartCoroutine(ChangePanelActiity(false, transitionTime + 0.1f));
    }

    public void StartBattle(bool vsPlayer)
    {
        CrossSceneData.isOpponentPlayer = vsPlayer;
        StartCoroutine(ChangePanelActiity(true, 0));
        animator.SetBool("isLoading", true);
        StartCoroutine(LoadBattle());
    }

    IEnumerator LoadBattle()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(1);
    }
    IEnumerator ChangePanelActiity(bool isActive, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        panel.SetActive(isActive);
    }
    public void CloseGame() => Application.Quit();
}
