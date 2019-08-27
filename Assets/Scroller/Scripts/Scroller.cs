using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Code to handle generic scroll list, like leaderboards or achievements
/// </summary>
public class Scroller : MonoBehaviour
{
    public enum ScrollMode
    {
        Horizontal,
        Vertical
    }

    #region INSPECTOR
    [SerializeField] protected ScrollRect scroll = null;
    [SerializeField] protected GridLayoutGroup gridLayout = null;    
    [SerializeField] protected ScrollMode scrollMode = ScrollMode.Horizontal;
    #endregion

    #region PRIVATE
    private List<GameObject> elements = new List<GameObject>();
    private RectTransform contentRect = null;
    private float minSize = 0.0f;
    #endregion

    protected RectTransform scrollRect = null;
    protected Action onReachEndOfList = null;

    public GridLayoutGroup GridLayout
    {
        get { return gridLayout; }
    }

    protected virtual void Awake()
    {
        //get rect
        scrollRect = scroll.GetComponent<RectTransform>();
        contentRect = gridLayout.GetComponent<RectTransform>();
        minSize = contentRect.rect.size.y;

        //set listener for end of list
        scroll.onValueChanged.AddListener( delegate (Vector2 v)
         {
             if (scroll.verticalNormalizedPosition <= 0.0f && onReachEndOfList != null)
             {
                 onReachEndOfList();
             }
         } );
    }


    /// <summary>
    /// add element to scroll list
    /// </summary>
    public void AddElement(GameObject element)
    {
        element.transform.parent = gridLayout.transform;
        elements.Add( element );
        ResizeContentRect();
    }
    
    /// <summary>
    /// Resizes the content rect to fit new elements
    /// </summary>
    protected void ResizeContentRect()
    {

        float size = 0.0f;

        if (scrollMode == ScrollMode.Horizontal)
        {
            size = ( gridLayout.cellSize.x + gridLayout.spacing.x ) * elements.Count;
        }
        else if (scrollMode == ScrollMode.Vertical)
        {
            size = ( gridLayout.cellSize.y + gridLayout.spacing.y ) * elements.Count;
        }

        //resize contentRect to fit new element
        if (size > minSize)
        {
            if (scrollMode == ScrollMode.Vertical)
            {
                contentRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, size );
            }
            else if (scrollMode == ScrollMode.Horizontal)
            {
                contentRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, size );
            }


            //reposition
            Vector2 pos = contentRect.anchoredPosition;

            if (scrollMode == ScrollMode.Vertical)
            {
                contentRect.anchoredPosition = new Vector2( pos.x, pos.y - ( ( gridLayout.cellSize.y + gridLayout.spacing.y ) / 2.0f ) );
            }
            else if (scrollMode == ScrollMode.Horizontal)
            {
                contentRect.anchoredPosition = new Vector2( pos.x + ( ( gridLayout.cellSize.x + gridLayout.spacing.x ) / 2.0f ), pos.y );
            }

        }


    }

    /// <summary>
    /// Deletes all elements from list
    /// </summary>
    protected void ClearList()
    {
        for (int n = 0; n < elements.Count; n++)
        {
            Destroy( elements[n].gameObject );
        }

        elements = new List<GameObject>();
        contentRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, minSize );
    }

}
