using Polly;
using Polly.Retry;
using Polly.CircuitBreaker;

namespace InvestControl.Worker.Policies;

public class ResiliencePolicy
{
    public static AsyncRetryPolicy RetryPolicy =>
        Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, time, retryCount, context) =>
                {
                    Console.WriteLine($"[Retry #{retryCount}] - {exception.Message}");
                });
    public static AsyncCircuitBreakerPolicy CircuitBreaker =>
        Policy
            .Handle<Exception>()
            .AdvancedCircuitBreakerAsync(
                failureThreshold: 0.5,
                samplingDuration: TimeSpan.FromSeconds(15),
                minimumThroughput: 4,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (ex, breakTime) =>
                {
                    Console.WriteLine($"[Circuit Breaker] OPEN for {breakTime.TotalSeconds} seconds: {ex.Message}");
                },
                onReset: () => Console.WriteLine("[Circuit Breaker] CLOSED"),
                onHalfOpen: () => Console.WriteLine("[Circuit Breaker] HALF-OPEN"));
            
}
