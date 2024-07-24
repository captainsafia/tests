||| RDF | RDG |
|---|---|---|---|
| `(int number) => { }` | `/?number=1` | `1` | `1` |
| `(int number) => { }` | `/?number=` | `1` | `1` |
| `(int number) => { }` | `/` | Throws exception | `1` |
| `(int? number) => { }` | `/?number=1` | `1` | `1` |
| `(int? number) => { }` | `/?number=` | Throws exception | `1` |
| `(int? number) => { }` | `/` | `null` | `1` |
| `(int[] number) => { }` | `/?numbers=1&numbers=&numbers=3` | Throws exception | `[1, 0, 3]` |
| `(int[] number) => { }` | `/?numbers=1&number2s=&numbers=3` | `[1, 2, 3]` | `[1, 2, 3]` |
| `(int?[] number) => { }` | `/?numbers=1&numbers=&numbers=3` | `[1, 2, 3]` | `[1, null, 3]` |
| `(int?[] number) => { }` | `/?numbers=1&number2s=&numbers=3` | `[1, 2, 3]` | `[1, 2, 3]` |