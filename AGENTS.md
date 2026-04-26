# AGENTS.md - AaDS Codebase Guide

## Project Overview
**AaDS** (Algorithms and Data Structures) is an educational C# .NET 8.0 console application implementing fundamental data structures from scratch. This is a learning project with Russian-language documentation.

### Key Technologies
- **.NET 8.0 LTS** with ImplicitUsings and Nullable enabled
- **Console application** (single project, no external dependencies)
- **Generic programming** with constraint support (e.g., `IComparable<T>`)

## Architecture: Three Core Components

### 1. Custom Linear Lists (`Custom Linear List/`)
Multiple linked list implementations showcasing different approaches:

**Implementations:**
- **`MyLinkedList<T>`** (Node.cs): Generic, maintains both Head and Tail pointers. Most feature-rich with Insert/RemoveAt supporting 1-indexed positions. Used as primary educational structure.
- **`LinkedListWithoutTail<T>`** (LinkedListWithoutTail.cs): Simplified generic version with Head-only navigation. Returns size by traversal.
- **`DoubleLinkedList`** (doubleLinkedList.cs): Non-generic (int only), bidirectional traversal. Partially implemented (AddToEnd incomplete).

**Key Pattern:** All use nested `Node<T>` or `DoubleNode` classes. Support common operations:
- `AddFirst()`, `AddLast()`, `InsertAt(data, position)`
- `RemoveAt(position)`, `Reverse()`, `Search(item)`, `GetIndexes(item)`
- `Print()` for console visualization using `->` arrow notation
- **Positional indexing is 1-based** (not 0-based) - critical for RemoveAt/InsertAt

### 2. CustomArrayList (`CustomArrayList/`)
Dynamic array with smart index management:

**Architecture:**
- Tracks `_startIndex` and `_endIndex` instead of shifting all elements
- Doubles capacity when full: `newLength = _data.Length * 2`
- Maintains `Count` property: `_endIndex - _startIndex + 1`

**Operations:**
- `AddFirst()/AddLast()`: Reuses freed space before doubling capacity
- `Insert(value, index)`: Shifts elements right. Index is 0-based.
- `RemoveAt(index)`: Efficient removal by incrementing `_startIndex` (head removal) or `_endIndex` (tail removal)
- `RemoveRange(index)`: Truncates from index onwards (keeps 0 to index-1)
- `Reverse()`: In-place reversal using index boundaries

**Constraint:** Implements `where T : IComparable<T>` (not currently enforced in implementation, but required)

### 3. Forest - Tree Structures (`Forest/`)

**General Tree** (Tree/):
- `TreeNode`: Public `int Info` and `List<TreeNode> Children`
- `Tree` class: Implements breadth-first traversal via `TraverseBreadthFirst2()`
- Extension method: `root.TraverseBreadthFirst2()` for fluent call style
- Traversal uses recursive level-by-level processing

**Binary Search Tree** (Binary Search Tree/):
- `BtNode`: Key-value pairs with Parent, Left, Right pointers. Includes computed fields: `DescendantCount`, `SubtreeKeySum`, `SubtreeHeight` (metadata for future algorithms)
- `BsTree`: Manages insertion and height calculation
  - Insert via recursive descent (maintains BST property: left < key ≤ right)
  - Duplicate keys update Value
  - `GetHeight()` returns -1 for null nodes, supporting calculation of 1-node trees

## Development Workflow

### Build & Run
```bash
dotnet build
dotnet run
```

### Current Entry Point
Program.cs calls `TreeProcessor.ProcessTree()` by default. Swap to test other components:
```csharp
// In Program.cs:
// ProcessLinkedList.Run();
// ProcessArrayList.Run();
var process = new TreeProcessor();
process.ProcessTree();
```

### Testing Pattern
Each component has a **Processor class** encapsulating demo/test logic:
- `ProcessLinkedList.Run()` - Interactive demonstration of linked list operations
- `ProcessArrayList.Run()` - Tests all CustomArrayList methods including edge cases (empty list, string types)
- `TreeProcessor.ProcessTree()` - Constructs sample tree and displays traversal

## Project-Specific Conventions

1. **Russian Comments**: Educational content includes Russian language explaining algorithms. Preserve when maintaining.

2. **Namespace Mapping**: Folder structure → namespace using underscores:
   - `Custom Linear List/` → `AaDS.Custom_Linear_List`
   - `Forest/Tree/` → `AaDS.Forest.Tree`
   - `Forest/Binary Search Tree/` → `AaDS.Forest.Binary_Search_Tree`

3. **Print-for-Verify Pattern**: No unit tests; validation via console output:
   - LinkedLists use `Print()` showing `10 -> 20 -> 30 -> null`
   - ArrayLists use `Print()` showing `[10, 20, 30]`

4. **Position/Index Convention**: **Mixed**—linked lists use 1-based, arrays use 0-based. Always verify when adding new operations.

5. **Generic Constraints**: Use `where T : IComparable<T>` for sortable structures; nested types (Node<T>) are doubly parameterized.

## Integration Points & Dependencies

- **No external NuGet packages** - pure .NET Base Class Library only
- **Implicit Global Usings**: System, System.Collections.Generic, etc. already available
- **Nullable support enabled**: Use `?` for optional references, check null explicitly before access

## Common Pitfalls & Design Decisions

1. **DoubleLinkedList is incomplete**: `AddToEnd()` has empty body. RemoveFromEnd logic needs completion.
2. **LinkedListWithoutTail has O(n) size calculation** - calls GetSize() internally for bounds checking.
3. **CustomArrayList Index Boundaries**: Insert/RemoveAt differ; RemoveRange truncates (not symmetric to Remove).
4. **BST metadata fields** (DescendantCount, etc.) not yet calculated - prepared for future algorithms.
5. **No duplicate prevention** in BST - duplicate keys just update the value.

## Working with This Codebase

**When modifying data structures:**
- Update corresponding Processor class to verify behavior
- Test both edge cases (empty, single element, multiple elements)
- Maintain 1-based vs 0-based convention clarity in method documentation

**When adding new structures:**
- Create folder under Forest/ or as new sibling directory
- Follow Processor pattern for testing
- Use nested Node classes for linked structures
- Add French/Russian explanatory comments for educational value

