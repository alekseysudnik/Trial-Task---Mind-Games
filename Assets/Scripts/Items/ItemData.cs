public class ItemData
{
    public ItemSO itemSO { get; private set; }
    public int quantity { get; private set; }

    public ItemData(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
    }

    public void AddQuantity(int addQuantity)
    {
        quantity += addQuantity;
    }
}
