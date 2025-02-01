using BillSplitter;

using (BillStore store = new("BillSplitter.sqlite"))
{
    var rowId = store.AddBill("John", "Dinner");
    Console.WriteLine($"Added bill with id {rowId}");
}