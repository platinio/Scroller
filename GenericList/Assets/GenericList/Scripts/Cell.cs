using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	[SerializeField] private Text m_elementName = null;


	public string elementName{ set { m_elementName.text = value; } }
}
