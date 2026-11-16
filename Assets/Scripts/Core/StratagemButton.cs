using UnityEngine;

public class StratagemButton : MonoBehaviour
{
    [SerializeField] private StratagemEffect _stratagem;
    [SerializeField] private LoadOut _loadOut;

    [SerializeField] private bool _choice;

    public void ChoiceStratagem()
    {
        if (!_choice)
        {
            _loadOut.StratagemList.Add(_stratagem);
            _choice = true;
        }
        else
        {
            _loadOut.StratagemList.Remove(_stratagem);
            _choice = false;
        }
    }
}
