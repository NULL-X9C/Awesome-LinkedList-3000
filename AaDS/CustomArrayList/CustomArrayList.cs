namespace AaDS.CustomArrayList;

public class CustomArrayList<T> where T : IComparable<T>
{
    private T[]? _data;
    private int _startIndex;
    private int _endIndex;
    public int Count => _endIndex - _startIndex + 1;

    public CustomArrayList()
    {
        _data = null;
        _startIndex = 0;
        _endIndex = 0;
    }

    public CustomArrayList(T someValue)
    {
        _data = new T[] { someValue };
        _startIndex = 0;
        _endIndex = 0;
    }

    public CustomArrayList(T[] someValue)
    {
        _data = someValue;
        _startIndex = 0;
        _endIndex = someValue.Length - 1;
    }

    public void AddLast(T value)
    {
        if (_data == null)
        {
            _data = new T[] { value };
            return;
        }

        if (_endIndex < _data.Length - 1)
        {
            _data[_endIndex + 1] = value;
            ++_endIndex;
            return;
        }

        var newInfo = new T[_data.Length * 2];
        for (int i = 0; i < Count; i++)
        {
            newInfo[i] = _data[_startIndex + i];
        }
        _data = newInfo;
        _endIndex = Count;
        _data[_endIndex] = value;
        _startIndex = 0;
    }

    public void AddFirst(T value)
    {
        if (_data == null)
        {
            _data = new T[] { value };
            return;
        }

        if (_startIndex > 0)
        {
            _data[--_startIndex] = value;
            return;
        }

        var newInfo = new T[_data.Length * 2];
        for (int i = 0; i < Count; i++)
        {
            newInfo[i + 1] = _data[_startIndex + i];
        }
        _data = newInfo;
        _startIndex = 0;
        _endIndex = Count;
        _data[_startIndex] = value;
    }

    public void Insert(T value, int index)
    {
        if (_data == null)
        {
            _data = new T[] { value };
            return;
        }
        
        if (index < 0 || index > Count)
        {
            Console.WriteLine("Индекс выходит за границы");
            return;
        }

        if (index == 0)
        {
            AddFirst(value);
            return;
        }

        if (index == Count)
        {
            AddLast(value);
            return;
        }

        if (Count < _data.Length)
        {
            int realIndex = _startIndex + index;
            
            for (int i = _endIndex; i >= realIndex; i--)
            {
                _data[i + 1] = _data[i];
            }
            
            _data[realIndex] = value;
            _endIndex++;
        }
        else
        {
            var newInfo = new T[_data.Length * 2];
            
            for (int i = 0; i < index; i++)
            {
                newInfo[i] = _data[_startIndex + i];
            }
            newInfo[index] = value;
            for (int i = 0; i < Count - index; i++)
            {
                newInfo[index + 1 + i] = _data[_startIndex + index + i];
            }
            
            _data = newInfo;
            _startIndex = 0;
            _endIndex = _endIndex - _startIndex + 1;
        }
    }

    public void RemoveAt(int index)
    {
        if (_data == null)
        {
            Console.WriteLine("Список пуст");
            return;
        }
        
        if (index < 0 || index >= Count)
        {
            Console.WriteLine("Индекс выходит за границы");
            return;
        }

        int realIndex = _startIndex + index;

        if (index == 0)
        {
            _startIndex++;
            if (_startIndex > _endIndex)
            {
                _data = null;
                _startIndex = 0;
                _endIndex = 0;
            }
            return;
        }

        if (index == Count - 1)
        {
            _endIndex--;
            if (_startIndex > _endIndex)
            {
                _data = null;
                _startIndex = 0;
                _endIndex = 0;
            }
            return;
        }

        for (int i = realIndex; i < _endIndex; i++)
        {
            _data[i] = _data[i + 1];
        }
        
        _endIndex--;
    }

    public void RemoveRange(int index)
    {
        if (_data == null)
        {
            Console.WriteLine("Список пуст");
            return;
        }


        if (index < 0 || index >= Count)
        {
            Console.WriteLine("Индекс выходит за границы");
            return;
        }

        if (index == 0)
        {
            _data = null;
            _startIndex = 0;
            _endIndex = 0;
            return;
        }

        int realIndex = _startIndex + index;
        _endIndex = realIndex - 1;
    }

    public void Reverse()
    {
        if (_data == null)
            return;

        int left = _startIndex;
        int right = _endIndex;

        while (left < right)
        {
            (_data[left], _data[right]) = (_data[right], _data[left]);
            left++;
            right--;
        }
    }

    public void Print()
    {
        if (_data == null)
        {
            Console.WriteLine("[]");
            return;
        }

        Console.Write("[");
        for (int i = _startIndex; i <= _endIndex; i++)
        {
            Console.Write(_data[i]);
            if (i < _endIndex)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }
}