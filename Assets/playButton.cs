using UnityEngine.SceneManagement;
using UnityEngine;
public class playButton : MonoBehaviour
{
    public AudioSource click;
    public void startGame(){
        click.Play();
        SceneManager.UnloadSceneAsync("startScreen");
        SceneManager.LoadScene("Game");
    }
}
