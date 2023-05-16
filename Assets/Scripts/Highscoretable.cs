using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscoretable : MonoBehaviour
{
	private Transform entryContainer;
	private Transform entryTemplate;
	private List<HighscoreEntry> highscoreEntryList;
	private List<Transform> highscoreEntryTransformList;
	private void Awake()
	{
		entryContainer = transform.Find("Highscore_Entry_container");
		entryTemplate = entryContainer.Find("Highscore_Entry_Template");

		entryTemplate.gameObject.SetActive(false);


		string jsonString = PlayerPrefs.GetString("highscoreTable");
		Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


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
		for (int i = 0; i < 6; i++) {
			CreateHighscoreEntryTransform(highscores.highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
		}
		/*foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
			CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
		}*/

	}

	private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
	{
		float templateHeight = 8f;

			Transform entryTransfrom = Instantiate(entryTemplate, container);
			RectTransform entryRectTransform = entryTransfrom.GetComponent<RectTransform>();
			entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
			entryTransfrom.gameObject.SetActive(true);

			int rang = transformList.Count + 1;
			string rangString;
			switch (rang)
			{
				default:
					rangString = rang + "th";
					break;
				case 1:
					rangString = "1st";
					break;
				case 2:
					rangString = "2nd";
					break;
				case 3:
					rangString = "3rd";
					break;
			}

		int score = highscoreEntry.score;

			entryTransfrom.Find("Entry_Position").GetComponent<TextMeshProUGUI>().text = rangString;

			entryTransfrom.Find("Entry_Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

			string name = highscoreEntry.name;
			entryTransfrom.Find("Entry_Name").GetComponent<TextMeshProUGUI>().text = name;

		transformList.Add(entryTransfrom);
	}

	private class Highscores {
		public List<HighscoreEntry> highscoreEntryList;
	}

	private void AddHighscoreEntry(int score, string name) {
		HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

		string jsonString = PlayerPrefs.GetString("highscoreTable");
		Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

		highscores.highscoreEntryList.Add(highscoreEntry);

		string json = JsonUtility.ToJson(highscores);
		PlayerPrefs.SetString("highscoreTable", json);
		PlayerPrefs.Save();
	}

	[System.Serializable]
	private class HighscoreEntry {
		public int score;
		public string name;
	}
}
