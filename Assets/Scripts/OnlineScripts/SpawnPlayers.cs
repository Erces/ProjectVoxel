using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
	

	public Transform spawnPoint;

	private void Start()
	{
		if(PlayerPrefs.GetInt("Character") == 0)
        {
			PhotonNetwork.Instantiate("Characters/PlayerArcher", spawnPoint.position, Quaternion.identity, 0);

		}
		else if (PlayerPrefs.GetInt("Character") == 1)
        {
			PhotonNetwork.Instantiate("Characters/PlayerDwarf", spawnPoint.position, Quaternion.identity, 0);

		}
		else if (PlayerPrefs.GetInt("Character") == 2)
		{
			PhotonNetwork.Instantiate("Characters/PlayerMage", spawnPoint.position, Quaternion.identity, 0);

		}
		EUActions.OnGameStart?.Invoke();
	}

	private void Update()
	{
	}
}
