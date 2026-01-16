using System.Collections;

namespace CustomLinkedList.DataStructures;

public class CustomLinkedList<T> : ILinkedList<T?>
{
    private Node? head = null;
    private Node? tail = null;
    public int Count { get; private set; } = 0;

    public bool IsReadOnly { get => false; }

    public void Add(T? item) => AddToEnd(item);

    public void AddToEnd(T? item)
    {
        if (Count == 0)
        {
            head = new Node(item);
            tail = head;
        }
        else
        {
            tail = tail!.Next = new Node(item);
        }
        Count++;
    }

    public void AddToFront(T? item)
    {
        if (Count == 0)
        {
            head = new Node(item);
            tail = head;
        }
        else
        {
            var next = head;
            head = new Node(item)
            {
                Next = next
            };
        }
        Count++;
    }

    public void Clear()
    {
        Count = 0;
        while (head != null)
        {
            var next = head.Next;
            head.Next = null;
            head = next;
        }
        tail = null;
    }

    public bool Contains(T? item)
    {
        var current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
                return true;
            current = current.Next;
        }
        return false;
    }

    public void CopyTo(T?[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        if(Count == 0)
            return;
        if (arrayIndex < 0 || arrayIndex >= array.Length || array.Length - arrayIndex < Count)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        var current = head;
        while (current != null)
        {
            array[arrayIndex++] = current.Value;
            current = current.Next;
        }
    }

    public bool Remove(T? item)
    {
        if (head != null && EqualityComparer<T>.Default.Equals(head.Value, item))
        {
            head = head.Next;
            if (head == null)
                tail = null;
            Count--;
            return true;
        }
        Node? previous = head;
        Node? current = head?.Next;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                previous!.Next = current.Next;
                if (previous.Next == null)
                    tail = previous;
                Count--;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }

    public IEnumerator<T?> GetEnumerator()
    {
        var current = head;
        while (current != null)
        {
            yield return current.Value!;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var array = new T?[Count];
        CopyTo(array, 0);
        return $"CustomLinkedList with {Count} items. [{string.Join(", ", array)}]";
    }

    private class Node(T? value)
    {
        public T? Value { get; set; } = value;
        public Node? Next { get; set; } = null;
    }
}