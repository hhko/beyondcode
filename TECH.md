
## Domain 레이어
- [ ] Entity
- [ ] ValueObject
- [ ] Enum
- [ ] Domain Service
- [ ] Aggregate Root
- [ ] Domain Event
- [ ] Factory
- [ ] Specification
- [ ] Error
- [ ] Pipeline
  - [ ] Validation
  - [ ] Exception
  - [ ] OpenTelemetry: Logs, Metrics, Traces
  - [ ] Command Transaction??? Event 발생 후 Transaction 호출 순서???
  - [ ] Query Cache

## Adapter
- [ ] Repository
- [ ] Unit of Work
- [ ] Saga 패턴
- [ ] Outbox 패턴

## Resilience
- [ ] Reactive `Retry`: Try again if something fails. This can be useful when the problem is temporary and might go away.
- [ ] Reactive `Circuit Breaker`: Stop trying if something is broken or busy. This can benefit you by avoiding wasting time and making things worse. It can also support the system to recover.
- [ ] Reactive `Fallback`: Do something else if something fails. This can improve your user experience and keep the program working.
- [ ] Reactive `Hedging`: Do more than one thing at the same time and take the fastest one. This can make your program faster and more responsive.
- [ ] Proactive `Timeout`: Give up if something takes too long. This can improve your performance by freeing up space and resources.
- [ ] Proactive `Rate Limiter`: Limit how many requests you make or accept. This can enable you to control the load and prevent problems or penalties.

### Reactive

| Strategy | Premise | AKA | How does the strategy mitigate?|
| ------------- | ------------- |:-------------: |------------- |
| Retry |Many faults are transient and may self-correct after a short delay.| *Maybe it's just a blip* |  Allows configuring automatic retries. |
| Circuit-breaker |When a system is seriously struggling, failing fast is better than making users/callers wait.  <br/><br/>Protecting a faulting system from overload can help it recover. | *Stop doing it if it hurts* <br/><br/>*Give that system a break* | Breaks the circuit (blocks executions) for a period, when faults exceed some pre-configured threshold. |
| Fallback |Things will still fail - plan what you will do when that happens.| *Degrade gracefully*  |Defines an alternative value to be returned (or action to be executed) on failure. |
| Hedging |Things can be slow sometimes, plan what you will do when that happens.| *Hedge your bets*  | Executes parallel actions when things are slow and waits for the fastest one.  |

### Proactive

| Strategy | Premise | AKA | How does the strategy prevent?|
| ------------- | ------------- |:-------------: |------------- |
| Timeout |Beyond a certain wait, a success result is unlikely.| *Don't wait forever*  |Guarantees the caller won't have to wait beyond the timeout. |
| Rate Limiter |Limiting the rate a system handles requests is another way to control load. <br/><br/> This can apply to the way your system accepts incoming calls, and/or to the way you call downstream services. | *Slow down a bit, will you?*  |Constrains executions to not exceed a certain rate. |
