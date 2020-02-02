using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CharacterRepositoryCsv : ICharacterRepository
    {
        private IAbilityCardRepository _cardRepository;
        public CharacterRepositoryCsv(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public IEnumerable<CharacterDataObject> GetAll()
        {
            List<CharacterDataObject> characters = ReadCharacters();
            List<CharacterAbilityCardRelation> relations = ReadRelations();
            List<AbilityCardDataObject> cards = _cardRepository.GetAll().ToList();

            foreach(CharacterDataObject character in characters)
            {
                character.AvailableCards = relations.Where(rel => rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault( card => card.Id == rel.AbilityCardId));
                character.SelectedCards = relations.Where(rel => rel.IsSelected && rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault( card => card.Id == rel.AbilityCardId));
            }

            return characters;
        }

        private List<CharacterDataObject> ReadCharacters()
        {
            using (var reader = new StreamReader(FileNames.Characters))
            {
                using (var csv = new CsvReader(reader))
                {
                    return csv.GetRecords<CharacterDataObject>().ToList();
                }
            }
        }

        private List<CharacterAbilityCardRelation> ReadRelations()
        {
             using (var reader = new StreamReader(FileNames.CharacterCards))
            {
                using (var csv = new CsvReader(reader))
                {    
                   return csv.GetRecords<CharacterAbilityCardRelation>().ToList();
                }
            }
        }

        public void SaveAll(IEnumerable<CharacterDataObject> characters)
        {
            List<CharacterAbilityCardRelation> relations = new List<CharacterAbilityCardRelation>();
            foreach(CharacterDataObject character in characters)
            {
                foreach(int cardId in character.AvailableCards.Select(card => card.Id))
                {
                    var rel = new CharacterAbilityCardRelation()
                    {
                        CharacterId = character.Id,
                        AbilityCardId = cardId,
                        IsSelected = character.SelectedCards.Any(card => card.Id == cardId)
                    };
                    relations.Add(rel);
                }
                character.AvailableCards = null;
                character.SelectedCards = null;
            }

            using (var writer = new StreamWriter(FileNames.CharacterCards))
            {
                using (var csv = new CsvWriter(writer))
                {    
                   csv.WriteRecords(relations);
                }
            }


            using (var writer = new StreamWriter(FileNames.Characters))
            {
                using (var csv = new CsvWriter(writer))
                {    
                   csv.WriteRecords(characters);
                }
            }
        }
    }
}
