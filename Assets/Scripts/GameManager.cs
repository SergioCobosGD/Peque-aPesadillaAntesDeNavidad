using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject hitImagenGameObject;
    public Image hitImagen;
    public Color flashColorLose;
    public Color flashColorWin;
    public float flashSpeed;
    public float timer = 0.0f;
    public bool final = false;


    private void Update()
    {
        hitImagen.color = Color.Lerp(hitImagen.color, Color.clear, flashSpeed * Time.deltaTime);
        if (final)
        {
            timer += Time.deltaTime;
        }
        if(timer >= 3)
        {
            hitImagenGameObject.SetActive(false);
        }
    }
    public void GameOver()
    {
        hitImagenGameObject.SetActive(true);
        panelLose.SetActive(true);
        hitImagen.color = flashColorLose;
        final = true;
    }
    public void YouWin()
    {
        hitImagenGameObject.SetActive(true);
        panelWin.SetActive(true);
        hitImagen.color = flashColorWin;
        final = true;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGame");
    }
}
