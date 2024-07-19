using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Interface for code to refer to all
/// static scripts in the project
/// </summary>
public class Scripts : MonoBehaviour
{
    // example of usage
    //[SerializeField][Required] public static TYPE _TYPE;
    //public TYPE TYPE;

    private void Start()
    {
        //TYPE = _TYPE;
        //TYPE.Init();
    }
}
