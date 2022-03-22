using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCombat : MonoBehaviour
{
    [SerializeField] float timeBeforeLoad = 3f;
    // Start is called before the first frame update
    void Start()
    {
        UnitHealth.OnDeath += LoadCoroutine;
    }

    private void OnDisable()
    {
        UnitHealth.OnDeath -= LoadCoroutine;
    }

    void LoadCoroutine(bool b) => StartCoroutine(StartLoading());
    IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        SceneManager.LoadSceneAsync(0);
    }
}
