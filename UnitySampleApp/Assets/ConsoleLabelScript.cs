using UnityEngine;
using System.Collections;

public class ConsoleLabelScript : MonoBehaviour {

	public UILabel label;
	ArrayList logEntries = new ArrayList();
	static int maxLines = 20;

	void AppendText(string text)
	{
		if (label == null) return;

		logEntries.Add(text);

		if (logEntries.Count > maxLines)
		{
			logEntries.RemoveAt(0);
		}

		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		foreach (string line in logEntries)
		{
			sb.AppendLine(line);
		}
		label.text = sb.ToString();
	}
}
