using UnityEngine;

[CreateAssetMenu(fileName = "New Target", menuName = "Target")]
public class Target : ScriptableObject
{
    public string targetName;
    public float maxLife, currentLife;
    public Sprite artwork;
}