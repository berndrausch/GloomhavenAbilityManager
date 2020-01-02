using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.Logic.Contracts;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CharacterRepositoryCsv : ICharacterRepository
    {
       public IEnumerable<Character> GetAll()
        {
            List<Character> characters = null;
            List<CharacterAbilityCardRelation> relations = null;

            using (var reader = new StreamReader("DataAccess.Csv\\characters.csv"))
            {
                using (var csv = new CsvReader(reader))
                {    
                   characters = csv.GetRecords<Character>().ToList();
                }
            }

            using (var reader = new StreamReader("DataAccess.Csv\\charcards.csv"))
            {
                using (var csv = new CsvReader(reader))
                {    
                   relations = csv.GetRecords<CharacterAbilityCardRelation>().ToList();
                }
            }

            foreach(Character character in characters)
            {
                character.AvailableCardIds = relations.Where(rel => rel.CharacterId == character.Id).Select(rel => rel.AbilityCardId);
                character.SelectedCardIds = relations.Where(rel => rel.IsSelected && rel.CharacterId == character.Id).Select(rel => rel.AbilityCardId);
            }

            return characters;
        }
    }
}
