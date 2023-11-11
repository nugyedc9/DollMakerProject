using UnityEngine;
namespace player
{
    public  static class Sounds
    {
        public static void MakeSound(Sound sound)
        {
            Collider[] col = Physics.OverlapSphere(sound.pos, sound.range);
            for(int i = 0; i < col.Length; i++)
                if (col[i].TryGetComponent(out HearPlayer Hplayer))
                    Hplayer.RespondToSound(sound);
        }
    }
}
