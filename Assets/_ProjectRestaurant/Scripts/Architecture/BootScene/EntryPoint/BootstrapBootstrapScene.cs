using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapBootstrapScene : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

}
