# HangfireContext

## Purpose
POC to demonstrate that:
- Passing in cross-cutting-concern related data into Hangfire jobs is kind of hard without some level of abstraction
- Consumer code such as Jobs should not have to have parameters like 'userId', 'tenantId' or 'performContext' in their signature
- There is an easy way to enforce jobs to not be random methods in a service, but actual Job units.

# Contents
- `HangfireContext.Core` contains:
  - A filter that takes care of `PerformContext` initialization
  - A base job implementation that enforces a certain contract, but also gives you the flexibility of getting a PerformContext
  - A background job manager abstraction that enforces people to use the base job implementation and its interfaces to schedule jobs
  - A custom job activator for Hangfire (WIP)
- `HangfireContext.Sample` contains:
  - A concrete implementation of passing in a `userId` to a job using all the tools HangfireContext.Core provides. 