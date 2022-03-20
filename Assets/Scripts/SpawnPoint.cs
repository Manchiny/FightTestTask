using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int _teamID;
    public int TeamId => _teamID;
}
