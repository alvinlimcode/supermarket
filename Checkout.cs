using System.Collections.Generic;
using System.Linq;

public class Checkout
{
    private Dictionary<char, decimal> _prices;
    private Dictionary<char, int> _quantities;
    private List<Offer> _offers;

    public Checkout()
    {
        _prices = new Dictionary<char, decimal> { { 'A', 50 }, { 'B', 30 }, { 'C', 20 }, { 'D', 15 } };
        _quantities = new Dictionary<char, int> { { 'A', 0 }, { 'B', 0 }, { 'C', 0 }, { 'D', 0 } };
        _offers = new List<Offer>
        {
            new Offer { Discount = 20, RequiredItems = new Dictionary<char, int> { { 'A', 3 } } },
            new Offer { Discount = 30, RequiredItems = new Dictionary<char, int> { { 'B', 2 } } },
            new Offer { Discount = 10, RequiredItems = new Dictionary<char, int> { { 'A', 1 },  { 'B', 1} } },
        };
    }

    public decimal TotalPrice { get; private set; }

    public void Scan(char item)
    {
        _quantities[item]++;
        TotalPrice += _prices[item];

        foreach(var offer in _offers)
        {
            if (HasOffer(offer, item))
            {
                TotalPrice -= offer.Discount;
                
            }
        }
    }

    private bool HasOffer(Offer offer, char item)
    {
        bool hasOffer = false;
        Dictionary<char, int> minusQuantities = new Dictionary<char, int>();

        foreach(var requireItem in offer.RequiredItems)
        {
            if (_quantities.ContainsKey(requireItem.Key) && requireItem.Value <= _quantities[requireItem.Key])
            {
                hasOffer = true;
                minusQuantities[requireItem.Key] = requireItem.Value;
            }
            else {
                return false;
            }
            
        }

        if (hasOffer)
        {
            foreach(var quantity in minusQuantities)
            {
                _quantities[quantity.Key] -= quantity.Value;
                if (_quantities[quantity.Key] < 0)
                    _quantities[quantity.Key] = 0;
            }
            
        }

        return hasOffer;
    }
}


