using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*singleton de tipagem generica:
* Como usar?
* Simplesmente herde qualquer componente disso aqui ao inves de monobehaviour, e use
*/
public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    //instancia com propriedade readonly publica
    private static T instance;
    public static T Instance   
    {
        /*
        * Se a instancia nao for nula, retorna ela.
        * Se for, todos os objetos do tipo T;
        * Se nao conseguir achar, cria um objeto com um componente T;
        * Destroi todos os objetos achados depois do primeiro;
        * Atribui o primeiro objeto achado/criado a instance;
        * Retorna instance.
        */
        get
        {
            if(instance != null) { return instance; }

            List<T> loadedObjs = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
            if(loadedObjs == null) { loadedObjs[0] = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>(); }
            while(loadedObjs.Count > 1)
            {
                loadedObjs.RemoveAt(loadedObjs.Count -1);
            }

            instance = loadedObjs[0];
            return instance;
        }
    }
}
