using BillSplitter;


using (BillStore store = new("BillSplitter.sqlite"))
{

    store.AddParticipant(47, "x");
}