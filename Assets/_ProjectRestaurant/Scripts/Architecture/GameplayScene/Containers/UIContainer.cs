using UnityEngine;

public class UIContainer : MonoBehaviour
{
    [SerializeField] private GameObject windowGameOver;
    [SerializeField] private GameObject windowGame;
    public GameObject WindowGameOver => windowGameOver;
    public GameObject WindowGame => windowGame;
}
