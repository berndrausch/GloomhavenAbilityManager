using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public class AbilityCardRepository : IAbilityCardRepository
    {
        public Task<IEnumerable<AbilityCardInfo>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<AbilityCardInfo>>(
                new List<AbilityCardInfo>
                {
                    new AbilityCardInfo{Id = 0, Name = "Cragheart 1", Level = "1", ClassId = 2},
                    new AbilityCardInfo{Id = 1, Name = "Cragheart 2", Level = "1", ClassId = 2},
                    new AbilityCardInfo{Id = 2, Name = "Cragheart 3", Level = "2", ClassId = 2},
                    new AbilityCardInfo{Id = 3, Name = "Cragheart 4", Level = "3", ClassId = 2},
                    new AbilityCardInfo{Id = 4, Name = "Cragheart 5", Level = "X", ClassId = 2, ImagePath=@"\images\04.png"},
                    new AbilityCardInfo{Id = 5, Name = "Tinkerer 1", Level = "1", ClassId = 3},
                    new AbilityCardInfo{Id = 6, Name = "Tinkerer 2", Level = "1", ClassId = 3},
                    new AbilityCardInfo{Id = 7, Name = "Tinkerer 3", Level = "2", ClassId = 3},
                    new AbilityCardInfo{Id = 8, Name = "Tinkerer 4", Level = "3", ClassId = 3},
                    new AbilityCardInfo{Id = 9, Name = "Tinkerer 5", Level = "X", ClassId = 3},
                    new AbilityCardInfo{Id = 10, Name = "Tinkerer 6", Level = "X", ClassId = 3},
                    new AbilityCardInfo{Id = 11, Name = "Brute 1", Level = "1", ClassId = 1},
                });
        }
    }
}