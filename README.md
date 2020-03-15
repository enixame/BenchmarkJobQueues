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

|                                        Method | JobCounts | CpuOperationsPerJob |        Mean |       Error |      StdDev |      Median |         Min |         Max | Rank |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |---------- |-------------------- |------------:|------------:|------------:|------------:|------------:|------------:|-----:|--------:|------:|------:|----------:|
|               SwapChainNoDedicatedThreadQueue |      1000 |                   1 |    103.5 us |     2.01 us |     1.88 us |    104.0 us |    101.2 us |    106.8 us |    1 | 27.0996 |     - |     - |  83.39 KB |
|                                  ChannelQueue |      1000 |                   1 |    104.5 us |     0.74 us |     0.69 us |    104.6 us |    103.3 us |    105.4 us |    1 | 24.4141 |     - |     - |  75.15 KB |
|                              TPLDataflowQueue |      1000 |                   1 |    107.8 us |     0.40 us |     0.38 us |    107.8 us |    107.2 us |    108.5 us |    2 | 24.1699 |     - |     - |  74.49 KB |
|                        NoDedicatedThreadQueue |      1000 |                   1 |    136.4 us |     2.04 us |     1.90 us |    136.2 us |    133.3 us |    140.0 us |    3 | 25.3906 |     - |     - |  78.74 KB |
|                       ReactiveExtensionsQueue |      1000 |                   1 |    310.2 us |     9.02 us |    23.75 us |    306.1 us |    280.8 us |    421.0 us |    4 | 24.4141 |     - |     - |  75.99 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                   1 |    462.0 us |    18.23 us |    49.58 us |    441.7 us |    412.3 us |    607.9 us |    5 | 27.3438 |     - |     - |  84.18 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                   1 |    505.7 us |    30.76 us |    83.68 us |    471.5 us |    439.5 us |    830.0 us |    6 | 27.3438 |     - |     - |  84.46 KB |
|                                  ChannelQueue |      1000 |                 100 |    576.9 us |     7.04 us |     6.24 us |    576.4 us |    566.5 us |    591.5 us |    7 | 26.3672 |     - |     - |  82.71 KB |
|                              TPLDataflowQueue |      1000 |                 100 |    590.9 us |    11.80 us |    13.59 us |    588.0 us |    577.2 us |    624.4 us |    7 | 31.2500 |     - |     - |  97.53 KB |
|               SwapChainNoDedicatedThreadQueue |      1000 |                 100 |    593.6 us |    11.69 us |    18.21 us |    594.5 us |    570.8 us |    624.7 us |    7 | 26.3672 |     - |     - |   82.1 KB |
|                        NoDedicatedThreadQueue |      1000 |                 100 |    648.9 us |    12.63 us |    18.11 us |    651.6 us |    618.7 us |    675.5 us |    8 | 25.3906 |     - |     - |  78.84 KB |
|                       ReactiveExtensionsQueue |      1000 |                 100 |    804.5 us |    15.98 us |    24.41 us |    802.7 us |    774.8 us |    888.6 us |    9 | 24.4141 |     - |     - |     76 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |                 100 |    905.1 us |    17.11 us |    18.31 us |    907.8 us |    853.7 us |    929.8 us |   10 | 27.3438 |     - |     - |  84.46 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |                 100 |    916.2 us |    17.13 us |    17.59 us |    916.3 us |    887.1 us |    942.7 us |   10 | 27.3438 |     - |     - |  84.16 KB |
|                                  ChannelQueue |      1000 |               10000 | 51,422.1 us |   521.79 us |   488.08 us | 51,290.8 us | 50,710.8 us | 52,233.3 us |   11 |       - |     - |     - |  86.45 KB |
|                       ReactiveExtensionsQueue |      1000 |               10000 | 53,388.8 us |   588.59 us |   459.53 us | 53,447.8 us | 52,607.4 us | 54,037.0 us |   12 |       - |     - |     - |     76 KB |
|                        NoDedicatedThreadQueue |      1000 |               10000 | 53,934.4 us |   648.91 us |   606.99 us | 53,828.0 us | 52,914.8 us | 55,019.7 us |   12 |       - |     - |     - |  79.25 KB |
| ReactiveExtensionsWithBlockingCollectionQueue |      1000 |               10000 | 54,433.6 us |   688.62 us |   537.63 us | 54,533.3 us | 53,162.2 us | 55,376.9 us |   12 |       - |     - |     - |   84.8 KB |
|               SwapChainNoDedicatedThreadQueue |      1000 |               10000 | 54,465.1 us |   710.40 us |   664.51 us | 54,359.7 us | 53,586.2 us | 55,412.1 us |   12 |       - |     - |     - |  82.71 KB |
|                              TPLDataflowQueue |      1000 |               10000 | 54,810.3 us |   745.56 us |   697.40 us | 54,783.6 us | 53,822.6 us | 56,002.4 us |   12 |       - |     - |     - |  97.69 KB |
|        BlockingCollectionDedicatedThreadQueue |      1000 |               10000 | 58,172.7 us | 6,985.28 us | 6,192.27 us | 56,737.3 us | 55,553.4 us | 79,609.4 us |   13 |       - |     - |     - |     84 KB |
