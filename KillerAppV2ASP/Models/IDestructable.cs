using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerAppV2ASP.Models
{
    public interface IDestructable
    {
        void DamageItem(int damage);
        void RepairItem();
    }
}
