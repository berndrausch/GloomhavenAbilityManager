﻿using System;
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
        private IAbilityCardRepository _cardRepository;
        public CharacterRepositoryCsv(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public IEnumerable<Character> GetAll()
        {
            List<Character> characters = ReadCharacters();
            List<CharacterAbilityCardRelation> relations = ReadRelations();
            List<AbilityCard> cards = _cardRepository.GetAll().ToList();

            foreach(Character character in characters)
            {
                character.AvailableCards = relations.Where(rel => rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault( card => card.Id == rel.AbilityCardId));
                character.SelectedCards = relations.Where(rel => rel.IsSelected && rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault( card => card.Id == rel.AbilityCardId));
            }

            return characters;
        }

        private List<Character> ReadCharacters()
        {
            using (var reader = new StreamReader("DataAccess.Csv\\characters.csv"))
            {
                using (var csv = new CsvReader(reader))
                {
                    return csv.GetRecords<Character>().ToList();
                }
            }
        }

        private List<CharacterAbilityCardRelation> ReadRelations()
        {
             using (var reader = new StreamReader("DataAccess.Csv\\charcards.csv"))
            {
                using (var csv = new CsvReader(reader))
                {    
                   return csv.GetRecords<CharacterAbilityCardRelation>().ToList();
                }
            }
        }

        public void SaveAll(IEnumerable<Character> characters)
        {
            List<CharacterAbilityCardRelation> relations = new List<CharacterAbilityCardRelation>();
            foreach(Character character in characters)
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

            using (var writer = new StreamWriter("DataAccess.Csv\\charcards.csv"))
            {
                using (var csv = new CsvWriter(writer))
                {    
                   csv.WriteRecords(relations);
                }
            }


            using (var writer = new StreamWriter("DataAccess.Csv\\characters.csv"))
            {
                using (var csv = new CsvWriter(writer))
                {    
                   csv.WriteRecords(characters);
                }
            }
        }
    }
}
