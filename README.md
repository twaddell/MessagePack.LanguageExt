

![GitHub](https://img.shields.io/github/license/twaddell/MessagePack.LanguageExt)
[![NuGet version (MessagePack.LanguageExt)](https://img.shields.io/nuget/v/MessagePack.LanguageExt.svg)](https://www.nuget.org/packages/MessagePack.LanguageExt/)
[![Build status](https://ci.appveyor.com/api/projects/status/wxvtr0nr1y2lhsoh/branch/master?svg=true)](https://ci.appveyor.com/project/twaddell/messagepack-languageext/branch/master)

# MessagePack.LanguageExt
This library adds support for LanguageExt types to MessagePack C#

## Getting Started

### Installation
#### Prerequisities for C#
* [MessagePack](https://www.nuget.org/packages/MessagePack/)
* [LanguageExt.Core](https://www.nuget.org/packages/LanguageExt.Core)


This library is provided in NuGet.

Support for .NET Framework 4.6 and .NET Standard 2.0.

In the Package Manager Console -
```
Install-Package MessagePack.LanguageExt
```
or download directly from NuGet.

## How to use
To use the LanguageExt resolver, you will have to add it to the composite resolver, as shown in the example below:
```csharp
 CompositeResolver.RegisterAndSetAsDefault(
            BuiltinResolver.Instance,
            LanguageExtResolver.Instance,
            ContractlessStandardResolver.Instance
            );
```
## Quick Start
For more information on either MessagePack or LanguageExt, please follow the respective links below. 
* [MesssagePack](https://github.com/neuecc/MessagePack-CSharp/blob/master/README.md)
* [LanguageExt](https://github.com/louthy/language-ext/blob/master/README.md)

This is a quick guide on a basic serialization and de-serialization of a LanguageExt type.

```csharp
Lst<int> list = List(1, 2, 3, 4, 5);;
var bin = MessagePackSerializer.Serialize(list);
var res = MessagePackSerializer.Deserialize<Lst<int>>(bin);
// inst == res
```

## Usage
### Supported LanguageExt types
 `Arr<T>`, `Seq<T>`, `Lst<T>`, `Map<K,V>`, `Set<T>`, `HashSet<T>`, `HashMap<K,V>` `Que<T>`, `Stck<T>`, `Option<T>`, and `Either<L,R>`

## Contributing
*TBC*

## Links
* [Nuget](https://www.nuget.org/packages/MessagePack.LanguageExt/)
* [Github](https://github.com/twaddell/MessagePack.LanguageExt/)

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/twaddell/MessagePack.LanguageExt/blob/master/LICENSE) file for details

## Acknowledgments
* [MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp)
* [LanguageExt](https://github.com/louthy/language-ext/)
* [MsgPack Spec](https://github.com/msgpack/msgpack/blob/master/spec.md)
