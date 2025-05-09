using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillianSearcher.DAL.Data;
using VillianSearcher.DAL.Models;

namespace VillianSearcher.DAL.Repositories
{
    public class VillainRepository : IVillainRepository
    {
        JsonDataProvider m_dataProvider;

        public VillainRepository(JsonDataProvider jsonDataProvider)
        {
            m_dataProvider = jsonDataProvider;
        }

        public void Add(Villain entity)
        {
            Villain last = m_dataProvider.Villains.LastOrDefault();

            int index = 0;

            if (last != null)
            {
                index = last.Id + 1;
            }

            entity.Id = index;

            m_dataProvider.Villains.Add(entity);
        }

        public bool Edit(int id, Villain entity)
        {
            var villains = m_dataProvider.Villains;
            int len = villains.Count;
            int index = -1;
            for (int i = 0; i < len; i++)
            {
                if (villains[i].Id.Equals(id))
                {
                    index = i;
                    break;
                }
            }

            if (index <= 0)
            {
                return false;
            }
            else
            {
                villains[index].UpdateValues(entity);
                return true;
            }
        }

        public Villain Get(int id)
        {
            return m_dataProvider.Villains.Where(x => x.Id.Equals(id)).Select(x => x).FirstOrDefault();
        }

        public IEnumerable<Villain> GetAll()
        {
            return m_dataProvider.Villains;
        }

        public bool Remove(int id)
        {
            var villians = m_dataProvider.Villains;
            int len = villians.Count;
            int index = -1;
            for (int i = 0; i < len; i++)
            {
                if (villians[i].Id.Equals(id))
                {
                    index = i;
                    break;
                }
            }

            if (index <= 0)
            {
                return false;
            }
            else
            {
                villians.RemoveAt(index);
                return true;
            }
        }

        public void SaveData()
        {
            m_dataProvider.SaveData();
        }
    }
}
