

![GitHub](https://img.shields.io/github/license/twaddell/MessagePack.LanguageExt)
[![NuGet version (MessagePack.LanguageExt)](https://img.shields.io/nuget/v/MessagePack.LanguageExt.svg)](https://www.nuget.org/packages/MessagePack.LanguageExt/)
[![CI](https://github.com/twaddell/MessagePack.LanguageExt/actions/workflows/ci.yml/badge.svg)](https://github.com/twaddell/MessagePack.LanguageExt/actions/workflows/ci.yml)
[![Release Pipeline](https://github.com/twaddell/MessagePack.LanguageExt/actions/workflows/release.yml/badge.svg)](https://github.com/twaddell/MessagePack.LanguageExt/actions/workflows/release.yml)

# MessagePack.LanguageExt
This library adds support for LanguageExt types to MessagePack C#

## Getting Started

### Installation
#### Prerequisities for C#
* [MessagePack](https://www.nuget.org/packages/MessagePack/)
* [LanguageExt.Core](https://www.nuget.org/packages/LanguageExt.Core)


This library is provided in NuGet.

Support for .NET Framework 4.6.1 and .NET Standard 2.0.

In the Package Manager Console -
```
Install-Package MessagePack.LanguageExt
```
or download directly from NuGet.

## How to use
To use the LanguageExt resolver, you will have to add it to the composite resolver, as shown in the example below:
```csharp
var resolver = CompositeResolver.Create(
            BuiltinResolver.Instance,
            LanguageExtResolver.Instance,
            ContractlessStandardResolver.Instance
            );
			
MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options.WithResolver(resolver);  
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
Issues and pull requests are welcome. Please include tests for any behavioural change and make sure `dotnet test` passes before submitting.

We use GitHub Flow:

1. Branch from `main`.
2. Open a PR to `main` with exactly one `semver:patch`, `semver:minor` or `semver:major` label. The `semver label` check fails until it has one.
3. Get green checks.
4. Squash-merge. Merging releases the package.

## Releasing
Every merge to `main` is released by the [Release Pipeline](.github/workflows/release.yml), with no manual steps:

1. The next version is the latest `X.Y.Z` tag, bumped by the highest `semver:*` label across the PRs merged since it. A PR with no label counts as a patch.
2. The tests run against the merged commit, and the package is pushed to [NuGet](https://www.nuget.org/packages/MessagePack.LanguageExt/) with that version.
3. The commit is tagged `X.Y.Z` and a [GitHub Release](https://github.com/twaddell/MessagePack.LanguageExt/releases) is created, with notes listing the PRs and the `.nupkg` file attached.

Choose the label from what consumers of the package will see: `semver:major` for a breaking API change, `semver:minor` for new supported types or other additions, and `semver:patch` for fixes, docs, dependency updates and CI changes.

If the `release` job fails after the package was pushed, re-run only that job, so the tag and Release are created for the version that's already published. Until it's tagged, later release runs compute the same version and fail at the push, because that version already exists. A bad release is fixed forward with a new PR.

## Links
* [Nuget](https://www.nuget.org/packages/MessagePack.LanguageExt/)
* [Github](https://github.com/twaddell/MessagePack.LanguageExt/)

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/twaddell/MessagePack.LanguageExt/blob/master/LICENSE) file for details

## Acknowledgments
* [MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp)
* [LanguageExt](https://github.com/louthy/language-ext/)
* [MsgPack Spec](https://github.com/msgpack/msgpack/blob/master/spec.md)
