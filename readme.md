# Introduction
This is a repo to test the performance after some optimizations in Microsoft.Extensions.Caching.SqlServer

## Running the test

This test assumes that you have a SQL server instance running locally. To use a SQL server instance running in another server, change the connection string accordingly.

1. Create the cache database. Type in command line

```
sqlcmd
```

Run in the command prompt these lines one by one

```
CREATE DATABASE CacheSampleDb
go
exit
```

2. Install the tool to create the objects needed in the database

```
dotnet tool install --global dotnet-sql-cache --version 2.1.1
dotnet sql-cache create "Server=localhost;Initial Catalog=CacheSampleDb; Trusted_Connection=True;" "dbo" "CacheSample"
```

3. Then run the test applications and see the execution times (should take less than 30 seconds)

Assuming that the current directory is root and starting with the non optimized test:

```
cd current-not-optimized-package
dotnet restore
```

And run multiple times:
```
dotnet run
```

Then go to the path that uses the optimizations:

```
cd ..\optimized\perf-test
dotnet restore
```

And run multiple times:
```
dotnet run
```

## My test results

### Optimized

* A total of 100000 queries took: 24621 millis
* A total of 100000 queries took: 27052 millis
* A total of 100000 queries took: 23713 millis
* A total of 100000 queries took: 28287 millis
* A total of 100000 queries took: 22166 millis
* A total of 100000 queries took: 22954 millis
* A total of 100000 queries took: 22572 millis
* A total of 100000 queries took: 21845 millis
* A total of 100000 queries took: 23643 millis

### Non-optimized

* A total of 100000 queries took: 25276 millis
* A total of 100000 queries took: 28089 millis
* A total of 100000 queries took: 24667 millis
* A total of 100000 queries took: 31622 millis
* A total of 100000 queries took: 25427 millis
* A total of 100000 queries took: 28124 millis
* A total of 100000 queries took: 24696 millis
* A total of 100000 queries took: 23004 millis
* A total of 100000 queries took: 24160 millis

### Totals in millis

* **Average. Optimized: 23643, non optimized 25276 (optimized is 1633 faster)**
* Slowest. Optimized: 28287, non optimized 31622 (optimized is 3335 faster)
* Fastest. Optimized: 21845, non optimized 23004 (optimized is 1159 faster)
