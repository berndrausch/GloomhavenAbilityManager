﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Exceptions;
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

            foreach (CharacterDataObject character in characters)
            {
                character.AvailableCards = relations.Where(rel => rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault(card => card.Id == rel.AbilityCardId));
                character.SelectedCards = relations.Where(rel => rel.IsSelected && rel.CharacterId == character.Id).Select(rel => cards.FirstOrDefault(card => card.Id == rel.AbilityCardId));
            }

            return characters;
        }

        private List<CharacterDataObject> ReadCharacters()
        {
            string fileName = FileNames.Characters;

            try
            {
                var csvFileReader = new CsvFileReader<CharacterDataObject>(new FileSystem(), fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read characters from {fileName}", ex);
            }
        }

        private List<CharacterAbilityCardRelation> ReadRelations()
        {
            string fileName = FileNames.CharacterCards;

            try
            {
                var csvFileReader = new CsvFileReader<CharacterAbilityCardRelation>(new FileSystem(), fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read character cards from {fileName}", ex);
            }
        }

        public void SaveAll(IEnumerable<CharacterDataObject> characters)
        {
            string fileName = FileNames.Characters;

            try
            {
                var csvFileWriter = new CsvFileWriter<CharacterDataObject>(new FileSystem(), fileName);
                csvFileWriter.SaveAll(characters);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to write character cards from {fileName}", ex);
            }

            List<CharacterAbilityCardRelation> relations = new List<CharacterAbilityCardRelation>();
            foreach (CharacterDataObject character in characters)
            {
                foreach (int cardId in character.AvailableCards.Select(card => card.Id))
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

            fileName = FileNames.CharacterCards;

            try
            {
                var csvFileWriter = new CsvFileWriter<CharacterAbilityCardRelation>(new FileSystem(), fileName);
                csvFileWriter.SaveAll(relations);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to write character cards from {fileName}", ex);
            }
        }
    }
}
