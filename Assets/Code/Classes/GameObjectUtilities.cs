using UnityEngine;

public static class GameObjectUtilities
{
    public static TType GetChildComponent<TType> (Transform rootTransform) where TType : Component
    {
        // If we don't find the component in this object
        // recursively iterate it's children until we do.
        TType returnComponent = rootTransform.gameObject.GetComponent<TType> ();

        if (null == returnComponent)
        {
            // Store the current number of children on this object.
            var childCount = rootTransform.childCount;

            // For loop is used due to foreach() mono issues.
            for (int child = 0; child < childCount; ++child)
            {
                // Recusively call this method on each child.
                // Break from the loop and return the component
                // when it is found.
                returnComponent = GetChildComponent<TType> (rootTransform.GetChild (child).transform);

                if (null != returnComponent)
                    break;
            }
        }

        return returnComponent;
    }
}
