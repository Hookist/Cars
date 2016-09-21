using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.PX
{
    class GameOver
    {
        public void Over(List<Car> cr)
        {
            int[] cw = { 0, 0 };
            for (int i = 0; i < cr.Count; i++)
            {
               if( cr[i].Hp == 0)
                {
                    cr[i].Destroed();
                }
            }
        }
    }
}
