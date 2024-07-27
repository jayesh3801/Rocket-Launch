using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject goalObject;
    [SerializeField] private float goalPos;

    private void Awake()
    {
        Instantiate(goalObject,new Vector2(0,goalPos),Quaternion.identity);
    }
}
