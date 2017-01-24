using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class QuestionExporter : MonoBehaviour {

	[SerializeField] List<TextAsset> m_rawSurveys;


	Dictionary<string, Dictionary<string,string>> m_surveys = new Dictionary<string, Dictionary<string,string>>();
	// Use this for initialization
	void Start () 
	{
		foreach(TextAsset textAsset in m_rawSurveys)
		{

			StringReader surveyBuff = new StringReader(textAsset.text);

			string line = null;
			string workTitle = "";
			string lastAnswer = "";
			string lastPoints = "";

			bool nextAnswer = true;

			while( (line = surveyBuff.ReadLine()) != null)
			{
				int beginIdx = line.IndexOf('>') + 1;
				int endIdx = line.IndexOf('<',beginIdx);

				if(workTitle != "" && beginIdx == endIdx)
					continue;

				// Found a new survey
				if(line.Contains("<td class=\"column-2\">"))
				{
					workTitle = line.Substring(beginIdx, endIdx - beginIdx);
					m_surveys.Add(workTitle, new Dictionary<string, string>());
				}
				else if(workTitle != "" && !line.Contains("</tr>"))
				{
					if(nextAnswer)
					{
						lastAnswer = line.Substring(beginIdx, endIdx - beginIdx);
					}
					else
					{
						lastPoints = line.Substring(beginIdx, endIdx - beginIdx);
						m_surveys[workTitle].Add(lastAnswer,lastPoints);
					}
					nextAnswer = !nextAnswer;
				}
				else if(line.Contains("</tr>"))
				{
						workTitle = "";
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
