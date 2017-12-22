using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class List : GenericScrollList<Cell>
{
	[SerializeField] private int m_numberOfElements = 10;

	private void Start()
	{
		//add elements
		/*
		for(int n = 0 ; n < m_numberOfElements ; n++)
		{
			Cell cell = CreateElement();
			cell.elementName = n.ToString();
		}
		*/

		StartCoroutine( CO_Test() );

	}

	private IEnumerator CO_Test()
	{
		//create 10 elements one by one
		for(int n = 0 ; n < 10 ; n++)
		{
			Cell cell = CreateElement();
			cell.elementName = n.ToString();
			yield return new WaitForSeconds(0.4f);
		}

		yield return new WaitForSeconds(1.0f);
		//clear all the elements
		ClearList();

		//fill it aigan like crazy
		for(int n = 0 ; n < 100 ; n++)
		{
			Cell cell = CreateElement();
			cell.elementName = n.ToString();
			yield return new WaitForSeconds(0.2f);
		}

	}

}
