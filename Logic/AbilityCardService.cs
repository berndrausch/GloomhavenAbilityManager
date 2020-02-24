using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Exceptions;
using GloomhavenAbilityManager.Logic.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Converters;

namespace GloomhavenAbilityManager.Logic
{
    public class AbilityCardService : IAbilityCardService
    {
        private readonly IAbilityCardRepository _cardRepository;

        public AbilityCardService(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public AbilityCard GetCard(int id)
        {
            IEnumerable<AbilityCard> allCards = GetCards();

            try
            {
                return allCards.First(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new LogicException($"Unable to find card with id {id} in all {allCards.Count()} cards", ex);
            }
        }

        public IEnumerable<AbilityCard> GetCharacterClassCards(int classId)
        {
            IEnumerable<AbilityCard> allCards = GetCards();
            var resultingCards = allCards.Where(c => classId <= 0 || c.ClassId == classId);

            if (!resultingCards.Any())
            {
                throw new LogicException($"Unable to find any cards for class id {classId} in all {allCards.Count()} cards");
            }

            return resultingCards;
        }

        private IEnumerable<AbilityCard> GetCards()
        {
            IEnumerable<AbilityCardDataObject> allCardsObjects;

            try
            {
                allCardsObjects = _cardRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new LogicException($"Unable to get card data from repository", ex);
            }

            return allCardsObjects.Select(AbilityCardConverter.FromDataObject);
        }

        public IEnumerable<AbilityCard> GetAvailableCards(Character character)
        {
            IEnumerable<AbilityCard> allClassCards = GetCharacterClassCards(character.ClassId);
            return allClassCards.Where(card =>
                IsCardAvailableForLevel(character.Level, card) && !IsCardInCharacterPool(character, card));
        }

        private bool IsCardInCharacterPool(Character character, AbilityCard card)
        {
            return character.PoolCards.Any(poolCard => poolCard.Id == card.Id);
        }

        private bool IsCardAvailableForLevel(int characterLevel, AbilityCard card)
        {
            if (card.Level.Equals("X", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            if (int.TryParse(card.Level, out int cardLevel))
            {
                return cardLevel <= characterLevel;
            }

            return false;
        }
    }
}