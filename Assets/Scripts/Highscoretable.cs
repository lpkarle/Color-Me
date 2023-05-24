using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscoretable : MonoBehaviour
{
	private Transform entryContainer;
	private Transform entryTemplate;
	private List<HighscoreEntry> highscoreEntryList;
	private List<Transform> highscoreEntryTransformList;

	private void Start()
	{
		var playerName = ColorMeGameManager.Instance.playerName;
		var playerScore = ColorMeGameManager.Instance.playerScore;

		if (playerName != "-")
			AddHighscoreEntry(playerScore, playerName);

		entryContainer = transform.Find("Highscore_Entry_container");
		entryTemplate = entryContainer.Find("Highscore_Entry_Template");

		entryTemplate.gameObject.SetActive(false);


		string jsonString = PlayerPrefs.GetString("highscoreTable");

		
		Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

		if (highscores == null)
		{
			highscoreEntryList = new List<HighscoreEntry>() {
				new HighscoreEntry{score = 0, name = "-"}
			};
			string json = JsonUtility.ToJson(highscoreEntryList);
			PlayerPrefs.SetString("highscoreTable", json);
			jsonString = PlayerPrefs.GetString("highscoreTable");

		
			highscores = JsonUtility.FromJson<Highscores>(jsonString);
			
		}

		//sort
		for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
			for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
				if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
					HighscoreEntry tmp = highscores.highscoreEntryList[i];
					highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
					highscores.highscoreEntryList[j] = tmp;
				}
			}
		}

		highscoreEntryTransformList = new List<Transform>();

		int tableLength = highscores.highscoreEntryList.Count;

		if (highscores.highscoreEntryList.Count > 5)
			tableLength = 5;

		for (int i = 0; i < tableLength; i++) {
			CreateHighscoreEntryTransform(highscores.highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
		}
	}

	private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
	{
		float templateHeight = 8f;

		Transform entryTransfrom = Instantiate(entryTemplate, container);
		RectTransform entryRectTransform = entryTransfrom.GetComponent<RectTransform>();
		entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
		entryTransfrom.gameObject.SetActive(true);

		int rang = transformList.Count + 1;

        string rangString = rang switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            _ => rang + "th",
        };

        int score = highscoreEntry.score;

		entryTransfrom.Find("Entry_Position").GetComponent<TextMeshProUGUI>().text = rangString;

		entryTransfrom.Find("Entry_Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

		string name = highscoreEntry.name;
		entryTransfrom.Find("Entry_Name").GetComponent<TextMeshProUGUI>().text = name;

		transformList.Add(entryTransfrom);
	}

	private class Highscores
	{
		public List<HighscoreEntry> highscoreEntryList;
	}

	private void AddHighscoreEntry(int score, string name)
	{
		HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

		string jsonString = PlayerPrefs.GetString("highscoreTable");
		Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

		highscores.highscoreEntryList.Add(highscoreEntry);

		string json = JsonUtility.ToJson(highscores);
		PlayerPrefs.SetString("highscoreTable", json);
		PlayerPrefs.Save();
	}

	[System.Serializable]
	private class HighscoreEntry
	{
		public int score;
		public string name;
	}
}
