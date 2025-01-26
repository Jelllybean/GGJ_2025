using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem particles;

    public static ParticleManager instance;

	private void Awake()
	{
		instance = this;
	}

    public void PositionParticles(Transform _position)
    {
        particles.transform.position = _position.position;
        particles.Play();
    }
}
