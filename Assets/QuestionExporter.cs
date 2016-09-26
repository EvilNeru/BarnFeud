using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class QuestionExporter : MonoBehaviour {

	[SerializeField] List<TextAsset> m_rawSurveys;

	public static string UnescapeXml( string s )
	{
		string unxml = s;
		if ( !string.IsNullOrEmpty( unxml ) )
		{
			// replace entities with literal values
			unxml = unxml.Replace( "&apos;", "'" );
			unxml = unxml.Replace( "&quot;", "\"" );
			unxml = unxml.Replace( "&gt;", "&gt;" );
			unxml = unxml.Replace( "&lt;", "&lt;" );
			unxml = unxml.Replace( "&amp;", "&" );
		}
		return unxml;
	}

	Dictionary<string, Dictionary<string,string>> m_surveys;
	// Use this for initialization
	void Start () 
	{
		foreach(TextAsset textAsset in m_rawSurveys)
		{

			StringReader surveyBuff = new StringReader(textAsset.text);

			string line = null;
			string workTitle = "";

			while( (line = surveyBuff.ReadLine()) != null)
			{
				// Found a new survey
				if(line.Contains("<td class=\"column-2\">"))
				{
					int beginIdx = line.IndexOf('>') + 1;
					int endIdx = line.IndexOf('<',beginIdx);

					workTitle = line.Substring(beginIdx, endIdx - beginIdx);
					workTitle = UnescapeXml(workTitle);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
