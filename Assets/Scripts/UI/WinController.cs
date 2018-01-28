using UnityEngine;

public class WinController : MonoBehaviour
{
	public GameObject WinScreen;

	public void Win()
	{
		WinScreen.SetActive(true);
	}
}