# CG.Business
---

## 2021.3

* bug fixes

## 2021.2

* bug fixes

* I switched to the .NET 5.0 framework

* I added back manager and director types

* I added a client iface, exception and base type

* I added a hosting environment and configuration parameter to the UseRepositories method.

* I added a hosting environment and configuration parameter to the UseStrategies method.

* I added code to allow strategies and/or repositories to use either two or three parameter extension methods.

* I rewrote the strategy/repository code to allow selecting multiple or single abstractions via the configuration.

* I added a loader for the repository / strategy loader extension methods.

## 2021.1

* I bumped the major version for the new year.

* I added additional keys to the crud repository and model types.

* I switched the build target to .net core 3.1

* I added userepositories and usestrategies methods.

* I moved the CRUD repository and store types to CG.Linq

* I moved the generic model types to CG.Linq.

* I added service lifetime to the LoaderOptions type, and I modified the related
    extension methods to pass that hint down during registration, so all parts
    of a service are registered using the same lifetime rules.

## 2020.1

* I copied the code from the old CG.Core.

* I dropped support for .NET 4.61

* I added back suppport for services and strategies.

* I added back basic CRUD support to the repository and store types.

