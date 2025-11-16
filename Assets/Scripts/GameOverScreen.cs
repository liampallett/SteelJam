 using UnityEngine;

 public class GameOverScreen : MonoBehaviour
 {
    public void Restart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
 }