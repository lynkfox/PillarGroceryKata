# PillarGroceryKata
Grocery Store API Kata for Pillar Technology

See Full Requirements for Kata here: https://github.com/PillarTechnology/kata-checkout-order-total

# API:

## Receipt(ItemRepository) - Constructor
Requirements said not to use a Database - all information must be stored in memory.

When a New Receipt is called, it requets a copy of the information regarding item prices and sales.
This was done specifically so it would be easy to change to a Database Accessor or an Entity Framework object containing the necessary information.

## void Receipt.Buy(ItemName)

Buy's one item at Cost-Markdown Price, taking into account individual sale modifiers. If item has a weight, assumes 1lb.

## void Receipt.Buy(ItemName, Qty)

Buy's multiple items, at Cost-Markdown Price, taking into account individual sale modifiers. If item has a Weight, assumes 1lb

## void Receipt.Buy(ItemName, Qty, Weight)

Buys multiple items, at Cost-Markdown Price, taking into account individual sale modifiers, each item weighting the weight entered.

## void Receipt.Void()

Removes the last item added off the receipt.

## void Receipt.Void(ItemName)

Removes the last item of that name from the receipt.

## void Receipt.Void(itemName, Qty)

Removes a number of items of the same name from the receipt, starting with the last one added.

## decimal Receipt.Total()

Returns the total cost of the order, pre tax as a Decimal. 

### NOTE:
Each Item is added to the receipt with its cost being rounded with "Banker Rounding" at the time of adding it to the receipt. This means that prices in the Item Repository can be longer than 2 decimal places, and the Receipt rounds when it adds the price. Because each item - even in multiple quantities - is added as a seperate entry into the receipt, rounding happens for each item's price. This may cause the Total to be different than what you think due to Midpoint.ToEven rounding.

## int Receipt.ItemCount()

Returns the total number of items on the receipt.

## string Receipt.Last()

Returns the last item added to the receipt

## iList Receipt.AllItems()

Returns an indexed List of all the items in the Receipt. 

### NOTE

The above 2 methods currently have no error handling. They may return Null or IndexNotFound .



# Build Test
`````
dotnet build GroceryStoreReceiptLibrary/GroceryStoreReceiptLibrary.csproj
dotnet build PillarGroceryStoreKata/PillarGroceryStoreKata.csproj
````

# Run Tests
````
dotnet vstest PillarGroceryStoreKata/bin/Debug/netcoreapp2.2/PillarGroceryStoreKata.dll
````
