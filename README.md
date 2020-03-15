# BenchmarkJobQueues
Based on Michael's Coding Spot: https://michaelscodingspot.com/c-job-queues/. You can go on his website for details.

# Benchmark 
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362

Intel Core i5-6600K CPU 3.50GHz (Skylake), 1 CPU, 4 logical and 4 physical cores

  [Host]     : .NET Framework 4.8 (4.8.4121.0), X64 RyuJIT
  Job-AQUCUT : .NET Framework 4.8 (4.8.4121.0), X64 RyuJIT

# Legend

  JobCounts           : Value of the 'JobCounts' parameter 
  
  CpuOperationsPerJob : Value of the 'CpuOperationsPerJob' parameter
  
  Mean                : Arithmetic mean of all measurements
  
  Error               : Half of 99.9% confidence interval
  
  StdDev              : Standard deviation of all measurements 
  
  Min                 : Minimum 
  
  Max                 : Maximum 
  
  Rank                : Relative position of current benchmark mean among all benchmarks (Arabic style)  
  
  Gen 0               : GC Generation 0 collects per 1000 operations
  
  Gen 1               : GC Generation 1 collects per 1000 operations
  
  Gen 2               : GC Generation 2 collects per 1000 operations
  
  Allocated           : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B) 
  
# Implementations

ChannelQueue: based on System.Threading.Channels

TPLDataflowQueue: based on System.Threading.Tasks.Dataflow

ReactiveExtensionsQueue: based on System.Reactive

NoDedicatedThreadQueue: Ordered Execution With ThreadPool using a simple queue and locks

SwapChainNoDedicatedThreadQueue: Optimisation of NoDedicatedThreadQueue using swap queues. Optimised for fast execution jobs. 

BlockingCollectionDedicatedThreadQueue: A dedicated thread with BlockingCollection.

ReactiveExtensionsWithBlockingCollectionQueue: An observable BlockingCollection.

# Very short execution time

 1 ms                : 1 Millisecond (0.001 sec)


|                                        Method | JobCounts | CpuOperationsPerJob |      Mean |     Error |    StdDev |       Min |       Max | Rank |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|---------------------------------------------- |---------- |-------------------- |----------:|----------:|----------:|----------:|----------:|-----:|----------:|---------:|---------:|----------:|
|                                  ChannelQueue |    100000 |                   1 |  8.227 ms | 0.1620 ms | 0.2374 ms |  7.775 ms |  8.675 ms |    1 | 1781.2500 | 312.5000 |  78.1250 |    6.5 MB |
|                              TPLDataflowQueue |    100000 |                   1 |  8.779 ms | 0.1645 ms | 0.1539 ms |  8.508 ms |  9.086 ms |    2 | 1812.5000 | 453.1250 | 171.8750 |   6.81 MB |
|               SwapChainNoDedicatedThreadQueue |    100000 |                   1 | 10.137 ms | 0.1994 ms | 0.2522 ms |  9.703 ms | 10.605 ms |    3 | 1437.5000 | 625.0000 | 343.7500 |   8.36 MB |
|                       ReactiveExtensionsQueue |    100000 |                   1 | 12.112 ms | 0.1855 ms | 0.1549 ms | 11.818 ms | 12.328 ms |    4 | 1656.2500 | 687.5000 |        - |   7.32 MB |
|                        NoDedicatedThreadQueue |    100000 |                   1 | 16.973 ms | 0.3274 ms | 0.3898 ms | 16.156 ms | 17.789 ms |    5 | 1218.7500 | 718.7500 | 468.7500 |   8.12 MB |
|        BlockingCollectionDedicatedThreadQueue |    100000 |                   1 | 26.393 ms | 0.4731 ms | 0.3951 ms | 26.006 ms | 27.202 ms |    6 | 1281.2500 | 625.0000 |        - |   7.33 MB |
| ReactiveExtensionsWithBlockingCollectionQueue |    100000 |                   1 | 28.398 ms | 0.4278 ms | 0.3340 ms | 27.789 ms | 29.030 ms |    7 | 1250.0000 | 625.0000 |        - |   7.33 MB |

