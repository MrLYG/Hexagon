using UnityEngine;
using UnityEditor;
using Models;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;

public class AnalyticsDemo : MonoBehaviour
{
	private readonly string basePath = "https://hexagon-11bf5-default-rtdb.firebaseio.com";
	private RequestHelper currentRequest;
	[SerializeField] private string Test;

	private void LogMessage(string title, string message)
	{
		#if UNITY_EDITOR
				EditorUtility.DisplayDialog(title, message, "Ok");
		#else
				Debug.Log(message);
		#endif
	}


	public void Post()
	{
		/*
		// We can add default query string params for all requests
		RestClient.DefaultRequestParams["param1"] = "My first param";
		RestClient.DefaultRequestParams["param3"] = "My other param";

		currentRequest = new RequestHelper
		{
			Uri = basePath + "/posts",
			Params = new Dictionary<string, string> {
				{ "param1", "value 1" },
				{ "param2", "value 2" }
			},
			Body = new Post
			{
				title = "foo",
				body = "bar",
				userId = 1
			},
			EnableDebug = true
		};
		RestClient.Post<Post>(currentRequest)
		.Then(res => {

			// And later we can clear the default query string params for all requests
			RestClient.ClearDefaultParams();

			this.LogMessage("Success", JsonUtility.ToJson(res, true));
		})
		.Catch(err => this.LogMessage("Error", err.Message));*/
		RestClient.Post<Post>(basePath + "/posts", new Post
		{
			title = "My first title",
			body = "My first message",
			userId = 26
		})
		.Then(res => this.LogMessage("Success", JsonUtility.ToJson(res, true)))
		.Catch(err => this.LogMessage("Error", err.Message));
	}


	// Start is called before the first frame update
	void Start()
    {
		this.Post();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
