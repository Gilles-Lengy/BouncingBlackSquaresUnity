using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public void OnClickGoToLevel(string level)
    {
        Time.timeScale = 1F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html
        SceneManager.LoadScene(level);
        Debug.Log("Go to level" + level);

    }
}
