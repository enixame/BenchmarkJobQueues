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

|                                        Method | JobCounts | CpuOperationsPerJob |        Mean |      Error |     StdDev |      Median |         Min |         Max | Rank |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |---------- |-------------------- |------------:|-----------:|-----------:|------------:|------------:|------------:|-----:|--------:|------:|------:|----------:|
|               SwapChainNoDedicatedThreadQueue |      1000 |                   1 |    96.27 us |   0.490 us |   0.435 us |    96.32 us |    95.69 us |    97.19 us |    1 | 27.0996 |     - |     - |  83.55 KB |
|                                  ChannelQueue |      1000 |                   1 |   102.52 us |   0.928 us |   0.868 us |   102.58 us |   101.00 us |   103.70 us |    2 | 24.4141 |     - |     - |  75.09 KB |
|                              TPLDataflowQueue |      1000 |                   1 |   106.24 us |   0.326 us |   0.289 us |   106.20 us |   105.81 us |   106.90 us |    3 | 26.2451 |     - |     - |  80.83 KB |
|                        NoDedicatedThreadQueue |      1000 |                   1 |   134.71 us |   2.330 us |   2.179 us |   133.84 us |   132.07 us |   139.73 us |    4 | 25.6348 |     - |     - |  78.78 KB |
|                       ReactiveExtensionsQueue |      1000 |                   1 |   282.99 us |   5.394 us |   6.624 us |   281.83 us |   272.03 us |   300.36 us |    5 | 24.4141 |     - |     - |  75.99 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                   1 |   415.61 us |   7.763 us |   6.882 us |   416.34 us |   401.93 us |   425.16 us |    6 | 27.3438 |     - |     - |  84.17 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                   1 |   449.43 us |   8.962 us |  22.320 us |   440.76 us |   423.42 us |   512.20 us |    7 | 27.3438 |     - |     - |  84.46 KB |
|                              TPLDataflowQueue |      1000 |                 100 |   555.06 us |  10.827 us |  12.468 us |   550.56 us |   537.80 us |   578.66 us |    8 | 31.2500 |     - |     - |  97.38 KB |
|               SwapChainNoDedicatedThreadQueue |      1000 |                 100 |   570.30 us |  11.284 us |  15.446 us |   572.01 us |   530.55 us |   593.24 us |    9 | 26.3672 |     - |     - |  82.33 KB |
|                                  ChannelQueue |      1000 |                 100 |   590.49 us |  11.656 us |  14.314 us |   589.73 us |   570.23 us |   611.84 us |   10 | 26.3672 |     - |     - |  82.58 KB |
|                        NoDedicatedThreadQueue |      1000 |                 100 |   665.45 us |  13.121 us |  14.040 us |   668.28 us |   631.17 us |   682.19 us |   11 | 25.3906 |     - |     - |  78.84 KB |
|                       ReactiveExtensionsQueue |      1000 |                 100 |   901.91 us |  41.280 us | 115.071 us |   858.31 us |   759.74 us | 1,257.33 us |   12 | 24.4141 |     - |     - |     76 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                 100 |   929.28 us |  16.967 us |  15.041 us |   929.29 us |   901.76 us |   955.18 us |   13 | 27.3438 |     - |     - |  84.16 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                 100 |   982.58 us |  19.568 us |  53.568 us |   970.09 us |   894.30 us | 1,147.24 us |   14 | 27.3438 |     - |     - |  84.47 KB |
|                              TPLDataflowQueue |      1000 |                1000 | 5,355.35 us |  73.366 us |  68.626 us | 5,365.08 us | 5,230.48 us | 5,466.48 us |   15 | 31.2500 |     - |     - |   97.4 KB |
|                                  ChannelQueue |      1000 |                1000 | 5,393.43 us |  88.710 us |  82.979 us | 5,391.49 us | 5,254.76 us | 5,532.75 us |   15 | 23.4375 |     - |     - |  82.84 KB |
|                        NoDedicatedThreadQueue |      1000 |                1000 | 5,412.08 us |  43.033 us |  40.253 us | 5,413.06 us | 5,342.71 us | 5,491.37 us |   15 | 23.4375 |     - |     - |  78.88 KB |
|               SwapChainNoDedicatedThreadQueue |      1000 |                1000 | 5,612.93 us |  46.929 us |  43.897 us | 5,611.02 us | 5,511.76 us | 5,691.02 us |   16 | 23.4375 |     - |     - |  81.27 KB |
|                       ReactiveExtensionsQueue |      1000 |                1000 | 5,945.10 us |  86.625 us |  76.791 us | 5,948.73 us | 5,815.62 us | 6,103.42 us |   17 | 23.4375 |     - |     - |     76 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                1000 | 5,989.46 us |  77.100 us |  68.347 us | 5,980.29 us | 5,870.88 us | 6,126.24 us |   17 | 23.4375 |     - |     - |  84.13 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                1000 | 6,003.77 us | 116.258 us | 129.220 us | 5,979.89 us | 5,848.82 us | 6,245.12 us |   17 | 23.4375 |     - |     - |   84.5 KB |
|                                  ChannelQueue |      1000 |               10000 | 51,422.1 us |   521.79 us |   488.08 us | 51,290.8 us | 50,710.8 us | 52,233.3 us |   11 |       - |     - |     - |  86.45 KB |
|                       ReactiveExtensionsQueue |      1000 |               10000 | 53,388.8 us |   588.59 us |   459.53 us | 53,447.8 us | 52,607.4 us | 54,037.0 us |   12 |       - |     - |     - |     76 KB |
|                        NoDedicatedThreadQueue |      1000 |               10000 | 53,934.4 us |   648.91 us |   606.99 us | 53,828.0 us | 52,914.8 us | 55,019.7 us |   12 |       - |     - |     - |  79.25 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |               10000 | 54,433.6 us |   688.62 us |   537.63 us | 54,533.3 us | 53,162.2 us | 55,376.9 us |   12 |       - |     - |     - |   84.8 KB |
|               SwapChainNoDedicatedThreadQueue |      1000 |               10000 | 54,465.1 us |   710.40 us |   664.51 us | 54,359.7 us | 53,586.2 us | 55,412.1 us |   12 |       - |     - |     - |  82.71 KB |
|                              TPLDataflowQueue |      1000 |               10000 | 54,810.3 us |   745.56 us |   697.40 us | 54,783.6 us | 53,822.6 us | 56,002.4 us |   12 |       - |     - |     - |  97.69 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |               10000 | 58,172.7 us | 6,985.28 us | 6,192.27 us | 56,737.3 us | 55,553.4 us | 79,609.4 us |   13 |       - |     - |     - |     84 KB |
