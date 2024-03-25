using UnityEngine;


public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    public bool AutoUnparentOnAwake = true;

    protected static T instance;

    public static bool HasInstance => instance != null;
    public static T Current => instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if(instance == null)
                {
                    GameObject obj = new GameObject();
                    var go = new GameObject(typeof(T).Name + " Auto-Generated");
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }


    protected virtual void Awake()
    {
    //  InitializeSingleton();   
    }


    protected virtual void InitializeSingleton()
    {
        if(!Application.isPlaying) return;

        if(AutoUnparentOnAwake)
        {
            transform.SetParent(null);
        }

        if (Instance == null)
        {
            instance = this as T;
            Debug.Log("Null");
            DontDestroyOnLoad(transform.gameObject);
            enabled = true;
        }
        else
        {
            if(instance != null)
            {
                Debug.Log("!Null");
                Destroy(gameObject);
            }
        }

      
    }

}
