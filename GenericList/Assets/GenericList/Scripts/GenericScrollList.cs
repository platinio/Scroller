using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Code to handle generic scroll list, like leaderboards or achievements
/// </summary>
public class GenericScrollList<T> : MonoBehaviour where T : MonoBehaviour
{
	#region INSPECTOR
	[SerializeField] protected 	RectTransform		m_scrollRect	= null;
	[SerializeField] protected 	GridLayoutGroup 	m_gridLayout 	= null;
	[SerializeField] protected 	T 					m_elementPrefab = null;
	#endregion

	#region PRIVATE
	private List<T> 		m_elements 		= new List<T>();
	private RectTransform	m_contentRect 	= null;
	private float			m_minSize		= 0.0f;
	#endregion

	protected virtual void Awake()
	{
		//get rect
		m_contentRect 	= m_gridLayout.GetComponent<RectTransform>();
		m_minSize		= m_contentRect.rect.size.y;

	}

	/// <summary>
	/// add element to scroll list
	/// </summary>
	private void AddElement(T element)
	{
		m_elements.Add(element);	
		//resize list to fit new element
		ResizeContentRect();
	}

	/// <summary>
	/// Creates new element and add it to the list
	/// </summary>
	/// <returns>The element.</returns>
	protected T CreateElement()
	{
		T t = MonoBehaviour.Instantiate( m_elementPrefab , m_gridLayout.transform );
		AddElement(t);
		return t;
	}

	/// <summary>
	/// Resizes the content rect to fit new elements
	/// </summary>
	protected void ResizeContentRect()
	{


		//desire height
		float height = (m_gridLayout.cellSize.y + m_gridLayout.spacing.y ) * m_elements.Count;

		//resize contentRect to fit new element
		if(height > m_minSize)
		{
			m_contentRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical , height );

			//reposition
			Vector2 pos = m_contentRect.anchoredPosition;
			m_contentRect.anchoredPosition = new Vector2( pos.x , pos.y - ( ( m_gridLayout.cellSize.y + m_gridLayout.spacing.y ) / 2.0f ) );
		}


	}

	/// <summary>
	/// Deletes all elements from list
	/// </summary>
	protected void ClearList()
	{
		for(int n = 0 ; n < m_elements.Count ; n++)
		{
			Destroy( m_elements[n].gameObject );
		}

		m_elements = new List<T>();
		m_contentRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical , m_minSize );
	}

}
