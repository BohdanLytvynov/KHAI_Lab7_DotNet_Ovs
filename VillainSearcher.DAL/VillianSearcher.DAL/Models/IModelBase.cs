using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillianSearcher.DAL.Models
{
    public interface IModelBase<TId>
    {
        TId Id { get; set; }
    }
}
