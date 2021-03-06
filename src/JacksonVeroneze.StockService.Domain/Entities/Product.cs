using System.Collections.Generic;
using JacksonVeroneze.StockService.Core.DomainObjects;

namespace JacksonVeroneze.StockService.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Description { get; private set; }

        public bool IsActive { get; private set; } = true;

        private readonly List<PurchaseItem> _itemsPurchase = new List<PurchaseItem>();
        private readonly List<AdjustmentItem> _itemsAdjustment = new List<AdjustmentItem>();
        private readonly List<OutputItem> _itemsOutput = new List<OutputItem>();
        private readonly List<Movement> _itemsMovement = new List<Movement>();

        public virtual IReadOnlyCollection<PurchaseItem> ItemsPurchase => _itemsPurchase;
        public virtual IReadOnlyCollection<AdjustmentItem> ItemsAdjustment => _itemsAdjustment;
        public virtual IReadOnlyCollection<OutputItem> ItemsOutput => _itemsOutput;
        public virtual IReadOnlyCollection<Movement> ItemsMovement => _itemsMovement;

        protected Product()
        {
        }

        public Product(string description)
        {
            Description = description;

            Validate();
        }

        public void Update(string description, bool isActive)
        {
            Description = description;
            IsActive = isActive;

            Validate();
        }

        public void Activate() => IsActive = true;

        public void Inactivate() => IsActive = false;

        private void Validate()
        {
            Validacoes.ValidarSeVazio(Description, "A descrição não pode estar vazia");
            Validacoes.ValidarTamanho(Description, 1, 100, "A descrição não pode estar vazia");
        }
    }
}
