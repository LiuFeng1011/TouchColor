using UnityEngine;
using System.Collections;
using System.Runtime.Hosting;
using UnityEngine.SceneManagement;

public class InitializeOnLoad : MonoBehaviour {

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
        if (SceneManager.GetActiveScene().name == "Gate" || SceneManager.GetActiveScene().name == "TestScene")
		{
			return;
		}
        SceneManager.LoadScene("Gate");
	}
}
