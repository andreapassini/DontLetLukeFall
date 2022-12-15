using UnityEngine;

namespace DLLF
{
	[CreateAssetMenu(fileName = "PlatformSequence", menuName = "Platfomrs/PlatformSequence", order = 1)]
	public class PlatformSequence : ScriptableObject
	{
		public ActionType[] actions;
	}
}