# Medium execution time

1 us                : 1 Microsecond (0.000001 sec)

|                                        Method | JobCounts | CpuOperationsPerJob |       Mean |     Error |    StdDev |     Median |        Min |        Max | Rank |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |---------- |-------------------- |-----------:|----------:|----------:|-----------:|-----------:|-----------:|-----:|--------:|-------:|------:|----------:|
|                                  ChannelQueue |      1000 |                   1 |   103.4 us |   0.26 us |   0.25 us |   103.4 us |   103.0 us |   103.9 us |    1 | 24.4141 |      - |     - |  75.13 KB |
|               SwapChainNoDedicatedThreadQueue |      1000 |                   1 |   106.2 us |   1.24 us |   0.97 us |   106.1 us |   105.2 us |   108.3 us |    2 | 26.9775 |      - |     - |  83.11 KB |
|                              TPLDataflowQueue |      1000 |                   1 |   106.9 us |   1.03 us |   0.96 us |   106.9 us |   104.8 us |   108.2 us |    2 | 24.4141 | 0.1221 |     - |  75.14 KB |
|                        NoDedicatedThreadQueue |      1000 |                   1 |   137.8 us |   1.11 us |   0.98 us |   137.8 us |   136.5 us |   140.4 us |    4 | 25.6348 |      - |     - |  78.77 KB |
|                       ReactiveExtensionsQueue |      1000 |                   1 |   299.4 us |   8.83 us |  23.87 us |   290.5 us |   274.9 us |   381.2 us |    6 | 24.4141 |      - |     - |     76 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                   1 |   456.6 us |   9.45 us |  25.55 us |   453.3 us |   410.6 us |   534.2 us |    7 | 27.3438 |      - |     - |  84.17 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                   1 |   458.0 us |   8.85 us |   8.69 us |   457.4 us |   440.8 us |   469.9 us |    7 | 27.3438 |      - |     - |  84.45 KB |

|                                        Method | JobCounts | CpuOperationsPerJob |       Mean |     Error |    StdDev |     Median |        Min |        Max | Rank |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |---------- |-------------------- |-----------:|----------:|----------:|-----------:|-----------:|-----------:|-----:|--------:|-------:|------:|----------:|
|               SwapChainNoDedicatedThreadQueue |      1000 |                  10 |   110.3 us |   2.15 us |   2.65 us |   109.4 us |   106.8 us |   115.3 us |    3 | 26.7334 |      - |     - |  82.51 KB |
|                                  ChannelQueue |      1000 |                  10 |   111.3 us |   0.91 us |   0.85 us |   111.1 us |   109.9 us |   112.6 us |    3 | 24.6582 |      - |     - |  75.82 KB |
|                              TPLDataflowQueue |      1000 |                  10 |   112.8 us |   1.31 us |   1.22 us |   113.3 us |   110.8 us |   114.2 us |    3 | 26.6113 |      - |     - |  82.02 KB |
|                        NoDedicatedThreadQueue |      1000 |                  10 |   141.9 us |   2.66 us |   2.49 us |   142.4 us |   133.1 us |   143.5 us |    5 | 25.3906 |      - |     - |  78.69 KB |
|                       ReactiveExtensionsQueue |      1000 |                  10 |   294.4 us |   6.06 us |  17.09 us |   290.4 us |   272.3 us |   347.0 us |    6 | 24.4141 |      - |     - |     76 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                  10 |   557.0 us |  26.53 us |  76.12 us |   555.6 us |   448.4 us |   798.8 us |    8 | 27.3438 |      - |     - |  84.45 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                  10 |   624.8 us |  51.35 us | 141.43 us |   576.8 us |   433.0 us |   987.4 us |    9 | 23.4375 |      - |     - |  84.13 KB |

