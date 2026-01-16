using CustomLinkedList.DataStructures;

var list = new CustomLinkedList<int?>();
list.AddToEnd(1);
list.AddToEnd(2);
list.AddToFront(0);
list.Add(3);

Console.WriteLine(list.ToString());
Console.WriteLine($"List contains 2: {list.Contains(2)}");
Console.WriteLine($"List count: {list.Count}");
list.Remove(2);
Console.WriteLine($"List contains 2: {list.Contains(2)}");
Console.WriteLine($"List count: {list.Count}");
foreach (var item in list)
{
    list.Remove(item);
}
Console.WriteLine(list.ToString());
foreach (var item in list)
{
    Console.WriteLine(item);
}
Console.WriteLine(
    $"List count after removing all items: {list.Count}"
);
Console.WriteLine(
    "Adding items 4, 5, 6 to the front of the list."
);
list.AddToFront(6);
list.AddToFront(5);
list.AddToFront(4);
Console.WriteLine(list.ToString());
Console.WriteLine(
    $"List count after adding items to front: {list.Count}"
);
var array = new int?[list.Count + 5];
list.CopyTo(array, 2);
Console.WriteLine("Array contents after CopyTo with offset 2:");
for (int i = 0; i < array.Length; i++)
{
    Console.WriteLine($"Index {i}: {array[i]}");
}
list.Clear();
Console.WriteLine(list.ToString());
