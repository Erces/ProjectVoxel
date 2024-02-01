using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviourPunCallbacks
{
	public TMP_InputField createInput;

	public TMP_InputField joinInput;

	public void CreateRoom()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 20;
		PhotonNetwork.CreateRoom(createInput.text, roomOptions);
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(joinInput.text);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel("Game");
	}

	public void PickArcher()
    {
		PlayerPrefs.SetInt("Character", 0);
    }
	public void PickBerserker()
	{
		PlayerPrefs.SetInt("Character", 1);
	}
	public void PickWizard()
	{
		PlayerPrefs.SetInt("Character", 2);
	}
}