|                                        Method | JobCounts | CpuOperationsPerJob |       Mean |     Error |    StdDev |     Median |        Min |        Max | Rank |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |---------- |-------------------- |-----------:|----------:|----------:|-----------:|-----------:|-----------:|-----:|--------:|-------:|------:|----------:|
|                              TPLDataflowQueue |      1000 |                 100 |   554.7 us |  12.25 us |  17.96 us |   547.8 us |   536.5 us |   605.2 us |    8 | 31.2500 |      - |     - |  97.49 KB |
|                                  ChannelQueue |      1000 |                 100 |   578.2 us |   8.55 us |   8.00 us |   574.5 us |   567.2 us |   594.8 us |    8 | 26.3672 |      - |     - |  82.73 KB |
|               SwapChainNoDedicatedThreadQueue |      1000 |                 100 |   599.4 us |  11.89 us |  16.67 us |   599.8 us |   574.4 us |   634.3 us |    9 | 26.3672 |      - |     - |  81.93 KB |
|                        NoDedicatedThreadQueue |      1000 |                 100 |   646.7 us |  12.46 us |  16.63 us |   643.2 us |   620.8 us |   678.9 us |    9 | 25.3906 |      - |     - |  78.85 KB |
|                       ReactiveExtensionsQueue |      1000 |                 100 |   847.0 us |  16.80 us |  47.12 us |   833.3 us |   774.2 us |   984.8 us |   10 | 24.4141 |      - |     - |     76 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                 100 |   964.6 us |  19.23 us |  32.65 us |   965.9 us |   907.9 us | 1,044.3 us |   11 | 27.3438 |      - |     - |  84.47 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                 100 |   981.9 us |  24.77 us |  68.23 us |   966.8 us |   889.5 us | 1,196.0 us |   11 | 27.3438 |      - |     - |  84.17 KB |

|                                        Method | JobCounts | CpuOperationsPerJob |       Mean |     Error |    StdDev |     Median |        Min |        Max | Rank |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |---------- |-------------------- |-----------:|----------:|----------:|-----------:|-----------:|-----------:|-----:|--------:|-------:|------:|----------:|
|               SwapChainNoDedicatedThreadQueue |      1000 |                1000 | 5,177.3 us |  66.32 us |  62.04 us | 5,166.2 us | 5,071.9 us | 5,279.2 us |   12 | 23.4375 |      - |     - |  81.35 KB |
|                              TPLDataflowQueue |      1000 |                1000 | 5,512.1 us |  82.30 us |  76.98 us | 5,529.8 us | 5,371.4 us | 5,619.7 us |   13 | 31.2500 |      - |     - |   97.5 KB |
|                                  ChannelQueue |      1000 |                1000 | 5,549.9 us |  70.33 us |  65.78 us | 5,537.5 us | 5,414.4 us | 5,646.4 us |   13 | 23.4375 |      - |     - |   82.7 KB |
|                        NoDedicatedThreadQueue |      1000 |                1000 | 5,608.9 us |  71.61 us |  66.98 us | 5,576.7 us | 5,543.0 us | 5,732.5 us |   13 | 23.4375 |      - |     - |  78.88 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                1000 | 5,765.2 us | 130.79 us | 134.31 us | 5,743.2 us | 5,570.4 us | 6,118.9 us |   14 | 23.4375 |      - |     - |  84.13 KB |
|                       ReactiveExtensionsQueue |      1000 |                1000 | 5,836.9 us | 109.17 us |  96.78 us | 5,844.7 us | 5,644.1 us | 5,975.1 us |   14 | 23.4375 |      - |     - |     76 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                1000 | 6,052.6 us |  84.24 us |  74.68 us | 6,055.8 us | 5,931.1 us | 6,225.1 us |   15 | 23.4375 |      - |     - |   84.5 KB |
